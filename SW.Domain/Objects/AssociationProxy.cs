using System;
using System.Xml.Serialization;
using Orca.Core;
using Orca.Core.Domain;
using Orca.Domain.Objects.BaseObjects;

namespace Orca.Domain.Objects
{
    [Serializable]
    public class AssociationProxy : DomainObject
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref = "AssociationProxy" /> class.
        /// </summary>
        protected internal AssociationProxy()
        {
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref = "AssociationProxy" /> class. This class
        ///   holds the association between two domain objects by storing the primary keys and the
        ///   object instance types.
        /// </summary>
        /// <param name = "parentID">The parent ID.</param>
        /// <param name = "childID">The child ID.</param>
        /// <param name = "parentType">Type of the parent.</param>
        /// <param name = "childType">Type of the child.</param>
        public AssociationProxy(Guid parentID, Guid childID, Type parentType, Type childType)
        {
            if (parentType == null) throw new ArgumentNullException("parentType");
            if (childType == null) throw new ArgumentNullException("childType");

            ParentID = parentID;
            ChildID = childID;
            ParentType = parentType;
            ChildType = childType;
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref = "AssociationProxy" /> class.
        /// </summary>
        /// <param name = "parentObject">The parent object.</param>
        /// <param name = "childObject">The child object.</param>
        public AssociationProxy(IDomainObject parentObject, IDomainObject childObject)
        {
            if (parentObject == null) throw new ArgumentNullException("parentObject");
            if (childObject == null) throw new ArgumentNullException("childObject");

            ParentID = parentObject.Id;
            ChildID = childObject.Id;
            ParentType = parentObject.GetType();
            ChildType = childObject.GetType();
        }

        /// <summary>
        ///   Gets or sets the type of the child.
        /// </summary>
        /// <value>The type of the child.</value>
        [XmlIgnore]
        public virtual Type ChildType { get; protected internal set; }

        /// <summary>
        ///   Gets or sets the name of the child type.
        /// </summary>
        /// <value>The name of the child type.</value>
        public virtual string ChildTypeName
        {
            get { return TypeEnumerator.GetShortTypeNameFor(ChildType); }
            protected internal set { if (value != null) ChildType = Type.GetType(value); }
        }

        /// <summary>
        ///   Gets or sets the type of the parent.
        /// </summary>
        /// <value>The type of the parent.</value>
        [XmlIgnore]
        public virtual Type ParentType { get; protected internal set; }

        /// <summary>
        ///   Gets or sets the name of the parent type.
        /// </summary>
        /// <value>The name of the parent type.</value>
        public virtual string ParentTypeName
        {
            get { return TypeEnumerator.GetShortTypeNameFor(ParentType); }
            protected internal set { if (value != null) ParentType = Type.GetType(value); }
        }

        /// <summary>
        ///   Gets or sets the child ID.
        /// </summary>
        /// <value>The child ID.</value>
        public virtual Guid ChildID { get; protected internal set; }

        /// <summary>
        ///   Gets or sets the parent ID.
        /// </summary>
        /// <value>The parent ID.</value>
        public virtual Guid ParentID { get; protected internal set; }
    }
}