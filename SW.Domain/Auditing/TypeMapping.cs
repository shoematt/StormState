using System;
using Orca.Core.Domain;
using Orca.Domain.Objects.BaseObjects;

namespace Orca.Domain.Auditing
{
    /// <summary>
    ///   Defines the type of audit that the context and scope
    ///   apply to.
    /// </summary>
    [Serializable]
    public class TypeMapping : PublishableDomainObject, IPublishableDomainObject
    {
        /// <summary>
        ///   Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public virtual string Description { get; set; }

        /// <summary>
        ///   Maps to AuditingScopeDefinition.Value
        /// </summary>
        /// <value>The value.</value>
        public virtual string Type { get; set; }
    }
}