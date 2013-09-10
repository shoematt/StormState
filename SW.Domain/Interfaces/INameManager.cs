using System;
using Orca.Domain.Objects;
using Orca.Persistence.Core.Interfaces;

namespace Orca.Domain.Interfaces
{
    public interface INameManager
    {
        /// <summary>
        /// Resets this instance.
        /// </summary>
        void Reset();
        /// <summary>
        /// Gets the registered type count.
        /// </summary>
        /// <value>The registered type count.</value>
        int RegisteredTypeCount { get; }
        /// <summary>
        /// Checks to see if the name is available for the type specified.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        bool IsNameAvailable(Type objectType, string name);
        /// <summary>
        /// Gets the name association.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        NameToTypeAssociation GetNameAssociation(Type objectType, string name);
        /// <summary>
        /// Registers the name and immediately Pesists to the db..
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        NameToTypeAssociation RegisterName(Type objectType, string name);
        /// <summary>
        /// Registers the name and adds it to the transaction
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="name">The name.</param>
        /// <param name="transactionName">Name of the transaction.</param>
        /// <returns></returns>
        NameToTypeAssociation RegisterName(Type objectType, string name, IPersistCommandsTransaction transactionName);
        /// <summary>
        /// Renames the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="oldName">The old name.</param>
        /// <param name="newName">The new name.</param>
        /// <returns></returns>
        NameToTypeAssociation Rename(Type objectType, string oldName, string newName);
        /// <summary>
        /// Un Registers the name and adds it to the transaction
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="objectName">The name.</param>
        /// <param name="transaction">The transaction.</param>
        void UnregisterName(Type objectType, string objectName, IPersistCommandsTransaction transaction);
    }
}