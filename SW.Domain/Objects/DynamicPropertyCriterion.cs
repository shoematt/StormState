using System;
using Orca.Core.Domain;
using Orca.Domain.Interfaces;
using Orca.Domain.Objects.BaseObjects;

namespace Orca.Domain.Objects
{
    public interface ICriterion
    {

    }


    [Serializable]
    public class DynamicPropertyCriterion : DomainObject, IDomainObject, ICriterion
    {
        DefaultPropertyValue propertyNameProvider;  //the property to get the name

        DefaultPropertyValue propertyValueProvider; //the property to use to pull the value

        ItemSelectionCriteria selectionCriteria;

        string name = string.Empty;

        /// <summary>
        /// Initializes a new instance of the DynamicPropertyCriterion class.
        /// </summary>
        /// <param name="propertyNameProvider"></param>
        /// <param name="propertyValueProvider"></param>
        public DynamicPropertyCriterion( DefaultPropertyValue propertyNameProvider, DefaultPropertyValue propertyValueProvider )
        {
            if ( propertyNameProvider == null )
            {
                throw new ArgumentException( "The propertyNameProvider can not be null" );
            }

            if ( propertyValueProvider == null )
            {
                throw new ArgumentException( "The propertyValueProvider can not be null" );
            }

            this.propertyNameProvider = propertyNameProvider;
            this.propertyValueProvider = propertyValueProvider;

            this.Name = string.Format( "PropertyCriterion {2} Keyprovider = '{0}'; ValueProvider = '{1}'", propertyNameProvider.ExternalMessageKey, propertyValueProvider.ExternalMessageKey, propertyNameProvider.Name );
        }
        /// <summary>
        /// Initializes a new instance of the DynamicPropertyCriterion class.
        /// </summary>
        protected internal DynamicPropertyCriterion( )
        {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicPropertyCriterion"></see> class.
        /// </summary>
        /// <param name="name">The name.</param>
        protected internal DynamicPropertyCriterion( string name )
            : base( name )
        {

        }

        public virtual string PropertyValueName
        {
            get
            {
                return PropertyValueProvider.Name;
            }
        }
        public virtual string PropertyValueExternalKey
        {
            get
            {
                return PropertyValueProvider.ExternalMessageKey;
            }
        }

        public virtual ItemSelectionCriteria SelectionCriteria
        {
            get
            {
                return selectionCriteria;
            }
            set
            {
                if ( selectionCriteria == value )
                    return;
                selectionCriteria = value;
            }
        }

        public virtual string PropertyKeyName
        {
            get
            {
                return PropertyNameProvider.Name;
            }
        }

        public virtual string PropertyKeyExternalKey
        {
            get
            {
                return PropertyNameProvider.ExternalMessageKey;
            }
        }



        public virtual DefaultPropertyValue PropertyNameProvider
        {
            get
            {
                return propertyNameProvider;
            }
            set
            {
                propertyNameProvider = value;
            }
        }

        public virtual DefaultPropertyValue PropertyValueProvider
        {
            get
            {
                return propertyValueProvider;
            }
            set
            {
                propertyValueProvider = value;
            }
        }




        public virtual string GetValue( ISupportDefaultPropertyValues DomainObject )
        {
            if ( !DomainObject.HasProperty( this.PropertyValueProvider.StaticInstanceID ) )
            {
                return string.Empty;
            }

            return DomainObject[PropertyValueProvider.StaticInstanceID].ToString( );
        }
    }
}
