#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Workflow
// 
// File:	IStateMachineConfiguration.cs
// Author:	Matthew Shoemate
// Date:	08/27/2012
// 
// http://www.StormWorksSoftware.com
// 

#endregion

using System;

using SW.Workflow.Engine;
using SW.Workflow.States;

namespace SW.Workflow
{
    /// <summary>
    ///     Represents the configuration of actions (behavior) for starting
    ///     and stopping a <see cref="StateMachine" /> instance.
    /// </summary>
    public interface IStateMachineConfiguration
    {
        /// <summary>
        ///     Called when configuring the behavior that is executed upon starting an <see cref="StateMachine" />
        /// </summary>
        /// <param name="startEvent"> The start event. </param>
        /// <returns>
        ///     the modified <see cref="IStateMachineConfiguration" /> instance.
        /// </returns>
        IStateMachineConfiguration OnStart ( IStateEvent startEvent );

        /// <summary>
        ///     Called when configuring the behavior that is executed upon stopping an <see cref="StateMachine" />
        /// </summary>
        /// <param name="stopEvent"> The stop event. </param>
        /// <returns>
        ///     the modified <see cref="IStateMachineConfiguration" /> instance.
        /// </returns>
        IStateMachineConfiguration OnStop ( IStateEvent stopEvent );

        /// <summary>
        ///     Called when configuring the behavior that is executed upon starting an <see cref="StateMachine" />
        /// </summary>
        /// <param name="startEvent"> The start event. </param>
        /// <returns>
        ///     the modified <see cref="IStateMachineConfiguration" /> instance.
        /// </returns>
        IStateMachineConfiguration OnStart ( Action startEvent );

        /// <summary>
        ///     Called when configuring the behavior that is executed upon starting an <see cref="StateMachine" />
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="startEvent"> The start event. </param>
        /// <returns>
        ///     the modified <see cref="IStateMachineConfiguration" /> instance.
        /// </returns>
        IStateMachineConfiguration OnStart<T> ( T startEvent ) where T : IStateEvent;

        /// <summary>
        ///     Called when configuring the behavior that is executed upon stopping an <see cref="StateMachine" />
        /// </summary>
        /// <param name="stopEvent"> The stop event. </param>
        /// <returns>
        ///     the modified <see cref="IStateMachineConfiguration" /> instance.
        /// </returns>
        IStateMachineConfiguration OnStop ( Action stopEvent );

        /// <summary>
        ///     Called when configuring the behavior that is executed upon stopping an <see cref="StateMachine" />
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="stopEvent"> The stop event. </param>
        /// <returns>
        ///     the modified <see cref="IStateMachineConfiguration" /> instance.
        /// </returns>
        IStateMachineConfiguration OnStop<T> ( T stopEvent ) where T : IStateEvent;

        /// <summary>
        ///     Called when configuring the behavior that is executed upon starting an <see cref="StateMachine" />
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="configureAction"> The configure action. </param>
        /// <returns>
        ///     the modified <see cref="IStateMachineConfiguration" /> instance.
        /// </returns>
        IStateMachineConfiguration OnStart<T> ( Func<T> configureAction ) where T : class, IStateEvent;

        /// <summary>
        ///     Called when configuring the behavior that is executed upon stopping an <see cref="StateMachine" />
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="configureAction"> The configure action. </param>
        /// <returns>
        ///     the modified <see cref="IStateMachineConfiguration" /> instance.
        /// </returns>
        IStateMachineConfiguration OnStop<T> ( Func<T> configureAction ) where T : class, IStateEvent;

        /// <summary>
        ///     Called when configuring the behavior that is executed upon starting an <see cref="StateMachine" />
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="startEvent"> The start event. </param>
        /// <param name="startEventConfigurationAction"> The start event configuration action. </param>
        /// <returns>
        ///     the modified <see cref="IStateMachineConfiguration" /> instance.
        /// </returns>
        IStateMachineConfiguration OnStart<T> ( T startEvent, Action<T> startEventConfigurationAction ) where T : IStateEvent;

        /// <summary>
        ///     Called when configuring the behavior that is executed upon stopping an <see cref="StateMachine" />
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="stopEvent"> The stop event. </param>
        /// <param name="stopEventConfigurationAction"> The stop event configuration action. </param>
        /// <returns>
        ///     the modified <see cref="IStateMachineConfiguration" /> instance.
        /// </returns>
        IStateMachineConfiguration OnStop<T> ( T stopEvent, Action<T> stopEventConfigurationAction ) where T : IStateEvent;
    }
}