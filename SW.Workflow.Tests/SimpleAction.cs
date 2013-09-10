#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Workflow.Testing
// 
// File:	SimpleAction.cs
// Author:	Matthew Shoemate
// Date:	08/27/2012
// 
// http://www.StormWorksSoftware.com
// 

#endregion

using System;

using SW.Workflow.Behavior;

namespace SW.Workflow.Tests
{
    [ Serializable ]
    [ StateEventHandler ( typeof ( OnEnterStateLogicHandler ) ) ]
    [ StateEventHandler ( typeof ( OnLeaveStateLogicHandler ) ) ]
    public class SimpleAction : StateEventProxyBase
    {
        private readonly Action _action;

        public SimpleAction ( Action action )
        {
            _action = action;
        }

        /// <summary>
        ///     Gets the action.
        /// </summary>
        public Action Action
        {
            get { return _action; }
        }

        /// <summary>
        ///     Gets or sets the message.
        /// </summary>
        /// <value> The message. </value>
        public string Message { get; set; }
    }
}