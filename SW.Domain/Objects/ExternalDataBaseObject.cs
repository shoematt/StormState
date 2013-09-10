using System;
using System.Collections.Generic;
using Orca.Core.Caching;
using Orca.Domain.Objects.BaseObjects;

namespace Orca.Domain.Objects
{
    /// <summary>
    /// Data packet created by the translater to be used to create and or update the workinstruction container.
    /// </summary>
    /// 
    [Serializable]
    public class ExternalDataBaseObject : DomainObject
    {



        DateTime creationDate = DateTime.Now;

        ExternalDataLink externalDataLink = new ExternalDataLink();

        WorkInstructionContainerData parent = null;

        OrcaCache<string, OrcaKeyValue> externalPropertyCache = new OrcaCache<string, OrcaKeyValue>(new Dictionary<string, OrcaKeyValue>(StringComparer.CurrentCultureIgnoreCase), OnMissing);

        OrcaCache<string, OrcaKeyValue> combinedExternalPropertyCache = new OrcaCache<string, OrcaKeyValue>(new Dictionary<string, OrcaKeyValue>(StringComparer.CurrentCultureIgnoreCase), OnMissing);

        /// <summary>
        /// Initializes a new instance of the WorkInstructionContainerData class.
        /// </summary>
        public ExternalDataBaseObject()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkInstructionContainerData"></see> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public ExternalDataBaseObject(string name)
            : base(name)
        {


        }


        /// <summary>
        ///   Called when [missing].
        /// </summary>
        /// <param name = "objectType">Type of the object.</param>
        /// <returns></returns>
        private static OrcaKeyValue OnMissing(string propertyName)
        {
            return new OrcaKeyValue(propertyName);
        }


        public virtual void AddExternalPropertyValue(OrcaKeyValue PropValue)
        {
            StoreOwnedExternalPropertyValue(PropValue);
        }


        public virtual void AddExternalPropertyValue(string Key, string Value)
        {
            StoreOwnedExternalPropertyValue(new OrcaKeyValue(Key, Value));
        }

        public virtual DateTime CreationDate
        {
            get
            {
                return creationDate;
            }
            protected internal set
            {
                creationDate = value;
            }
        }

        public virtual string ExternalKey
        {
            get
            {
                return externalDataLink.ExternalID;
            }
            set
            {

                externalDataLink.ExternalID = value;
            }
        }

        public virtual string ExternalKeyFieldName
        {
            get
            {
                return externalDataLink.ExternalField;
            }
            set
            {

                externalDataLink.ExternalField = value;
            }
        }


        public virtual bool HasExternalProperty(string ExternalPropertyName)
        {
            return combinedExternalPropertyCache.Has(ExternalPropertyName);
        }



        public virtual string GetExternalPropertyValue(string ExternalPropertyName)
        {
            return combinedExternalPropertyCache.Retrieve(ExternalPropertyName).Value;
        }


        /// <summary>
        /// Gets or sets the external property values which are owned by this data object and not part of its parents values.
        /// </summary>
        /// <value>The owned external property values.</value>
        public virtual IList<OrcaKeyValue> OwnedExternalPropertyValues
        {
            get
            {
                return new List<OrcaKeyValue>(externalPropertyCache.GetAll());
            }
            set
            {
                if (value == null)
                {
                    return;
                }
                foreach (OrcaKeyValue item in value)
                {
                    StoreOwnedExternalPropertyValue(item);
                }

            }
        }


        private void StoreOwnedExternalPropertyValue(OrcaKeyValue item)
        {
            externalPropertyCache.Store(item.KeyName, item);
            combinedExternalPropertyCache.Store(item.KeyName, item);
        }



        public virtual IList<OrcaKeyValue> ExternalPropertyValues
        {
            get
            {
                List<OrcaKeyValue> result = new List<OrcaKeyValue>(combinedExternalPropertyCache.GetAll());

                return result;
            }
            set
            {
                if (value == null)
                {
                    return;
                }

            }
        }

        public virtual WorkInstructionContainerData Parent
        {
            get
            {
                return parent;
            }
            set
            {
                if (parent == value)
                    return;
                parent = value;
                if (parent == null)
                {
                    return;
                }

                AddParentPropertyValues(parent.ExternalPropertyValues); //add the parents property values to the containers property values.


            }
        }

        private void AddParentPropertyValues(IList<OrcaKeyValue> parentProperties)
        {
            foreach (OrcaKeyValue item in parentProperties)
            {

                if (!externalPropertyCache.Has(item.KeyName))  //only add the property if it does not already exist. the child will always override the parent properties.
                {
                    combinedExternalPropertyCache.Store(item.KeyName, item);
                }

            }
        }

    }
}
