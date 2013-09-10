using System;

namespace Orca.Domain.Objects
{
    public interface IDynamicValue
    {
        Type DataType { get; set; }

        /// <summary>
        ///   Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        object Value { get; set; }

        //   bool TryParse(string stringValue,ref object Value);

        string ToString();
    }
}