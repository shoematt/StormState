using System.Collections.Generic;
using System.ComponentModel;

namespace Orca.Workflow.ComponentModel
{
    [TypeConverter(typeof(ICustomCommandProviderTypeConverter))]
    public interface ICustomCommandProvider
    {
        IEnumerable<ICustomCommand> GetCustomTriggers();
    }
}
