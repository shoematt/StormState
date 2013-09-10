#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Workflow
// 
// File:	ConditionalTransitionEvent.cs
// Author:	Matthew Shoemate
// Date:	08/27/2012
// 
// http://www.StormWorksSoftware.com
// 

#endregion

using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using SW.Core;
using SW.Core.Logging;
using SW.Expressions.Expressions;
using SW.Workflow.Engine.Transitions;
using SW.Workflow.Operations;
using SW.Workflow.States;
using SW.Workflow.Transitions;

using StructureMap;

namespace SW.Workflow.Events
{
    [ Serializable ]
    public class ConditionalTransition : IStateTransition,
                                         IOperationEvaluator
    {
        [ NonSerialized ] private readonly ILog _log = Logger.GetNamedLogger ( "ConditionalTransition" );
        [ NonSerialized ] private IContainer _container;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ConditionalTransition" /> class.
        /// </summary>
        /// <param name="fromState"> From state. </param>
        /// <param name="trueState"> From state. </param>
        /// <param name="falseState"> State of the true. </param>
        /// <param name="expression"> The expression. </param>
        public ConditionalTransition ( IState fromState, IState trueState, IState falseState, Func<bool> expression )
        {
            To = new List<IState> ( );

            Expression = new StateTransitionExpression {Expression = EditableExpression.CreateEditableExpression ( ( ) => expression )};

            True = trueState;
            False = falseState;
            From = fromState;

            To.Add ( trueState );
            To.Add ( falseState );
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ConditionalTransition" /> class.
        /// </summary>
        /// <param name="fromState"> From state. </param>
        /// <param name="trueState"> From state. </param>
        /// <param name="falseState"> State of the true. </param>
        /// <param name="transitionExpression"> expression used to determine which branch to traverse. </param>
        public ConditionalTransition ( IState fromState, IState trueState, IState falseState, Action<IExpressionEvaluator> transitionExpression )
        {
            To = new List<IState> ( );

            Expression = new StateTransitionExpression {Expression = EditableExpression.CreateEditableExpression ( ( ) => transitionExpression )};

            True = trueState;
            False = falseState;
            From = fromState;

            To.Add ( trueState );
            To.Add ( falseState );
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ConditionalTransition" /> class.
        /// </summary>
        /// <param name="fromState"> From state. </param>
        /// <param name="trueState"> From state. </param>
        /// <param name="falseState"> State of the true. </param>
        /// <param name="transitionExpression"> expression used to determine which branch to traverse. </param>
        public ConditionalTransition ( IState fromState, IState trueState, IState falseState, StateTransitionExpression transitionExpression )
        {
            To = new List<IState> ( );
            Expression = transitionExpression;

            True = trueState;
            False = falseState;
            From = fromState;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ConditionalTransition" /> class.
        /// </summary>
        public ConditionalTransition ( )
        {
            To = new List<IState> ( );
            Expression = new StateTransitionExpression ( );

            True = null;
            False = null;
            From = null;

            if ( _log.IsDebugEnabled )
            {
                _log.DebugFormat ( "un-configured ConditionalTransition created." );
            }
        }

        /// <summary>
        ///     Gets or sets the conditions.
        /// </summary>
        /// <value> The conditions. </value>
        public IStateTransitionExpression Expression { get; set; }

        /// <summary>
        ///     Gets or sets to state.
        /// </summary>
        /// <value> To state. </value>
        public IState False { get; set; }

        /// <summary>
        ///     Gets or sets to state.
        /// </summary>
        /// <value> To state. </value>
        public IState True { get; set; }

        /// <summary>
        ///     Gets or sets the event.
        /// </summary>
        /// <value> The event. </value>
        public IStateTransitionEvent Event { get; set; }

        /// <summary>
        ///     Gets or sets the container.
        /// </summary>
        /// <value> The container. </value>
        [ XmlIgnore ]
        // [SetterProperty]
        public IContainer Container
        {
            get { return _container; }
            set { _container = value; }
        }

        #region IOperationEvaluator Members

        /// <summary>
        ///     Evaluates this instance.
        /// </summary>
        /// <returns> </returns>
        public bool Evaluate ( )
        {
            return Expression.Evaluate ( );
        }

        #endregion

        #region IStateTransition Members

        /// <summary>
        ///     Gets or sets from state.
        /// </summary>
        /// <value> From state. </value>
        public IState From { get; set; }

        /// <summary>
        ///     Gets or sets the destination <see cref="IState" />.
        /// </summary>
        /// <value> The destination state. </value>
        public List<IState> To { get; set; }

        /// <summary>
        ///     Executes the specified transition action.
        /// </summary>
        /// <param name="transitionAction"> The transition action. </param>
        public void DoTransition ( Action<List<IState>, IState, IStateTransition> transitionAction )
        {
            To.Clear ( );

            var result = Evaluate ( );
            var state = result ? True : False;

            if ( state != null )
            {
                To.Add ( state );
            }

            transitionAction ( To, From, this );
        }

        /// <summary>
        ///     Resets this instance.
        /// </summary>
        public void Reset ( )
        {
            To.Clear ( );
            To.Add ( True );
            To.Add ( False );
        }

        /// <summary>
        ///     Initializes the container.
        /// </summary>
        /// <param name="container"> The container. </param>
        public void InitializeContainer ( IContainer container )
        {
            Container = container;

            if ( From != null )
            {
                From.InitializeContainer ( container );
            }

            foreach ( var item in To )
            {
                if ( item != null )
                {
                    item.InitializeContainer ( container );
                }
            }

            if ( True != null )
            {
                True.InitializeContainer ( container );
            }

            if ( False != null )
            {
                False.InitializeContainer ( container );
            }
        }

        #endregion
    }
}