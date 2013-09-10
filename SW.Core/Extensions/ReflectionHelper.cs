#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Core
// 
// File:	ReflectionHelper.cs
// Author:	Matthew Shoemate
// Date:	08/27/2012
// 
// http://www.StormWorksSoftware.com
// 

#endregion

using System;
using System.Linq.Expressions;
using System.Reflection;

namespace SW.Core.Extensions
{
    //Taken from FubuMVC project
    public static class ReflectionHelper
    {
        public static bool MeetsSpecialGenericConstraints ( Type genericArgType, Type proposedSpecificType )
        {
            var gpa = genericArgType.GenericParameterAttributes;
            var constraints = gpa & GenericParameterAttributes.SpecialConstraintMask;

            // No constraints, away we go!
            if ( constraints == GenericParameterAttributes.None )
            {
                return true;
            }

            // "class" constraint and this is a value type
            if ( ( constraints & GenericParameterAttributes.ReferenceTypeConstraint ) != 0 && proposedSpecificType.IsValueType )
            {
                return false;
            }

            // "struct" constraint and this is a value type
            if ( ( constraints & GenericParameterAttributes.NotNullableValueTypeConstraint ) != 0 && !proposedSpecificType.IsValueType )
            {
                return false;
            }

            // "new()" constraint and this type has no default constructor
            if ( ( constraints & GenericParameterAttributes.DefaultConstructorConstraint ) != 0 && proposedSpecificType.GetConstructor ( Type.EmptyTypes ) == null )
            {
                return false;
            }

            return true;
        }

        /// <summary>
        ///     Gets the property.
        /// </summary>
        /// <typeparam name="MODEL"> The type of the ODEL. </typeparam>
        /// <param name="expression"> The expression. </param>
        /// <returns> </returns>
        public static PropertyInfo GetProperty<MODEL> ( Expression<Func<MODEL, object>> expression )
        {
            MemberExpression memberExpression = GetMemberExpression ( expression );
            return ( PropertyInfo ) memberExpression.Member;
        }

        /// <summary>
        ///     Gets the property.
        /// </summary>
        /// <typeparam name="MODEL"> The type of the ODEL. </typeparam>
        /// <typeparam name="T"> </typeparam>
        /// <param name="expression"> The expression. </param>
        /// <returns> </returns>
        public static PropertyInfo GetProperty<MODEL, T> ( Expression<Func<MODEL, T>> expression )
        {
            MemberExpression memberExpression = GetMemberExpression ( expression );
            return ( PropertyInfo ) memberExpression.Member;
        }

        /// <summary>
        ///     Gets the member expression.
        /// </summary>
        /// <typeparam name="MODEL"> The type of the ODEL. </typeparam>
        /// <typeparam name="T"> </typeparam>
        /// <param name="expression"> The expression. </param>
        /// <returns> </returns>
        private static MemberExpression GetMemberExpression<MODEL, T> ( Expression<Func<MODEL, T>> expression )
        {
            MemberExpression memberExpression = null;
            if ( expression.Body.NodeType == ExpressionType.Convert )
            {
                var body = ( UnaryExpression ) expression.Body;
                memberExpression = body.Operand as MemberExpression;
            }
            else if ( expression.Body.NodeType == ExpressionType.MemberAccess )
            {
                memberExpression = expression.Body as MemberExpression;
            }

            if ( memberExpression == null )
            {
                throw new ArgumentException ( "Not a member access", "expression" );
            }

            return memberExpression;
        }

        /// <summary>
        ///     Gets the method.
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="expression"> The expression. </param>
        /// <returns> </returns>
        public static MethodInfo GetMethod<T> ( Expression<Func<T, object>> expression )
        {
            var methodCall = ( MethodCallExpression ) expression.Body;
            return methodCall.Method;
        }

        /// <summary>
        ///     Gets the method.
        /// </summary>
        /// <typeparam name="DELEGATET"> </typeparam>
        /// <param name="expression"> The expression. </param>
        /// <returns> </returns>
        public static MethodInfo GetMethod<DELEGATET> ( Expression<DELEGATET> expression )
        {
            var methodCall = ( MethodCallExpression ) expression.Body;
            return methodCall.Method;
        }

        /// <summary>
        ///     Gets the method.
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <typeparam name="U"> </typeparam>
        /// <param name="expression"> The expression. </param>
        /// <returns> </returns>
        public static MethodInfo GetMethod<T, U> ( Expression<Func<T, U>> expression )
        {
            var methodCall = ( MethodCallExpression ) expression.Body;
            return methodCall.Method;
        }

        /// <summary>
        ///     Gets the method.
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <typeparam name="U"> </typeparam>
        /// <typeparam name="V"> </typeparam>
        /// <param name="expression"> The expression. </param>
        /// <returns> </returns>
        public static MethodInfo GetMethod<T, U, V> ( Expression<Func<T, U, V>> expression )
        {
            var methodCall = ( MethodCallExpression ) expression.Body;
            return methodCall.Method;
        }

        /// <summary>
        ///     Gets the method.
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="expression"> The expression. </param>
        /// <returns> </returns>
        public static MethodInfo GetMethod<T> ( Expression<Action<T>> expression )
        {
            var methodCall = ( MethodCallExpression ) expression.Body;
            return methodCall.Method;
        }

        /// <summary>
        ///     Gets the attribute.
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="provider"> The provider. </param>
        /// <returns> </returns>
        public static T GetAttribute<T> ( this ICustomAttributeProvider provider ) where T : Attribute
        {
            object[] atts = provider.GetCustomAttributes ( typeof ( T ), true );
            return atts.Length > 0 ? atts [0] as T : null;
        }

        /// <summary>
        ///     For the specified attribute, perform an action.
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="provider"> The provider. </param>
        /// <param name="action"> The action. </param>
        public static void ForAttribute<T> ( this ICustomAttributeProvider provider, Action<T> action ) where T : Attribute
        {
            foreach ( T attribute in provider.GetCustomAttributes ( typeof ( T ), true ) )
            {
                action ( attribute );
            }
        }
    }
}