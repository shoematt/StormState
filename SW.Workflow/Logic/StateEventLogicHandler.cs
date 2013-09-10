#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Workflow
// 
// File:	StateEventLogicHandler.cs
// Author:	Matthew Shoemate
// Date:	08/27/2012
// 
// http://www.StormWorksSoftware.com
// 

#endregion

using System;
using System.Xml.Serialization;

using StructureMap;

namespace SW.Workflow.Logic
{
    public class StateEventLogicEventArgs
    {
        /// <summary>
        ///     Gets or sets the context.
        /// </summary>
        /// <value> The context. </value>
        public IStateEventContext Context { get; set; }
    }

    public enum StateEventMode
    {
        OnEnterState,
        OnLeaveState
    }

    /// <summary>
    ///     Provides a base class that can be used to define action handlers that can provide a mechanism for handling the logic / behavior of an action shape.
    ///     of objects of the supported data types.
    /// </summary>
    [ Serializable ]
    public abstract class StateEventLogicHandler
    {
        [ NonSerialized ] [ XmlIgnore ] private IContainer _container;

        /// <summary>
        ///     Initializes a new instance of the <see cref="StateEventLogicHandler" /> class.
        /// </summary>
        /// <param name="container"> The container. </param>
        protected StateEventLogicHandler ( IContainer container )
        {
            Container = container;
        }

        /// <summary>
        ///     Gets or sets the container.
        /// </summary>
        /// <value> The container. </value>
        [ XmlIgnore ]
        public IContainer Container
        {
            get { return _container; }
            set { _container = value; }
        }

        /// <summary>
        ///     Executes the behavior for this proxy instance.
        /// </summary>
        /// <param name="context"> The context. </param>
        public void Execute ( IStateEventContext context )
        {
            OnExecuteEvent ( this, new StateEventLogicEventArgs {Context = context} );
        }

        /// <summary>
        ///     Called when [execute action].
        /// </summary>
        /// <param name="sender"> The sender. </param>
        /// <param name="e">
        ///     The <see cref="StateEventLogicEventArgs" /> instance containing the event data.
        /// </param>
        protected virtual void OnExecuteEvent ( object sender, StateEventLogicEventArgs e )
        {
        }

        /// <summary>
        ///     Gets the state event mode, the default of which is <see cref="StateEventMode.OnEnterState" />
        /// </summary>
        /// <returns> </returns>
        public virtual StateEventMode GetStateEventMode ( )
        {
            return StateEventMode.OnEnterState;
        }
    }
}