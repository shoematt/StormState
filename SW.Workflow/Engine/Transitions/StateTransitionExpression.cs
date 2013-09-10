#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Workflow
// 
// File:	StateTransitionExpression.cs
// Author:	Matthew Shoemate
// Date:	08/27/2012
// 
// http://www.StormWorksSoftware.com
// 

#endregion

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Xml.Serialization;

using SW.Expressions.Expressions;
using SW.Workflow.Operations;
using SW.Workflow.Transitions;

using StructureMap;

namespace SW.Workflow.Engine.Transitions
{
    [ Serializable ]
    public class StateTransitionExpression : IStateTransitionExpression
    {
        private readonly List<ExpressionContainer> _conditionChains;

        [ NonSerialized ] private IContainer _container;

        /// <summary>
        ///     Initializes a new instance of the <see cref="StateTransitionExpression" /> class.
        /// </summary>
        public StateTransitionExpression ( )
        {
            // operations in a chain indicate a nested expression
            // operations at the same level are evaluated together for this instance
            _conditionChains = new List<ExpressionContainer> ( );
        }

        [ XmlIgnore ]
        //   [SetterProperty]
        public IContainer Container
        {
            get { return _container; }
            set { _container = value; }
        }

        #region IStateTransitionExpression Members

        /// <summary>
        ///     Ands the specified action.
        /// </summary>
        /// <param name="action"> The action. </param>
        /// <returns> </returns>
        ILogical ILogical.And ( ILogical action )
        {
            _conditionChains.Add ( new ExpressionContainer ( ExpressionType.And, action ) );

            return this;
        }

        /// <summary>
        ///     Ors the specified action.
        /// </summary>
        /// <param name="action"> The action. </param>
        /// <returns> </returns>
        ILogical ILogical.Or ( ILogical action )
        {
            _conditionChains.Add ( new ExpressionContainer ( ExpressionType.Or, action ) );

            return this;
        }

        /// <summary>
        ///     Nots the specified action.
        /// </summary>
        /// <param name="action"> The action. </param>
        /// <returns> </returns>
        ILogical ILogical.Not ( ILogical action )
        {
            _conditionChains.Add ( new ExpressionContainer ( ExpressionType.Not, action ) );

            return this;
        }

        /// <summary>
        ///     Customs the specified action.
        /// </summary>
        /// <param name="action"> The action. </param>
        /// <returns> </returns>
        ILogical ILogical.Custom ( ILogical action )
        {
            _conditionChains.Add ( new ExpressionContainer ( ExpressionType.Custom, action ) );

            return this;
        }

        /// <summary>
        ///     Ands the specified action.
        /// </summary>
        /// <param name="action"> The action. </param>
        /// <returns> </returns>
        ILogical ILogical.And ( Func<bool> action )
        {
            _conditionChains.Add ( new ExpressionContainer ( ExpressionType.And, action ) );

            return this;
        }

        /// <summary>
        ///     Ors the specified action.
        /// </summary>
        /// <param name="action"> The action. </param>
        /// <returns> </returns>
        ILogical ILogical.Or ( Func<bool> action )
        {
            _conditionChains.Add ( new ExpressionContainer ( ExpressionType.Or, action ) );

            return this;
        }

        /// <summary>
        ///     Nots the specified action.
        /// </summary>
        /// <param name="action"> The action. </param>
        /// <returns> </returns>
        ILogical ILogical.Not ( Func<bool> action )
        {
            _conditionChains.Add ( new ExpressionContainer ( ExpressionType.Not, action ) );

            return this;
        }

        /// <summary>
        ///     Customs the specified action.
        /// </summary>
        /// <param name="action"> The action. </param>
        /// <returns> </returns>
        ILogical ILogical.Custom ( Func<bool> action )
        {
            _conditionChains.Add ( new ExpressionContainer ( ExpressionType.Custom, action ) );

            return this;
        }

        /// <summary>
        ///     Ands the specified action.
        /// </summary>
        /// <param name="action"> The action. </param>
        /// <returns> </returns>
        ILogical ILogical.And ( Action<IExpressionEvaluator> action )
        {
            StateTransitionExpression subExpression = new StateTransitionExpression ( );
            action ( subExpression );

            _conditionChains.Add ( new ExpressionContainer ( ExpressionType.And, subExpression ) );

            return this;
        }

        /// <summary>
        ///     Ors the specified action.
        /// </summary>
        /// <param name="action"> The action. </param>
        /// <returns> </returns>
        ILogical ILogical.Or ( Action<IExpressionEvaluator> action )
        {
            StateTransitionExpression subExpression = new StateTransitionExpression ( );
            action ( subExpression );

            _conditionChains.Add ( new ExpressionContainer ( ExpressionType.Or, subExpression ) );

            return this;
        }

        /// <summary>
        ///     Nots the specified action.
        /// </summary>
        /// <param name="action"> The action. </param>
        /// <returns> </returns>
        ILogical ILogical.Not ( Action<IExpressionEvaluator> action )
        {
            StateTransitionExpression subExpression = new StateTransitionExpression ( );
            action ( subExpression );

            _conditionChains.Add ( new ExpressionContainer ( ExpressionType.Not, subExpression ) );

            return this;
        }

        /// <summary>
        ///     Customs the specified action.
        /// </summary>
        /// <param name="action"> The action. </param>
        /// <returns> </returns>
        ILogical ILogical.Custom ( Action<IExpressionEvaluator> action )
        {
            StateTransitionExpression subExpression = new StateTransitionExpression ( );
            action ( subExpression );

            _conditionChains.Add ( new ExpressionContainer ( ExpressionType.Custom, subExpression ) );

            return this;
        }

        /// <summary>
        ///     Evaluates the specified expression.
        /// </summary>
        /// <param name="expression"> The expression. </param>
        /// <returns> </returns>
        ILogical IExpressionEvaluator.Evaluate ( Func<bool> expression )
        {
            _conditionChains.Add ( new ExpressionContainer ( ExpressionType.Evaluate, expression ) );

            return this;
        }

        /// <summary>
        ///     Evaluates the specified expression.
        /// </summary>
        /// <param name="expression"> The expression. </param>
        /// <returns> </returns>
        ILogical IExpressionEvaluator.Evaluate ( Action<IStateTransitionExpression> expression )
        {
            StateTransitionExpression subExpression = new StateTransitionExpression ( );
            expression ( subExpression );

            _conditionChains.Add ( new ExpressionContainer ( ExpressionType.Evaluate, subExpression ) );

            return this;
        }

        /// <summary>
        ///     Gets or sets the expression.
        /// </summary>
        /// <value> The expression. </value>
        public EditableExpression Expression { get; set; }

        /// <summary>
        ///     Evaluates this instance. (at the moment, only two explicit lamda forms are supported)
        /// </summary>
        /// <returns> </returns>
        bool IOperationEvaluator.Evaluate ( )
        {
            if ( Expression != null )
            {
                EditableExpression expression = Expression;

                LambdaExpression delegateExpression = ( LambdaExpression ) expression.ToExpression ( );

                object compiledDelegate = delegateExpression.Compile ( );

                if ( compiledDelegate is MulticastDelegate )
                {
                    MulticastDelegate multicastDelegate = ( MulticastDelegate ) compiledDelegate;

                    compiledDelegate = multicastDelegate.DynamicInvoke ( null );
                }

                if ( compiledDelegate is Func<bool> )
                {
                    Func<bool> delegateMethod = ( Func<bool> ) compiledDelegate;

                    return delegateMethod ( );
                }
                if ( compiledDelegate is Action<IExpressionEvaluator> )
                {
                    Action<IExpressionEvaluator> evaluatorDelegate = ( Action<IExpressionEvaluator> ) compiledDelegate;

                    evaluatorDelegate ( this );
                }
            }

            bool? evaluation = null;

            foreach ( ExpressionContainer container in  _conditionChains )
            {
                if ( container.Type == ExpressionType.Evaluate )
                {
                    evaluation = container.Evaluate ( );
                }

                if ( container.Type == ExpressionType.And )
                {
                    evaluation = evaluation.GetValueOrDefault ( ) && container.Evaluate ( );
                }

                if ( container.Type == ExpressionType.Or )
                {
                    evaluation = evaluation.GetValueOrDefault ( ) || container.Evaluate ( );
                }

                if ( container.Type == ExpressionType.Is )
                {
                    evaluation = container.Evaluate ( );
                }

                if ( container.Type == ExpressionType.Not )
                {
                    evaluation = !container.Evaluate ( );
                }

                if ( container.Type == ExpressionType.Custom )
                {
                    evaluation = container.Evaluate ( );
                }
            }

            return evaluation.GetValueOrDefault ( );
        }

        /// <summary>
        ///     Initializes the container.
        /// </summary>
        /// <param name="container"> The container. </param>
        public void InitializeContainer ( IContainer container )
        {
            Container = container;
        }

        #endregion

        /// <summary>
        ///     Ands the specified action.
        /// </summary>
        /// <param name="action"> The action. </param>
        /// <returns> </returns>
        protected virtual ILogical And ( ILogical action )
        {
            return ( ( ILogical ) this ).And ( action );
        }

        /// <summary>
        ///     Ors the specified action.
        /// </summary>
        /// <param name="action"> The action. </param>
        /// <returns> </returns>
        protected virtual ILogical Or ( ILogical action )
        {
            return ( ( ILogical ) this ).Or ( action );
        }

        /// <summary>
        ///     Nots the specified action.
        /// </summary>
        /// <param name="action"> The action. </param>
        /// <returns> </returns>
        protected virtual ILogical Not ( ILogical action )
        {
            return ( ( ILogical ) this ).Not ( action );
        }

        /// <summary>
        ///     Customs the specified action.
        /// </summary>
        /// <param name="action"> The action. </param>
        /// <returns> </returns>
        protected virtual ILogical Custom ( ILogical action )
        {
            return ( ( ILogical ) this ).Custom ( action );
        }

        /// <summary>
        ///     Ands the specified action.
        /// </summary>
        /// <param name="action"> The action. </param>
        /// <returns> </returns>
        protected virtual ILogical And ( Func<bool> action )
        {
            return ( ( ILogical ) this ).And ( action );
        }

        /// <summary>
        ///     Ors the specified action.
        /// </summary>
        /// <param name="action"> The action. </param>
        /// <returns> </returns>
        protected virtual ILogical Or ( Func<bool> action )
        {
            return ( ( ILogical ) this ).Or ( action );
        }

        /// <summary>
        ///     Nots the specified action.
        /// </summary>
        /// <param name="action"> The action. </param>
        /// <returns> </returns>
        protected virtual ILogical Not ( Func<bool> action )
        {
            return ( ( ILogical ) this ).Not ( action );
        }

        /// <summary>
        ///     Customs the specified action.
        /// </summary>
        /// <param name="action"> The action. </param>
        /// <returns> </returns>
        protected virtual ILogical Custom ( Func<bool> action )
        {
            return ( ( ILogical ) this ).Custom ( action );
        }

        /// <summary>
        ///     Ands the specified action.
        /// </summary>
        /// <param name="action"> The action. </param>
        /// <returns> </returns>
        protected virtual ILogical And ( Action<IExpressionEvaluator> action )
        {
            return ( ( ILogical ) this ).And ( action );
        }

        /// <summary>
        ///     Nots the specified action.
        /// </summary>
        /// <param name="action"> The action. </param>
        /// <returns> </returns>
        protected virtual ILogical Not ( Action<IExpressionEvaluator> action )
        {
            return ( ( ILogical ) this ).Not ( action );
        }

        /// <summary>
        ///     Customs the specified action.
        /// </summary>
        /// <param name="action"> The action. </param>
        /// <returns> </returns>
        protected virtual ILogical Custom ( Action<IExpressionEvaluator> action )
        {
            return ( ( ILogical ) this ).Custom ( action );
        }

        /// <summary>
        ///     Ors the specified action.
        /// </summary>
        /// <param name="action"> The action. </param>
        /// <returns> </returns>
        protected virtual ILogical Or ( Action<IExpressionEvaluator> action )
        {
            return ( ( ILogical ) this ).Or ( action );
        }

        /// <summary>
        ///     Evaluates this instance.
        /// </summary>
        /// <returns> </returns>
        protected virtual bool Evaluate ( )
        {
            return ( ( IOperationEvaluator ) this ).Evaluate ( );
        }
    }
}