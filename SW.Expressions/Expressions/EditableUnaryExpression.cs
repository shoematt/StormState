#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Expressions
// 
// File:	EditableUnaryExpression.cs
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
    public sealed class EditableUnaryExpression : EditableExpression
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="EditableUnaryExpression" /> class.
        /// </summary>
        /// <param name="info"> The info. </param>
        /// <param name="context"> The context. </param>
        public EditableUnaryExpression ( SerializationInfo info, StreamingContext context ) : base ( info, context )
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="EditableUnaryExpression" /> class.
        /// </summary>
        public EditableUnaryExpression ( )
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="EditableUnaryExpression" /> class.
        /// </summary>
        /// <param name="nodeType"> Type of the node. </param>
        /// <param name="operand"> The operand. </param>
        /// <param name="type"> The type. </param>
        public EditableUnaryExpression ( ExpressionType nodeType, EditableExpression operand, Type type )
        {
            NodeType = nodeType;
            Operand = operand;
            Type = type;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="EditableUnaryExpression" /> class.
        /// </summary>
        /// <param name="unEx"> The un ex. </param>
        public EditableUnaryExpression ( UnaryExpression unEx ) : this ( unEx.NodeType, CreateEditableExpression ( unEx.Operand ), unEx.Type )
        {
        }

        /// <summary>
        ///     Gets or sets the operand.
        /// </summary>
        /// <value> The operand. </value>
        public EditableExpression Operand { get; set; }

        /// <summary>
        ///     Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.
        /// </summary>
        /// <param name="info"> The info. </param>
        /// <param name="context"> The context. </param>
        protected override void OnSerialize ( SerializationInfo info, StreamingContext context )
        {
            base.OnSerialize ( info, context );

            info.AddValueWithType ( "Operand", Operand );
        }

        /// <summary>
        ///     Populates an object with data from a  <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed.
        /// </summary>
        /// <param name="info"> The info. </param>
        /// <param name="context"> The context. </param>
        protected override void OnDeserialize ( SerializationInfo info, StreamingContext context )
        {
            base.OnDeserialize ( info, context );

            Operand = ( EditableExpression ) info.GetValueWithType ( "Operand" );
        }

        public override Expression ToExpression ( )
        {
            return NodeType == ExpressionType.UnaryPlus ? Expression.UnaryPlus ( Operand.ToExpression ( ) ) : Expression.MakeUnary ( NodeType, Operand.ToExpression ( ), Type );
        }
    }
}