using System;
using System.Collections.Generic;
using Orca.Domain.Objects;

namespace Orca.Domain.Commands
{

    [Serializable]
    public class WorkInstructionCreatedCommand
    {

        List<WorkInstruction> workInstructions = new List<WorkInstruction>( );


        /// <summary>
        /// Initializes a new instance of the <see cref="WorkInstructionComplete"/> class.
        /// </summary>
        public WorkInstructionCreatedCommand( )
        {
        }


        public WorkInstructionCreatedCommand( List<WorkInstruction> workInstructionsValue )
            : this( )
        {
            this.workInstructions.AddRange( workInstructionsValue );
        }

        public WorkInstructionCreatedCommand( WorkInstruction workInstruction )
        {
            workInstructions.Add( workInstruction );
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
