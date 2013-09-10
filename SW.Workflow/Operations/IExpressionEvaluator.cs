#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Workflow
// 
// File:	IExpressionEvaluator.cs
// Author:	Matthew Shoemate
// Date:	08/27/2012
// 
// http://www.StormWorksSoftware.com
// 

#endregion

using System;

using SW.Expressions.Expressions;
using SW.Workflow.Transitions;

namespace SW.Workflow.Operations
{
    public interface IExpressionEvaluator
    {
        /// <summary>
        ///     Gets or sets the expression.
        /// </summary>
        /// <value> The expression. </value>
        EditableExpression Expression { get; set; }

        /// <summary>
        ///     Evaluates the specified expression.
        /// </summary>
        /// <param name="expression"> The expression. </param>
        ILogical Evaluate ( Func<bool> expression );

        /// <summary>
        ///     Evaluates the specified expression.
        /// </summary>
        /// <param name="expression"> The expression. </param>
        ILogical Evaluate ( Action<IStateTransitionExpression> expression );
    }
}