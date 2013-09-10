using System;
using System.Xml.Serialization;
using StructureMap;

namespace Orca.Workflow.ComponentModel.Logic
{
    public class StateTransitionLogicEventArgs
    {
        /// <summary>
        ///   Gets or sets the context.
        /// </summary>
        /// <value>
        ///   The context.
        /// </value>
        public IStateTransitionContext Context { get; set; }
    }

    /// <summary>
    ///   Handles decision point evaluation logic upon invoking a transition from state engine state node.
    /// </summary>
    [ Serializable ]
    public class StateTransitionLogicHandler
    {
        [NonSerialized]
        [XmlIgnore]
        private IContainer _container;

        /// <summary>
        /// Gets or sets the container.
        /// </summary>
        /// <value>
        /// The container.
        /// </value>
        [XmlIgnore]
        public IContainer Container
        {
            get { return _container; }
            set { _container = value; }
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="StateTransitionLogicHandler"/> class from being created.
        /// </summary>
        /// <param name="container">The container.</param>
        protected StateTransitionLogicHandler( IContainer container )
        {
            Container = container;
        }

        /// <summary>
        ///   Executes the behavior for this proxy instance.
        /// </summary>
        /// <param name = "context">The context.</param>
        public void Transition( IStateTransitionContext context )
        {
            var args = new StateTransitionLogicEventArgs {Context = context};

            OnExecuteTransition( this, args );
        }

        /// <summary>
        ///   Called when [execute action].
        /// </summary>
        /// <param name = "sender">The sender.</param>
        /// <param name = "e">The <see cref = "Orca.Workflow.ComponentModel.Logic.StateTransitionLogicEventArgs" /> instance containing the event data.</param>
        protected virtual void OnExecuteTransition( object sender, StateTransitionLogicEventArgs e )
        {
        }
    }
}