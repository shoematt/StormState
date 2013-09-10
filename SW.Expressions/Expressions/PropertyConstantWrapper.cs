#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Expressions
// 
// File:	PropertyConstantWrapper.cs
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
    public class PropertyConstantWrapper<T> : EditableExpression
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="FieldConstantWrapper{T}" /> class.
        /// </summary>
        /// <param name="info"> The info. </param>
        /// <param name="context"> The context. </param>
        public PropertyConstantWrapper ( SerializationInfo info, StreamingContext context ) : base ( info, context )
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="FieldConstantWrapper{T}" /> class.
        /// </summary>
        /// <param name="value"> The value. </param>
        public PropertyConstantWrapper ( T value )
        {
            Value = value;
        }

        /// <summary>
        ///     Gets or sets the value.
        /// </summary>
        /// <value> The value. </value>
        public T Value { get; set; }

        /// <summary>
        ///     Gets the node type of this <see cref="T:System.Linq.Expressions.Expression" />.
        /// </summary>
        /// <value> </value>
        /// <returns>
        ///     One of the <see cref="T:System.Linq.Expressions.ExpressionType" /> values.
        /// </returns>
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

            Value = ( T ) info.GetValueWithType ( "Value" );
        }

        public override Expression ToExpression ( )
        {
            return Expression.Constant ( Value, Value.GetType ( ) );
        }
    }
}