using System;
using Orca.Domain.Objects.BaseObjects;

namespace Orca.Domain.Auditing
{
    /// <summary>
    ///   Defines the type of audit that the context and scope
    ///   apply to.
    /// </summary>
    [ Serializable ]
    public class AuditingDefinition : PublishableDomainObject
    {
        public static AuditingDefinition Default = new AuditingDefinition
                                                       {
                                                           StaticInstanceID = Guid.Empty,
                                                           Name = "Default",
                                                           Description = "General Audit"
                                                       };

        /// <summary>
        ///   Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public virtual string Description { get; set; }

        /// <summary>
        ///   Maps to AuditingScopeDefinition.Value
        /// </summary>
        /// <value>The value.</value>
        public virtual string Value { get; set; }
    }
}