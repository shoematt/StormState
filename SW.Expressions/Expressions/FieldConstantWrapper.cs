#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Expressions
// 
// File:	FieldConstantWrapper.cs
// Author:	Matthew Shoemate
// Date:	08/27/2012
// 
// http://www.StormWorksSoftware.com
// 

#endregion

using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;

using SW.Core.Extensions;

using Wintellect.PowerCollections;

namespace SW.Expressions.Expressions
{
    [ Serializable ]
    public class FieldConstantWrapper<T> : EditableExpression,
                                           IComparable<FieldConstantWrapper<T>>,
                                           IComparable
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="FieldConstantWrapper{T}" /> class.
        /// </summary>
        /// <param name="info"> The info. </param>
        /// <param name="context"> The context. </param>
        public FieldConstantWrapper ( SerializationInfo info, StreamingContext context ) : base ( info, context )
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="FieldConstantWrapper{T}" /> class.
        /// </summary>
        /// <param name="value"> The value. </param>
        public FieldConstantWrapper ( T value )
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

        #region IComparable Members

        /// <summary>
        ///     Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.
        /// </summary>
        /// <param name="obj"> An object to compare with this instance. </param>
        /// <returns>
        ///     A value that indicates the relative order of the objects being compared. The return value has these meanings: Value Meaning Less than zero This instance is less than
        ///     <paramref
        ///         name="obj" />
        ///     . Zero This instance is equal to <paramref name="obj" /> . Greater than zero This instance is greater than
        ///     <paramref
        ///         name="obj" />
        ///     .
        /// </returns>
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="obj" />
        ///     is not the same type as this instance.
        /// </exception>
        public int CompareTo ( object obj )
        {
            return CompareTo ( obj as FieldConstantWrapper<T> );
        }

        #endregion

        #region IComparable<FieldConstantWrapper<T>> Members

        /// <summary>
        ///     Compares to.
        /// </summary>
        /// <param name="other"> The other. </param>
        /// <returns> </returns>
        public int CompareTo ( FieldConstantWrapper<T> other )
        {
            if ( other.Value is IEnumerable )
            {
                Set<object> setA = new Set<object> ( ( ( IList ) Value ).Cast<object> ( ) );
                Set<object> setB = new Set<object> ( ( ( IList ) other.Value ).Cast<object> ( ) );

                Set<object> difference = setA.Difference ( setB );
                return difference.Count == 0 ? 0 : 1;
            }

            if ( other.Type == Type && other.Value.Equals ( Value ) && other.NodeType == NodeType )
            {
                return 0;
            }

            return -1;
        }

        #endregion

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