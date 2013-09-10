#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Workflow
// 
// File:	StateEvent.cs
// Author:	Matthew Shoemate
// Date:	08/27/2012
// 
// http://www.StormWorksSoftware.com
// 

#endregion

using System;
using System.Xml.Serialization;

using SW.Workflow.States;

using StructureMap;

namespace SW.Workflow.Engine.States
{
    [ Serializable ]
    public class StateEvent<T> : IStateEventConfiguration,
                                 IStateEvent<T> //where T : class, IStateEvent
    {
        [ NonSerialized ] private IContainer _container;

        private IStateEvent _postExecuteEvent;
        private IStateEvent _preExecuteEvent;

        /// <summary>
        ///     Initializes a new instance of the <see cref="StateEvent{T}" /> class.
        /// </summary>
        /// <param name="proxy"> The proxy. </param>
        public StateEvent ( T proxy )
        {
            Event = proxy;
        }

        /// <summary>
        ///     Gets or sets the container.
        /// </summary>
        /// <value> The container. </value>
        [ XmlIgnore ]
        //  [SetterProperty]
        public IContainer Container
        {
            get { return _container; }
            set { _container = value; }
        }

        #region IStateEvent<T> Members

        /// <summary>
        ///     Gets or sets the proxy.
        /// </summary>
        /// <value> The proxy. </value>
        public T Event { get; set; }

        /// <summary>
        ///     Initializes the container.
        /// </summary>
        /// <param name="container"> The container. </param>
        public void InitializeContainer ( IContainer container )
        {
            Container = container;
        }

        /// <summary>
        ///     Executes the behavior for this instance.
        /// </summary>
        public void Execute ( IContainer container, StateMap stateMap )
        {
            if ( _preExecuteEvent != null )
            {
                _preExecuteEvent.Execute ( container, stateMap );
            }

            OnExecute ( Event, stateMap );

            if ( _postExecuteEvent != null )
            {
                _postExecuteEvent.Execute ( container, stateMap );
            }
        }

        #endregion

        #region IStateEventConfiguration Members

        /// <summary>
        ///     Called when configuring the behavior that is executed prior to the current <see cref="IStateEvent" />
        /// </summary>
        /// <param name="preExecuteEvent"> The pre execute event. </param>
        /// <returns>
        ///     the modified <see cref="IStateEventConfiguration" /> instance.
        /// </returns>
        public IStateEventConfiguration OnBeforeExecuted ( IStateEvent preExecuteEvent )
        {
            _preExecuteEvent = preExecuteEvent;

            return this;
        }

        /// <summary>
        ///     Called when configuring the behavior that is executed after to the current <see cref="IStateEvent" />
        /// </summary>
        /// <param name="postExecuteEvent"> The post execute event. </param>
        /// <returns>
        ///     the modified <see cref="IStateEventConfiguration" /> instance.
        /// </returns>
        public IStateEventConfiguration OnAfterExecuted ( IStateEvent postExecuteEvent )
        {
            _postExecuteEvent = postExecuteEvent;

            return this;
        }

        /// <summary>
        ///     Called when [before executed].
        /// </summary>
        /// <typeparam name="T1"> The type of the 1. </typeparam>
        /// <param name="preExecuteEvent"> The pre execute event. </param>
        /// <returns> </returns>
        public IStateEventConfiguration OnBeforeExecuted<T1> ( T1 preExecuteEvent ) where T1 : IStateEvent
        {
            _preExecuteEvent = preExecuteEvent;

            return this;
        }

        /// <summary>
        ///     Called when [after executed].
        /// </summary>
        /// <typeparam name="T1"> The type of the 1. </typeparam>
        /// <param name="postExecuteEvent"> The post execute event. </param>
        /// <returns> </returns>
        public IStateEventConfiguration OnAfterExecuted<T1> ( T1 postExecuteEvent ) where T1 : IStateEvent
        {
            _postExecuteEvent = postExecuteEvent;

            return this;
        }

        /// <summary>
        ///     Called when [before executed].
        /// </summary>
        /// <typeparam name="T1"> The type of the 1. </typeparam>
        /// <param name="configureAction"> The configure action. </param>
        /// <returns> </returns>
        public IStateEventConfiguration OnBeforeExecuted<T1> ( Func<T1> configureAction ) where T1 : class, IStateEvent
        {
            _preExecuteEvent = configureAction ( );

            return this;
        }

        /// <summary>
        ///     Called when [after executed].
        /// </summary>
        /// <typeparam name="T1"> The type of the 1. </typeparam>
        /// <param name="configureAction"> The configure action. </param>
        /// <returns> </returns>
        public IStateEventConfiguration OnAfterExecuted<T1> ( Func<T1> configureAction ) where T1 : class, IStateEvent
        {
            _postExecuteEvent = configureAction ( );

            return this;
        }

        /// <summary>
        ///     Called when [before executed].
        /// </summary>
        /// <typeparam name="T1"> The type of the 1. </typeparam>
        /// <param name="preExecuteEvent"> The pre execute event. </param>
        /// <param name="preExecuteEventConfigurationAction"> The pre execute event configuration action. </param>
        /// <returns> </returns>
        public IStateEventConfiguration OnBeforeExecuted<T1> ( T1 preExecuteEvent, Action<T1> preExecuteEventConfigurationAction ) where T1 : IStateEvent
        {
            _preExecuteEvent = preExecuteEvent;
            preExecuteEventConfigurationAction ( preExecuteEvent );

            return this;
        }

        /// <summary>
        ///     Called when [after executed].
        /// </summary>
        /// <typeparam name="T1"> The type of the 1. </typeparam>
        /// <param name="postExecuteEvent"> The post execute event. </param>
        /// <param name="postExecuteEventConfigurationAction"> The post execute event configuration action. </param>
        /// <returns> </returns>
        public IStateEventConfiguration OnAfterExecuted<T1> ( T1 postExecuteEvent, Action<T1> postExecuteEventConfigurationAction ) where T1 : IStateEvent
        {
            _postExecuteEvent = postExecuteEvent;
            postExecuteEventConfigurationAction ( postExecuteEvent );

            return this;
        }

        #endregion

        /// <summary>
        ///     Called during execution, allows additional custom behavior to be added to an existing state event type
        /// </summary>
        /// <param name="event"> The @event. </param>
        /// <param name="stateMap"> </param>
        protected virtual void OnExecute ( T @event, StateMap stateMap )
        {
        }
    }
}