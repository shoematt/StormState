#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Workflow
// 
// File:	IStateTransitionProxy.cs
// Author:	Matthew Shoemate
// Date:	08/27/2012
// 
// http://www.StormWorksSoftware.com
// 

#endregion

using SW.Workflow.Transitions;

namespace SW.Workflow.Logic
{
    public interface IStateTransitionProxy
    {
        /// <summary>
        ///     Gets or sets the transition.
        /// </summary>
        /// <value> The transition. </value>
        IStateTransition Transition { get; set; }

        /// <summary>
        ///     Executes the specified context.
        /// </summary>
        /// <param name="context"> The context. </param>
        void Execute ( IStateTransitionContext context );
    }
}