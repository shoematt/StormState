using System.ComponentModel;

namespace Orca.Workflow.ComponentModel
{
    public class ICustomCommandProviderTypeConverter : TypeConverter
    {
        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if(value is ICustomCommandProvider)
            {
                return TypeDescriptor.GetComponentName(value);
            }

            return base.ConvertFrom(context, culture, value);
        }
    }
}