using System;

namespace Orca.Domain.Interfaces
{
    public interface IDynamicProperty
    {
        /// <summary>
        ///   Gets or sets the type of the data.
        /// </summary>
        /// <value>The type of the data.</value>
        Type DataType { get; }

        /// <summary>
        ///   Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        string Description { get; set; }

        /// <summary>
        ///   Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        object Value { get; set; }
    }
}