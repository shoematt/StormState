using System.Xml.Serialization;
using StructureMap;

namespace Orca.Workflow.ComponentModel.Logic
{
    public interface IStateEventContext
    {
        /// <summary>
        ///   Gets the state map.
        /// </summary>
        IStateMap StateMap { get; }

        /// <summary>
        ///   Gets the container.
        /// </summary>
        [XmlIgnore]
        IContainer Container { get; }

        /// <summary>
        ///   Gets the state event.
        /// </summary>
        IStateEventProxy EventProxy { get; }
    }
}