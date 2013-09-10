using System;
using Orca.Core.Domain;
using Orca.Domain.Cache;
using Orca.Domain.Objects.Constants;

namespace Orca.Domain.Objects.ViewObjects
{
    [Serializable]
    public class WorkInstructionWithPropertyValuesView : ICacheable, IDomainObject, IDomainViewObject
    {
        // public static readonly IEnumerable<PropertyDescriptor> Properties = typeof( WorkInstructionWithPropertyValuesView ).GetPropertyDescriptors( null );

        private Guid _id;
        private string _propertyValue;
        private string _property;
        public const string NamePropertyName = "Name";
        public const string PropertyName = "Property";
        public const string ExternalMessageKeyPropertyName = "ExternalMessageKey";
        public const string PropertyValueName = "PropertyValue";
        public const string IdPropertyName = "Id";
        public const string DestinationLocation_idPropertyName = "DestinationLocation_Id";
        public const string OriginLocation_idPropertyName = "OriginLocation_Id";
        public const string SystemStatusPropertyName = "SystemStatus";
        public const string TemplateIdPropertyName = "TemplateId";
        public const string CompleteDatePropertyName = "CompleteDate";
        public const string StartDatePropertyName = "StartDate";
        public const string CacheIdPropertyName = "CacheId";

        public virtual string Name { get; set; }
        public virtual string Property
        {
            get
            {
                return _property;
            }
            set
            {
                _property = value;
            }
        }
        public virtual string ExternalMessageKey { get; set; }
        public virtual string PropertyValue
        {
            get
            {
                return _propertyValue;
            }
            set
            {
                _propertyValue = value;
            }
        }
        public virtual Guid Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }
        public virtual Guid CacheId { get; set; }
        public virtual Guid DestinationLocation_Id { get; set; }
        public virtual Guid OriginLocation_Id { get; set; }
        public virtual WorkInstructionSystemState SystemStatus { get; set; }
        public virtual Guid TemplateId { get; set; }
        public virtual DateTime? CompleteDate { get; set; }
        public virtual DateTime? StartDate { get; set; }


        public WorkInstructionWithPropertyValuesView( )
        {
        }


        public virtual DateTime ModifiedDate { get; set; }


        public virtual Guid UserTypeID
        {
            get
            {
                return Guid.Empty;
            }
            set
            {
            }
        }

        public virtual int Version
        {
            get
            {
                return 0;
        }
    }
}
}
