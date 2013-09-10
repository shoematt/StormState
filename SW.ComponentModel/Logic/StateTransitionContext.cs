using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Orca.Workflow.States;
using Orca.Workflow.Transitions;
using StructureMap;

namespace Orca.Workflow.ComponentModel.Logic
{
    [ Serializable ]
    public class StateTransitionContext : IStateTransitionContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StateEventContext"/> class.
        /// </summary>
        /// <param name="stateMap">The state map.</param>
        /// <param name="container">The container.</param>
        /// <param name="transition">The transition.</param>
        /// <param name="proxy">The proxy.</param>
        /// <param name="debug"></param>
        public StateTransitionContext( IStateMap stateMap, IContainer container, Action<List<IState>, IState, IStateTransition> transition, IStateTransitionProxy proxy, bool debug )
        {
            Debug = debug;
            StateMap = stateMap;
            Container = container;
            TransitionProxy = proxy;
            TransitionAction = transition;
        }

        #region IStateTransitionContext Members

        /// <summary>
        /// Gets a value indicating whether this <see cref="StateTransitionContext"/> is debug.
        /// </summary>
        /// <value>
        ///   <c>true</c> if debug; otherwise, <c>false</c>.
        /// </value>
        public bool Debug { get; private set; }

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
        public Action<List<IState>, IState, IStateTransition> TransitionAction { get; set; }

        /// <summary>
        /// Gets or sets the transition proxy.
        /// </summary>
        /// <value>
        /// The transition proxy.
        /// </value>
        public IStateTransitionProxy TransitionProxy { get; set; }

        #endregion
    }
}