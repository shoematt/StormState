using System;
using System.Collections.Generic;
using System.Linq;
using Orca.Core;
using Orca.Core.Caching;
using Orca.Core.Commands;
using Orca.Core.Extensions;
using Orca.Core.Logging;
using Orca.Core.Persistence;
using Orca.Domain.Exceptions;
using Orca.Domain.Interfaces;
using Orca.Domain.Objects;
using Orca.Persistence.Core.Interfaces;
using Orca.Persistence.Core.Messages;

namespace Orca.Domain.DynamicProperties
{
    public class DynamicPropertyManager : ICommandHandler<PersistDataMessage>
    {
        private static readonly ILog _logger = Logger.GetNamedLogger(LoggerNames.DynamicPropertyManager);

        private readonly IEntityRepository _repository;

        private readonly Type dynamicPropertyDefinionType = typeof(PropertyDefinition);
        private readonly INameManager nameManager;

        private readonly OrcaCache<Guid, Dictionary<string, DefaultPropertyValue>> typeProperties = new OrcaCache<Guid, Dictionary<string, DefaultPropertyValue>>();


        private Dictionary<Guid, PropertyDefinition> newPropertyDefintions = new Dictionary<Guid, PropertyDefinition>();

        private IPersistor persistor;

        private IPublishService publishService;

        //    private GuardAgainstNulls nullValidation = new GuardAgainstNulls();
        //Dictionary<string, Guid> TransactionNameSavedTransactions = new Dictionary<string, Guid>();
        //     Dictionary<Guid, Guid> newObjectsSavedTransactions = new Dictionary<Guid, Guid>();


        /// <summary>
        ///   Initializes a new instance of the DynamicPropertyManager class.
        /// </summary>
        /// <param name = "repository"></param>
        public DynamicPropertyManager(IEntityRepository repository, INameManager nameManager, IPersistor Persistor, IPublishService publishService)
        {
            _repository = repository;

            persistor = Persistor;

            this.nameManager = nameManager;

            typeProperties.OnMissing = OnMissingType;

            this.publishService = publishService;
        }

        #region ICommandHandler<PersistDataMessage> Members

        public void Handle(PersistDataMessage message)
        {
            if (message == null)
            {
                return;
            }

            lock (newPropertyDefintions)
            {
                //Clean up the newPropertyDefintions collection of objects that have now been persisted to the db.
                foreach (PersistDataEvent item in message.PersistedContents)
                {
                    if (string.IsNullOrEmpty(item.DomainObject.Name)) //some of the persisted objects may not have a name. like published node.
                    {
                        continue;
                    }


                    ISupportDynamicProperties objectWithProps = item.DomainObject as ISupportDynamicProperties;

                    if (objectWithProps == null)
                    {
                        continue;
                    }

                    if (newPropertyDefintions.ContainsKey(item.DomainObject.Id))
                    {
                        lock (newPropertyDefintions)
                        {
                            newPropertyDefintions.Remove(item.DomainObject.Id);
                        }
                    }

                    lock (typeProperties)
                    {
                        typeProperties.Remove(objectWithProps.UserTypeID);
                    }
                }
            }
            OnPersistDataMessageRecieved();
        }

        #endregion

        public virtual event EventHandler PersistDataMessageRecieved;

        public void Reset()
        {
            newPropertyDefintions.Clear();

            typeProperties.ClearAll();
        }


        private Dictionary<string, DefaultPropertyValue> OnMissingType(Guid typeToAdd)
        {
            return new Dictionary<string, DefaultPropertyValue>();
        }


        public PropertyDefinition DefinePropertyDefinition(object DefaultValue, string Name)
        {

            OperationReport validationReport = GuardAgainstNulls.ArgsContainsNull(new object[] { DefaultValue, Name });

            if (!validationReport.Success)
            {
                throw new OrcaArgumentNullException(validationReport);
            }

            OperationReport result = new OperationReport();

            PropertyDefinition def = new PropertyDefinition(Name, DefaultValue);

            persistor.AssignNewID(def);

            //the save only saves to the session, not persisted to the db 
            //so the only way to get the object again is by its id
            //to allow for lookup by name we need to keep an assocation of name to id
            //when the object has been persisted out to the db then we remove it from the collection.
            lock (newPropertyDefintions)
            {
                newPropertyDefintions.Add(def.Id, def);
            }

            return def;
        }

        /// <summary>
        /// Saves the specified property definition and commits it to the database.
        /// </summary>
        /// <param name="PropertyDef">The PropertyDefinition.</param>
        /// <returns></returns>
        public OperationReport Save(PropertyDefinition PropertyDef)
        {

            OperationReport validationReport = GuardAgainstNulls.ArgsContainsNull(new object[] { PropertyDef });

            if (!validationReport.Success)
            {
                throw new OrcaArgumentNullException(validationReport);
            }

            OperationReport result = new OperationReport();

            try
            {
                IPersistCommandsTransaction trans = GetPersistTransactionFor(PropertyDef.Id);
                if (trans != null)
                {
                    if (newPropertyDefintions.ContainsKey(PropertyDef.Id))
                    {
                        if (PropertyDefinitionNameAvaliable(PropertyDef.Name))
                        {
                            NameToTypeAssociation propertyDefNameRegistration = RegisterPropertyDefintionName(PropertyDef.Name);
                            trans.Save(propertyDefNameRegistration);
                        }
                        else
                        {
                            throw new PropertyNameAlreadyDefinedException(PropertyDef.Name);
                        }
                    }
                    else
                    {
                        trans.SaveOrUpdate(PropertyDef);

                    }
                    result = persistor.ProcessCommandsTransaction(trans);

                    if (result.Success)
                    {
                        lock (newPropertyDefintions)
                        {
                            newPropertyDefintions.Remove(PropertyDef.Id);
                        }
                    }
                    else
                    {
                        //not sure what to do
                    }
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.AddNotification(ex.Message, ex);
            }

            return result;
        }


        /// <summary>
        /// Deletes the property definition and removes it from the database.
        /// </summary>
        /// <param name="PropDef">The prop def.</param>
        /// <returns></returns>
        public OperationReport DeletePropertyDefinition(PropertyDefinition PropDef)
        {
            OperationReport validationReport = GuardAgainstNulls.ArgsContainsNull(new object[] { PropDef });

            if (!validationReport.Success)
            {
                throw new OrcaArgumentNullException(validationReport);
            }
            if (PropDef.IsPublished)
            {
                throw new CanNotDeletePublishedDomainObject(PropDef.Name);
            }

            if (newPropertyDefintions.ContainsKey(PropDef.Id))
            {
                newPropertyDefintions.Remove(PropDef.Id);
                persistor.CancelTransaction(PropDef.Id);

                return new OperationReport("", true);
            }


            var trans = GetPersistTransactionFor(PropDef.Id);

            trans.Delete(PropDef);

            return persistor.ProcessCommandsTransaction(trans);
        }


        public bool PropertyDefinitionNameAvaliable(string PropertyName)
        {

            return nameManager.IsNameAvailable(dynamicPropertyDefinionType, PropertyName);
        }


        public NameToTypeAssociation RegisterPropertyDefintionName(string PropertyName)
        {
            return nameManager.RegisterName(dynamicPropertyDefinionType, PropertyName);

        }

        public OperationReport CanPublish(PropertyDefinition propertyDef)
        {
            OperationReport validationReport = GuardAgainstNulls.ArgsContainsNull(new object[] { propertyDef });

            if (!validationReport.Success)
            {
                throw new OrcaArgumentNullException(validationReport);
            }

            return publishService.CanPublish(propertyDef);
        }

        public OperationReport PublishPropertyDefinition(PropertyDefinition propertyDef)
        {
            var operationReport = CanPublish(propertyDef);
            if (operationReport.Success)
            {
                return publishService.Publish(propertyDef);
            }

            if (operationReport.ExceptionCount > 0)
            {
                throw operationReport.Exceptions.First();
            }

            return operationReport;
        }

        private PropertyDefinition GetFromNewPropertyList(string PropertyDefinitionName)
        {
            var found = newPropertyDefintions.Where(x => x.Value.Name == PropertyDefinitionName)
                .Select(x => new { x.Key, x.Value })
                .FirstOrDefault();

            if (found == null)
            {
                return null;
            }

            return found.Value;
        }

        public PropertyDefinition FindDynamicPropertyDefinition(string propertyDefinitionName)
        {
            var found = GetFromNewPropertyList(propertyDefinitionName);

            if (found != null) //check to see if it is a new definition that has not been persisted to the db.
            {
                return found;
            }
            return _repository.GetFirst<PropertyDefinition>(x => x.Name == propertyDefinitionName);
        }


        public List<PropertyDefinition> GetAllDynamicPropertyDefintions()
        {
            var result = new List<PropertyDefinition>();

            //var defvaluesFromDb = _repository.Query<PropertyDefinition>().ToList();

            var defvaluesFromDb = _repository.GetAll<PropertyDefinition>();

            result.AddRange(defvaluesFromDb);

            return result;
        }

        public List<PropertyDefinition> GetPublishedDynamicPropertyDefintions()
        {
            var result = new List<PropertyDefinition>();

            var defvaluesFromDb = _repository.GetAll<PropertyDefinition>(x => x.IsPublished);
            //var defvaluesFromDb = _repository.Query<PropertyDefinition>().Where(x => x.IsPublished).ToList();

            result.AddRange(defvaluesFromDb);

            return result;
        }




        private void DeletePropertyDef(PropertyDefinition def)
        {
            IPersistCommandsTransaction transaction;

            transaction = persistor.GetTransaction(def.Id);

            transaction.Delete(def);
        }



        private void OnPersistDataMessageRecieved()
        {
            if (PersistDataMessageRecieved != null)
                PersistDataMessageRecieved(this, EventArgs.Empty);
        }

        #region Define Default Property Values for a type of user object

        public List<DefaultPropertyValue> GetDefaultPropertyValuesDomainObject(Guid objectId)
        {
            var result = new List<DefaultPropertyValue>();

            result.AddRange(typeProperties[objectId].Values.ToList());

            IList<DefaultPropertyValue> defvaluesFromDb = _repository.GetAll<DefaultPropertyValue>(x => x.PropertyOwnerId == objectId);

            result.AddRange(defvaluesFromDb);

            return result;
        }


        public DefaultPropertyValue AddPropertyDefintionToDomainObject(ISupportDefaultPropertyValues DomainObjectForProperty, PropertyDefinition PropertyDef, OperationReport Report)
        {
            OperationReport validationReport = GuardAgainstNulls.ArgsContainsNull(new object[] { DomainObjectForProperty, PropertyDef });

            if (!validationReport.Success)
            {
                throw new OrcaArgumentNullException(validationReport);
            }

            return CreateAndAssignDefaultPropertyValue(DomainObjectForProperty, PropertyDef, Report);
        }


        private DefaultPropertyValue CreateAndAssignDefaultPropertyValue(ISupportDefaultPropertyValues supportPropertiesObject, PropertyDefinition propertyDef, OperationReport operationReport)
        {
            OperationReport report = GuardAgainstNulls.ArgsContainsNull(new object[] { supportPropertiesObject, propertyDef });

            if (!report.Success)
            {
                operationReport.SetValues(report);
                return null;
            }

            report = ValidateCanAssociateDefaultPropertyValueToObject(supportPropertiesObject, propertyDef);

            if (!report.Success)
            {
                operationReport.SetValues(report);

                return null;
            }

            var propDefaultValue = new DefaultPropertyValue(propertyDef);



            supportPropertiesObject.AddDefaultPropertyValue(propDefaultValue);

            lock (typeProperties)
            {
                typeProperties[supportPropertiesObject.UserTypeID].Add(propDefaultValue.Name, propDefaultValue);
            }

            return propDefaultValue;
        }


        public OperationReport RemovePropertyValueFromDomainObject(ISupportDefaultPropertyValues UserTypeObject, PropertyDefinition PropertyDef)
        {
            OperationReport report = GuardAgainstNulls.ArgsContainsNull(new object[] { UserTypeObject, PropertyDef });

            if (!report.Success)
            {
                throw new OrcaArgumentNullException(report);

            }
            return RemoveDefaultPropertyValue(UserTypeObject, PropertyDef);
        }

        private OperationReport RemoveDefaultPropertyValue(ISupportDefaultPropertyValues supportPropertiesObject, PropertyDefinition PropertyDef)
        {
            OperationReport result = new OperationReport();

            var defaultPropValue = GetDefaultPropertyValue(supportPropertiesObject, PropertyDef);

            if (defaultPropValue == null)
            {
                result.Success = false;
                result.AddNotification(string.Format("The {0} domain object does not contain the property {1}", supportPropertiesObject.Name, PropertyDef.Name));
                return result;
            }


            supportPropertiesObject.RemoveDefaultPropertyValue(defaultPropValue);

            var trans = persistor.GetTransaction(supportPropertiesObject.Id);

            trans.Delete(defaultPropValue);

            result.Success = true;

            lock (typeProperties)
            {
                typeProperties[supportPropertiesObject.UserTypeID].Remove(defaultPropValue.Name);
            }

            return result;

        }


        public DefaultPropertyValue GetDefaultPropertyValue(ISupportDefaultPropertyValues dynamicPropertyObject, PropertyDefinition propertyDef)
        {
            OperationReport report = GuardAgainstNulls.ArgsContainsNull(new object[] { dynamicPropertyObject, propertyDef });

            if (!report.Success)
            {
                throw new OrcaArgumentNullException(report);

            }

            if (!typeProperties[dynamicPropertyObject.UserTypeID].ContainsKey(propertyDef.Name))
            {

                var result = _repository.GetFirst<DefaultPropertyValue>(x => x.PropertyDefinitionStaticID == propertyDef.StaticInstanceID && x.PropertyOwnerId == dynamicPropertyObject.Id);
                //                var result = _repository.Query<DefaultPropertyValue>().Where(
                //                x => x.PropertyDefinition.StaticInstanceID == propertyDef.StaticInstanceID && x.UserTypeID == dynamicPropertyObject.UserTypeID).FirstOrDefault();

                return result;
            }

            return typeProperties[dynamicPropertyObject.UserTypeID][propertyDef.Name];

        }


        public bool HasDefaultPropertyValue(ISupportDefaultPropertyValues dynamicPropertyObject, PropertyDefinition propertyDef)
        {
            OperationReport report = GuardAgainstNulls.ArgsContainsNull(new object[] { dynamicPropertyObject, propertyDef });

            if (!report.Success)
            {
                throw new OrcaArgumentNullException(report);
            }


            if (typeProperties[dynamicPropertyObject.UserTypeID].ContainsKey(propertyDef.Name))
            {
                return true;
            }

            var count = _repository.GetCount<DefaultPropertyValue>(
                x => x.PropertyDefinitionStaticID == propertyDef.StaticInstanceID && x.PropertyOwnerId == dynamicPropertyObject.Id);


            return count != 0;

        }

        public OperationReport UpdatePropertyDefinition(ISupportDefaultPropertyValues domainObject, DefaultPropertyValue propDefValue)
        {
            OperationReport newOperationReport = new OperationReport { Success = true };

            if (!domainObject.HasProperty(propDefValue.StaticInstanceID))
            {
                newOperationReport.Success = false;

                newOperationReport.AddNotification(string.Format("The domain object {0} does not contain the property definition value {1}", domainObject.Name, propDefValue.Name));

                return newOperationReport;
            }

            //get the transaction for the worktype name or id? need to be consistent.
            try
            {
                var Trans = GetPersistTransactionFor(domainObject.Id);

                Trans.SaveOrUpdate(propDefValue); //save the newly defined propertydefinition value object (to get the id) and add to the worktype

            }
            catch (Exception ex)
            {
                newOperationReport.AddNotification(ex.Message, ex);
            }

            return newOperationReport;
        }




        private OperationReport ValidateCanAssociateDefaultPropertyValueToObject(ISupportDefaultPropertyValues dynamicPropertyObject, PropertyDefinition PropertyDef)
        {
            OperationReport report = new OperationReport();
            report.Success = true;




            //ISupportDefaultPropertyValues dynamicPropertyObject = dynamicPropertyObject.As<ISupportDefaultPropertyValues>();


            if (!PropertyDef.IsPublished)
            {
                //var ex =  ;
                report.AddNotification(string.Format("The property Definition {0} has not been published", PropertyDef.Name), new PropertyDefinitionMustBePublishedException(PropertyDef.Name));
                report.Success = false;
            }



            if (HasDefaultPropertyValue(dynamicPropertyObject, PropertyDef))
            {
                report.AddNotification(string.Format("The domain object {0} already has the property {1} .", dynamicPropertyObject.Name, PropertyDef.Name), new PropertyAlreadyExistException(PropertyDef.Name, dynamicPropertyObject.Name));
                report.Success = false;

            }
            return report;
        }

        #endregion

        #region PropertyValues

        public void BuildPropertyValuesForObject(ISupportPropertyValues domainObject)
        {
            OperationReport report = GuardAgainstNulls.ArgsContainsNull(new object[] { domainObject });

            if (!report.Success)
            {
                throw new OrcaArgumentNullException(report);
            }

            foreach (DefaultPropertyValue propertyDef in GetDefaultPropertyValuesDomainObject(domainObject.Id))
            {
                if (domainObject.HasProperty(propertyDef.StaticInstanceID))
                {
                    continue;
                }

                CreateAndAssignPropertyValue(domainObject, propertyDef);
            }
        }


        private void CreateAndAssignPropertyValue(ISupportPropertyValues dynamicPropertyObject, DefaultPropertyValue propertyDefValue)
        {
            dynamicPropertyObject.AddPropertyValue(propertyDefValue.CreatePropertyValue());
        }

        #endregion



        private IPersistCommandsTransaction GetPersistTransactionFor(Guid DomainObjectId)
        {
            IPersistCommandsTransaction transaction = persistor.GetTransaction(DomainObjectId);

            return transaction;
        }
    }
}