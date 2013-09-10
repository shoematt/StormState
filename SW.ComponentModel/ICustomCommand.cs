using System;
using System.ComponentModel;

namespace Orca.Workflow.ComponentModel
{
    [TypeConverter(typeof (ICustomCommandTypeConverter))]
    public interface ICustomCommand
    {
        Guid CommandID { get; }

        string Command { get; }

        string Description { get;  }
    }
}