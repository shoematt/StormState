#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Workflow
// 
// File:	ProviderAttributeBaseExtensions.cs
// Author:	Matthew Shoemate
// Date:	08/27/2012
// 
// http://www.StormWorksSoftware.com
// 

#endregion

using System;

using StructureMap;

namespace SW.Workflow
{
    public static class ProviderAttributeBaseExtensions
    {
        /// <summary>
        ///     Creates the provider.
        /// </summary>
        /// <param name="providerAttribute"> The provider attribute. </param>
        /// <param name="container"> The container. </param>
        /// <returns> </returns>
        public static object Create ( this ProviderAttributeBase providerAttribute, IContainer container )
        {
            if ( providerAttribute == null )
            {
                throw new ArgumentNullException ( "providerAttribute" );
            }
            if ( container == null )
            {
                throw new ArgumentNullException ( "container" );
            }
            if ( providerAttribute.ProviderType == null )
            {
                throw new NullProviderTypeException ( );
            }

            if ( !providerAttribute.ProviderBaseType.IsAssignableFrom ( providerAttribute.ProviderType ) )
            {
                throw new Exception ( "The provider type specified is not assignable to the provider base type specified" );
            }

            container.Configure ( x => x.For ( providerAttribute.ProviderType )
                                        .Use ( providerAttribute.ProviderType ) );

            object provider = container.TryGetInstance ( providerAttribute.ProviderType );

            if ( provider == null )
            {
                throw new ProviderException ( );
            }

            return provider;
        }

        ///// <summary>
        /////   Creates the provider.
        ///// </summary>
        ///// <param name = "providerAttribute">The provider attribute.</param>
        ///// <returns></returns>
        //public static object Create( this ProviderAttributeBase providerAttribute )
        //{
        //    if ( providerAttribute == null ) throw new ArgumentNullException( "providerAttribute" );
        //    if ( providerAttribute.ProviderType == null ) throw new NullProviderTypeException( );

        //    ObjectFactory.Configure( x => x.For( providerAttribute.ProviderType ).Use( providerAttribute.ProviderType ) );

        //    object provider = ObjectFactory.TryGetInstance( providerAttribute.ProviderType );

        //    if ( provider == null ) throw new ProviderException( );

        //    return provider;
        //}
    }
}