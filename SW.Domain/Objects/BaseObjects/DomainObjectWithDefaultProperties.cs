using System;
using System.Collections.Generic;
using System.ComponentModel;
using Orca.Core.Extensions;
using Orca.Domain.DynamicProperties;
using Orca.Domain.Exceptions;
using Orca.Domain.Interfaces;

namespace Orca.Domain.Objects.BaseObjects
{
    [Serializable]
    public abstract class DomainObjectWithDefaultProperties : PublishableDomainObject
                                                              , ISupportDefaultPropertyValues
                                                              , IEquatable<DomainObjectWithDefaultProperties>
    {
        protected readonly PropertiesImplementation<DefaultPropertyValue> DefaultPropertyValueContainer;// = new PropertiesImplementation<DefaultPropertyValue>( );





        /// <summary>
        ///   Initializes a new instance of the DomainObjectWithDefaultProperties class.
        /// </summary>
        protected DomainObjectWithDefaultProperties( )
        {
            DefaultPropertyValueContainer = new PropertiesImplementation<DefaultPropertyValue>( this );
        }

        /// <summary>
        ///   Initializes a new instance of the DomainObjectWithDefaultPropertiesDTO class.
        /// </summary>
        protected DomainObjectWithDefaultProperties( string name )
            : base( name )
        {
            DefaultPropertyValueContainer = new PropertiesImplementation<DefaultPropertyValue>( this );
        }


        /// <summary>
        ///   Gets the dynamic properties.
        /// </summary>
        /// <value>The dynamic properties.</value>
        [Browsable( false )]
        public virtual IList<DefaultPropertyValue> DefaultPropertyValues
        {
            get { return DefaultPropertyValueContainer.Properties; }
            set { DefaultPropertyValueContainer.Properties = value; }
        }

        /// <summary>
        ///   Gets or sets the <see cref = "System.Object" /> with the specified static instance ID.
        /// </summary>
        /// <value></value>
        public virtual object this[Guid staticInstanceID]
        {
            get { return DefaultPropertyValueContainer[staticInstanceID].As<DefaultPropertyValue>( ).Value; }
            set { DefaultPropertyValueContainer[staticInstanceID].As<DefaultPropertyValue>( ).Value = value; }
        }

        /// <summary>
        ///   Gets or sets the <see cref = "System.Object" /> with the specified property name.
        /// </summary>
        /// <value></value>
        public virtual object this[string propertyName]
        {
            get { return DefaultPropertyValueContainer[propertyName].As<DefaultPropertyValue>( ).Value; }
            set { DefaultPropertyValueContainer[propertyName].As<DefaultPropertyValue>( ).Value = value; }
        }


        public virtual bool HasProperty( Guid staticInstanceID )
        {
            return DefaultPropertyValueContainer.HasProperty( staticInstanceID );
        }

        /// <summary>
        ///   Determines whether the specified property name has property.
        /// </summary>
        /// <param name = "propertyName">Name of the property.</param>
        /// <returns>
        ///   <c>true</c> if the specified property name has property; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool HasProperty( string propertyName )
        {
            return DefaultPropertyValueContainer.HasProperty( propertyName );
        }

        /// <summary>
        ///   Adds the default property value.
        /// </summary>
        /// <param name = "defaultPropValue">The dyn prop def value.</param>
        public virtual void AddDefaultPropertyValue( DefaultPropertyValue defaultPropValue )
        {
            defaultPropValue.PropertyOwner = this;
            DefaultPropertyValueContainer.AddProperty( defaultPropValue );
            OnDefaultPropertiesChanged( true, defaultPropValue );
        }

        /// <summary>
        ///   Removes the default property value.
        /// </summary>
        /// <param name = "dynPropDefValue">The dyn prop def value.</param>
        public virtual void RemoveDefaultPropertyValue( DefaultPropertyValue dynPropDefValue )
        {
            DefaultPropertyValueContainer.Remove( dynPropDefValue );
            OnDefaultPropertiesChanged( false, dynPropDefValue );
        }



        public virtual DefaultPropertyValue GetPropertyValueObject( string propertyName )
        {
            if ( !HasProperty( propertyName ) )
            {
                throw new PropertyDoesNotExistException( propertyName );
            }
            return DefaultPropertyValueContainer[propertyName].As<DefaultPropertyValue>( );
        }



        public virtual event EventHandler<DefaultProperyValuesChangedEventArgs> DefaultPropertiesChanged;

        /// <summary>
        ///   Triggers the DefaultPropertiesChanged event.
        /// </summary>
        protected virtual void OnDefaultPropertiesChanged( bool isAdding, DefaultPropertyValue propDef )
        {
            if ( DefaultPropertiesChanged != null )
                DefaultPropertiesChanged( this, new DefaultProperyValuesChangedEventArgs( isAdding, propDef ) );
        }

        public virtual bool Equals( DomainObjectWithDefaultProperties other )
        {
            return base.Equals( other );
        }
    }
}