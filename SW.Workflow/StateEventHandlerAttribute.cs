#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Workflow
// 
// File:	StateEventHandlerAttribute.cs
// Author:	Matthew Shoemate
// Date:	08/27/2012
// 
// http://www.StormWorksSoftware.com
// 

#endregion

using System;

using SW.Workflow.Logic;

namespace SW.Workflow
{
    /// <summary>
    ///     Attribute that defines an action that can be performed on an object, via a
    ///     type action handler.  (Similar to a verb)
    /// </summary>
    [ AttributeUsage ( AttributeTargets.Class, AllowMultiple = true, Inherited = true ) ]
    public class StateEventHandlerAttribute : ProviderAttributeBase
    {
        public StateEventHandlerAttribute ( string actionProviderType ) : base ( actionProviderType, typeof ( StateEventLogicHandler ) )
        {
        }

        public StateEventHandlerAttribute ( Type logicHandlerType ) : base ( logicHandlerType, typeof ( StateEventLogicHandler ) )
        {
        }
    }
}