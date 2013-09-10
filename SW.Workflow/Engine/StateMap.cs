#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Workflow
// 
// File:	StateMap.cs
// Author:	Matthew Shoemate
// Date:	08/27/2012
// 
// http://www.StormWorksSoftware.com
// 

#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

using SW.Workflow.Engine.States;
using SW.Workflow.States;
using SW.Workflow.Transitions;

using StructureMap;

namespace SW.Workflow.Engine
{
    /// <summary>
    ///     Defines a mapping of states and transitions on those states.
    /// </summary>
    [ Serializable ]
    public class StateMap : IStateMap
    {
        [ NonSerialized ] private IContainer _container;

        /// <summary>
        ///     Initializes a new instance of the <see cref="StateMap" /> class.
        /// </summary>
        public StateMap ( )
        {
            _container = ObjectFactory.Container;

            States = new List<IState> ( );

            Transitions = new List<IStateTransition> ( );

            Components = new List<IStateComponent> ( );
        }

        #region IStateMap Members

        /// <summary>
        ///     Gets the container.
        /// </summary>
        /// <value> The container. </value>
        [ XmlIgnore ]
        public IContainer Container
        {
            get { return _container; }
            set { _container = value; }
        }

        /// <summary>
        ///     Gets or sets the states.
        /// </summary>
        /// <value> The states. </value>
        public List<IState> States { get; private set; }

        /// <summary>
        ///     Gets or sets the transitions.
        /// </summary>
        /// <value> The transitions. </value>
        public List<IStateTransition> Transitions { get; private set; }

        /// <summary>
        ///     Gets or sets the components.
        /// </summary>
        /// <value> The components. </value>
        public List<IStateComponent> Components { get; private set; }

        #endregion

        /// <summary>
        ///     Adds a state that has already been configured elsewhere (or does not have a configuration).
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="state"> The state. </param>
        /// <returns> </returns>
        public T AddState<T> ( T state ) where T : IState
        {
            States.Add ( state );

            Container.BuildUp ( state );

            return state;
        }

        /// <summary>
        ///     Adds a new state instance and provides a configuration entry point.
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <returns> </returns>
        public T AddState<T> ( T state, Action<T> stateConfiguration ) where T : IState
        {
            stateConfiguration ( state );

            States.Add ( state );

            Container.BuildUp ( state );

            return state;
        }

        /// <summary>
        ///     Adds a new state instance and provides a configuration entry point.
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="stateConfiguration"> The state configuration. </param>
        /// <returns> </returns>
        public T AddState<T> ( Action<T> stateConfiguration ) where T : class, IState, new ( )
        {
            _container.Configure ( x => x.For<T> ( )
                                         .Use<T> ( ) );

            T state = _container.GetInstance<T> ( );

            stateConfiguration ( state );

            States.Add ( state );

            Container.BuildUp ( state );

            return state;
        }

        /// <summary>
        ///     Adds the component.
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="componentConfiguration"> The component configuration. </param>
        /// <returns> </returns>
        public T AddComponent<T> ( Action<T> componentConfiguration ) where T : class, IStateComponent, new ( )
        {
            _container.Configure ( x => x.For<T> ( )
                                         .Use<T> ( ) );

            T state = _container.GetInstance<T> ( );

            componentConfiguration ( state );

            Components.Add ( state );

            Container.BuildUp ( state );

            return state;
        }

        /// <summary>
        ///     Adds a new state instance and provides a configuration entry point.
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="component"> The component. </param>
        /// <param name="componentConfiguration"> The component configuration. </param>
        /// <returns> </returns>
        public T AddComponent<T> ( T component, Action<T> componentConfiguration ) where T : IStateComponent
        {
            componentConfiguration ( component );

            Components.Add ( component );

            Container.BuildUp ( component );

            return component;
        }

        /// <summary>
        ///     Adds the component.
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="component"> The component. </param>
        /// <returns> </returns>
        public IStateComponent AddComponent<T> ( T component ) where T : IStateComponent
        {
            Components.Add ( component );

            Container.BuildUp ( component );

            return component;
        }

        /// <summary>
        ///     Adds the transition.
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="configureTransitionAction"> The configure transition action. </param>
        /// <returns> </returns>
        public IStateTransition AddTransition<T> ( Action<T> configureTransitionAction ) where T : class, IStateTransition, new ( )
        {
            if ( configureTransitionAction == null )
            {
                throw new ArgumentNullException ( "configureTransitionAction" );
            }

            _container.Configure ( x => x.For<T> ( )
                                         .Use<T> ( ) );

            var transition = _container.GetInstance<T> ( );

            configureTransitionAction ( transition );

            AddTransition ( transition );

            Container.BuildUp ( transition );

            return transition;
        }

        /// <summary>
        ///     Adds the transition.
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="createAndConfigure"> The create and configure. </param>
        /// <returns> </returns>
        public IStateTransition AddTransition<T> ( Func<T> createAndConfigure ) where T : class, IStateTransition
        {
            if ( createAndConfigure == null )
            {
                throw new ArgumentNullException ( "createAndConfigure" );
            }

            return AddTransition ( createAndConfigure ( ) );
        }

        /// <summary>
        ///     Adds the transition.
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="transition"> The transition. </param>
        /// <returns> </returns>
        public IStateTransition AddTransition<T> ( T transition ) where T : class, IStateTransition
        {
            Transitions.Add ( transition );

            Container.BuildUp ( transition );

            return transition;
        }

        /// <summary>
        ///     Gets or sets the initial state.
        /// </summary>
        /// <value> The initial state. </value>
        public List<IState> GetInitialStates ( )
        {
            List<IState> initialStates = new List<IState> ( );

            if ( Transitions.Count == 1 )
            {
                initialStates.Add ( ( Transitions [0] ).From );
                return initialStates;
            }

            try
            {
                initialStates.AddRange ( States.Where ( state => state.Extended != null && state.Extended.GetValue<bool> ( State.IsInitial ) ) );
            }
            catch
            {
                // Temporary hack -- for supporting existing flows that don't have extensions registered yet.

                initialStates.AddRange ( from trans in Transitions where ( trans.To != null && trans.To.Count != 0 && trans.From != null ) where !Transitions.Any ( transition => transition.To.Contains ( trans.From ) ) select trans.From );
            }

            if ( initialStates.Count == 0 )
            {
                throw new CyclicStateMapException ( );
            }

            return initialStates;
        }

        /// <summary>
        ///     Gets or sets the final state.
        /// </summary>
        /// <value> The final state. </value>
        public IState GetFinalState ( )
        {
            return null;
        }

        /// <summary>
        ///     Configures the specified state map.
        /// </summary>
        /// <param name="stateMap"> The state map. </param>
        public void Configure ( Action<StateMap> stateMap )
        {
            stateMap ( this );
        }

        /// <summary>
        ///     Gets the transitions for states.
        /// </summary>
        /// <param name="activeStates"> The active states. </param>
        /// <returns> </returns>
        public List<IStateTransition> GetTransitionsForStates ( List<IState> activeStates )
        {
            var transitions = new List<IStateTransition> ( );

            foreach ( IState state in activeStates )
            {
                IState state1 = state;
                transitions.AddRange ( from transition in Transitions where transition.From == state1 select transition );
            }

            return transitions;
        }

        ///// <summary>
        /////   Gets a <see cref = "StateMap" /> instance from the provided <see cref = "StateMapData" />
        ///// </summary>
        ///// <param name = "stateMapData">The state map data.</param>
        ///// <param name="container"></param>
        ///// <returns></returns>
        //public static StateMap FromData(StateMapData stateMapData, IContainer container)
        //{
        //    BinaryFormatter formatter = new BinaryFormatter();
        //    MemoryStream stream = new MemoryStream(stateMapData.Data);

        //    using (stream)
        //    {
        //        StateMap map = (StateMap)formatter.Deserialize(stream);

        //        map.InitializeContainers(container);

        //        return map;
        //    }
        //}

        /// <summary>
        ///     Initializes the containers.
        /// </summary>
        public void InitializeContainers ( IContainer container )
        {
            Container = container.GetNestedContainer ( );

            Container.Configure ( x => x.For<StateMap> ( )
                                        .Use ( this ) );

            foreach ( IStateTransition transition in Transitions )
            {
                transition.InitializeContainer ( container );
            }

            foreach ( IState state in States )
            {
                state.InitializeContainer ( container );
            }

            foreach ( IStateComponent component in Components )
            {
                component.InitializeContainer ( container );
            }
        }

        /// <summary>
        ///     Resolves the cycles.
        /// </summary>
        /// <returns> </returns>
        public GraphType ResolveCycles ( )
        {
            if ( Transitions.Count > 1 )
            {
                var initialStates = GetInitialStates ( );

                if ( initialStates.Count == 0 )
                {
                    return GraphType.Cyclic;
                }
            }

            return GraphType.Aclyclic;
        }
    }
}