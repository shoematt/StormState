#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Expressions
// 
// File:	EditableConstantExpression.cs
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
    public class EditableConstantExpression : EditableExpression
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="EditableConstantExpression" /> class.
        /// </summary>
        /// <param name="info"> The info. </param>
        /// <param name="context"> The context. </param>
        public EditableConstantExpression ( SerializationInfo info, StreamingContext context ) : base ( info, context )
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="EditableConstantExpression" /> class.
        /// </summary>
        public EditableConstantExpression ( )
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="EditableConstantExpression" /> class.
        /// </summary>
        /// <param name="value"> The value. </param>
        public EditableConstantExpression ( object value )
        {
            Value = value;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="EditableConstantExpression" /> class.
        /// </summary>
        /// <param name="startConstEx"> The start const ex. </param>
        public EditableConstantExpression ( ConstantExpression startConstEx )
        {
            Value = startConstEx.Value;
        }

        /// <summary>
        ///     Gets or sets the value.
        /// </summary>
        /// <value> The value. </value>
        public object Value { get; set; }

        /// <summary>
        ///     Gets or sets the type of the node.
        /// </summary>
        /// <value> The type of the node. </value>
        public override ExpressionType NodeType
        {
            get { return ExpressionType.Constant; }
        }

        /// <summary>
        ///     Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.
        /// </summary>
        /// <param name="info"> The info. </param>
        /// <param name="context"> The context. </param>
        protected override void OnSerialize ( SerializationInfo info, StreamingContext context )
        {
            base.OnSerialize ( info, context );

            info.AddValueWithType ( "Value", Value );
        }

        /// <summary>
        ///     Populates an object with data from a  <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed.
        /// </summary>
        /// <param name="info"> The info. </param>
        /// <param name="context"> The context. </param>
        protected override void OnDeserialize ( SerializationInfo info, StreamingContext context )
        {
            base.OnDeserialize ( info, context );

            Value = info.GetValueWithType ( "Value" );
        }

        public override Expression ToExpression ( )
        {
            return Expression.Constant ( Value );
        }
    }
}