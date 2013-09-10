#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Expressions
// 
// File:	EditableConditionalExpression.cs
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
    public sealed class EditableConditionalExpression : EditableExpression
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="EditableConditionalExpression" /> class.
        /// </summary>
        /// <param name="info"> The info. </param>
        /// <param name="context"> The context. </param>
        public EditableConditionalExpression ( SerializationInfo info, StreamingContext context ) : base ( info, context )
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="EditableConditionalExpression" /> class.
        /// </summary>
        public EditableConditionalExpression ( )
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="EditableConditionalExpression" /> class.
        /// </summary>
        /// <param name="condEx">
        ///     The <see cref="ConditionalExpression" /> ex.
        /// </param>
        public EditableConditionalExpression ( ConditionalExpression condEx )
        {
            NodeType = condEx.NodeType;
            Test = CreateEditableExpression ( condEx.Test );
            IfTrue = CreateEditableExpression ( condEx.IfTrue );
            IfFalse = CreateEditableExpression ( condEx.IfFalse );
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="EditableConditionalExpression" /> class.
        /// </summary>
        /// <param name="nodeType"> Type of the node. </param>
        /// <param name="test"> The test. </param>
        /// <param name="ifTrue"> If true. </param>
        /// <param name="ifFalse"> If false. </param>
        public EditableConditionalExpression ( ExpressionType nodeType, EditableExpression test, EditableExpression ifTrue, EditableExpression ifFalse )
        {
            NodeType = nodeType;
            Test = test;
            IfTrue = ifTrue;
            IfFalse = ifFalse;
        }

        /// <summary>
        ///     Gets or sets the test.
        /// </summary>
        /// <value> The test. </value>
        public EditableExpression Test { get; set; }

        /// <summary>
        ///     Gets or sets if true.
        /// </summary>
        /// <value> If true. </value>
        public EditableExpression IfTrue { get; set; }

        /// <summary>
        ///     Gets or sets if false.
        /// </summary>
        /// <value> If false. </value>
        public EditableExpression IfFalse { get; set; }

        /// <summary>
        ///     Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.
        /// </summary>
        /// <param name="info"> The info. </param>
        /// <param name="context"> The context. </param>
        protected override void OnSerialize ( SerializationInfo info, StreamingContext context )
        {
            base.OnSerialize ( info, context );

            info.AddValueWithType ( "Test", Test );
            info.AddValueWithType ( "IfTrue", IfTrue );
            info.AddValueWithType ( "IfFalse", IfFalse );
        }

        /// <summary>
        ///     Populates an object with data from a  <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed.
        /// </summary>
        /// <param name="info"> The info. </param>
        /// <param name="context"> The context. </param>
        protected override void OnDeserialize ( SerializationInfo info, StreamingContext context )
        {
            base.OnDeserialize ( info, context );

            Test = ( EditableExpression ) info.GetValueWithType ( "Test" );
            IfTrue = ( EditableExpression ) info.GetValueWithType ( "IfTrue" );
            IfFalse = ( EditableExpression ) info.GetValueWithType ( "IfFalse" );
        }

        public override Expression ToExpression ( )
        {
            return Expression.Condition ( Test.ToExpression ( ), IfTrue.ToExpression ( ), IfFalse.ToExpression ( ) );
        }
    }
}