using System;
using System.ComponentModel;
using Orca.Domain.Objects.BaseObjects;

namespace Orca.Domain.Objects
{
    /// <summary>
    ///   The object which contains the values to use when a dynamic property definition is associated with a domain object.
    ///   Acts as a proxy to the dynamic property definition object to allow changing the name for the associated domain object.
    /// </summary>
    [Serializable]
    public class DefaultPropertyValue : PropertyBase, IEquatable<DefaultPropertyValue>
    {

        private DomainObjectWithDefaultProperties propertyOwner;

        protected internal DefaultPropertyValue( )
        {

        }


        public DefaultPropertyValue( PropertyDefinition dynamicPropertyDefinition )
            : base( dynamicPropertyDefinition.DataType, dynamicPropertyDefinition.Name )
        {
            //  propertyDefinition = dynamicPropertyDefinition;

            PropertyOwnerId = Guid.Empty;

            PropertyDefinitionID = dynamicPropertyDefinition.Id;

            PropertyDefinitionStaticID = dynamicPropertyDefinition.StaticInstanceID;

            this.StaticInstanceID = PropertyDefinitionStaticID;

            if ( dynamicPropertyDefinition.Value != null )
            {
                Value = dynamicPropertyDefinition.Value;
            }
        }

        public virtual PropertyValue CreatePropertyValue( )
        {
            var dynamicPropValueObject = new PropertyValue( this );

            return dynamicPropValueObject;
        }



        public virtual Guid PropertyDefinitionID { get; set; }

        public virtual Guid PropertyDefinitionStaticID { get; set; }

        public Guid PropertyOwnerId { get; set; }

        [Browsable( false )]
        public virtual DomainObjectWithDefaultProperties PropertyOwner
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
                PropertyOwnerId = propertyOwner.Id;

            }
        }


        //for default property value, equals means the same property definition static instance id and the object it is attached to
        public virtual bool Equals( DefaultPropertyValue other )
        {
            if ( other == null )
            {
                return false;
            }

            //       bool result = (this as PropertyBase).Equals(other as PropertyBase);

            return (PropertyDefinitionStaticID == other.PropertyDefinitionStaticID)
                //&& (PropertyDefinitionID == other.PropertyDefinitionID) //the id of the definition could change but static should remain the same
                && (PropertyOwnerId == other.PropertyOwnerId);
        }




        public override bool Equals( object obj )
        {
            if ( ReferenceEquals( this, obj ) ) return true;
            return Equals( obj as DefaultPropertyValue );
        }


        public override int GetHashCode( )
        {
            int result = 2;

            result = 29 * result + PropertyDefinitionID.GetHashCode( );

            return result;
        }


    }



}