using System;
using System.ComponentModel;

using Orca.Domain.Objects.BaseObjects;
using Orca.Domain.Cache;
using Orca.Domain.Interfaces;

namespace Orca.Domain.Objects
{
    public enum ItemSystemStatus
    {
        Available,
        NotAvailable,
        Unknown
    }

    [Serializable]
    public class Item : DomainObjectWithPropertyValues
                               , ISupportPropertyValues
                               , IHasTemplate
                               , IEquatable<Item>
                                , ICacheable
    {

        public const string SystemStatusPropertyName = "SystemStatus";
        //    public const string TemplateIdPropertyName = "TemplateId";

        private ItemTemplate _template;
        private Location _currentLocation;
        private string _currentLocationName;
        private string _currentLocationSgln;
        private string _defaultLocationName;
        private string _defaultLocationSgln;
        private Location _defaultStorageLocation;

        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class.
        /// </summary>
        public Item( )
        {
            TemplateId = Guid.Empty;
            SystemStatus = ItemSystemStatus.Available;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public Item( string name )
            : base( name )
        {
            TemplateId = Guid.Empty;
            SystemStatus = ItemSystemStatus.Available;
        }

        /// <summary>
        /// Gets or sets the system status of an item.  This indicates the availability
        /// of the item to fulfill work instructions.
        /// </summary>
        /// <value>
        /// The item status.
        /// </value>
        public virtual ItemSystemStatus SystemStatus { get; set; }

        public virtual string Epc { get; set; }

        public virtual string CurrentLocationSgln
        {
            get
            {
                return _currentLocationSgln;
            }
            set
            {
                _currentLocationSgln = value;
            }
        }

        public virtual string DefaultLocationSgln
        {
            get
            {
                return _defaultLocationSgln;
            }
            set
            {
                _defaultLocationSgln = value;
            }
        }

        public virtual string DefaultLocationName
        {
            get
            {
                return _defaultLocationName;
            }
            set
            {
                _defaultLocationName = value;
            }
        }


        public virtual Guid CurrentLocationId { get; set; }

        public virtual string CurrentLocationName
        {
            get
            {
                return _currentLocationName;
            }
            set
            {
                _currentLocationName = value;
            }
        }

        public virtual Location DefaultStorageLocation
        {
            get
            {
                return _defaultStorageLocation;
            }
            set
            {
                _defaultStorageLocation = value;
                if ( _defaultStorageLocation != null )
                {
                    DefaultLocationSgln = _defaultStorageLocation.Sgln;
                    DefaultLocationName = _defaultStorageLocation.Name;
                }
                else
                {
                    DefaultLocationSgln = string.Empty;
                    DefaultLocationName = string.Empty;
                }
            }
        }

        [Browsable( false )]
        public virtual Location CurrentLocation
        {
            get
            {
                return _currentLocation;
            }
            set
            {
                _currentLocation = value;

                if ( _currentLocation != null )
                {
                    CurrentLocationSgln = _currentLocation.Sgln;
                    CurrentLocationName = _currentLocation.Name;
                    CurrentLocationId = _currentLocation.Id;
                }
                else
                {
                    CurrentLocationSgln = string.Empty;
                    CurrentLocationName = string.Empty;
                    CurrentLocationId = Guid.Empty;
                }
            }
        }

        /// <summary>
        ///   Gets or sets the template.
        /// </summary>
        /// <value>The template.</value>
        public virtual ItemTemplate Template
        {
            get
            {
                return _template;
            }
            set
            {
                _template = value;

                if ( _template != null )
                {
                    TemplateId = _template.Id;
                }
            }
        }



        public virtual bool Equals( Item other )
        {
            return base.Equals( other );
        }

        //public virtual Guid CacheId
        //{
        //    get { return Id; }
        //}
    }
}