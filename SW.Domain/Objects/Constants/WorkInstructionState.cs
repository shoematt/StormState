using System;
using Orca.Core.Domain;
using Orca.Domain.Objects.BaseObjects;

namespace Orca.Domain.Objects.Constants
{
    public enum WorkInstructionSystemState
    {
        Incomplete = 1,
        Hold = 2,
        Complete = 3,
        Active = 4
    }

    public interface IWorkInstructionState : IDomainObject
    {
        Guid WorkInstructionId { get; set; }

        bool RequireReason { get; set; }

        string Reason { get; set; }

        DateTime Date { get; set; }

        string UserName { get; set; }

        WorkInstructionState Prev { get; set; }

        WorkInstructionState Next { get; set; }
    }

    [ Serializable ]
    public class WorkInstructionState : DomainObject, IWorkInstructionState
    {
        private DateTime _date;

        /// <summary>
        /// Initializes a new instance of the WorkInstructionState class.
        /// </summary>
        public WorkInstructionState( )
        {
            _date = DateTime.Now;
        }

        /// <summary>
        /// Initializes a new instance of the WorkInstructionState class.
        /// </summary>
        public WorkInstructionState( string name )
            : base( name )
        {
            _date = DateTime.Now;
        }

        #region IWorkInstructionState Members

        public virtual Guid WorkInstructionId { get; set; }

        public virtual bool RequireReason { get; set; }

        public virtual string Reason { get; set; }

        public virtual DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }

        public virtual string UserName { get; set; }

        public virtual WorkInstructionState Prev { get; set; }

        public virtual WorkInstructionState Next { get; set; }

        #endregion
    }

    //public interface IWorkInstructionStatus : IDomainObject
    //{
    //}

    //[ Serializable ]
    //public class WorkInstructionStatus : DomainObject, IWorkInstructionStatus
    //{
    //}
}

/*
 * Status is different from state, in that status gets associated with an epc
 * on workinstruction this would show as epcstatus
 * and be defined by the users, similar to location categories
 * except a workinstruction can only have one status value.
 * 
 * 
 * 
 * 
 
 * 
 * * /


 

/*
 * Use some sort of workinstructionStateManager to track the different states and messages that go with it.
 * each state would be an object that implements an interface which could be loaded at runtime so we can add or remove states.
 * the interface would have to have some sort of static identifier per type
indicate complete
indicate can not complete
        capture reason why can not complete
indicate cancelled
indicate active
 
Base Status
The base status represents the basic state of the work instruction.  Values include:
 * work containers should show the status of the workinstruction if it is anything but complete.
 * should we be able to set the container status and have it set the the workinstructions it holds status.
•	Open -- active
•	On Hold --
•	Completed --
•	Cancelled
•	CantComplete
        capture reason why can not complete
 * 
 
  
Working Status
The working status gives an indication of where in the execution process this work instruction is.   Values include:
•	Viewed (indicates work instruction is present on one or more trucks)
•	Loaded (indicates operator has load on forks matching work instruction)

 
  
  Ownership Status
The ownership status indicates if an operator has indicated explicit ownership of a work instruction.  Values include:
•	Owned (indicates work instruction is owned by a specific driver)


*/