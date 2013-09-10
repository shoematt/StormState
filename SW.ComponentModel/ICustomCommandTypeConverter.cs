using System;
using System.ComponentModel;
using System.Globalization;

namespace Orca.Workflow.ComponentModel
{
    public class ICustomCommandTypeConverter : TypeConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (value is ICustomCommand)
            {
                return ((ICustomCommand) value).Description;
            }

            return value == null ? string.Empty : base.ConvertFrom(context, culture, value);
        }
    }
}