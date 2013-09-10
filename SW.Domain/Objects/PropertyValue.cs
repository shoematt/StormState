using System;
using System.ComponentModel;
using Orca.Domain.Objects.BaseObjects;

namespace Orca.Domain.Objects
{
    /// <summary>
    ///   The dynamic property associated with an instance of an IDomain object that holds the runtime value
    /// </summary>
    [Serializable]
    public class PropertyValue : PropertyBase, IEquatable<PropertyValue>, IComparable<PropertyValue>, IComparable
    {


        private DomainObjectWithPropertyValues propertyOwner;

        private string _defaultValue;


        protected internal PropertyValue( )
        {
        }


        public PropertyValue( DefaultPropertyValue DefaultPropertyValue )
            : base( DefaultPropertyValue.DataType, DefaultPropertyValue.Name )
        {
            this.ExternalMessageKey = DefaultPropertyValue.ExternalMessageKey;

            DefaultPropertyID = DefaultPropertyValue.Id;

            DefaultPropertyStaticID = DefaultPropertyValue.StaticInstanceID;

            this.StaticInstanceID = DefaultPropertyStaticID;

            Value = DefaultPropertyValue.Value;

            _defaultValue = DefaultPropertyValue.StringValue;
        }


        public Guid PropertyOwnerId { get; set; }

        public virtual Guid DefaultPropertyID { get; set; }

        public virtual Guid DefaultPropertyStaticID { get; set; }


        /// <summary>
        ///   Gets or sets the domain object instance which the property is associated with.
        /// </summary>
        /// <value>The domain object.</value>
        [Browsable( false )]
        public virtual DomainObjectWithPropertyValues PropertyOwner
        {
            get { return propertyOwner; }
            set
            {
                if ( propertyOwner == value )
                    return;
                propertyOwner = value;
                if ( value == null )
                {
                    PropertyOwnerId = Guid.Empty;
                    return;
                }
                PropertyOwnerId = value.Id;
            }
        }



        [Browsable( false )]
        public virtual string DefaultValue
        {
            get
            {
                return _defaultValue;
            }
            set
            {
                if ( string.Compare( _defaultValue, value ) == 0 )
                {
                    return;
                }
                _defaultValue = value;
                OnPropertyChanged( "DefaultValue" );
            }
        }




        //for property value, equals means the same value and the object it is attached to
        public virtual bool Equals( PropertyValue other )
        {
            if ( other == null )
            {
                return false;
            }

            bool result = (this as PropertyBase).Equals( other as PropertyBase );
            return result && (PropertyOwnerId == other.PropertyOwnerId);
        }


        public virtual int CompareTo( PropertyValue other )
        {

            if ( other == null )
            {
                return -1;
            }
            if ( Equals( other ) )
            {
                return 0;
            }
            return -1;
        }

        public virtual int CompareTo( object obj )
        {
            return CompareTo( obj as PropertyValue );
        }


    }
}