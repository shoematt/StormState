#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Workflow
// 
// File:	IStateConfiguration.cs
// Author:	Matthew Shoemate
// Date:	08/27/2012
// 
// http://www.StormWorksSoftware.com
// 

#endregion

using System;

namespace SW.Workflow.States
{
    /// <summary>
    ///     Represents the configuration of actions (behavior) for entering
    ///     and leaving an <see cref="IState" /> instance.
    /// </summary>
    public interface IStateConfiguration
    {
        /// <summary>
        ///     Gets or sets the name of the <see cref="IState" />
        /// </summary>
        /// <value>
        ///     The name of the <see cref="IState" /> .
        /// </value>
        string Name { get; set; }

        /// <summary>
        ///     Called when configuring the behavior that is executed upon entering an <see cref="IState" />
        /// </summary>
        /// <param name="stateEvent">
        ///     The <see cref="IStateEvent" /> .
        /// </param>
        /// <returns>
        ///     the modified <see cref="IStateConfiguration" /> instance.
        /// </returns>
        IStateConfiguration OnEnter ( IStateEvent stateEvent );

        /// <summary>
        ///     Called when configuring the behavior that is executed upon leaving an <see cref="IState" />
        /// </summary>
        /// <param name="stateEvent">
        ///     The <see cref="IStateEvent" /> .
        /// </param>
        /// <returns>
        ///     the modified <see cref="IStateConfiguration" /> instance.
        /// </returns>
        IStateConfiguration OnLeave ( IStateEvent stateEvent );

        /// <summary>
        ///     Called when configuring the behavior that is executed upon entering an <see cref="IState" />
        /// </summary>
        /// <typeparam name="T"> Strongly typed result returned by the assigned function. </typeparam>
        /// <param name="configureAction"> The configure action. </param>
        /// <returns>
        ///     the modified <see cref="IStateConfiguration" /> instance.
        /// </returns>
        IStateConfiguration OnEnter<T> ( Func<T> configureAction ) where T : class, IStateEvent;

        /// <summary>
        ///     Called when configuring the behavior that is executed upon leaving an <see cref="IState" />
        /// </summary>
        /// <typeparam name="T"> Strongly typed result returned by the assigned function. </typeparam>
        /// <param name="configureAction"> The configure action. </param>
        /// <returns>
        ///     the modified <see cref="IStateConfiguration" /> instance.
        /// </returns>
        IStateConfiguration OnLeave<T> ( Func<T> configureAction ) where T : class, IStateEvent;

        /// <summary>
        ///     Called when configuring the behavior that is executed upon entering an <see cref="IState" />
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="enterEvent"> The enter event. </param>
        /// <returns>
        ///     the modified <see cref="IStateConfiguration" /> instance.
        /// </returns>
        IStateConfiguration OnEnter<T> ( T enterEvent ) where T : IStateEvent;

        /// <summary>
        ///     Called when configuring the behavior that is executed upon leaving an <see cref="IState" />
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="leaveEvent"> The leave event. </param>
        /// <returns>
        ///     the modified <see cref="IStateConfiguration" /> instance.
        /// </returns>
        IStateConfiguration OnLeave<T> ( T leaveEvent ) where T : IStateEvent;

        /// <summary>
        ///     Called when configuring the behavior that is executed upon entering an <see cref="IState" />
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="enterEvent"> The enter event. </param>
        /// <param name="enterEventConfigurationAction"> The enter event configuration action. </param>
        /// <returns>
        ///     the modified <see cref="IStateConfiguration" /> instance.
        /// </returns>
        IStateConfiguration OnEnter<T> ( T enterEvent, Action<T> enterEventConfigurationAction ) where T : IStateEvent;

        /// <summary>
        ///     Called when configuring the behavior that is executed upon leaving an <see cref="IState" />
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="leaveEvent"> The leave event. </param>
        /// <param name="leaveEventConfigurationAction"> The leave event configuration action. </param>
        /// <returns>
        ///     the modified <see cref="IStateConfiguration" /> instance.
        /// </returns>
        IStateConfiguration OnLeave<T> ( T leaveEvent, Action<T> leaveEventConfigurationAction ) where T : IStateEvent;
    }
}