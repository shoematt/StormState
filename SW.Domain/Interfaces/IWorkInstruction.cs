using System;
using Orca.Core.Domain;
using Orca.Domain.Objects;
using Orca.Domain.Objects.Constants;

namespace Orca.Domain.Interfaces
{
    /// <summary>
    ///   Represents a single move task.
    /// </summary>
    public interface IWorkInstruction : IDomainObject
    {
        /// <summary>
        ///   The location to move the item from
        /// </summary>
        Location OriginLocation { get; set; }

        /// <summary>
        ///   The location to move the item to.
        /// </summary>
        Location DestinationLocation { get; set; }

        /// <summary>
        ///   The unique id of the truck which completed the work instruction.
        /// </summary>
        int TruckID { get; set; }

        /// <summary>
        ///   The current state of the work instruction
        /// </summary>
        WorkInstructionState State { get; set; }

        /// <summary>
        ///   The date and time the work instruction was started by an operator
        /// </summary>
        DateTime? StartDate { get; set; }

        /// <summary>
        ///   The date and time the work instruction was completed.
        /// </summary>
        DateTime? CompleteDate { get; set; }

        WorkInstructionSystemState SystemStatus { get; }

        Item Item { get; set; }


        void Complete(bool IsCompleted);
    }
}