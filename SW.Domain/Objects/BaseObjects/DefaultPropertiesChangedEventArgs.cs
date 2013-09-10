using System;

namespace Orca.Domain.Objects.BaseObjects
{
    public class DefaultPropertiesChangedEventArgs : EventArgs
    {
        /// <summary>
        ///   Initializes a new instance of the DefaultPropertiesChangedEventArgs class.
        /// </summary>
        /// <param name = "isAdding"></param>
        /// <param name = "property"></param>
        public DefaultPropertiesChangedEventArgs(bool isAdding, PropertyDefinition property)
        {
            Add = isAdding;
            Property = property;
        }

        /// <summary>
        ///   Gets or sets the property.
        /// </summary>
        /// <value>The property.</value>
        public PropertyDefinition Property { get; private set; }

        /// <summary>
        ///   Gets or sets a value indicating whether this <see cref = "DefaultPropertiesChangedEventArgs" /> is add.
        /// </summary>
        /// <value><c>true</c> if add; otherwise, <c>false</c>.</value>
        public bool Add { get; private set; }
    }



    public class DefaultProperyValuesChangedEventArgs : EventArgs
    {
        /// <summary>
        ///   Initializes a new instance of the DefaultPropertiesChangedEventArgs class.
        /// </summary>
        /// <param name = "isAdding"></param>
        /// <param name = "property"></param>
        public DefaultProperyValuesChangedEventArgs(bool isAdding, DefaultPropertyValue property)
        {
            Add = isAdding;
            Property = property;
        }

        /// <summary>
        ///   Gets or sets the property.
        /// </summary>
        /// <value>The property.</value>
        public DefaultPropertyValue Property { get; private set; }

        /// <summary>
        ///   Gets or sets a value indicating whether this <see cref = "DefaultPropertiesChangedEventArgs" /> is add.
        /// </summary>
        /// <value><c>true</c> if add; otherwise, <c>false</c>.</value>
        public bool Add { get; private set; }
    }
}
