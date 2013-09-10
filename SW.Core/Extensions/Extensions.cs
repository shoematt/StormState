#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Core
// 
// File:	Extensions.cs
// Author:	Matthew Shoemate
// Date:	08/27/2012
// 
// http://www.StormWorksSoftware.com
// 

#endregion

using System;
using System.Linq;
using System.Reflection;

namespace SW.Core.Extensions
{
    public static class Extensions
    {
        private const int _seedPrimeNumber = 691;
        private const int _fieldPrimeNumber = 397;

        /// <summary>
        ///     Gets a custom attribute of type T from the specified element type.
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="element"> The element. </param>
        /// <returns> </returns>
        public static T GetAttribute<T> ( this Assembly element ) where T : Attribute
        {
            return Attribute.GetCustomAttribute ( element, typeof ( T ) ) as T;
        }

        /// <summary>
        ///     Gets a custom attribute of type T from the specified element type.
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="element"> The element. </param>
        /// <returns> </returns>
        public static T GetAttribute<T> ( this MemberInfo element ) where T : Attribute
        {
            return Attribute.GetCustomAttribute ( element, typeof ( T ) ) as T;
        }

        /// <summary>
        ///     Gets a custom attribute of type T from the specified element type.
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="element"> The element. </param>
        /// <returns> </returns>
        public static T GetAttribute<T> ( this Module element ) where T : Attribute
        {
            return Attribute.GetCustomAttribute ( element, typeof ( T ) ) as T;
        }

        /// <summary>
        ///     Gets a custom attribute of type T from the specified element type.
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="element"> The element. </param>
        /// <returns> </returns>
        public static T GetAttribute<T> ( this ParameterInfo element ) where T : Attribute
        {
            return Attribute.GetCustomAttribute ( element, typeof ( T ) ) as T;
        }


        public static int GetHashCodeFromFields ( this object obj, params object[] fields )
        {
            unchecked
            {
                //unchecked to prevent throwing overflow exception

                return fields.Where ( t => t != null )
                             .Aggregate ( _seedPrimeNumber, ( current, t ) => current * ( _fieldPrimeNumber + t.GetHashCode ( ) ) );
            }
        }

        public static int GetHashCodeFromFields<T1> ( this object obj, T1 obj1 )
        {
            int hashCode = _seedPrimeNumber;
            if ( obj1 != null )
            {
                hashCode *= _fieldPrimeNumber + obj1.GetHashCode ( );
            }

            return hashCode;
        }


        public static int GetHashCodeFromFields<T1, T2> ( this object obj, T1 obj1, T2 obj2 )
        {
            int hashCode = _seedPrimeNumber;
            if ( obj1 != null )
            {
                hashCode *= _fieldPrimeNumber + obj1.GetHashCode ( );
            }
            if ( obj2 != null )
            {
                hashCode *= _fieldPrimeNumber + obj2.GetHashCode ( );
            }

            return hashCode;
        }

        public static int GetHashCodeFromFields<T1, T2, T3> ( this object obj, T1 obj1, T2 obj2, T3 obj3 )
        {
            int hashCode = _seedPrimeNumber;
            if ( obj1 != null )
            {
                hashCode *= _fieldPrimeNumber + obj1.GetHashCode ( );
            }
            if ( obj2 != null )
            {
                hashCode *= _fieldPrimeNumber + obj2.GetHashCode ( );
            }
            if ( obj3 != null )
            {
                hashCode *= _fieldPrimeNumber + obj3.GetHashCode ( );
            }

            return hashCode;
        }


        public static int GetHashCodeFromFields<T1, T2, T3, T4> ( this object obj, T1 obj1, T2 obj2, T3 obj3, T4 obj4 )
        {
            int hashCode = _seedPrimeNumber;
            if ( obj1 != null )
            {
                hashCode *= _fieldPrimeNumber + obj1.GetHashCode ( );
            }
            if ( obj2 != null )
            {
                hashCode *= _fieldPrimeNumber + obj2.GetHashCode ( );
            }
            if ( obj3 != null )
            {
                hashCode *= _fieldPrimeNumber + obj3.GetHashCode ( );
            }
            if ( obj4 != null )
            {
                hashCode *= _fieldPrimeNumber + obj4.GetHashCode ( );
            }
            return hashCode;
        }
    }
}