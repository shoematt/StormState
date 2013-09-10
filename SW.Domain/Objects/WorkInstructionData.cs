using System;

namespace Orca.Domain.Objects
{
    /// <summary>
    /// Data packet created by the translater to be used to create and or update the workinstruction container.
    /// </summary>
    /// 
    [Serializable]
    public class WorkInstructionData : ExternalDataBaseObject
    {

        private WorkType _workType;

        private int subLineItem = 0;

        /// <summary>
        /// Initializes a new instance of the WorkInstructionData class.
        /// </summary>
        public WorkInstructionData()
        {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="WorkInstructionData"></see> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public WorkInstructionData(string name)
            : base(name)
        {

        }


        public virtual WorkType WorkType
        {
            get
            {
                return _workType;
            }
            set
            {
                if (_workType == value)
                    return;
                _workType = value;
            }
        }


        public virtual int SubLineItem
        {
            get
            {
                return subLineItem;
            }
            set
            {
                if (subLineItem == value)
                    return;
                subLineItem = value;
            }
        }

    }
}
