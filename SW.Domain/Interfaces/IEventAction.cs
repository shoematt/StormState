using Orca.Core.Domain;
using StructureMap;

namespace Orca.Domain.Interfaces
{
    public interface IEventAction : IDomainObject
    {
        /// <summary>
        ///   Executes the specified work instruction.
        /// </summary>
        /// <param name = "WorkInstruction">The work instruction.</param>
        /// <param name = "IOCContainer">The IOC container.</param>
        void Execute( IWorkInstruction WorkInstruction, IContainer IOCContainer );
    }
}