#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Expressions
// 
// File:	EditableNewArrayExpression.cs
// Author:	Matthew Shoemate
// Date:	08/27/2012
// 
// http://www.StormWorksSoftware.com
// 

#endregion

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.Serialization;

using SW.Core.Extensions;
using SW.Expressions.Collections;

namespace SW.Expressions.Expressions
{
    [ Serializable ]
    public class EditableNewArrayExpression : EditableExpression
    {
        private ExpressionType _nodeType;

        /// <summary>
        ///     Initializes a new instance of the <see cref="EditableNewArrayExpression" /> class.
        /// </summary>
        /// <param name="info"> The info. </param>
        /// <param name="context"> The context. </param>
        public EditableNewArrayExpression ( SerializationInfo info, StreamingContext context ) : base ( info, context )
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="EditableNewArrayExpression" /> class.
        /// </summary>
        public EditableNewArrayExpression ( )
        {
            Expressions = new EditableExpressionCollection ( );
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="EditableNewArrayExpression" /> class.
        /// </summary>
        /// <param name="newEx"> The new ex. </param>
        public EditableNewArrayExpression ( NewArrayExpression newEx ) : this ( new EditableExpressionCollection ( newEx.Expressions ), newEx.NodeType, newEx.Type )
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="EditableNewArrayExpression" /> class.
        /// </summary>
        /// <param name="expressions"> The expressions. </param>
        /// <param name="nodeType"> Type of the node. </param>
        /// <param name="type"> The type. </param>
        public EditableNewArrayExpression ( IEnumerable<EditableExpression> expressions, ExpressionType nodeType, Type type ) : this ( new EditableExpressionCollection ( expressions ), nodeType, type )
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="EditableNewArrayExpression" /> class.
        /// </summary>
        /// <param name="expressions"> The expressions. </param>
        /// <param name="nodeType"> Type of the node. </param>
        /// <param name="type"> The type. </param>
        public EditableNewArrayExpression ( EditableExpressionCollection expressions, ExpressionType nodeType, Type type ) : base ( type )
        {
            Expressions = expressions;
            NodeType = nodeType;
        }

        /// <summary>
        ///     Gets or sets the expressions.
        /// </summary>
        /// <value> The expressions. </value>
        public EditableExpressionCollection Expressions { get; set; }

        /// <summary>
        ///     Gets or sets the type of the node.
        /// </summary>
        /// <value> The type of the node. </value>
        public override sealed ExpressionType NodeType
        {
            get { return _nodeType; }
            set
            {
                if ( value == ExpressionType.NewArrayInit || value == ExpressionType.NewArrayBounds )
                {
                    _nodeType = value;
                }
                else
                {
                    throw new InvalidOperationException ( "NodeType for NewArrayExpression must be ExpressionType.NewArrayInit or ExpressionType.NewArrayBounds" );
                }
            }
        }

        /// <summary>
        ///     Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.
        /// </summary>
        /// <param name="info"> The info. </param>
        /// <param name="context"> The context. </param>
        protected override void OnSerialize ( SerializationInfo info, StreamingContext context )
        {
            base.OnSerialize ( info, context );

            info.AddValueWithType ( "Expressions", Expressions );
        }

        /// <summary>
        ///     Populates an object with data from a  <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed.
        /// </summary>
        /// <param name="info"> The info. </param>
        /// <param name="context"> The context. </param>
        protected override void OnDeserialize ( SerializationInfo info, StreamingContext context )
        {
            base.OnDeserialize ( info, context );

            Expressions = ( EditableExpressionCollection ) info.GetValueWithType ( "Expressions" );
        }

        public override Expression ToExpression ( )
        {
            switch ( NodeType )
            {
                case ExpressionType.NewArrayBounds:
                    return Expression.NewArrayBounds ( Type.GetElementType ( ), Expressions.GetExpressions ( ) );
                case ExpressionType.NewArrayInit:
                    return Expression.NewArrayInit ( Type.GetElementType ( ), Expressions.GetExpressions ( ) );
                default:
                    throw new InvalidOperationException ( "NodeType for NewArrayExpression must be ExpressionType.NewArrayInit or ExpressionType.NewArrayBounds" );
            }
        }
    }
}