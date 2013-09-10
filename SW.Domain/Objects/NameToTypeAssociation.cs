using System;
using Orca.Core.Domain;
using Orca.Core.Extensions;
using Orca.Domain.Objects.BaseObjects;

namespace Orca.Domain.Objects
{
    /// <summary>
    ///   Used by the name manager to track the use of names by object type.
    /// </summary>
    [Serializable]
    public class NameToTypeAssociation : PublishableDomainObject, IPublishableDomainObject, IDomainObject
    {

        private Type objectType;
        private string objectTypeName;


        protected internal NameToTypeAssociation()
        {
        }


        public NameToTypeAssociation(Type objectType, string name)
            : base(name)
        {

            this.objectTypeName = objectType.GetShortName();

            this.objectType = objectType;

        }


        public virtual Type ObjectType
        {
            get
            {
                return this.objectType;
            }
            internal set
            {
                this.objectType = value;
            }
        }

        public virtual string ObjectTypeName
        {
            get
            {
                return objectType.Name;
            }
            protected internal set
            {
                objectTypeName = value;
            }
        }

        public override string ToString()
        {
            return string.Format("The name {0} is associated with type {1}", objectType.Name, objectType.Name);

        }

    }
}