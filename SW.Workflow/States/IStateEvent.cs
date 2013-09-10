#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Workflow
// 
// File:	IStateEvent.cs
// Author:	Matthew Shoemate
// Date:	08/27/2012
// 
// http://www.StormWorksSoftware.com
// 

#endregion

using SW.Workflow.Engine;

using StructureMap;

namespace SW.Workflow.States
{
    public interface IStateEvent
    {
        /// <summary>
        ///     Initializes the container.
        /// </summary>
        /// <param name="container"> The container. </param>
        void InitializeContainer ( IContainer container );

        /// <summary>
        ///     Executes the behavior for this instance.
        /// </summary>
        /// <param name="container"> </param>
        /// <param name="stateMap"> </param>
        void Execute ( IContainer container, StateMap stateMap );
    }

    public interface IStateEvent<T> : IStateEvent
    {
        /// <summary>
        ///     Gets or sets the proxy event proxy.
        /// </summary>
        /// <value> The proxy. </value>
        /// <remarks>
        ///     The event proxy is a custom action that is to / will be
        ///     executed by entering or leaving an <see cref="IState" />
        /// </remarks>
        T Event { get; set; }
    }
}