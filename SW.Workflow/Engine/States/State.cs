#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Workflow
// 
// File:	State.cs
// Author:	Matthew Shoemate
// Date:	08/27/2012
// 
// http://www.StormWorksSoftware.com
// 

#endregion

using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using SW.Workflow.States;

using StructureMap;

namespace SW.Workflow.Engine.States
{
    [ Serializable ]
    public class State : IState<State>
    {
        public static StateMetaProperty IsInitial = new StateMetaProperty ( new Guid ( "{0d9c5590-10fb-4172-b2e4-5133d5b3db40}" ) ) {Name = "IsInitial", Default = false, Type = typeof ( bool )};
        public static StateMetaProperty IsFinal = new StateMetaProperty ( new Guid ( "{61391727-547b-461f-84fd-0cf9d9254fa5}" ) ) {Name = "IsFinal", Default = false, Type = typeof ( bool )};

        [ NonSerialized ] private IContainer _container;

        /// <summary>
        ///     Initializes a new instance of the <see cref="State" /> class.
        /// </summary>
        public State ( )
        {
            Extended = new StateMetaContainer ( );

            Id = Guid.NewGuid ( );

            Enter = new List<IStateEvent> ( );
            Leave = new List<IStateEvent> ( );
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="State" /> clas.s.
        /// </summary>
        /// <param name="container"> The container. </param>
        public State ( IContainer container ) : this ( )
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

        #region IState<State> Members

        /// <summary>
        ///     Gets or sets any extended information about the state.
        /// </summary>
        /// <value> The info. </value>
        public IStateMetaContainer Extended { get; set; }

        /// <summary>
        ///     Configures the specified state.
        /// </summary>
        /// <param name="configureAction"> The configured state. </param>
        /// <returns> </returns>
        public State Configure ( Action<IStateConfiguration> configureAction )
        {
            configureAction ( this );

            return this;
        }

        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        /// <value> The id. </value>
        public Guid Id { get; set; }

        /// <summary>
        ///     Gets or sets the enter.
        /// </summary>
        /// <value> The enter. </value>
        public List<IStateEvent> Enter { get; set; }

        /// <summary>
        ///     Gets or sets the leave.
        /// </summary>
        /// <value> The leave. </value>
        public List<IStateEvent> Leave { get; set; }

        /// <summary>
        ///     Initializes the container.
        /// </summary>
        /// <param name="container"> The container. </param>
        public void InitializeContainer ( IContainer container )
        {
            Container = container;
        }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value> The name. </value>
        public string Name { get; set; }

        /// <summary>
        ///     Called when configuring the behavior that is executed upon entering a state
        /// </summary>
        /// <param name="stateEvent"> The state event. </param>
        /// <returns> </returns>
        public IStateConfiguration OnEnter ( IStateEvent stateEvent )
        {
            Enter.Add ( stateEvent );
            return this;
        }

        /// <summary>
        ///     Called when configuring the behavior that is executed upon leaving a state
        /// </summary>
        /// <param name="stateEvent"> The state event. </param>
        /// <returns> </returns>
        public IStateConfiguration OnLeave ( IStateEvent stateEvent )
        {
            Leave.Add ( stateEvent );
            return this;
        }

        /// <summary>
        ///     Called when configuring the behavior that is executed upon entering a state
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        public IStateConfiguration OnEnter<T> ( Func<T> configureAction ) where T : class, IStateEvent
        {
            Enter.Add ( configureAction ( ) );
            return this;
        }

        /// <summary>
        ///     Called when configuring the behavior that is executed upon leaving a state
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        public IStateConfiguration OnLeave<T> ( Func<T> configureAction ) where T : class, IStateEvent
        {
            Leave.Add ( configureAction ( ) );
            return this;
        }

        /// <summary>
        ///     Called when configuring the behavior that is executed upon entering a state
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        public IStateConfiguration OnEnter<T> ( T enterEvent ) where T : IStateEvent
        {
            Enter.Add ( enterEvent );
            return this;
        }

        /// <summary>
        ///     Called when configuring the behavior that is executed upon leaving a state
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        public IStateConfiguration OnLeave<T> ( T leaveEvent ) where T : IStateEvent
        {
            Leave.Add ( leaveEvent );
            return this;
        }

        /// <summary>
        ///     Called when configuring the behavior that is executed upon entering a state
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="enterEvent"> The enter event. </param>
        /// <param name="configureEvent"> The configure event. </param>
        /// <returns> </returns>
        public IStateConfiguration OnEnter<T> ( T enterEvent, Action<T> configureEvent ) where T : IStateEvent
        {
            Enter.Add ( enterEvent );

            configureEvent ( enterEvent );

            return this;
        }

        /// <summary>
        ///     Called when configuring the behavior that is executed upon leaving a state
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="leaveEvent"> The leave event. </param>
        /// <param name="configureEvent"> The configure event. </param>
        /// <returns> </returns>
        public IStateConfiguration OnLeave<T> ( T leaveEvent, Action<T> configureEvent ) where T : IStateEvent
        {
            Leave.Add ( leaveEvent );

            configureEvent ( leaveEvent );

            return this;
        }

        #endregion
    }
}