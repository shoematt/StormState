#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Workflow
// 
// File:	IStateTransition.cs
// Author:	Matthew Shoemate
// Date:	08/27/2012
// 
// http://www.StormWorksSoftware.com
// 

#endregion

using System;
using System.Collections.Generic;

using SW.Workflow.States;

using StructureMap;

namespace SW.Workflow.Transitions
{
    public interface IStateTransition<T> : IStateTransition //where T : IStateTransition
    {
    }

    public interface IStateTransition
    {
        /// <summary>
        ///     Gets or sets to.
        /// </summary>
        /// <value> To. </value>
        List<IState> To { get; set; }

        /// <summary>
        ///     Gets or sets from.
        /// </summary>
        /// <value> From. </value>
        IState From { get; set; }

        /// <summary>
        ///     Executes the behavior for this instance.
        /// </summary>
        void DoTransition ( Action<List<IState>, IState, IStateTransition> transitionAction );

        /// <summary>
        ///     Resets this instance.
        /// </summary>
        void Reset ( );

        /// <summary>
        ///     Initializes the container.
        /// </summary>
        /// <param name="container"> The container. </param>
        void InitializeContainer ( IContainer container );
    }
}