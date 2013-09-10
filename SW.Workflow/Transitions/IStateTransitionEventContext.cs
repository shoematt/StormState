#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Workflow
// 
// File:	IStateTransitionEventContext.cs
// Author:	Matthew Shoemate
// Date:	08/27/2012
// 
// http://www.StormWorksSoftware.com
// 

#endregion

using System.Xml.Serialization;

using StructureMap;

namespace SW.Workflow.Transitions
{
    public interface IStateTransitionEventContext
    {
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
        IStateTransitionEvent Event { get; }

        /// <summary>
        ///     Initializes the container.
        /// </summary>
        /// <param name="container"> The container. </param>
        void InitializeContainer ( IContainer container );
    }
}