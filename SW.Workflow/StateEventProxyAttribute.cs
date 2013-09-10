#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Workflow
// 
// File:	StateEventProxyAttribute.cs
// Author:	Matthew Shoemate
// Date:	08/27/2012
// 
// http://www.StormWorksSoftware.com
// 

#endregion

using System;

using SW.Workflow.States;

namespace SW.Workflow
{
    /// <summary>
    ///     Attribute that defines an action that can be performed on an object, via a
    ///     type action handler.  (Similar to a verb)
    /// </summary>
    [ AttributeUsage ( AttributeTargets.Class, AllowMultiple = false, Inherited = true ) ]
    public class StateEventProxyAttribute : ProviderAttributeBase
    {
        public StateEventProxyAttribute ( string actionProviderType ) : base ( actionProviderType, typeof ( IStateEvent ) )
        {
        }

        public StateEventProxyAttribute ( Type proxyType ) : base ( proxyType, typeof ( IStateEvent ) )
        {
        }
    }
}