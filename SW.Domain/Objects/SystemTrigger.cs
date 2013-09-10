using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orca.Core;
using Orca.Core.Domain;
using Orca.Domain.Objects.BaseObjects;

namespace Orca.Domain.Objects
{
    [Serializable]
    public sealed class SystemTrigger : EventTrigger, IDomainObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SystemTrigger"/> class.
        /// </summary>
        public SystemTrigger()
        {
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public override string Name { get; set; }


        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Gets the id.
        /// </summary>
        /// <value>The id.</value>
        public Guid Id { get; set; }

        /// <summary>
        /// The unique id for a type to be used by the dynamic property system.
        /// </summary>
        /// <value></value>
        public Guid UserTypeID { get; set; }

        /// <summary>
        /// Gets or sets the trigger ID.
        /// </summary>
        /// <value>The trigger ID.</value>
        public override Guid TriggerID { get; set; }

        /// <summary>
        /// Gets the version.
        /// </summary>
        /// <value>The version.</value>
        public int Version { get; internal set; }

        ///// <summary>
        ///// Gets or sets the data.
        ///// </summary>
        ///// <value>The data.</value>
        //public SystemTriggerData Data {get; internal set;}
    }
}
