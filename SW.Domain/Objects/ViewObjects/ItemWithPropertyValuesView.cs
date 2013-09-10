using System;
using Orca.Core.Domain;
using Orca.Domain.Cache;

namespace Orca.Domain.Objects.ViewObjects
{
    [Serializable]
    public class ItemWithPropertyValuesView : ICacheable, IDomainObject, IDomainViewObject
    {
        public const string IdPropertyName = "Id";
        public const string NamePropertyName = "Name";
        public const string EpcPropertyName = "Epc";
        public const string DefaultStorageLocationIdPropertyName = "DefaultStorageLocationId";
        public const string CurrentLocationIdPropertyName = "CurrentLocationId";
        public const string PropertyName = "Property";
        public const string PropertyValueName = "PropertyValue";
        public const string ItemStatusPropertyName = "SystemStatus";
        public const string TemplateIdPropertyName = "TemplateId";
        public const string ExternalMessageKeyPropertyName = "ExternalMessageKey";
        public const string CacheIdPropertyName = "CacheId";

        public virtual string Epc { get; set; }
        public virtual Guid DefaultStorageLocationId { get; set; }
        public virtual Guid CurrentLocationId { get; set; }
        public virtual ItemSystemStatus SystemStatus { get; set; }
        public virtual Guid TemplateId { get; set; }

        public virtual int Version
        {
            get { return 0; }
        }

        #region ICacheable Members

        public virtual Guid Id { get; set; }

        public virtual string Name { get; set; }

        public virtual DateTime ModifiedDate { get; set; }

        public virtual Guid UserTypeID
        {
            get { return Guid.Empty; }
            set { }
        }

        #endregion

        #region IDomainViewObject Members

        public virtual Guid CacheId { get; set; }
        public virtual string ExternalMessageKey { get; set; }
        public virtual string Property { get; set; }
        public virtual string PropertyValue { get; set; }

        #endregion
    }
}