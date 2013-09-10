#region

using System;
using Orca.Domain.Objects.BaseObjects;

#endregion

namespace Orca.Domain.Auditing
{
    [ Serializable ]
    public class AuditingRecord : PublishableDomainObject
    {
        /// <summary>
        ///   Maps to AuditingRecord.AuditingScopeID
        /// </summary>
        /// <value>The auditing scope.</value>
        public virtual AuditingScope AuditingScope { get; set; }

        /// <summary>
        ///   Maps to AuditingRecord.AuditingDefinitionID
        /// </summary>
        /// <value>The auditing definition.</value>
        public virtual AuditingDefinition AuditingRecordDefinition { get; set; }

        /// <summary>
        ///   Gets or sets the index of the scope.
        /// </summary>
        /// <value>The index of the scope.</value>
        public virtual int ScopeIndex { get; set; }

        /// <summary>
        ///   Gets or sets the entity ID.
        /// </summary>
        /// <value>The entity ID.</value>
        public virtual Guid EntityID { get; set; }

        /// <summary>
        ///   Maps to AuditingRecord.Value
        /// </summary>
        /// <value>The value.</value>
        public virtual string Value { get; set; }
    }
}