#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Workflow
// 
// File:	OnLeaveStateLogicHandler.cs
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
    ///     Handles action execution event logic upon leaving a state engine state node. When the active state transitions away from
    ///     a new state to which the action is tied.
    /// </summary>
    [ Serializable ]
    public class OnLeaveStateLogicHandler : StateEventLogicHandler
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="OnLeaveStateLogicHandler" /> class.
        /// </summary>
        /// <param name="container"> The container. </param>
        public OnLeaveStateLogicHandler ( IContainer container ) : base ( container )
        {
        }

        /// <summary>
        ///     Gets the state event mode, the default of which is <see cref="StateEventMode.OnEnterState" />
        /// </summary>
        /// <returns> </returns>
        public override StateEventMode GetStateEventMode ( )
        {
            return StateEventMode.OnLeaveState;
        }
    }
}