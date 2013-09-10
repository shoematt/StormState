#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Workflow
// 
// File:	ExpressionContainer.cs
// Author:	Matthew Shoemate
// Date:	08/27/2012
// 
// http://www.StormWorksSoftware.com
// 

#endregion

using System;

using SW.Workflow.Operations;
using SW.Workflow.States;

namespace SW.Workflow.Engine
{
    [ Serializable ]
    public class ExpressionContainer : IOperationEvaluator
    {
        [ NonSerialized ] private object _operation;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ExpressionContainer" /> class.
        /// </summary>
        /// <param name="type"> The type. </param>
        /// <param name="operation"> The operation. </param>
        public ExpressionContainer ( ExpressionType type, ILogical operation )
        {
            Type = type;
            Operation = operation;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ExpressionContainer" /> class.
        /// </summary>
        /// <param name="type"> The type. </param>
        /// <param name="operation"> The operation. </param>
        public ExpressionContainer ( ExpressionType type, Func<bool> operation )
        {
            Type = type;
            Operation = operation;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ExpressionContainer" /> class.
        /// </summary>
        /// <param name="type"> The type. </param>
        /// <param name="operation"> The operation. </param>
        public ExpressionContainer ( ExpressionType type, Func<ILogical, ILogical> operation )
        {
            Type = type;
            Operation = operation;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ExpressionContainer" /> class.
        /// </summary>
        /// <param name="type"> The type. </param>
        /// <param name="operation"> The operation. </param>
        public ExpressionContainer ( ExpressionType type, Action operation )
        {
            Type = type;
            Operation = operation;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ExpressionContainer" /> class.
        /// </summary>
        /// <param name="type"> The type. </param>
        /// <param name="operationConfiguration"> The operation configuration. </param>
        public ExpressionContainer ( ExpressionType type, Action<IExpressionEvaluator> operationConfiguration )
        {
            Type = type;
            Operation = operationConfiguration;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ExpressionContainer" /> class.
        /// </summary>
        /// <param name="type"> The type. </param>
        /// <param name="operationConfiguration"> The operation configuration. </param>
        public ExpressionContainer ( ExpressionType type, IState operationConfiguration )
        {
            Type = type;
            Operation = operationConfiguration;
        }

        /// <summary>
        ///     Gets or sets the type.
        /// </summary>
        /// <value> The type. </value>
        public ExpressionType Type { get; private set; }

        /// <summary>
        ///     Gets or sets the operation.
        /// </summary>
        /// <value> The operation. </value>
        public object Operation
        {
            get { return _operation; }
            private set { _operation = value; }
        }

        #region IOperationEvaluator Members

        /// <summary>
        ///     Evaluates this instance.
        /// </summary>
        /// <returns> </returns>
        public bool Evaluate ( )
        {
            if ( Operation is IOperationEvaluator )
            {
                var operation = ( IOperationEvaluator ) Operation;

                return operation.Evaluate ( );
            }

            if ( Operation is Func<bool> )
            {
                var operation = ( Func<bool> ) Operation;

                return operation ( );
            }

            return false;
        }

        #endregion

        /// <summary>
        ///     Gets the state.
        /// </summary>
        /// <returns> </returns>
        public IState GetState ( )
        {
            return Operation as IState;
        }

        /// <summary>
        ///     Executes this instance.
        /// </summary>
        public void Execute ( )
        {
            if ( Operation is Action )
            {
                var operation = ( Action ) Operation;

                operation ( );
            }
        }
    }
}