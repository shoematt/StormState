#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Expressions
// 
// File:	EditableMemberExpression.cs
// Author:	Matthew Shoemate
// Date:	08/27/2012
// 
// http://www.StormWorksSoftware.com
// 

#endregion

using System;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;

using SW.Core.Extensions;

namespace SW.Expressions.Expressions
{
    [ Serializable ]
    public class EditableMemberExpression : EditableExpression
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="EditableMemberExpression" /> class.
        /// </summary>
        /// <param name="info"> The info. </param>
        /// <param name="context"> The context. </param>
        public EditableMemberExpression ( SerializationInfo info, StreamingContext context ) : base ( info, context )
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="EditableMemberExpression" /> class.
        /// </summary>
        public EditableMemberExpression ( )
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="EditableMemberExpression" /> class.
        /// </summary>
        /// <param name="membEx">
        ///     The <see cref="MemberExpression" /> ex.
        /// </param>
        public EditableMemberExpression ( MemberExpression membEx )
        {
            Member = membEx.Member;

            if ( membEx.Expression is ConstantExpression && Member is FieldInfo )
            {
                var fieldValue = ( ( FieldInfo ) Member ).GetValue ( ( ( ConstantExpression ) membEx.Expression ).Value );

                Type wrapperType = typeof ( FieldConstantWrapper<> );
                var closedWrapperType = wrapperType.MakeGenericType ( ( ( FieldInfo ) Member ).FieldType );
                var wrapperInstance = Activator.CreateInstance ( closedWrapperType, new[] {fieldValue} );
                Expression = new EditableConstantExpression ( wrapperInstance );
                Member = closedWrapperType.GetProperty ( "Value" );
            }
            else if ( membEx.Expression is ConstantExpression && Member is PropertyInfo )
            {
                Type wrapperType = typeof ( PropertyConstantWrapper<> );
                var closedWrapperType = wrapperType.MakeGenericType ( ( ( PropertyInfo ) Member ).PropertyType );
                var wrapperInstance = Activator.CreateInstance ( closedWrapperType, new[] {( ( PropertyInfo ) Member ).GetValue ( ( ( ConstantExpression ) membEx.Expression ).Value, null )} );
                Expression = new EditableConstantExpression ( wrapperInstance );
                Member = closedWrapperType.GetProperty ( "Value" );
            }
            else
            {
                Expression = CreateEditableExpression ( membEx.Expression );
            }
        }

        /// <summary>
        ///     Gets or sets the member.
        /// </summary>
        /// <value> The member. </value>
        public MemberInfo Member { get; set; }

        /// <summary>
        ///     Gets or sets the expression.
        /// </summary>
        /// <value> The expression. </value>
        public EditableExpression Expression { get; set; }

        /// <summary>
        ///     Gets or sets the type of the node.
        /// </summary>
        /// <value> The type of the node. </value>
        public override ExpressionType NodeType
        {
            get { return ExpressionType.MemberAccess; }
        }

        /// <summary>
        ///     Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.
        /// </summary>
        /// <param name="info"> The info. </param>
        /// <param name="context"> The context. </param>
        protected override void OnSerialize ( SerializationInfo info, StreamingContext context )
        {
            base.OnSerialize ( info, context );

            info.AddValueWithType ( "Member", Member );
            info.AddValueWithType ( "Expression", Expression );
        }

        /// <summary>
        ///     Populates an object with data from a  <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed.
        /// </summary>
        /// <param name="info"> The info. </param>
        /// <param name="context"> The context. </param>
        protected override void OnDeserialize ( SerializationInfo info, StreamingContext context )
        {
            base.OnDeserialize ( info, context );

            Member = ( MemberInfo ) info.GetValueWithType ( "Member" );
            Expression = ( EditableExpression ) info.GetValueWithType ( "Expression" );
        }

        public override Expression ToExpression ( )
        {
            Expression expression = null;
            if ( Expression != null )
            {
                expression = Expression.ToExpression ( );
            }

            Debug.Assert ( expression != null, "expression != null" );
            return System.Linq.Expressions.Expression.MakeMemberAccess ( expression, Member );
        }
    }
}