using System;
using System.Collections.Generic;
using Orca.Domain.Objects.BaseObjects;
using Orca.Domain.Objects.Constants;

namespace Orca.Domain.Objects
{



    [Serializable]
    public class WorkInstructionContainer : DomainObject
    {
        List<WorkInstructionContainer> _containers = new List<WorkInstructionContainer>();

        private Dictionary<Guid, WorkInstruction> _workInstructions = new Dictionary<Guid, WorkInstruction>();

        WorkInstructionContainer _parentContainer = null;

        private DateTime _lastUpdateDate;

        CompletionInfo _completionInfo;

        DateTime creationDate = DateTime.Now;

        //      ExternalDataLink externalContainerLink;



        IWorkInstructionState state;



        WorkInstructionContainerData data = null;


        /// <summary>
        /// Initializes a new instance of the WorkInstructionContainer class.
        /// </summary>
        protected internal WorkInstructionContainer()
        {
            _completionInfo = new CompletionInfo();


        }


        public WorkInstructionContainer(WorkInstructionContainerData data)
            : this()
        {
            this.data = data;
        }

        public virtual DateTime CreationDate
        {
            get
            {
                return creationDate;
            }
            protected internal set
            {
                creationDate = value;
            }
        }


        public virtual WorkInstructionContainer ParentContainer
        {
            get
            {
                return _parentContainer;
            }
            protected internal set
            {
                if (_parentContainer == value)
                    return;
                _parentContainer = value;
            }
        }






        public virtual DateTime lastUpdateDate
        {
            get
            {
                return _lastUpdateDate;
            }
            set
            {
                if (_lastUpdateDate == value)
                    return;
                _lastUpdateDate = value;
            }
        }

        public virtual IWorkInstructionState State
        {
            get
            {
                return state;
            }
            set
            {
                if (state == value)
                    return;
                state = value;
            }
        }

        public virtual bool IsCompleted
        {
            get
            {
                return _completionInfo.IsFinialized;
            }
            protected internal set { }
        }



        public virtual CompletionInfo completionInfo
        {
            get
            {
                return _completionInfo;
            }
            protected internal set
            {
                if (_completionInfo == value)
                    return;
                _completionInfo = value;
            }
        }



    }
}