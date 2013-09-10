#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Workflow
// 
// File:	StateTransitionProxyAttribute.cs
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
    ///     Attribute that defines an transition handler that provides transition logic for a shape type, via a
    ///     type action handler.  (Similar to a verb)
    /// </summary>
    [ AttributeUsage ( AttributeTargets.Class, AllowMultiple = false, Inherited = true ) ]
    public class StateTransitionProxyAttribute : ProviderAttributeBase
    {
        public StateTransitionProxyAttribute ( string actionProviderType ) : base ( actionProviderType, typeof ( StateTransitionLogicHandler ) )
        {
        }

        public StateTransitionProxyAttribute ( Type logicHandlerType ) : base ( logicHandlerType, typeof ( StateTransitionLogicHandler ) )
        {
        }
    }
}