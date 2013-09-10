using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orca.Domain.Objects;
using System.Runtime.Serialization;

namespace Orca.Domain.Commands
{
    [Serializable]
    public class AddWorkInstructionCommand : BasePropertyValueCacheCommand<WorkInstruction>
    {

        public AddWorkInstructionCommand(WorkInstruction objectValue)
            : base(objectValue)
        {
        }
        public AddWorkInstructionCommand(List<WorkInstruction> objects)
            : base(objects)
        {
        }
        public AddWorkInstructionCommand()
        {
        }

    }
}
