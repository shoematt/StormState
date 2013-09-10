#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Workflow
// 
// File:	IState.cs
// Author:	Matthew Shoemate
// Date:	08/27/2012
// 
// http://www.StormWorksSoftware.com
// 

#endregion

using System;
using System.Collections.Generic;

using StructureMap;

namespace SW.Workflow.States
{
    /// <summary>
    ///     Represents a finite state and defines a set
    ///     of actions to be executed upon entering and leaving
    ///     the state.
    /// </summary>
    public interface IState : IStateConfiguration
    {
        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        /// <value> The id. </value>
        Guid Id { get; set; }

        /// <summary>
        ///     Gets or sets the enter.
        /// </summary>
        /// <value> The enter. </value>
        List<IStateEvent> Enter { get; set; }

        /// <summary>
        ///     Gets or sets the leave.
        /// </summary>
        /// <value> The leave. </value>
        List<IStateEvent> Leave { get; set; }

        /// <summary>
        ///     Gets or sets the extended.
        /// </summary>
        /// <value> The extended. </value>
        IStateMetaContainer Extended { get; set; }

        /// <summary>
        ///     Initializes the container.
        /// </summary>
        /// <param name="container"> The container. </param>
        void InitializeContainer ( IContainer container );
    }

    /// <summary>
    ///     Represents a configurable, strongly typed state.
    /// </summary>
    /// <typeparam name="T"> </typeparam>
    public interface IState<out T> : IState where T : IState
    {
        /// <summary>
        ///     Configures the specified configure action.
        /// </summary>
        /// <param name="configureAction"> The configure action. </param>
        /// <returns> </returns>
        T Configure ( Action<IStateConfiguration> configureAction );
    }
}