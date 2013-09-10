#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Workflow.Testing
// 
// File:	StateMachineTests.cs
// Author:	Matthew Shoemate
// Date:	08/27/2012
// 
// http://www.StormWorksSoftware.com
// 

#endregion

using System;

using NUnit.Framework;

using SW.Workflow.Behavior;
using SW.Workflow.Engine;
using SW.Workflow.Engine.States;
using SW.Workflow.Events;
using SW.Workflow.Logic;

using StructureMap;

namespace SW.Workflow.Tests
{
    [ TestFixture ( Category = "Workflows", Description = "Tests functionality of the state machine processing" ) ]
    public class WorkflowEngineTests
    {
        [ TestFixtureSetUp ]
        public void TestSetUp ( )
        {
            ObjectFactory.Initialize ( x => x.For<StateMachine> ( )
                                             .Use<StateMachine> ( ) );
        }

        [ TestFixtureTearDown ]
        public void FixtureTearDown ( )
        {
        }

        public void Is_Correct_Initial_State_Resolved ( )
        {
        }

        [ Obsolete ( "Test no longer applies, cyclic graphs are OK, but not good" ) ]
        [ ExpectedException ( typeof ( StateMachineException ) ) ]
        public void Attempt_To_Create_Cyclic_State_Map_Exception ( )
        {
            StateMap map = ObjectFactory.GetInstance<StateMap> ( );

            Assert.Throws ( typeof ( Exception ), ( ) => map.Configure ( stateMap =>
                                                                         {
                                                                             var state1 = stateMap.AddState ( new State ( ) )
                                                                                                  .Configure ( x => x.OnEnter ( new StateEvent<SimpleAction> ( new SimpleAction ( ( ) => { } ) ) )
                                                                                                                     .Name = "State 1" );

                                                                             var state2 = stateMap.AddState ( new State ( ) )
                                                                                                  .Configure ( x => x.OnEnter ( new StateEvent<SimpleAction> ( new SimpleAction ( ( ) => Console.WriteLine ( "Action 1 Fired" ) ) ) )
                                                                                                                     .Name = "State 2" );

                                                                             stateMap.AddTransition ( ( ) => new ConditionalTransition ( state1, state2, state1, x => x.Evaluate ( ( ) => true )
                                                                                                                                                                       .And ( b => b.Evaluate ( ( ) => true )
                                                                                                                                                                                    .And ( ( ) => true ) )
                                                                                                                                                                       .Or ( ( ) => true )
                                                                                                                                                                       .And ( ( ) => true ) ) );

                                                                             stateMap.AddTransition ( ( ) => new ConditionalTransition ( state2, state1, state2, x => x.Evaluate ( ( ) => true )
                                                                                                                                                                       .And ( b => b.Evaluate ( ( ) => true )
                                                                                                                                                                                    .And ( ( ) => true ) )
                                                                                                                                                                       .Or ( ( ) => true )
                                                                                                                                                                       .And ( ( ) => true ) ) );
                                                                         } ) );

            StateMachine stateMachineWithCyclicMap = ObjectFactory.GetInstance<StateMachine> ( );

            stateMachineWithCyclicMap.Configure ( map );

            stateMachineWithCyclicMap.Start ( );
        }

        [ Test ]
        public void Branch_To_Simultaneous_Active_States_One_State_Transition_False_With_Null_Final_States ( )
        {
            StateMachine stateMachine = ObjectFactory.GetInstance<StateMachine> ( );
            StateMap map = ObjectFactory.GetInstance<StateMap> ( );

            State state0 = map.AddState ( new State ( ) )
                              .Configure ( x => { x.Name = "State 0"; } );
            State state1 = map.AddState ( new State ( ) )
                              .Configure ( x => { x.Name = "State 1"; } );
            State state2 = map.AddState ( new State ( ) )
                              .Configure ( x => { x.Name = "State 2"; } );
            State state3 = map.AddState ( new State ( ) )
                              .Configure ( x => { x.Name = "State 3"; } );

            map.Configure ( stateMap =>
                            {
                                stateMap.AddTransition ( ( ) => new ConditionalTransition ( state0, state1, state2, expression => expression.Evaluate ( ( ) => true ) ) );
                                stateMap.AddTransition ( ( ) => new ConditionalTransition ( state1, state2, state3, expression => expression.Evaluate ( ( ) => true ) ) );
                                stateMap.AddTransition ( ( ) => new ConditionalTransition ( state2, state3, null, expression => expression.Evaluate ( ( ) => true ) ) );
                                stateMap.AddTransition ( ( ) => new ConditionalTransition ( state3, null, null, expression => expression.Evaluate ( ( ) => true ) ) );
                            } );

            stateMachine.Configure ( map );

            stateMachine.Start ( state0 );

            Assert.NotNull ( stateMachine.ActiveStates [0] );
            Assert.AreEqual ( stateMachine.ActiveStates [0].Name, "State 0" );

            stateMachine.Transition ( );

            Assert.NotNull ( stateMachine.ActiveStates [0] );
            Assert.AreEqual ( stateMachine.ActiveStates [0].Name, "State 1" );

            stateMachine.Transition ( );

            Assert.NotNull ( stateMachine.ActiveStates [0] );
            Assert.AreEqual ( stateMachine.ActiveStates [0].Name, "State 2" );

            stateMachine.Transition ( );

            Assert.NotNull ( stateMachine.ActiveStates [0] );
            Assert.AreEqual ( stateMachine.ActiveStates [0].Name, "State 3" );

            stateMachine.Transition ( );

            stateMachine.Stop ( );
        }

        [ Test ]
        public void Create_And_Populate_Engine_And_Transitions ( )
        {
            StateMachine stateMachine = ObjectFactory.GetInstance<StateMachine> ( );
            StateMap map = ObjectFactory.GetInstance<StateMap> ( );

            State initialState = map.AddState ( new State ( ) )
                                    .Configure ( x => { x.Name = "Initial State"; } );
            State state1 = map.AddState ( new State ( ) )
                              .Configure ( x => { x.Name = "State 1"; } );
            State state2 = map.AddState ( new State ( ) )
                              .Configure ( x => { x.Name = "State 2"; } );
            State state3 = map.AddState ( new State ( ) )
                              .Configure ( x => { x.Name = "State 3"; } );

            const bool isItemPickedUp = true;
            const bool doesProperty1Match = true;
            const bool doesProperty2Match = false;
            const bool ignorePropertyValues = true;

            const bool canTransitionTo2 = true;
            const bool canTransitionTo3 = true;

            map.Configure ( stateMap =>
                            {
                                stateMap.AddTransition ( ( ) => new ConditionalTransition ( initialState, state1, null, expression => expression.Evaluate ( ( ) => isItemPickedUp )
                                                                                                                                                .And ( ( ) => doesProperty1Match )
                                                                                                                                                .And ( ( ) => doesProperty2Match )
                                                                                                                                                .Or ( ( ) => ignorePropertyValues ) ) );

                                stateMap.AddTransition ( ( ) => new ConditionalTransition ( state1, state2, null, expression => expression.Evaluate ( ( ) => canTransitionTo2 ) ) );

                                stateMap.AddTransition ( ( ) => new ConditionalTransition ( state2, state3, null, expression => expression.Evaluate ( ( ) => canTransitionTo3 ) ) );
                            } );

            stateMachine.Configure ( map );

            stateMachine.Start ( initialState );

            Assert.NotNull ( stateMachine.ActiveStates [0] );
            Assert.AreEqual ( stateMachine.ActiveStates [0].Name, "Initial State" );

            stateMachine.Transition ( );

            Assert.NotNull ( stateMachine.ActiveStates [0] );
            Assert.AreEqual ( stateMachine.ActiveStates [0].Name, "State 1" );

            stateMachine.Transition ( );

            Assert.NotNull ( stateMachine.ActiveStates [0] );
            Assert.AreEqual ( stateMachine.ActiveStates [0].Name, "State 2" );

            stateMachine.Transition ( );

            Assert.NotNull ( stateMachine.ActiveStates [0] );
            Assert.AreEqual ( stateMachine.ActiveStates [0].Name, "State 3" );

            stateMachine.Stop ( );
        }

        [ Test ]
        public void Create_Conditional_Transitions_Map_And_Execute ( )
        {
            StateMap map = ObjectFactory.GetInstance<StateMap> ( );

            map.Configure ( stateMap =>
                            {
                                State initialState = stateMap.AddState ( new State ( ) )
                                                             .Configure ( x => x.OnEnter ( new StateEvent<SimpleAction> ( new SimpleAction ( ( ) => { } ) ) )
                                                                                .Name = "initialState" );

                                State trueState = stateMap.AddState ( new State ( ) )
                                                          .Configure ( x => x.OnEnter ( new StateEvent<SimpleAction> ( new SimpleAction ( ( ) => Console.WriteLine ( @"True Action Fired" ) ) ) )
                                                                             .Name = "trueState" );

                                State falseState = stateMap.AddState ( new State ( ) )
                                                           .Configure ( x => x.OnEnter ( new StateEvent<SimpleAction> ( new SimpleAction ( ( ) => Console.WriteLine ( @"False Action Fired" ) ) ) )
                                                                              .Name = "falseState" );

                                stateMap.AddTransition ( ( ) => new ConditionalTransition ( initialState, trueState, falseState, expression => expression.Evaluate ( ( ) => true ) ) );
                            } );

            StateMachine machine = ObjectFactory.GetInstance<StateMachine> ( );

            machine.Configure ( map );

            machine.Start ( );

            Assert.AreEqual ( machine.ActiveStates [0].Name, "initialState" );

            machine.Transition ( );

            Assert.AreEqual ( machine.ActiveStates [0].Name, "trueState" );

            machine.Transition ( );
        }

        [ Test ]
        public void Multiple_Branching_State_Transitions ( )
        {
            StateMachine stateMachine = ObjectFactory.GetInstance<StateMachine> ( );
            StateMap map = ObjectFactory.GetInstance<StateMap> ( );

            State state0 = map.AddState ( new State ( ) )
                              .Configure ( x => { x.Name = "State 0"; } );
            State state1 = map.AddState ( new State ( ) )
                              .Configure ( x => { x.Name = "State 1"; } );
            State state2 = map.AddState ( new State ( ) )
                              .Configure ( x => { x.Name = "State 2"; } );
            State state3 = map.AddState ( new State ( ) )
                              .Configure ( x => { x.Name = "State 3"; } );
            State state4 = map.AddState ( new State ( ) )
                              .Configure ( x => { x.Name = "State 4"; } );
            State state5 = map.AddState ( new State ( ) )
                              .Configure ( x => { x.Name = "State 5"; } );
            State state6 = map.AddState ( new State ( ) )
                              .Configure ( x => { x.Name = "State 6"; } );
            State state7 = map.AddState ( new State ( ) )
                              .Configure ( x => { x.Name = "State 7"; } );

            map.Configure ( stateMap =>
                            {
                                stateMap.AddTransition ( ( ) => new ConditionalTransition ( state0, state1, state2, expression => expression.Evaluate ( ( ) => true ) ) );
                                stateMap.AddTransition ( ( ) => new ConditionalTransition ( state1, state3, state4, expression => expression.Evaluate ( ( ) => false ) ) );
                                stateMap.AddTransition ( ( ) => new ConditionalTransition ( state3, state5, state6, expression => expression.Evaluate ( ( ) => true ) ) );
                                stateMap.AddTransition ( ( ) => new ConditionalTransition ( state4, state6, state7, expression => expression.Evaluate ( ( ) => false ) ) );
                            } );

            stateMachine.Configure ( map );

            stateMachine.Start ( state0 );

            Assert.NotNull ( stateMachine.ActiveStates [0] );
            Assert.AreEqual ( stateMachine.ActiveStates [0].Name, "State 0" );

            stateMachine.Transition ( );

            Assert.NotNull ( stateMachine.ActiveStates [0] );
            Assert.AreEqual ( stateMachine.ActiveStates [0].Name, "State 1" );

            stateMachine.Transition ( );

            Assert.NotNull ( stateMachine.ActiveStates [0] );
            Assert.AreEqual ( stateMachine.ActiveStates [0].Name, "State 4" );

            stateMachine.Transition ( );

            Assert.NotNull ( stateMachine.ActiveStates [0] );
            Assert.AreEqual ( stateMachine.ActiveStates [0].Name, "State 7" );

            stateMachine.Stop ( );
        }

        [ Test ]
        public void Transition_With_Complex_Conditional_Fail_Subcondition ( )
        {
            StateMachine stateMachine = ObjectFactory.GetInstance<StateMachine> ( );
            StateMap map = ObjectFactory.GetInstance<StateMap> ( );

            State state0 = map.AddState ( new State ( ) )
                              .Configure ( x => { x.Name = "State 0"; } );

            State state1 = map.AddState ( new State ( ) )
                              .Configure ( x => x.OnEnter ( new StateEvent<SimpleAction> ( new SimpleAction ( ( ) => Console.WriteLine ( @"Action 1 Fired" ) ) ) ) );

            map.Configure ( stateMap => stateMap.AddTransition ( ( ) => new ConditionalTransition ( state0, state1, null, expression => expression.Evaluate ( ( ) => true )
                                                                                                                                                  .And ( ( ) => true )
                                                                                                                                                  .And ( ( ) => false )
                                                                                                                                                  .Or ( subExpression => subExpression.Evaluate ( subExpression1 => subExpression1.Or ( subCondition2 => subCondition2.Evaluate ( ( ) => true )
                                                                                                                                                                                                                                                                      .And ( ( ) => false ) ) ) ) ) ) );

            stateMachine.Configure ( map );

            stateMachine.Start ( );

            Assert.NotNull ( stateMachine.ActiveStates [0] );
            Assert.AreEqual ( stateMachine.ActiveStates [0].Name, "State 0" );

            stateMachine.Transition ( );

            stateMachine.Stop ( );
        }

        [ Test ]
        public void Transition_With_Complex_Conditional_Pass_Subcondition ( )
        {
            StateMachine stateMachine = ObjectFactory.GetInstance<StateMachine> ( );
            StateMap map = ObjectFactory.GetInstance<StateMap> ( );

            State state0 = map.AddState ( new State ( ) )
                              .Configure ( x => x.OnEnter ( new StateEvent<StateEventLogicHandler> ( new OnEnterStateLogicHandler ( ObjectFactory.Container ) ) )
                                                 .Name = "State 0" );

            State state1 = map.AddState ( new State ( ) )
                              .Configure ( x => x.OnEnter ( new StateEvent<SimpleAction> ( new SimpleAction ( ( ) => Console.WriteLine ( @"Action 1 Fired" ) ) ) )
                                                 .Name = "State 1" );

            map.Configure ( stateMap => stateMap.AddTransition ( ( ) => new ConditionalTransition ( state0, state1, null, expression => expression.Evaluate ( ( ) => true )
                                                                                                                                                  .And ( ( ) => true )
                                                                                                                                                  .And ( ( ) => false )
                                                                                                                                                  .Or ( subCondition => subCondition.Evaluate ( ( ) => true )
                                                                                                                                                                                    .And ( ( ) => true ) ) ) ) );

            stateMachine.Configure ( map );

            stateMachine.Start ( );

            Assert.NotNull ( stateMachine.ActiveStates [0] );
            Assert.AreEqual ( stateMachine.ActiveStates [0].Name, "State 0" );

            stateMachine.Transition ( );

            Assert.NotNull ( stateMachine.ActiveStates [0] );
            Assert.AreEqual ( stateMachine.ActiveStates [0].Name, "State 1" );

            stateMachine.Stop ( );
        }
    }
}