using System;
using System.Collections.Generic;
using Orca.Core;
using Orca.Core.Domain;
using Orca.Domain.Objects.BaseObjects;

namespace Orca.Domain.Auditing
{
    [Serializable]
    public sealed class AuditingScope : PublishableDomainObject, IPublishableDomainObject
    {
        private IDomainObject _domainObject;

        public AuditingScope()
        {
            NestedScopes = new List<AuditingScope>();
            AuditingRecordCollection = new List<AuditingRecord>();
        }

        /// <summary>
        ///   Gets or sets the auditing context ID.
        /// </summary>
        /// <value>The auditing context ID.</value>
        public AuditingContext AuditingContext { get; set; }

        /// <summary>
        ///   Gets or sets the nested scopes.
        /// </summary>
        /// <value>The nested scopes.</value>
        public IList<AuditingScope> NestedScopes { get; set; }

        /// <summary>
        ///   Gets the auditing record collection.
        /// </summary>
        /// <value>The auditing record collection.</value>
        public IList<AuditingRecord> AuditingRecordCollection { get; set; }

        /// <summary>
        ///   Gets or sets the auditing definition.
        /// </summary>
        /// <value>The auditing definition.</value>
        public AuditingDefinition AuditingScopeDefinition { get; set; }

        /// <summary>
        ///   Gets or sets the domain object ID.
        /// </summary>
        /// <value>The domain object ID.</value>
        public Guid DomainObjectID { get; set; }

        /// <summary>
        ///   Gets or sets the domain object ID.
        /// </summary>
        /// <value>The domain object ID.</value>
        public IDomainObject DomainObject
        {
            get { return _domainObject; }
            set
            {
                _domainObject = value;
                if (value != null && !(value is Type))
                {
                    DomainObjectTypeMapping = new TypeMapping { Type = TypeEnumerator.GetShortTypeNameFor(value.GetType()) };
                }
                else if (value is Type)
                {
                    DomainObjectTypeMapping = new TypeMapping { Type = TypeEnumerator.GetShortTypeNameFor((Type)value) };
                }
                if (value != null) DomainObjectID = value.Id;
            }
        }

        /// <summary>
        ///   Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public TypeMapping DomainObjectTypeMapping { get; set; }
    }
}