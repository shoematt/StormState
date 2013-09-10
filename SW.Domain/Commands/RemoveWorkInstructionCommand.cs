using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orca.Domain.Objects;
using System.Runtime.Serialization;

namespace Orca.Domain.Commands
{
    [Serializable]
    public class RemoveWorkInstructionCommand : BasePropertyValueCacheCommand<WorkInstruction>
    {

        public RemoveWorkInstructionCommand(WorkInstruction objectValue)
            : base(objectValue)
        {

        }
        public RemoveWorkInstructionCommand(List<WorkInstruction> objects)
            : base(objects)
        {

        }
        public RemoveWorkInstructionCommand()
        {

        }


    }
}
