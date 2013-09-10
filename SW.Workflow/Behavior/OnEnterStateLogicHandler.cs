#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Workflow
// 
// File:	OnEnterStateLogicHandler.cs
// Author:	Matthew Shoemate
// Date:	08/27/2012
// 
// http://www.StormWorksSoftware.com
// 

#endregion

using System;

using SW.Workflow.Logic;

using StructureMap;

namespace SW.Workflow.Behavior
{
    /// <summary>
    ///     Handles action execution event logic upon entering a state engine state node.  When the active state transitions to a
    ///     new state to which the action is tied.
    /// </summary>
    [ Serializable ]
    public class OnEnterStateLogicHandler : StateEventLogicHandler
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="OnEnterStateLogicHandler" /> class.
        /// </summary>
        /// <param name="container"> The container. </param>
        public OnEnterStateLogicHandler ( IContainer container ) : base ( container )
        {
        }

        public override StateEventMode GetStateEventMode ( )
        {
            return StateEventMode.OnEnterState;
        }
    }
}