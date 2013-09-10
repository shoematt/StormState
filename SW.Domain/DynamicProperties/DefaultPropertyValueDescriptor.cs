using System;
using System.ComponentModel;
using Orca.Domain.Interfaces;
using Orca.Domain.Objects;
using Orca.Domain.Objects.BaseObjects;

namespace Orca.Domain.DynamicProperties
{
    public class DefaultPropertyValueDescriptor : PropertyDescriptor
    {
        private readonly Type _componentType;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultPropertyValueDescriptor"/> class.
        /// </summary>
        /// <param name="propertyDefinition">The property definition.</param>
        public DefaultPropertyValueDescriptor( DefaultPropertyValue propertyDefinition )
            : base( propertyDefinition.Name, new Attribute[0] )
        {
            CustomField = propertyDefinition;
            _componentType = typeof ( PublishableDomainObject );
        }

        /// <summary>
        /// Gets or sets the custom field.
        /// </summary>
        /// <value>The custom field.</value>
        public DefaultPropertyValue CustomField { get; private set; }

        /// <summary>
        /// When overridden in a derived class, gets the type of the component this property is bound to.
        /// </summary>
        /// <value></value>
        /// <returns>A <see cref="T:System.Type"/> that represents the type of component this property is bound to. When the <see cref="M:System.ComponentModel.PropertyDescriptor.GetValue(System.Object)"/> or <see cref="M:System.ComponentModel.PropertyDescriptor.SetValue(System.Object,System.Object)"/> methods are invoked, the object specified might be an instance of this type.</returns>
        public override Type ComponentType
        {
            get { return _componentType; }
        }

        /// <summary>
        /// Gets the description of the member, as specified in the <see cref="T:System.ComponentModel.DescriptionAttribute"/>.
        /// </summary>
        /// <value></value>
        /// <returns>The description of the member. If there is no <see cref="T:System.ComponentModel.DescriptionAttribute"/>, the property value is set to the default, which is an empty string ("").</returns>
        public override string Description
        {
            get { return CustomField.Description; }
        }

        /// <summary>
        /// When overridden in a derived class, gets a value indicating whether this property is read-only.
        /// </summary>
        /// <value></value>
        /// <returns>true if the property is read-only; otherwise, false.</returns>
        public override bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// When overridden in a derived class, gets the type of the property.
        /// </summary>
        /// <value></value>
        /// <returns>A <see cref="T:System.Type"/> that represents the type of the property.</returns>
        public override Type PropertyType
        {
            get { return CustomField.DataType; }
        }

        /// <summary>
        /// When overridden in a derived class, returns whether resetting an object changes its value.
        /// </summary>
        /// <param name="component">The component to test for reset capability.</param>
        /// <returns>
        /// true if resetting the component changes its value; otherwise, false.
        /// </returns>
        public override bool CanResetValue( object component )
        {
            return true;
        }

        /// <summary>
        /// When overridden in a derived class, gets the current value of the property on a component.
        /// </summary>
        /// <param name="component">The component with the property for which to retrieve the value.</param>
        /// <returns>
        /// The value of a property for a given component.
        /// </returns>
        public override object GetValue( object component )
        {
            var element = component as ISupportDynamicProperties;

            if ( element != null )
            {
                if ( !element.HasProperty( CustomField.StaticInstanceID ) )
                {
                    return null;
                }

                return element[CustomField.StaticInstanceID];
            }

            return null;
        }

        /// <summary>
        /// When overridden in a derived class, resets the value for this property of the component to the default value.
        /// </summary>
        /// <param name="component">The component with the property value that is to be reset to the default value.</param>
        public override void ResetValue( object component )
        {
            var element = component as ISupportDynamicProperties;

            if ( element != null )
            {
                element[CustomField.StaticInstanceID] = CustomField.Value;
            }
        }

        /// <summary>
        /// When overridden in a derived class, sets the value of the component to a different value.
        /// </summary>
        /// <param name="component">The component with the property value that is to be set.</param>
        /// <param name="value">The new value.</param>
        public override void SetValue( object component, object value )
        {
            if ( value.GetType( ) == PropertyType )
            {
                var element = component as ISupportDynamicProperties;

                if ( element != null )
                {
                    element[CustomField.StaticInstanceID] = value;
                }
            }
        }

        /// <summary>
        /// When overridden in a derived class, determines a value indicating whether the value of this property needs to be persisted.
        /// </summary>
        /// <param name="component">The component with the property to be examined for persistence.</param>
        /// <returns>
        /// true if the property should be persisted; otherwise, false.
        /// </returns>
        public override bool ShouldSerializeValue( object component )
        {
            return false;
        }
    }
}