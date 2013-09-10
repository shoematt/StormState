using System;
using System.Collections.Generic;
using Orca.Domain.Objects;

namespace Orca.Domain
{
    [Serializable]
    public class WorkInstructionCreatedEventArgs : EventArgs
    {

        List<WorkInstruction> workInstructions = new List<WorkInstruction>( );


        public WorkInstructionCreatedEventArgs( )
        {

        }


        public WorkInstructionCreatedEventArgs( WorkInstruction workInstruction )
        {
            workInstructions.Add( workInstruction );
        }


        public WorkInstructionCreatedEventArgs( List<WorkInstruction> workInstructionsValue )
            : this( )
        {

            this.workInstructions.AddRange( workInstructionsValue );
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