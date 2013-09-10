using System;
using System.Xml.Serialization;
using StructureMap;

namespace Orca.Workflow.ComponentModel.Logic
{
    [ Serializable ]
    public class StateEventContext : IStateEventContext
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref = "StateEventContext" /> class.
        /// </summary>
        /// <param name = "stateMap">The state map.</param>
        /// <param name = "container">The container.</param>
        /// <param name = "stateEvent">The state event.</param>
        public StateEventContext( IStateMap stateMap, IContainer container, IStateEventProxy stateEvent )
        {
            StateMap = stateMap;
            Container = container;
            EventProxy = stateEvent;
        }

        #region IStateEventContext Members

        /// <summary>
        ///   Gets the state map.
        /// </summary>
        public IStateMap StateMap { get; private set; }

        [NonSerialized]
        [XmlIgnore]
        private IContainer _container;

        /// <summary>
        ///   Gets the container.
        /// </summary>
        [XmlIgnore]
        public IContainer Container
        {
            get { return _container; }
            private set { _container = value; }
        }

        /// <summary>
        ///   Gets the state event.
        /// </summary>
        public IStateEventProxy EventProxy { get; set; }

        #endregion
    }
}