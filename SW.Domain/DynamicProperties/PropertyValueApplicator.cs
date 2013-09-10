using System;
using Orca.Core.Extensions;
using Orca.Domain.Exceptions;
using Orca.Domain.Interfaces;
using Orca.Domain.Objects;

namespace Orca.Domain.DynamicProperties
{
    /// <summary>
    /// Responsible for taking an ISupportDefaultPropertyValues and creating <see cref="PropertyValue"/> objects from each of the <see cref="DefaultPropertyValue"/> Object
    /// </summary>
    public class PropertyValueActivator
    {


    //    private readonly IEntityRepository _repository;

  //      private IPersistor persistor;

   //     private IPublishService publishService;


        /// <summary>
        /// Initializes a new instance of the PropertyValueApplicator class.
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="persistor"></param>
        /// <param name="publishService"></param>
        public PropertyValueActivator()
        {
        //    _repository = repository;
        //    this.persistor = persistor;
         //   this.publishService = publishService;
        }


        public void ApplyProperties(ISupportDefaultPropertyValues DefaultPropertyObject, ISupportPropertyValues DomainObjectInstance)
        {
            if (DefaultPropertyObject == null)
            {
                throw new ArgumentNullException("The DefaultPropertyObject parameter can not be null");
            }

            if (DomainObjectInstance == null)
            {
                throw new ArgumentNullException("The DomainObjectInstance parameter can not be null");
            }

            if (!ValidateDomainObjectInstance(DomainObjectInstance))
            {
                throw new PropertiesAlreadyAssignedExistException(DomainObjectInstance.Name);

            }

            // var trans = persistor.GetTransaction(DomainObjectInstance.Id);

            foreach (DefaultPropertyValue item in DefaultPropertyObject.DefaultPropertyValues)
            {
                PropertyValue propValue = item.CreatePropertyValue();

                //  trans.Save(propValue);

                try
                {
                    DomainObjectInstance.AddPropertyValue(propValue);
                }
                catch (Exception ex)
                {
                    throw new PropertyValueException(string.Format("Exception adding PropertyValue {0} to domain object {1}", item.Name, DomainObjectInstance.Name), ex);

                }
            }

            if (DomainObjectInstance is IHasTemplate)
            {
                DomainObjectInstance.As<IHasTemplate>().TemplateId = DefaultPropertyObject.Id;
            }

            //trans.Save(DomainObjectInstance);

            //OperationReport report = persistor.ProcessCommandsTransaction(trans);

            //if (!report.Success)
            //{
            //    Exception ex = null;
            //    if (report.ExceptionCount > 0)
            //    {
            //        ex = report.Exceptions.First();
            //    }
            //    throw new PropertyValueException(string.Format("Exception persisting PropertyValue to the db for {0} domain object", DomainObjectInstance.Name), ex);
            //}

        }


        private bool ValidateDomainObjectInstance(ISupportPropertyValues DomainObjectInstance)
        {
            return DomainObjectInstance.PropertyValues.Count == 0;
        }


    }
}
