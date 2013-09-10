using System;
using Orca.Domain.Objects.BaseObjects;

namespace Orca.Domain.Objects
{
    /// <summary>
    /// Object to tie the workinstruction container back to teh data from the external system.
    /// </summary>
    /// 
    [Serializable]
    public class ExternalDataLink : DomainObject
    {

        /// <summary>
        /// Initializes a new instance of the ExternalDataLink class.
        /// </summary>
        public ExternalDataLink()
        {
            Init();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ExternalDataLink"></see> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public ExternalDataLink(string name)
            : base(name)
        {
            Init();
        }

        /// <summary>
        /// Initializes a new instance of the ExternalDataLink class.
        /// </summary>
        public ExternalDataLink(string externalID, string externalField)
        {
            ExternalID = externalID;
            ExternalField = externalField;
        }



        private void Init()
        {
            ExternalID = string.Empty;
            ExternalField = string.Empty;
        }

        public virtual string ExternalID { get; set; }

        public virtual string ExternalField { get; set; }
    }
}
