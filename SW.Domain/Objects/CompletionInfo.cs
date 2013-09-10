using System;
using Orca.Domain.Objects.BaseObjects;

namespace Orca.Domain.Objects
{
    /// <summary>
    /// Class to capture information when the workinstruction and or container are completed.
    /// </summary>
    public class CompletionInfo : DomainObject
    {

        /// <summary>
        /// Initializes a new instance of the CompletionInfo class.
        /// </summary>
        public CompletionInfo()
        {
            Init();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="CompletionInfo"></see> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public CompletionInfo(string name)
            : base(name)
        {
            Init();
        }


        private void Init()
        {
            IsFinialized = false;
            CompletionDate = DateTime.MinValue;
            UserName = string.Empty;
            TruckID = string.Empty;
        }


        public virtual bool IsFinialized { get; protected internal set; }

        public virtual DateTime CompletionDate { get; protected internal set; }

        public virtual string UserName { get; protected internal set; }

        public virtual string TruckID { get; set; }



    }
}
