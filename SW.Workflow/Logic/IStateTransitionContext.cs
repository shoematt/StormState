#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Workflow
// 
// File:	IStateTransitionContext.cs
// Author:	Matthew Shoemate
// Date:	08/27/2012
// 
// http://www.StormWorksSoftware.com
// 

#endregion

using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using SW.Workflow.States;
using SW.Workflow.Transitions;

using StructureMap;

namespace SW.Workflow.Logic
{
    public interface IStateTransitionContext
    {
        /// <summary>
        ///     Gets a value indicating whether this <see cref="IStateTransitionContext" /> is debug.
        /// </summary>
        /// <value>
        ///     <c>true</c> if debug; otherwise, <c>false</c> .
        /// </value>
        bool Debug { get; }

        /// <summary>
        ///     Gets the state map.
        /// </summary>
        IStateMap StateMap { get; }

        /// <summary>
        ///     Gets the container.
        /// </summary>
        [ XmlIgnore ]
        IContainer Container { get; }

        /// <summary>
        ///     Gets the state event.
        /// </summary>
        Action<List<IState>, IState, IStateTransition> TransitionAction { get; }

        /// <summary>
        ///     Gets or sets the transition proxy.
        /// </summary>
        /// <value> The transition proxy. </value>
        IStateTransitionProxy TransitionProxy { get; set; }
    }
}