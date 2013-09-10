#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Expressions
// 
// File:	EditableTypeBinaryExpression.cs
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
    public class EditableTypeBinaryExpression : EditableExpression
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="EditableTypeBinaryExpression" /> class.
        /// </summary>
        /// <param name="info"> The info. </param>
        /// <param name="context"> The context. </param>
        public EditableTypeBinaryExpression ( SerializationInfo info, StreamingContext context ) : base ( info, context )
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="EditableTypeBinaryExpression" /> class.
        /// </summary>
        public EditableTypeBinaryExpression ( )
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="EditableTypeBinaryExpression" /> class.
        /// </summary>
        /// <param name="typeBinEx"> The type bin ex. </param>
        public EditableTypeBinaryExpression ( TypeBinaryExpression typeBinEx ) : this ( CreateEditableExpression ( typeBinEx.Expression ), typeBinEx.TypeOperand )
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="EditableTypeBinaryExpression" /> class.
        /// </summary>
        /// <param name="expression"> The expression. </param>
        /// <param name="type"> The type. </param>
        public EditableTypeBinaryExpression ( EditableExpression expression, Type type ) : base ( type )
        {
            Expression = expression;
        }

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
            get { return ExpressionType.TypeIs; }
        }

        /// <summary>
        ///     Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.
        /// </summary>
        /// <param name="info"> The info. </param>
        /// <param name="context"> The context. </param>
        protected override void OnSerialize ( SerializationInfo info, StreamingContext context )
        {
            base.OnSerialize ( info, context );

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

            Expression = ( EditableExpression ) info.GetValueWithType ( "Expression" );
        }

        public override Expression ToExpression ( )
        {
            return System.Linq.Expressions.Expression.TypeIs ( Expression.ToExpression ( ), Type );
        }
    }
}