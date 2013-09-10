using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Orca.Core.Domain;
using Orca.Core.Extensions;
using Orca.Domain.DynamicProperties;
using Orca.Domain.Exceptions;
using Orca.Domain.Interfaces;

namespace Orca.Domain.Objects.BaseObjects
{
    /// <summary>
    ///   The object which has the dynamic properties assigned
    /// </summary>
    [Serializable]
    public abstract class DomainObjectWithPropertyValues : PublishableDomainObject, IPublishableDomainObject, ISupportPropertyValues, IEquatable<DomainObjectWithPropertyValues>
    {
        public const string TemplateIdPropertyName = "TemplateId";


        protected readonly PropertiesImplementation<PropertyValue> PropertyValueContainer;// = new PropertiesImplementation<PropertyValue>( );

        protected Guid _templateId = Guid.Empty;

        /// <summary>
        ///   Initializes a new instance of the DynamicPropertyDomainObject class.
        /// </summary>
        public DomainObjectWithPropertyValues( )
        {
            PropertyValueContainer = new PropertiesImplementation<PropertyValue>( this );
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref = "DomainObjectWithPropertyValues" /> class.
        /// </summary>
        /// <param name = "name">The name.</param>
        public DomainObjectWithPropertyValues( string name )
            : base( name )
        {
            PropertyValueContainer = new PropertiesImplementation<PropertyValue>(this);
        }


        public virtual Guid TemplateId
        {
            get
            {
                return _templateId;
            }
            set
            {
                _templateId = value;
            }
        }



        [Browsable( false )]
        public virtual IList<PropertyValue> PropertyValues
        {
            get { return PropertyValueContainer.Properties; }
            set
            {
                PropertyValueContainer.Properties = value;
                foreach ( PropertyValue propValue in PropertyValueContainer.Properties )
                {
                    propValue.PropertyOwner = this;
                }
            }
        }


        public virtual object this[Guid staticInstanceID]
        {
            get
            {
                if ( !HasProperty( staticInstanceID ) )
                {
                    throw new PropertyDoesNotExistException( );
                }

                return PropertyValueContainer[staticInstanceID].As<PropertyValue>( ).Value;
            }

            set
            {
                if ( !HasProperty( staticInstanceID ) )
                {
                    throw new PropertyDoesNotExistException( );
                }
                PropertyValueContainer[staticInstanceID].As<PropertyValue>( ).Value = value;
            }
        }


        public virtual object this[string propertyName]
        {
            get
            {

                if ( !HasProperty( propertyName ) )
                {
                    throw new PropertyDoesNotExistException( propertyName );
                }
                return PropertyValueContainer[propertyName].As<PropertyValue>( ).Value;
            }

            set
            {
                if ( !HasProperty( propertyName ) )
                {
                    throw new PropertyDoesNotExistException( propertyName );
                }
                PropertyValueContainer[propertyName].As<PropertyValue>( ).Value = value;
            }
        }


        public virtual bool HasProperty( Guid staticInstanceID )
        {
            return PropertyValueContainer.HasProperty( staticInstanceID );
        }


        public virtual bool HasProperty( string propertyName )
        {
            return PropertyValueContainer.HasProperty( propertyName );
        }


        public virtual void AddPropertyValue( PropertyValue propertyValue )
        {
            propertyValue.PropertyOwner = this;
            PropertyValueContainer.AddProperty( propertyValue );
        }



        public virtual PropertyValue GetPropertyValueObject( string propertyName )
        {
            if ( !HasProperty( propertyName ) )
            {
                throw new PropertyDoesNotExistException( propertyName );
            }
            return PropertyValueContainer[propertyName].As<PropertyValue>( );
        }



        public virtual void RemovePropertyValue( PropertyValue propertyValue )
        {
            PropertyValueContainer.Remove( propertyValue );
        }


        public virtual List<Dictionary<string, PropertyDescriptor>> GetPropertyValues( )
        {
            List<Dictionary<string, PropertyDescriptor>> result = new List<Dictionary<string, PropertyDescriptor>>( );

            Dictionary<string, PropertyDescriptor> propertyValues = new Dictionary<string, PropertyDescriptor>( StringComparer.CurrentCultureIgnoreCase );

            Dictionary<string, PropertyDescriptor> dynamicPropertyValues = new Dictionary<string, PropertyDescriptor>( StringComparer.CurrentCultureIgnoreCase );

            result.Add( propertyValues );

            result.Add( dynamicPropertyValues );


            TypeDescriptionProvider typeProvider = TypeDescriptor.GetProvider( typeof( DomainObjectWithPropertyValues ) );

            var propDescriptors = typeProvider.GetTypeDescriptor( typeof( DomainObjectWithPropertyValues ), this ).GetProperties( ).Cast<PropertyDescriptor>( );

            var propertyInfos = propDescriptors.Where( p => !p.IsReadOnly );

            PropertyValueDescriptor propValueDescriptor = null;

            foreach ( PropertyDescriptor item in propertyInfos )
            {

                if ( !propertyValues.ContainsKey( item.Name ) )
                {
                    propertyValues.Add( item.Name, item );
                }


                propValueDescriptor = item as PropertyValueDescriptor;
                if ( propValueDescriptor != null )
                {


                    if ( !string.IsNullOrEmpty( propValueDescriptor.CustomField.ExternalMessageKey ) ) //only add it to the collection if the external message key is not empty
                    {
                        if ( !dynamicPropertyValues.ContainsKey( propValueDescriptor.CustomField.ExternalMessageKey ) )
                        {
                            dynamicPropertyValues.Add( propValueDescriptor.CustomField.ExternalMessageKey, item ); //add to the dynamic property collection.
                        }
                    }
                    else
                    {
                        if ( !dynamicPropertyValues.ContainsKey( item.Name ) )
                        {
                            dynamicPropertyValues.Add( item.Name, item ); //add to the dynamic property collection.
                        }
                    }
                }


            }
            return result;
        }

        public virtual bool Equals( DomainObjectWithPropertyValues other )
        {
            return base.Equals( other );
        }
    }
}