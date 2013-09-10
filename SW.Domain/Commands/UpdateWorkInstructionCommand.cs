using System;
using System.Collections.Generic;
using Orca.Domain.Objects;

namespace Orca.Domain.Commands
{
    [Serializable]
    public class UpdateWorkInstructionCommand
    {
        List<WorkInstruction> workInstructions = new List<WorkInstruction>( );

        public UpdateWorkInstructionCommand( WorkInstruction workInstruction )
        {
            workInstructions.Add( workInstruction );
        }
        public UpdateWorkInstructionCommand( List<WorkInstruction> workInstructionsValue )
            : this( )
        {
            this.workInstructions.AddRange( workInstructionsValue );
        }

        public UpdateWorkInstructionCommand( )
        {

        }

        public List<WorkInstruction> WorkInstructions
        {
            get
            {
                return workInstructions;
            }
        }
    }
}
