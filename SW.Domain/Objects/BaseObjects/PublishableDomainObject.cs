using System;
using System.ComponentModel;
using Orca.Core.Domain;

namespace Orca.Domain.Objects.BaseObjects
{
    [Serializable]
    public class PublishableDomainObject : DomainObject, IPublishableDomainObject, IDomainObject, IEquatable<PublishableDomainObject>
    {


        public PublishableDomainObject()
        {
            StaticInstanceID = Guid.NewGuid();
        }


        public PublishableDomainObject(string name)
            : base(name)
        {
            StaticInstanceID = Guid.NewGuid();
        }




        /// <summary>
        ///   Equalses the specified other.
        /// </summary>
        /// <param name = "other">The other.</param>
        /// <returns></returns>
        public virtual bool Equals(PublishableDomainObject other)
        {
            return base.Equals(other);
        }

        /// <summary>
        ///   Gets the instance ID; Does not change with revisions.
        /// </summary>
        /// <value>The instance ID.</value>
        [Browsable(false)]
        public virtual Guid StaticInstanceID { get; set; }

        /// <summary>
        ///   Gets a value indicating whether this instance is published.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is published; otherwise, <c>false</c>.
        /// </value>
        public virtual bool IsPublished { get; set; }

        /// <summary>
        ///   Gets the revision.
        /// </summary>
        /// <value>The revision.</value>
        public virtual int Revision { get; protected internal set; }




    }
}