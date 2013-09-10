#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Workflow
// 
// File:	MetaExtensions.cs
// Author:	Matthew Shoemate
// Date:	08/27/2012
// 
// http://www.StormWorksSoftware.com
// 

#endregion

using System;

namespace SW.Workflow.States
{
    public static class MetaExtensions
    {
        /// <summary>
        ///     Gets the value.
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="container"> The container. </param>
        /// <param name="property"> The property. </param>
        /// <returns> </returns>
        public static T GetValue<T> ( this IStateMetaContainer container, IStateMetaProperty property )
        {
            if ( container == null )
            {
                throw new ArgumentNullException ( "container" );
            }
            if ( property == null )
            {
                throw new ArgumentNullException ( "property" );
            }
            return ( T ) container.GetValue ( property );
        }

        /// <summary>
        ///     Sets the value.
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="container"> The container. </param>
        /// <param name="property"> The property. </param>
        /// <param name="value"> The value. </param>
        public static void SetValue<T> ( this IStateMetaContainer container, IStateMetaProperty property, T value )
        {
            if ( container == null )
            {
                throw new ArgumentNullException ( "container" );
            }
            if ( property == null )
            {
                throw new ArgumentNullException ( "property" );
            }
            container.SetValue ( property, value );
        }
    }
}