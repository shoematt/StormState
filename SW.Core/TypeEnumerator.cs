#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Core
// 
// File:	TypeEnumerator.cs
// Author:	Matthew Shoemate
// Date:	08/27/2012
// 
// http://www.StormWorksSoftware.com
// 

#endregion

using System;
using System.Collections.Generic;
using System.Linq;

using StructureMap;

namespace SW.Core
{
    public static class TypeEnumeratorExtensions
    {
        /// <summary>
        ///     Gets the short type name for.
        /// </summary>
        /// <param name="type"> The type. </param>
        /// <returns> </returns>
        public static string GetShortTypeName ( this Type type )
        {
            return TypeEnumerator.GetShortTypeNameFor ( type );
        }
    }

    public class TypeEnumerator
    {
        /// <summary>
        ///     Gets or sets the container.
        /// </summary>
        /// <value> The container. </value>
        public static IContainer Container { get; set; }

        /// <summary>
        ///     Gets all types with attribute.
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <returns> </returns>
        public static IList<Type> GetAllTypesWithAttribute<T> ( ) where T : Attribute
        {
            ObjectFactory.Configure ( x => x.For<List<Type>> ( )
                                            .Singleton ( )
                                            .Use ( new List<Type> ( ) )
                                            .Name = typeof ( T ).AssemblyQualifiedName );

            List<Type> plugins = ObjectFactory.TryGetInstance<List<Type>> ( typeof ( T ).AssemblyQualifiedName );

            if ( plugins.Count == 0 )
            {
                ObjectFactory.Configure ( x => x.Scan ( y =>
                                                        {
                                                            y.AssembliesFromPath ( Environment.CurrentDirectory, b => b != null && !string.IsNullOrEmpty ( b.FullName ) && b.FullName.Contains ( "Orca." ) );
                                                            y.Convention<HasAttributeConvention<T>> ( );
                                                        } ) );
            }

            return ObjectFactory.TryGetInstance<List<Type>> ( typeof ( T ).AssemblyQualifiedName );
        }

        /// <summary>
        ///     Gets the short type name for.
        /// </summary>
        /// <param name="type"> The type. </param>
        /// <returns> </returns>
        public static string GetShortTypeNameFor ( Type type )
        {
            return type.AssemblyQualifiedName.Split ( new[] {','} ) [0] + "," + type.AssemblyQualifiedName.Split ( new[] {','} ) [1];
        }

        /// <summary>
        ///     Gets the short type name for.
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <returns> </returns>
        public static string GetShortTypeNameFor<T> ( )
        {
            return GetShortTypeNameFor ( typeof ( T ) );
        }

        /// <summary>
        ///     Gets the short type names for.
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <returns> </returns>
        public static IList<string> GetShortTypeNamesFor<T> ( )
        {
            return ( from x in GetPluginTypesFor<T> ( ) select x.AssemblyQualifiedName.Split ( new[] {','} ) [0] + "," + x.AssemblyQualifiedName.Split ( new[] {','} ) [1] ).ToList ( );
        }

        /// <summary>
        ///     Gets the qualified names for.
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <returns> </returns>
        public static IList<string> GetQualifiedNamesFor<T> ( )
        {
            return ( from x in GetPluginTypesFor<T> ( ) select x.AssemblyQualifiedName ).ToList ( );
        }

        /// <summary>
        ///     Gets the plugin types for.
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="baseType"> Type of the base. </param>
        /// <returns> </returns>
        public static IList<Type> GetPluginTypesFor<T> ( Type baseType )
        {
            return ( from x in GetPluginTypesFor<T> ( ) where baseType.IsAssignableFrom ( x ) select x ).ToList ( );
        }

        /// <summary>
        ///     Toes the type of the generic.
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="obj"> The obj. </param>
        /// <returns> </returns>
        public T ToGenericType<T> ( object obj )
        {
            return ( T ) obj;
        }

        /// <summary>
        ///     Gets the plugin types for.
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="name"> The name. </param>
        /// <returns> </returns>
        public static Type GetPluginTypesFor<T> ( string name )
        {
            return ( from x in GetPluginTypesFor<T> ( ) where x.Name == name select x ).FirstOrDefault ( );
        }

        /// <summary>
        ///     Gets all types of.
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <returns> </returns>
        public static IList<Type> GetPluginTypesFor<T> ( )
        {
            ObjectFactory.Configure ( x => x.For<List<Type>> ( )
                                            .Singleton ( )
                                            .Use ( new List<Type> ( ) )
                                            .Name = typeof ( T ).AssemblyQualifiedName );

            List<Type> plugins = ObjectFactory.TryGetInstance<List<Type>> ( typeof ( T ).AssemblyQualifiedName );

            if ( plugins.Count == 0 )
            {
                ObjectFactory.Configure ( x => x.Scan ( y =>
                                                        {
                                                            y.AssembliesFromPath ( Environment.CurrentDirectory, b => b != null && !string.IsNullOrEmpty ( b.FullName ) && b.FullName.Contains ( "Orca." ) );

                                                            y.Convention<ImplementsConvention<T>> ( );
                                                        } ) );
            }

            return ObjectFactory.TryGetInstance<List<Type>> ( typeof ( T ).AssemblyQualifiedName );
        }

        /// <summary>
        ///     Gets the qualified names for.
        /// </summary>
        /// <param name="type"> The type. </param>
        /// <returns> </returns>
        public static string GetQualifiedNamesFor ( Type type )
        {
            return type.AssemblyQualifiedName;
        }
    }
}