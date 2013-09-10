#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Workflow
// 
// File:	ILogical.cs
// Author:	Matthew Shoemate
// Date:	08/27/2012
// 
// http://www.StormWorksSoftware.com
// 

#endregion

using System;

namespace SW.Workflow.Operations
{
    public interface ILogical
    {
        /// <summary>
        ///     Ands the specified action.
        /// </summary>
        /// <param name="action"> The action. </param>
        /// <returns> </returns>
        ILogical And ( Func<bool> action );

        /// <summary>
        ///     Ors the specified action.
        /// </summary>
        /// <param name="action"> The action. </param>
        /// <returns> </returns>
        ILogical Or ( Func<bool> action );

        /// <summary>
        ///     Nots the specified action.
        /// </summary>
        /// <param name="action"> The action. </param>
        /// <returns> </returns>
        ILogical Not ( Func<bool> action );

        /// <summary>
        ///     Customs the specified action.
        /// </summary>
        /// <param name="action"> The action. </param>
        /// <returns> </returns>
        ILogical Custom ( Func<bool> action );

        /// <summary>
        ///     Ands the specified action.
        /// </summary>
        /// <param name="action"> The action. </param>
        /// <returns> </returns>
        ILogical And ( ILogical action );

        /// <summary>
        ///     Ors the specified action.
        /// </summary>
        /// <param name="action"> The action. </param>
        /// <returns> </returns>
        ILogical Or ( ILogical action );

        /// <summary>
        ///     Nots the specified action.
        /// </summary>
        /// <param name="action"> The action. </param>
        /// <returns> </returns>
        ILogical Not ( ILogical action );

        /// <summary>
        ///     Customs the specified action.
        /// </summary>
        /// <param name="action"> The action. </param>
        /// <returns> </returns>
        ILogical Custom ( ILogical action );

        /// <summary>
        ///     Ands the specified action.
        /// </summary>
        /// <param name="action"> The action. </param>
        /// <returns> </returns>
        ILogical And ( Action<IExpressionEvaluator> action );

        /// <summary>
        ///     Ors the specified action.
        /// </summary>
        /// <param name="action"> The action. </param>
        /// <returns> </returns>
        ILogical Or ( Action<IExpressionEvaluator> action );

        /// <summary>
        ///     Nots the specified action.
        /// </summary>
        /// <param name="action"> The action. </param>
        /// <returns> </returns>
        ILogical Not ( Action<IExpressionEvaluator> action );

        /// <summary>
        ///     Customs the specified action.
        /// </summary>
        /// <param name="action"> The action. </param>
        /// <returns> </returns>
        ILogical Custom ( Action<IExpressionEvaluator> action );
    }
}