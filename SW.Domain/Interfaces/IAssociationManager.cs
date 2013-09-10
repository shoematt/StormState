using System;
using System.Collections.Generic;
using Orca.Core.Domain;
using Orca.Domain.Objects;

namespace Orca.Domain.Interfaces
{
    public interface IAssociationManager
    {
        /// <summary>
        ///   Gets the associated objects.
        /// </summary>
        /// <param name = "instance">The instance.</param>
        /// <returns></returns>
        IEnumerable<IDomainObject> GetAssociatedObjects(IDomainObject instance);

        /// <summary>
        /// Gets the association.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="child">The child.</param>
        /// <returns></returns>
        IDomainObject GetAssociation(IDomainObject parent, IDomainObject child);

        /// <summary>
        ///   Gets the associations.
        /// </summary>
        /// <param name = "instance">The instance.</param>
        /// <returns></returns>
        IEnumerable<AssociationProxy> GetAssociations(IDomainObject instance);

        /// <summary>
        ///   Gets the type of the associations for parent.
        /// </summary>
        /// <param name = "parentType">Type of the parent.</param>
        /// <returns></returns>
        IEnumerable<AssociationProxy> GetAssociationsForParentType(Type parentType);

        /// <summary>
        ///   Gets the type of the associations for child.
        /// </summary>
        /// <param name = "childType">Type of the child.</param>
        /// <returns></returns>
        IEnumerable<AssociationProxy> GetAssociationsForChildType(Type childType);

        /// <summary>
        /// Gets the associations.
        /// </summary>
        /// <param name="parentType">Type of the parent.</param>
        /// <param name="childType">Type of the child.</param>
        /// <returns></returns>
        IEnumerable<AssociationProxy> GetAssociations(Type parentType, Type childType);

        /// <summary>
        /// Creates the association.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="child">The child.</param>
        AssociationProxy CreateAssociation(IDomainObject parent, IDomainObject child);
    }
}