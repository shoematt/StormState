#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Workflow
// 
// File:	StateEventSet.cs
// Author:	Matthew Shoemate
// Date:	08/27/2012
// 
// http://www.StormWorksSoftware.com
// 

#endregion

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Serialization;

using SW.Workflow.Engine;
using SW.Workflow.States;

using StructureMap;
using StructureMap.Attributes;

namespace SW.Workflow.Events
{
    [ Serializable ]
    public enum EventExecutionMethod
    {
        Default,
        Parallel,
        Serial
    }

    /// <summary>
    ///     Allows a set of state events to be executed as a single state
    ///     in parallel or in series.
    /// </summary>
    [ Serializable ]
    public class StateEventSet : IStateEvent
    {
        private readonly Queue<IStateEvent> _parallelEvents;
        private readonly Queue<IStateEvent> _serializedEvents;
        [ NonSerialized ] private IContainer _container;

        /// <summary>
        ///     Initializes a new instance of the <see cref="StateEventSet" /> class.
        /// </summary>
        public StateEventSet ( )
        {
            _serializedEvents = new Queue<IStateEvent> ( );
            _parallelEvents = new Queue<IStateEvent> ( );
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="StateEventSet" /> class.
        /// </summary>
        /// <param name="configure"> The configure. </param>
        public StateEventSet ( Action<StateEventSet> configure ) : this ( )
        {
            configure ( this );
        }

        [ XmlIgnore ]
        [ SetterProperty ]
        public IContainer Container
        {
            get { return _container; }
            set { _container = value; }
        }

        #region IStateEvent Members

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
        /// <param name="container"> </param>
        /// <param name="stateMap"> </param>
        public void Execute ( IContainer container, StateMap stateMap )
        {
            Parallel.ForEach ( _parallelEvents, x => x.Execute ( container, stateMap ) );

            foreach ( IStateEvent @event in _serializedEvents )
            {
                @event.Execute ( container, stateMap );
            }
        }

        #endregion

        /// <summary>
        ///     Adds the state event.
        /// </summary>
        /// <param name="stateEvent"> The state event. </param>
        /// <param name="executionMethod"> The execution method. </param>
        public void AddStateEvent ( IStateEvent stateEvent, EventExecutionMethod executionMethod )
        {
            switch ( executionMethod )
            {
                case EventExecutionMethod.Parallel:
                    _parallelEvents.Enqueue ( stateEvent );
                    break;
                case EventExecutionMethod.Default:
                case EventExecutionMethod.Serial:
                    _serializedEvents.Enqueue ( stateEvent );
                    break;
                default:
                    throw new ArgumentOutOfRangeException ( "executionMethod" );
            }
        }
    }
}