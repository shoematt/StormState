using System;
using Orca.Core.Domain;
using Orca.Domain.Interfaces;
using Orca.Domain.Objects.BaseObjects;

namespace Orca.Domain.Objects
{
    //TODO: figure out a way to create a static epctemplate so the epc_id is not set to null in the db.

    [Serializable]
    public class ItemTemplate : DomainObjectWithDefaultProperties, IDomainObject, ISupportDefaultPropertyValues, IEquatable<ItemTemplate>
    {
        private readonly string _epc = string.Empty;
        /// <summary>
        /// Initializes a new instance of the ItemTemplate class.
        /// </summary>
        public ItemTemplate()
        {
        }

        /// <summary>
        /// Initializes a new instance of the EPCTemplateDTO class.
        /// </summary>
        public ItemTemplate(string name)
            : base(name)
        {
        }

        public virtual Location DefaultStorageLocation
        {
            get
            {
                return null;
            }
            set
            {
            }
        }

        public virtual Location CurrentLocation
        {
            get
            {
                return null;
            }
            set
            {
            }
        }

        public virtual ItemSystemStatus SystemStatus
        {
            get
            {
                return ItemSystemStatus.Unknown;
            }
            set
            {
            }
        }

        public virtual string Epc
        {
            get
            {
                return _epc;
            }
            set
            {
            }
        }

        public virtual bool Equals(ItemTemplate other)
        {
            return base.Equals(other);
        }


        public virtual string CurrentLocationSgln { get; set; }

        public virtual string DefaultLocationSgln { get; set; }

        public virtual string DefaultLocationName { get; set; }


        public virtual Guid CurrentLocationId { get; set; }

        public virtual string CurrentLocationName { get; set; }
    }
}