#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Workflow
// 
// File:	IStateEventContext.cs
// Author:	Matthew Shoemate
// Date:	08/27/2012
// 
// http://www.StormWorksSoftware.com
// 

#endregion

using System.Xml.Serialization;

using StructureMap;

namespace SW.Workflow.Logic
{
    public interface IStateEventContext
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
        IStateEventProxy EventProxy { get; }
    }
}