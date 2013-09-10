#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Workflow
// 
// File:	IStateTransitionExpression.cs
// Author:	Matthew Shoemate
// Date:	08/27/2012
// 
// http://www.StormWorksSoftware.com
// 

#endregion

using SW.Workflow.Operations;

using StructureMap;

namespace SW.Workflow.Transitions
{
    /// <summary>
    ///     Aggregate interface for fluidly defining expressions
    /// </summary>
    public interface IStateTransitionExpression : IExpressionEvaluator,
                                                  ILogical,
                                                  IOperationEvaluator
    {
        /// <summary>
        ///     Initializes the container.
        /// </summary>
        /// <param name="container"> The container. </param>
        void InitializeContainer ( IContainer container );
    }
}