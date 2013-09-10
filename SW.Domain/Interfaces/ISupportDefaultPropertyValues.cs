using System.Collections.Generic;
using Orca.Domain.Objects;

namespace Orca.Domain.Interfaces
{
    public interface ISupportDefaultPropertyValues : ISupportDynamicProperties
    {



        /// <summary>
        ///   Gets the dynamic properties.
        /// </summary>
        /// <value>The dynamic properties.</value>
        IList<DefaultPropertyValue> DefaultPropertyValues { get; }

        DefaultPropertyValue GetPropertyValueObject(string propertyName);


        /// <summary>
        ///   Adds the dynamic property.
        /// </summary>
        /// <param name = "DefaultPropertyValue">The property value.</param>
        void AddDefaultPropertyValue(DefaultPropertyValue DefaultPropertyValue);

        void RemoveDefaultPropertyValue(DefaultPropertyValue DefaultPropertyValue);
    }
}