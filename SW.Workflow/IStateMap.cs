#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Workflow
// 
// File:	IStateMap.cs
// Author:	Matthew Shoemate
// Date:	08/27/2012
// 
// http://www.StormWorksSoftware.com
// 

#endregion

using System.Collections.Generic;
using System.Xml.Serialization;

using SW.Workflow.States;
using SW.Workflow.Transitions;

using StructureMap;

namespace SW.Workflow
{
    public interface IStateMap
    {
        /// <summary>
        ///     Gets the container.
        /// </summary>
        /// <value> The container. </value>
        [ XmlIgnore ]
        IContainer Container { get; }

        /// <summary>
        ///     Gets or sets the states.
        /// </summary>
        /// <value> The states. </value>
        List<IState> States { get; }

        /// <summary>
        ///     Gets or sets the transitions.
        /// </summary>
        /// <value> The transitions. </value>
        List<IStateTransition> Transitions { get; }

        /// <summary>
        ///     Gets or sets the components.
        /// </summary>
        /// <value> The components. </value>
        List<IStateComponent> Components { get; }
    }
}