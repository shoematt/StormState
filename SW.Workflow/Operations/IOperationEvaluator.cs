#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Workflow
// 
// File:	IOperationEvaluator.cs
// Author:	Matthew Shoemate
// Date:	08/27/2012
// 
// http://www.StormWorksSoftware.com
// 

#endregion

namespace SW.Workflow.Operations
{
    public interface IOperationEvaluator
    {
        /// <summary>
        ///     Evaluates this instance.
        /// </summary>
        /// <returns> </returns>
        bool Evaluate ( );
    }
}