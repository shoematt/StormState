using System;
using System.Collections.Generic;
using Orca.Domain.Objects.BaseObjects;

namespace Orca.Domain.Auditing
{
    [Serializable]
    public class AuditingContext : PublishableDomainObject
    {
        /// <summary>
        ///   Initializes a new instance of the AuditingContext class.
        /// </summary>
        public AuditingContext()
        {
            AuditingScopeCollection = new List<AuditingScope>();
        }

        /// <summary>
        ///   Gets or sets the auditing scopes.
        /// </summary>
        /// <value>The auditing scopes.</value>
        public virtual IList<AuditingScope> AuditingScopeCollection { get; set; }

        /// <summary>
        ///   Gets or sets the SID.
        /// </summary>
        /// <value>The SID.</value>
        public virtual string SID { get; set; }

        /// <summary>
        ///   Gets or sets the user.
        /// </summary>
        /// <value>The user.</value>
        public virtual string UserName { get; set; }
    }
}