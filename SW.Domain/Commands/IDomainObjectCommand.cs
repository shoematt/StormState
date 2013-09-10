using Orca.Core.Domain;

namespace Orca.Domain.Commands
{
    public interface IDomainObjectCommand
    {
        /// <summary>
        /// Gets the domain object.
        /// </summary>
        /// <value>The domain object.</value>
        IDomainObject DomainObject { get; }

        /// <summary>
        /// Executes this instance.
        /// </summary>
        void Execute( );
    }
}