#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Workflow
// 
// File:	IStateTransitionEvent.cs
// Author:	Matthew Shoemate
// Date:	08/27/2012
// 
// http://www.StormWorksSoftware.com
// 

#endregion

namespace SW.Workflow.Transitions
{
    public interface IStateTransitionEvent
    {
        /// <summary>
        ///     Executes the behavior for this instance.
        /// </summary>
        void Execute ( IStateTransitionEventContext context );
    }

    public interface IStateTransitionEvent<T> : IStateTransitionEvent
    {
        /// <summary>
        ///     Gets or sets the proxy event proxy.
        /// </summary>
        /// <value> The proxy. </value>
        /// <remarks>
        ///     The event proxy is a custom action that is to / will be
        ///     executed when a transition occurs an <see cref="IStateTransition" />
        /// </remarks>
        T Event { get; set; }
    }
}