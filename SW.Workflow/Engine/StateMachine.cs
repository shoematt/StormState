#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Workflow
// 
// File:	StateMachine.cs
// Author:	Matthew Shoemate
// Date:	08/27/2012
// 
// http://www.StormWorksSoftware.com
// 

#endregion

using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using SW.Core.Extensions;
using SW.Workflow.Engine.States;
using SW.Workflow.States;
using SW.Workflow.Transitions;

using StructureMap;

using log4net;

namespace SW.Workflow.Engine
{
    [ Serializable ]
    public class StateMachine : IStateMachineConfiguration
    {
        private readonly ILog _logging = LogManager.GetLogger ( "State Machine" );

        [ NonSerialized ] private readonly IContainer _stateContainer;
        private readonly object _syncObject = new object ( );
        private bool _isStarted;
        private StateMap _stateMap;

        /// <summary>
        ///     Initializes a new instance of the <see cref="StateMachine" /> class.
        /// </summary>
        /// <param name="stateMap"> The state map. </param>
        /// <param name="container"> The container. </param>
        public StateMachine ( StateMap stateMap, IContainer container )
        {
            ActiveStates = new List<IState> ( );

            _stateContainer = container;
            _stateMap = stateMap;
        }

        /// <summary>
        ///     Gets the state container.
        /// </summary>
        /// <value> The state container. </value>
        [ XmlIgnore ]
        public IContainer StateContainer
        {
            get { return _stateContainer; }
        }

        /// <summary>
        ///     Gets or sets the state of the active.
        /// </summary>
        /// <value> The state of the active. </value>
        public List<IState> ActiveStates { get; set; }

        /// <summary>
        ///     Gets or sets the start event.
        /// </summary>
        /// <value> The start event. </value>
        public IStateEvent StartEvent { get; private set; }

        /// <summary>
        ///     Gets or sets the stop event.
        /// </summary>
        /// <value> The stop event. </value>
        public IStateEvent StopEvent { get; private set; }

        #region IStateMachineConfiguration Members

        /// <summary>
        ///     Called when [start].
        /// </summary>
        /// <param name="startEvent"> The start event. </param>
        /// <returns> </returns>
        IStateMachineConfiguration IStateMachineConfiguration.OnStart ( IStateEvent startEvent )
        {
            StartEvent = startEvent;

            return this;
        }

        /// <summary>
        ///     Called when [stop].
        /// </summary>
        /// <param name="stopEvent"> The stop event. </param>
        /// <returns> </returns>
        IStateMachineConfiguration IStateMachineConfiguration.OnStop ( IStateEvent stopEvent )
        {
            StopEvent = stopEvent;

            return this;
        }

        /// <summary>
        ///     Called when [start].
        /// </summary>
        /// <param name="startEvent"> The start event. </param>
        /// <returns> </returns>
        public IStateMachineConfiguration OnStart ( Action startEvent )
        {
            StartEvent = new StateEventActionWrapper ( startEvent );

            return this;
        }

        /// <summary>
        ///     Called when [start].
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="startEvent"> The start event. </param>
        /// <returns> </returns>
        IStateMachineConfiguration IStateMachineConfiguration.OnStart<T> ( T startEvent )
        {
            StartEvent = startEvent;

            return this;
        }

        /// <summary>
        ///     Called when [stop].
        /// </summary>
        /// <param name="stopEvent"> The stop event. </param>
        /// <returns> </returns>
        public IStateMachineConfiguration OnStop ( Action stopEvent )
        {
            StopEvent = new StateEventActionWrapper ( stopEvent );

            return this;
        }

        /// <summary>
        ///     Called when [stop].
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="stopEvent"> The stop event. </param>
        /// <returns> </returns>
        IStateMachineConfiguration IStateMachineConfiguration.OnStop<T> ( T stopEvent )
        {
            StopEvent = stopEvent;

            return this;
        }

        /// <summary>
        ///     Called when [start].
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <returns> </returns>
        IStateMachineConfiguration IStateMachineConfiguration.OnStart<T> ( Func<T> configureAction )
        {
            StartEvent = _stateContainer.GetInstance<T> ( );

            return this;
        }

        /// <summary>
        ///     Called when [stop].
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <returns> </returns>
        IStateMachineConfiguration IStateMachineConfiguration.OnStop<T> ( Func<T> configureAction )
        {
            StopEvent = _stateContainer.GetInstance<T> ( );

            return this;
        }

        /// <summary>
        ///     Called when [start].
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="startEvent"> The start event. </param>
        /// <param name="startEventConfigurationAction"> The start event configuration action. </param>
        /// <returns> </returns>
        IStateMachineConfiguration IStateMachineConfiguration.OnStart<T> ( T startEvent, Action<T> startEventConfigurationAction )
        {
            StartEvent = startEvent;

            startEventConfigurationAction ( startEvent );

            return this;
        }

        /// <summary>
        ///     Called when [stop].
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="stopEvent"> The stop event. </param>
        /// <param name="stopEventConfigurationAction"> The stop event configuration action. </param>
        /// <returns> </returns>
        IStateMachineConfiguration IStateMachineConfiguration.OnStop<T> ( T stopEvent, Action<T> stopEventConfigurationAction )
        {
            StopEvent = stopEvent;

            stopEventConfigurationAction ( stopEvent );

            return this;
        }

        #endregion

        /// <summary>
        ///     Configures the specified state map.
        /// </summary>
        /// <param name="stateMap"> The state map. </param>
        /// <returns> </returns>
        public IStateMachineConfiguration Configure ( StateMap stateMap )
        {
            Configure ( stateMap, null );

            return this;
        }

        /// <summary>
        ///     Configures the specified state map.
        /// </summary>
        /// <param name="stateMap"> The state map. </param>
        /// <param name="stateMachineConfigurationAction"> The state machine configuration action. </param>
        /// <returns> </returns>
        public IStateMachineConfiguration Configure ( StateMap stateMap, Action<IStateMachineConfiguration> stateMachineConfigurationAction )
        {
            if ( _isStarted )
            {
                throw new StateMachineException ( "The state machine has already been started" );
            }

            if ( _logging.IsDebugEnabled )
            {
                _logging.Debug ( "Configuring StateMachine StateMap - Started" );
            }

            _stateMap = stateMap;

            if ( stateMachineConfigurationAction != null )
            {
                stateMachineConfigurationAction ( this );
            }

            if ( _logging.IsDebugEnabled )
            {
                _logging.Debug ( "Configuring StateMachine StateMap - Complete" );
            }

            return this;
        }

        /// <summary>
        ///     Transitions the specified transition mode action.
        /// </summary>
        /// <param name="transitionModeAction"> The transition mode action. </param>
        public void Transition ( Action<ITransitionMode> transitionModeAction )
        {
        }

        /// <summary>
        ///     Transitions to (effectively a JUMP operation, which will bypass predefined transitions)
        /// </summary>
        /// <param name="TO"> The TO. </param>
        /// <returns> </returns>
        public bool TransitionTo ( IState TO )
        {
            if ( !_isStarted )
            {
                throw new StateMachineException ( "The state machine has not been started" );
            }

            lock ( _syncObject )
            {
                if ( _logging.IsDebugEnabled )
                {
                    _logging.Debug ( "StateMachine Transition Request - Started" );
                }

                foreach ( IState fromState in ActiveStates )
                {
                    try
                    {
                        fromState.Leave.CallOnEach ( x => x.Execute ( StateContainer, _stateMap ) );
                    }
                    catch ( Exception ex )
                    {
                        throw new StateMachineException ( ex.Message );
                    }
                }

                ActiveStates.Clear ( );

                try
                {
                    if ( TO != null )
                    {
                        ActiveStates.Add ( TO );

                        foreach ( IState toState in ActiveStates )
                        {
                            if ( toState.Enter != null )
                            {
                                toState.Enter.CallOnEach ( x => x.Execute ( StateContainer, _stateMap ) );
                            }
                        }
                    }
                }
                catch ( Exception ex )
                {
                    throw new StateMachineException ( ex.Message );
                }

                if ( _logging.IsDebugEnabled )
                {
                    _logging.Debug ( "StateMachine Transition Request - Complete" );
                }
            }

            return ActiveStates.Count != 0;
        }

        /// <summary>
        ///     Transitions this instance.
        /// </summary>
        public bool Transition ( )
        {
            if ( !_isStarted )
            {
                throw new StateMachineException ( "The state machine has not been started" );
            }

            lock ( _syncObject )
            {
                if ( _logging.IsDebugEnabled )
                {
                    _logging.Debug ( "StateMachine Transition Request - Started" );
                }

                var transitions = _stateMap.GetTransitionsForStates ( ActiveStates );

                ActiveStates.Clear ( );

                foreach ( IStateTransition transition in transitions )
                {
                    transition.DoTransition ( OnTransition );
                }

                if ( _logging.IsDebugEnabled )
                {
                    _logging.Debug ( "StateMachine Transition Request - Complete" );
                }
            }

            return ActiveStates.Count != 0;
        }

        /// <summary>
        ///     Called when [transition].
        /// </summary>
        /// <param name="TO"> The TO. </param>
        /// <param name="FROM"> The FROM. </param>
        /// <param name="transition"> The transition. </param>
        // ReSharper disable InconsistentNaming
        private void OnTransition ( List<IState> TO, IState FROM, IStateTransition transition ) // ReSharper restore InconsistentNaming
        {
            try
            {
                if ( FROM.Leave != null )
                {
                    foreach ( IState toState in ActiveStates )
                    {
                        if ( _logging.IsDebugEnabled )
                        {
                            _logging.DebugFormat ( "StateMachine Transition Activated From {0} to {1}", FROM.Name, toState.Name );
                        }
                    }

                    FROM.Leave.CallOnEach ( x => x.Execute ( StateContainer, _stateMap ) );
                }
            }
            catch ( Exception ex )
            {
                throw new StateMachineException ( ex.Message );
            }

            try
            {
                if ( TO != null )
                {
                    ActiveStates.AddRange ( TO );

                    foreach ( IState toState in ActiveStates )
                    {
                        if ( toState.Enter != null )
                        {
                            toState.Enter.CallOnEach ( x => x.Execute ( StateContainer, _stateMap ) );
                        }

                        if ( _logging.IsDebugEnabled )
                        {
                            _logging.DebugFormat ( "StateMachine Transition Completed From {0} to {1}", FROM.Name, toState.Name );
                        }
                    }
                }
                else
                {
                    if ( _logging.IsDebugEnabled )
                    {
                        _logging.DebugFormat ( "StateMachine has Transitioned To A Final State From {0}", FROM.Name );
                    }
                }
            }
            catch ( Exception ex )
            {
                throw new StateMachineException ( ex.Message );
            }
        }

        /// <summary>
        ///     Starts the specified initial state.
        /// </summary>
        /// <param name="initialState"> The initial state. </param>
        public void Start ( IState initialState )
        {
            try
            {
                if ( _isStarted )
                {
                    return;
                }

                _isStarted = true;

                if ( !_stateMap.States.Contains ( initialState ) )
                {
                    throw new StateMachineException ( "The specified start state does not exist in the state map" );
                }

                ActiveStates.Add ( initialState );

                if ( StartEvent != null )
                {
                    StartEvent.Execute ( StateContainer, _stateMap );
                }

                foreach ( IState state in ActiveStates )
                {
                    if ( state.Enter != null )
                    {
                        state.Enter.CallOnEach ( x => x.Execute ( StateContainer, _stateMap ) );
                    }
                }
            }
            catch ( Exception ex )
            {
                throw new StateMachineException ( ex.Message );
            }
        }

        /// <summary>
        ///     Starts this instance.
        /// </summary>
        public void Start ( )
        {
            try
            {
                if ( _isStarted )
                {
                    return;
                }

                _isStarted = true;

                if ( _stateMap.Transitions.Count > 0 )
                {
                    ActiveStates = _stateMap.GetInitialStates ( );
                }

                if ( StartEvent != null )
                {
                    StartEvent.Execute ( StateContainer, _stateMap );
                }

                foreach ( IState state in ActiveStates )
                {
                    if ( state.Enter != null )
                    {
                        state.Enter.CallOnEach ( x => x.Execute ( StateContainer, _stateMap ) );
                    }
                }
            }
            catch ( Exception ex )
            {
                throw new StateMachineException ( ex.Message );
            }
        }

        /// <summary>
        ///     Stops this instance.
        /// </summary>
        public void Stop ( )
        {
            try
            {
                if ( _isStarted )
                {
                    ActiveStates.Clear ( );

                    _isStarted = false;

                    if ( StopEvent != null )
                    {
                        StopEvent.Execute ( StateContainer, _stateMap );
                    }

                    _stateMap.Transitions.CallOnEach ( x => x.Reset ( ) );
                }
            }
            catch ( Exception ex )
            {
                throw new StateMachineException ( ex.Message );
            }
        }

        public void Reset ( )
        {
            Stop ( );

            if ( ActiveStates != null )
            {
                ActiveStates.Clear ( );
            }
        }
    }

    public interface ITransitionMode
    {
        /// <summary>
        ///     Ignores the conditions.
        /// </summary>
        /// <returns> </returns>
        ITransitionMode IgnoreConditions ( );
    }
}