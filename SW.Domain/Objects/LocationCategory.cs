using System;
using System.Collections.Generic;
using Orca.Domain.Objects.BaseObjects;

namespace Orca.Domain.Objects
{
    [Serializable]
    public class LocationCategory : DomainObject, IEquatable<LocationCategory>
    {
        private int _categoryValue;

        private IList<Location> _locations = new List<Location>( );

        /// <summary>
        /// Initializes a new instance of the LocationCategory class.
        /// </summary>
        public LocationCategory( )
        {
        }

        /// <summary>
        /// Initializes a new instance of the LocationCategoryDTO class.
        /// </summary>
        public LocationCategory( string name )
            : base( name )
        {
        }


        /// <summary>
        /// Gets or sets the category value.
        /// </summary>
        /// <value>
        /// The category value.
        /// </value>
        public virtual int CategoryValue
        {
            get { return _categoryValue; }
            set { _categoryValue = value; }
        }


        /// <summary>
        /// Gets or sets the locations.
        /// </summary>
        /// <value>
        /// The locations.
        /// </value>
        public virtual IList<Location> Locations
        {
            get { return _locations; }
            protected internal set
            {
                if ( value != null )
                {
                    _locations = value;
                }
            }
        }

        public override string ToString( )
        {
            return Name;
        }

        public bool Equals( LocationCategory other )
        {
            return base.Equals( other );
        }
    }
}