#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Workflow
// 
// File:	IStateEventConfiguration.cs
// Author:	Matthew Shoemate
// Date:	08/27/2012
// 
// http://www.StormWorksSoftware.com
// 

#endregion

using System;

namespace SW.Workflow.States
{
    public interface IStateEventConfiguration
    {
        /// <summary>
        ///     Called when configuring the behavior that is executed prior to the current <see cref="IStateEvent" />
        /// </summary>
        /// <param name="preExecuteEvent"> The pre execute event. </param>
        /// <returns>
        ///     the modified <see cref="IStateEventConfiguration" /> instance.
        /// </returns>
        IStateEventConfiguration OnBeforeExecuted ( IStateEvent preExecuteEvent );

        /// <summary>
        ///     Called when configuring the behavior that is executed after to the current <see cref="IStateEvent" />
        /// </summary>
        /// <param name="postExecuteEvent"> The post execute event. </param>
        /// <returns>
        ///     the modified <see cref="IStateEventConfiguration" /> instance.
        /// </returns>
        IStateEventConfiguration OnAfterExecuted ( IStateEvent postExecuteEvent );

        /// <summary>
        ///     Called when configuring the behavior that is executed prior to the current <see cref="IStateEvent" />
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="preExecuteEvent"> The pre execute event. </param>
        /// <returns>
        ///     the modified <see cref="IStateEventConfiguration" /> instance.
        /// </returns>
        IStateEventConfiguration OnBeforeExecuted<T> ( T preExecuteEvent ) where T : IStateEvent;

        /// <summary>
        ///     Called when configuring the behavior that is executed after to the current <see cref="IStateEvent" />
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="postExecuteEvent"> The post execute event. </param>
        /// <returns>
        ///     the modified <see cref="IStateEventConfiguration" /> instance.
        /// </returns>
        IStateEventConfiguration OnAfterExecuted<T> ( T postExecuteEvent ) where T : IStateEvent;

        /// <summary>
        ///     Called when configuring the behavior that is executed prior to the current <see cref="IStateEvent" />
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="configureAction"> The configure action. </param>
        /// <returns>
        ///     the modified <see cref="IStateEventConfiguration" /> instance.
        /// </returns>
        IStateEventConfiguration OnBeforeExecuted<T> ( Func<T> configureAction ) where T : class, IStateEvent;

        /// <summary>
        ///     Called when configuring the behavior that is executed after to the current <see cref="IStateEvent" />
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="configureAction"> The configure action. </param>
        /// <returns>
        ///     the modified <see cref="IStateEventConfiguration" /> instance.
        /// </returns>
        IStateEventConfiguration OnAfterExecuted<T> ( Func<T> configureAction ) where T : class, IStateEvent;

        /// <summary>
        ///     Called when configuring the behavior that is executed prior to the current <see cref="IStateEvent" />
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="preExecuteEvent"> The pre execute event. </param>
        /// <param name="preExecuteEventConfigurationAction"> The pre execute event configuration action. </param>
        /// <returns>
        ///     the modified <see cref="IStateEventConfiguration" /> instance.
        /// </returns>
        IStateEventConfiguration OnBeforeExecuted<T> ( T preExecuteEvent, Action<T> preExecuteEventConfigurationAction ) where T : IStateEvent;

        /// <summary>
        ///     Called when configuring the behavior that is executed after to the current <see cref="IStateEvent" />
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="postExecuteEvent"> The post execute event. </param>
        /// <param name="postExecuteEventConfigurationAction"> The post execute event configuration action. </param>
        /// <returns>
        ///     the modified <see cref="IStateEventConfiguration" /> instance.
        /// </returns>
        IStateEventConfiguration OnAfterExecuted<T> ( T postExecuteEvent, Action<T> postExecuteEventConfigurationAction ) where T : IStateEvent;
    }
}