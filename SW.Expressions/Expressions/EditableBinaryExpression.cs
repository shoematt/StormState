#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Expressions
// 
// File:	EditableBinaryExpression.cs
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
    public sealed class EditableBinaryExpression : EditableExpression
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="EditableBinaryExpression" /> class.
        /// </summary>
        /// <param name="info"> The info. </param>
        /// <param name="context"> The context. </param>
        public EditableBinaryExpression ( SerializationInfo info, StreamingContext context ) : base ( info, context )
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="EditableBinaryExpression" /> class.
        /// </summary>
        public EditableBinaryExpression ( )
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="EditableBinaryExpression" /> class.
        /// </summary>
        /// <param name="binex">
        ///     The <see cref="BinaryExpression" /> .
        /// </param>
        public EditableBinaryExpression ( BinaryExpression binex ) : base ( binex.Type )
        {
            Left = CreateEditableExpression ( binex.Left );
            Right = CreateEditableExpression ( binex.Right );
            NodeType = binex.NodeType;
        }

        /// <summary>
        ///     Gets or sets the left.
        /// </summary>
        /// <value> The left. </value>
        public EditableExpression Left { get; set; }

        /// <summary>
        ///     Gets or sets the right.
        /// </summary>
        /// <value> The right. </value>
        public EditableExpression Right { get; set; }

        /// <summary>
        ///     Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.
        /// </summary>
        /// <param name="info"> The info. </param>
        /// <param name="context"> The context. </param>
        protected override void OnSerialize ( SerializationInfo info, StreamingContext context )
        {
            base.OnSerialize ( info, context );

            info.AddValueWithType ( "Left", Left );
            info.AddValueWithType ( "Right", Right );
        }

        /// <summary>
        ///     Populates an object with data from a  <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed.
        /// </summary>
        /// <param name="info"> The info. </param>
        /// <param name="context"> The context. </param>
        protected override void OnDeserialize ( SerializationInfo info, StreamingContext context )
        {
            base.OnDeserialize ( info, context );

            Left = ( EditableExpression ) info.GetValueWithType ( "Left" );
            Right = ( EditableExpression ) info.GetValueWithType ( "Right" );
        }

        public override Expression ToExpression ( )
        {
            return Expression.MakeBinary ( NodeType, Left.ToExpression ( ), Right.ToExpression ( ) );
        }
    }
}