#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Workflow
// 
// File:	StateTransitionHandlerAttribute.cs
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
    public class StateTransitionHandlerAttribute : ProviderAttributeBase
    {
        public StateTransitionHandlerAttribute ( string actionProviderType ) : base ( actionProviderType, typeof ( StateTransitionLogicHandler ) )
        {
        }

        public StateTransitionHandlerAttribute ( Type logicHandlerType ) : base ( logicHandlerType, typeof ( StateTransitionLogicHandler ) )
        {
        }
    }

    public static class StateTransitionHandlerAttributeExtensions
    {
        /// <summary>
        ///     Creates the provider.
        /// </summary>
        /// <param name="providerAttribute"> The provider attribute. </param>
        /// <param name="params"> The container. </param>
        /// <returns> </returns>
        public static object Create ( this StateTransitionHandlerAttribute providerAttribute, object[] @params )
        {
            if ( providerAttribute == null )
            {
                throw new ArgumentNullException ( "providerAttribute" );
            }
            if ( providerAttribute.ProviderType == null )
            {
                throw new NullProviderTypeException ( );
            }

            object provider = Activator.CreateInstance ( providerAttribute.ProviderType, @params );

            if ( provider == null )
            {
                throw new ProviderException ( );
            }

            return provider;
        }
    }
}