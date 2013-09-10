using System;
using System.Collections.Generic;
using Orca.Core.Persistence;
using Orca.Domain.Objects;

namespace Orca.Domain.Interfaces
{
    public interface IWorkInstructionFactory
    {
        /// <summary>
        /// Creates the work instruction.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="report">The report.</param>
        /// <returns></returns>
        WorkInstruction CreateWorkInstruction( WorkInstructionData data, OperationReport report );
        /// <summary>
        /// Occurs when [work instruction created].
        /// </summary>
        event EventHandler<WorkInstructionCreatedEventArgs> WorkInstructionCreated;

        List<WorkInstruction> CreateWorkInstructions( List<WorkInstructionData> data, OperationReport report );
    }
}
