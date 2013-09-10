#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Core
// 
// File:	TypeExtensions.cs
// Author:	Matthew Shoemate
// Date:	08/27/2012
// 
// http://www.StormWorksSoftware.com
// 

#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace SW.Core.Extensions
{
    public static class TypeExtensions
    {
        private static readonly Func<MethodInfo, IEnumerable<Type>> ParameterTypeProjection = method => method.GetParameters ( )
                                                                                                              .Select ( p => p.ParameterType.GetGenericTypeDefinition ( ) );

        /// <summary>
        ///     Gets the generic method.
        /// </summary>
        /// <param name="type"> The type. </param>
        /// <param name="name"> The name. </param>
        /// <param name="parameterTypes"> The parameter types. </param>
        /// <returns> </returns>
        public static MethodInfo GetGenericMethod ( this Type type, string name, params Type[] parameterTypes )
        {
            return ( from method in type.GetMethods ( ) where method.Name == name where parameterTypes.SequenceEqual ( ParameterTypeProjection ( method ) ) select method ).SingleOrDefault ( );
        }


        public static object DefaultValue ( this Type type )
        {
            if ( type == typeof ( int ) )
            {
                return 0;
            }
            if ( type == typeof ( double ) )
            {
                return 0.0;
            }
            if ( type == typeof ( bool ) )
            {
                return false;
            }

            if ( type == typeof ( string ) )
            {
                return string.Empty;
            }

            if ( type == typeof ( float ) )
            {
                return 0.0;
            }

            if ( type == typeof ( decimal ) )
            {
                return 0.0m;
            }

            return null;
        }

        /// <summary>
        ///     Tries the catch.
        /// </summary>
        /// <param name="obj"> The obj. </param>
        /// <param name="action"> The action. </param>
        public static void TryCatch ( this object obj, Action action )
        {
            try
            {
                action ( );
            }
            catch ( Exception ex )
            {
                if ( Logger.Default.IsErrorEnabled )
                {
                    Logger.Default.Error ( ex );
                }
            }
        }

        /// <summary>
        ///     Tries the catch.
        /// </summary>
        /// <param name="obj"> The obj. </param>
        /// <param name="action"> The action. </param>
        public static void TryCatch<T> ( this object obj, Action<T> action )
        {
            try
            {
                action ( ( T ) obj );
            }
            catch ( Exception ex )
            {
                if ( Logger.Default.IsErrorEnabled )
                {
                    Logger.Default.Error ( ex );
                }
            }
        }

        public static string GetShortName ( this Type type )
        {
            return string.Format ( "{0}, {1}", type.FullName, type.Assembly.GetName ( )
                                                                  .Name );
        }

        /// <summary>
        ///     Determines whether [is] [the specified target].
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="target"> The target. </param>
        /// <returns>
        ///     <c>true</c> if [is] [the specified target]; otherwise, <c>false</c> .
        /// </returns>
        public static bool Is<T> ( this object target )
        {
            return target is T;
        }

        /// <summary>
        ///     Determines whether [is generic enumerable] [the specified type].
        /// </summary>
        /// <param name="type"> The type. </param>
        /// <returns>
        ///     <c>true</c> if [is generic enumerable] [the specified type]; otherwise, <c>false</c> .
        /// </returns>
        public static bool IsGenericEnumerable ( this Type type )
        {
            var genericArgs = type.GetGenericArguments ( );
            return genericArgs.Length == 1 && typeof ( IEnumerable<> ).MakeGenericType ( genericArgs )
                                                                      .IsAssignableFrom ( type );
        }

        /// <summary>
        ///     Calls the on.
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="target"> The target. </param>
        /// <param name="action"> The action. </param>
        public static void CallOn<T> ( this object target, Action<T> action ) where T : class
        {
            var subject = target as T;
            if ( subject != null )
            {
                try
                {
                    action ( subject );
                }
                catch ( InvalidOperationException e )
                {
                    if ( !e.ToString ( )
                           .Contains ( "The calling thread" ) )
                    {
                        throw;
                    }
                }
            }
        }

        /// <summary>
        ///     Merges as list.
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="enumerableSet"> The enumerable set. </param>
        /// <returns> </returns>
        public static List<T> MergeAsList<T> ( this IEnumerable<IEnumerable<T>> enumerableSet )
        {
            List<T> mergedSet = new List<T> ( );

            enumerableSet.CallOnEach ( mergedSet.AddRange );

            return mergedSet;
        }

        /// <summary>
        ///     Calls the on each.
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="enumerable"> The enumerable. </param>
        /// <param name="action"> The action. </param>
        public static void CallOnEach<T> ( this IEnumerable enumerable, Action<T> action ) where T : class
        {
            foreach ( object o in enumerable.Cast<object> ( )
                                            .ToArray ( ) )
            {
                o.CallOn ( action );
            }
        }

        /// <summary>
        ///     Calls the on each.
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="enumerable"> The enumerable. </param>
        /// <param name="action"> The action. </param>
        public static void CallOnEach<T> ( this IEnumerable<T> enumerable, Action<T> action ) where T : class
        {
            foreach ( object o in enumerable.ToArray ( ) )
            {
                o.CallOn ( action );
            }
        }


        public static IEnumerable<PropertyDescriptor> GetWriteablePropertyDescriptors ( this Type objectType, object value )
        {
            var test = GetPropertyDescriptors ( objectType, value );

            return test.Where ( p => !p.IsReadOnly );
        }

        public static IEnumerable<PropertyDescriptor> GetPropertyDescriptors ( this Type objectType, object value )
        {
            TypeDescriptionProvider typeProvider = TypeDescriptor.GetProvider ( value ?? objectType );

            if ( value != null )
            {
                var typeDescriptor = typeProvider.GetTypeDescriptor ( objectType, value );
                if ( typeDescriptor != null )
                {
                    return typeDescriptor.GetProperties ( )
                                         .Cast<PropertyDescriptor> ( );
                }
            }

            var customTypeDescriptor = typeProvider.GetTypeDescriptor ( objectType );
            if ( customTypeDescriptor != null )
            {
                return customTypeDescriptor.GetProperties ( )
                                           .Cast<PropertyDescriptor> ( );
            }

            return null;
        }


        public static object ToType<T> ( this object obj, T totype )
        {
            //create instance of T type object:
            var tmp = Activator.CreateInstance ( Type.GetType ( totype.ToString ( ) ) );

            //loop through the properties of the object you want to covert:          
            foreach ( PropertyInfo pi in obj.GetType ( )
                                            .GetProperties ( ) )
            {
                try
                {
                    //get the value of property and try 
                    //to assign it to the property of T type object:
                    tmp.GetType ( )
                       .GetProperty ( pi.Name )
                       .SetValue ( tmp, pi.GetValue ( obj, null ), null );
                }
                catch
                {
                }
            }

            //return the T type object:         
            return tmp;
        }

        public static object ToNonAnonymousList<T> ( this List<T> list, Type t )
        {
            //define system Type representing List of objects of T type:
            var genericType = typeof ( List<> ).MakeGenericType ( t );

            //create an object instance of defined type:
            var l = Activator.CreateInstance ( genericType );

            //get method Add from from the list:
            MethodInfo addMethod = l.GetType ( )
                                    .GetMethod ( "Add" );

            //loop through the calling list:
            foreach ( T item in list )
            {
                //convert each object of the list into T object 
                //by calling extension ToType<T>()
                //Add this object to newly created list:
                addMethod.Invoke ( l, new[] {item.ToType ( t )} );
            }

            //return List of T objects:
            return l;
        }


        public static string ToShortTypeName ( this Type type )
        {
            if ( type.IsGenericType )
            {
                string name = type.GetGenericTypeDefinition ( )
                                  .Name;
                name = name.Substring ( 0, name.IndexOf ( '`' ) );

                Type[] arguments = type.GetGenericArguments ( );
                string innerTypeName = string.Join ( ",", arguments.Select ( x => x.ToShortTypeName ( ) )
                                                                   .ToArray ( ) );

                return "{0}<{1}>".FormatWith ( name, innerTypeName );
            }

            return type.Name;
        }


    }
}