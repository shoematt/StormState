#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Workflow
// 
// File:	IStateTransitionEventConfiguration.cs
// Author:	Matthew Shoemate
// Date:	08/27/2012
// 
// http://www.StormWorksSoftware.com
// 

#endregion

using System;

namespace SW.Workflow.Transitions
{
    public interface IStateTransitionEventConfiguration
    {
        /// <summary>
        ///     Called when configuring the behavior that is executed prior to the current <see cref="IStateTransitionEvent" />
        /// </summary>
        /// <param name="preExecuteEvent"> The pre execute event. </param>
        /// <returns>
        ///     the modified <see cref="IStateTransitionEventConfiguration" /> instance.
        /// </returns>
        IStateTransitionEventConfiguration OnBeforeExecuted ( IStateTransitionEvent preExecuteEvent );

        /// <summary>
        ///     Called when configuring the behavior that is executed after to the current <see cref="IStateTransitionEvent" />
        /// </summary>
        /// <param name="postExecuteEvent"> The post execute event. </param>
        /// <returns>
        ///     the modified <see cref="IStateTransitionEventConfiguration" /> instance.
        /// </returns>
        IStateTransitionEventConfiguration OnAfterExecuted ( IStateTransitionEvent postExecuteEvent );

        /// <summary>
        ///     Called when configuring the behavior that is executed prior to the current <see cref="IStateTransitionEvent" />
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="preExecuteEvent"> The pre execute event. </param>
        /// <returns>
        ///     the modified <see cref="IStateTransitionEventConfiguration" /> instance.
        /// </returns>
        IStateTransitionEventConfiguration OnBeforeExecuted<T> ( T preExecuteEvent ) where T : IStateTransitionEvent;

        /// <summary>
        ///     Called when configuring the behavior that is executed after to the current <see cref="IStateTransitionEvent" />
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="postExecuteEvent"> The post execute event. </param>
        /// <returns>
        ///     the modified <see cref="IStateTransitionEventConfiguration" /> instance.
        /// </returns>
        IStateTransitionEventConfiguration OnAfterExecuted<T> ( T postExecuteEvent ) where T : IStateTransitionEvent;

        /// <summary>
        ///     Called when configuring the behavior that is executed prior to the current <see cref="IStateTransitionEvent" />
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="configureAction"> The configure action. </param>
        /// <returns>
        ///     the modified <see cref="IStateTransitionEventConfiguration" /> instance.
        /// </returns>
        IStateTransitionEventConfiguration OnBeforeExecuted<T> ( Func<T> configureAction ) where T : class, IStateTransitionEvent;

        /// <summary>
        ///     Called when configuring the behavior that is executed after to the current <see cref="IStateTransitionEvent" />
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="configureAction"> The configure action. </param>
        /// <returns>
        ///     the modified <see cref="IStateTransitionEventConfiguration" /> instance.
        /// </returns>
        IStateTransitionEventConfiguration OnAfterExecuted<T> ( Func<T> configureAction ) where T : class, IStateTransitionEvent;

        /// <summary>
        ///     Called when configuring the behavior that is executed prior to the current <see cref="IStateTransitionEvent" />
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="preExecuteEvent"> The pre execute event. </param>
        /// <param name="preExecuteEventConfigurationAction"> The pre execute event configuration action. </param>
        /// <returns>
        ///     the modified <see cref="IStateTransitionEventConfiguration" /> instance.
        /// </returns>
        IStateTransitionEventConfiguration OnBeforeExecuted<T> ( T preExecuteEvent, Action<T> preExecuteEventConfigurationAction ) where T : IStateTransitionEvent;

        /// <summary>
        ///     Called when configuring the behavior that is executed after to the current <see cref="IStateTransitionEvent" />
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="postExecuteEvent"> The post execute event. </param>
        /// <param name="postExecuteEventConfigurationAction"> The post execute event configuration action. </param>
        /// <returns>
        ///     the modified <see cref="IStateTransitionEventConfiguration" /> instance.
        /// </returns>
        IStateTransitionEventConfiguration OnAfterExecuted<T> ( T postExecuteEvent, Action<T> postExecuteEventConfigurationAction ) where T : IStateTransitionEvent;
    }
}