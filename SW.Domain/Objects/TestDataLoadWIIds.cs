using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orca.Domain.Objects.BaseObjects;

namespace Orca.Domain.Objects
{
    public class TestDataLoadWIIds : DomainObject
    {
        DateTime creationDate = DateTime.Now;
        string tag;
        Guid workInstructionId;
        string partnumber;
        string grandSeqNumber;

        /// <summary>
        /// Initializes a new instance of the TestDataLoadWIIds class.
        /// </summary>
        public TestDataLoadWIIds()
        {

        }

        /// <summary>
        /// Initializes a new instance of the TestDataLoadWIIds class.
        /// </summary>
        /// <param name="Tag"></param>
        /// <param name="WorkInstructionId"></param>
        public TestDataLoadWIIds(string Name, string Tag, Guid WorkInstructionId)
            : base(Name)
        {
            tag = Tag;
            workInstructionId = WorkInstructionId;
        }


        public virtual DateTime CreationDate
        {
            get
            {
                return creationDate;
            }
            set
            {

                creationDate = value;
            }
        }
        public virtual string Tag
        {
            get
            {
                return tag;
            }
            set
            {

                tag = value;
            }
        }
        public virtual Guid WorkInstructionId
        {
            get
            {
                return workInstructionId;
            }
            set
            {

                workInstructionId = value;
            }
        }

        public string Partnumber
        {
            get
            {
                return partnumber;
            }
            set
            {

                partnumber = value;
            }
        }
        public string GrandSeqNumber
        {
            get
            {
                return grandSeqNumber;
            }
            set
            {

                grandSeqNumber = value;
            }
        }
    }
}
