using System;
using Orca.Core.Domain;
using Orca.Domain.Objects.BaseObjects;

namespace Orca.Domain.Objects
{
    public enum FlowTypeEnumeration
    {
      //  SelectionCriteria,
        DefaultFlow,
        WorkInstructionFlow,
        WorkInstructionFilteringFlow
    }

    /// <summary>
    ///   Contains the serialized binary version of the StateMap
    /// </summary>
    [ Serializable ]
    public class StateMapData : PublishableDomainObject, IPublishableDomainObject
    {
        /// <summary>
        ///   Gets or sets the updatedData.
        /// </summary>
        /// <value>The updatedData.</value>
        public virtual byte[] Data { get; set; }

        /// <summary>
        ///   Gets or sets the type of the flow.
        /// </summary>
        /// <value>The type of the flow.</value>
        public virtual FlowTypeEnumeration FlowType { get; set; }

        /// <summary>
        ///   Gets or sets the parent work type ID.
        /// </summary>
        /// <value>The parent work type ID.</value>
        public virtual Guid ParentWorkTypeId { get; set; }

        /// <summary>
        /// Gets or sets the trigger ID.
        /// </summary>
        /// <value>The trigger ID.</value>
        public virtual Guid TriggerID { get; set; }
    }
}