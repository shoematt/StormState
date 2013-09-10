using System;
using System.Collections.Generic;

namespace Orca.Domain.Objects
{





    /// <summary>
    /// Data packet created by the translater to be used to create and or update the workinstruction container.
    /// </summary>
    /// 
    [Serializable]
    public class WorkInstructionContainerData : ExternalDataBaseObject
    {

        IList<WorkInstructionContainerData> children = new List<WorkInstructionContainerData>();

        IList<WorkInstructionData> workInstructionsData = new List<WorkInstructionData>();

        /// <summary>
        /// Initializes a new instance of the WorkInstructionContainerData class.
        /// </summary>
        public WorkInstructionContainerData()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkInstructionContainerData"></see> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public WorkInstructionContainerData(string name)
            : base(name)
        {


        }


        public virtual IList<WorkInstructionContainerData> Children
        {
            get
            {
                return children;
            }
            set
            {
                if (children == value)
                    return;
                children = value;
            }
        }

        public virtual void AddWIContainerData(WorkInstructionContainerData wiData)
        {
            wiData.Parent = this;

            children.Add(wiData);
        }



        public virtual IList<WorkInstructionData> WorkInstructionsData
        {
            get
            {
                return workInstructionsData;
            }
            set
            {
                if (workInstructionsData == value)
                    return;
                workInstructionsData = value;
            }
        }

        public virtual void AddWorkInstructionData(WorkInstructionData wiData)
        {
            wiData.Parent = this;

            workInstructionsData.Add(wiData);
        }



        public virtual List<WorkInstructionData> GetAllWorkInstructionData()
        {
            List<WorkInstructionData> data = new List<WorkInstructionData>(WorkInstructionsData);

            foreach (WorkInstructionContainerData item in Children)
            {
                data.AddRange(item.GetAllWorkInstructionData());
            }

            return data;
        }
    }
}
