#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Expressions
// 
// File:	EditableExpression.cs
// Author:	Matthew Shoemate
// Date:	08/27/2012
// 
// http://www.StormWorksSoftware.com
// 

#endregion

using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;

using SW.Core.Extensions;

namespace SW.Expressions.Expressions
{
    [ Serializable ]
    public abstract class EditableExpression : ISerializable
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="EditableExpression" /> class.
        /// </summary>
        /// <param name="info"> The info. </param>
        /// <param name="context"> The context. </param>
        protected EditableExpression ( SerializationInfo info, StreamingContext context )
        {
            RestoreObjectData ( info, context );
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="EditableExpression" /> class.
        /// </summary>
        protected EditableExpression ( )
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="EditableExpression" /> class.
        /// </summary>
        /// <param name="type"> The type. </param>
        protected EditableExpression ( Type type )
        {
            Type = type;
        }

        /// <summary>
        ///     Gets or sets the type of the node.
        /// </summary>
        /// <value> The type of the node. </value>
        public virtual ExpressionType NodeType { get; set; }

        /// <summary>
        ///     Gets or sets the type.
        /// </summary>
        /// <value> The type. </value>
        public Type Type { get; set; }

        #region ISerializable Members

        /// <summary>
        ///     Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.
        /// </summary>
        /// <param name="info">
        ///     The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.
        /// </param>
        /// <param name="context">
        ///     The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext" /> ) for this serialization.
        /// </param>
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
        public void GetObjectData ( SerializationInfo info, StreamingContext context )
        {
            info.AddShortTypeValueSafe ( "Type", Type );
            info.AddValue ( "NodeType", NodeType );

            OnSerialize ( info, context );
        }

        #endregion

        /// <summary>
        ///     Restores the object data.
        /// </summary>
        /// <param name="info"> The info. </param>
        /// <param name="context"> The context. </param>
        private void RestoreObjectData ( SerializationInfo info, StreamingContext context )
        {
            Type = info.GetShortTypeValueSafe ( "Type" );
            NodeType = ( ExpressionType ) info.GetValue ( "NodeType", typeof ( ExpressionType ) );

            OnDeserialize ( info, context );
        }

        /// <summary>
        ///     Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.
        /// </summary>
        /// <param name="info"> The info. </param>
        /// <param name="context"> The context. </param>
        protected virtual void OnSerialize ( SerializationInfo info, StreamingContext context )
        {
        }

        /// <summary>
        ///     Populates an object with data from a  <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed.
        /// </summary>
        /// <param name="info"> The info. </param>
        /// <param name="context"> The context. </param>
        protected virtual void OnDeserialize ( SerializationInfo info, StreamingContext context )
        {
        }

        /// <summary>
        ///     Creates the editable expression.
        /// </summary>
        /// <typeparam name="TResult"> The type of the result. </typeparam>
        /// <param name="ex"> The ex. </param>
        /// <returns> </returns>
        public static EditableExpression CreateEditableExpression<TResult> ( Expression<Func<TResult>> ex )
        {
            LambdaExpression lambEx = Expression.Lambda<Func<TResult>> ( ex.Body, ex.Parameters );
            return new EditableLambdaExpression ( lambEx );
        }

        /// <summary>
        ///     Creates the editable expression.
        /// </summary>
        /// <typeparam name="TArg0"> The type of the arg0. </typeparam>
        /// <typeparam name="TResult"> The type of the result. </typeparam>
        /// <param name="ex"> The ex. </param>
        /// <returns> </returns>
        public static EditableExpression CreateEditableExpression<TArg0, TResult> ( Expression<Func<TArg0, TResult>> ex )
        {
            LambdaExpression lambEx = Expression.Lambda<Func<TArg0, TResult>> ( ex.Body, ex.Parameters );
            return new EditableLambdaExpression ( lambEx );
        }

        /// <summary>
        ///     Creates the editable expression.
        /// </summary>
        /// <typeparam name="TArg0"> The type of the arg0. </typeparam>
        /// <typeparam name="TArg1"> The type of the arg1. </typeparam>
        /// <typeparam name="TResult"> The type of the result. </typeparam>
        /// <param name="ex"> The ex. </param>
        /// <returns> </returns>
        public static EditableExpression CreateEditableExpression<TArg0, TArg1, TResult> ( Expression<Func<TArg0, TArg1, TResult>> ex )
        {
            LambdaExpression lambEx = Expression.Lambda<Func<TArg0, TArg1, TResult>> ( ex.Body, ex.Parameters );
            return new EditableLambdaExpression ( lambEx );
        }

        /// <summary>
        ///     Creates the editable expression.
        /// </summary>
        /// <typeparam name="TArg0"> The type of the arg0. </typeparam>
        /// <typeparam name="TArg1"> The type of the arg1. </typeparam>
        /// <typeparam name="TArg2"> The type of the arg2. </typeparam>
        /// <typeparam name="TResult"> The type of the result. </typeparam>
        /// <param name="ex"> The ex. </param>
        /// <returns> </returns>
        public static EditableExpression CreateEditableExpression<TArg0, TArg1, TArg2, TResult> ( Expression<Func<TArg0, TArg1, TArg2, TResult>> ex )
        {
            LambdaExpression lambEx = Expression.Lambda<Func<TArg0, TArg1, TArg2, TResult>> ( ex.Body, ex.Parameters );
            return new EditableLambdaExpression ( lambEx );
        }

        /// <summary>
        ///     Creates the editable expression.
        /// </summary>
        /// <typeparam name="TArg0"> The type of the arg0. </typeparam>
        /// <typeparam name="TArg1"> The type of the arg1. </typeparam>
        /// <typeparam name="TArg2"> The type of the arg2. </typeparam>
        /// <typeparam name="TArg3"> The type of the arg3. </typeparam>
        /// <typeparam name="TResult"> The type of the result. </typeparam>
        /// <param name="ex"> The ex. </param>
        /// <returns> </returns>
        public static EditableExpression CreateEditableExpression<TArg0, TArg1, TArg2, TArg3, TResult> ( Expression<Func<TArg0, TArg1, TArg2, TArg3, TResult>> ex )
        {
            LambdaExpression lambEx = Expression.Lambda<Func<TArg0, TArg1, TArg2, TArg3, TResult>> ( ex.Body, ex.Parameters );
            return new EditableLambdaExpression ( lambEx );
        }

        /// <summary>
        ///     Creates the editable expression.
        /// </summary>
        /// <param name="ex"> The ex. </param>
        /// <returns> </returns>
        public static EditableExpression CreateEditableExpression ( Expression ex )
        {
            if ( ex is ConstantExpression )
            {
                return new EditableConstantExpression ( ex as ConstantExpression );
            }
            if ( ex is BinaryExpression )
            {
                return new EditableBinaryExpression ( ex as BinaryExpression );
            }
            if ( ex is ConditionalExpression )
            {
                return new EditableConditionalExpression ( ex as ConditionalExpression );
            }
            if ( ex is InvocationExpression )
            {
                return new EditableInvocationExpression ( ex as InvocationExpression );
            }
            if ( ex is LambdaExpression )
            {
                return new EditableLambdaExpression ( ex as LambdaExpression );
            }
            if ( ex is ParameterExpression )
            {
                return new EditableParameterExpression ( ex as ParameterExpression );
            }
            if ( ex is ListInitExpression )
            {
                return new EditableListInitExpression ( ex as ListInitExpression );
            }
            if ( ex is MemberExpression )
            {
                return new EditableMemberExpression ( ex as MemberExpression );
            }
            if ( ex is MemberInitExpression )
            {
                return new EditableMemberInitExpression ( ex as MemberInitExpression );
            }
            if ( ex is MethodCallExpression )
            {
                return new EditableMethodCallExpression ( ex as MethodCallExpression );
            }
            if ( ex is NewArrayExpression )
            {
                return new EditableNewArrayExpression ( ex as NewArrayExpression );
            }
            if ( ex is NewExpression )
            {
                return new EditableNewExpression ( ex as NewExpression );
            }
            if ( ex is TypeBinaryExpression )
            {
                return new EditableTypeBinaryExpression ( ex as TypeBinaryExpression );
            }
            if ( ex is UnaryExpression )
            {
                return new EditableUnaryExpression ( ex as UnaryExpression );
            }
            return null;
        }

        public abstract Expression ToExpression ( );
    }
}