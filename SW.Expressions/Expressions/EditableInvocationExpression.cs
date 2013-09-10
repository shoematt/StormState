#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Expressions
// 
// File:	EditableInvocationExpression.cs
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
using SW.Expressions.Collections;

namespace SW.Expressions.Expressions
{
    [ Serializable ]
    public class EditableInvocationExpression : EditableExpression
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="EditableInvocationExpression" /> class.
        /// </summary>
        /// <param name="info"> The info. </param>
        /// <param name="context"> The context. </param>
        public EditableInvocationExpression ( SerializationInfo info, StreamingContext context ) : base ( info, context )
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="EditableInvocationExpression" /> class.
        /// </summary>
        public EditableInvocationExpression ( )
        {
            Arguments = new EditableExpressionCollection ( );
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="EditableInvocationExpression" /> class.
        /// </summary>
        /// <param name="invocEx"> The invocation ex. </param>
        public EditableInvocationExpression ( InvocationExpression invocEx ) : this ( )
        {
            Expression = CreateEditableExpression ( invocEx.Expression );
            foreach ( Expression ex in invocEx.Arguments )
            {
                Arguments.Add ( CreateEditableExpression ( ex ) );
            }
        }

        /// <summary>
        ///     Gets or sets the expression.
        /// </summary>
        /// <value> The expression. </value>
        public EditableExpression Expression { get; set; }

        /// <summary>
        ///     Gets or sets the arguments.
        /// </summary>
        /// <value> The arguments. </value>
        public EditableExpressionCollection Arguments { get; set; }

        /// <summary>
        ///     Gets or sets the type of the node.
        /// </summary>
        /// <value> The type of the node. </value>
        public override ExpressionType NodeType
        {
            get { return ExpressionType.Invoke; }
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
            info.AddValue ( "Arguments", Arguments );
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
            Arguments = ( EditableExpressionCollection ) info.GetValue ( "Arguments", typeof ( EditableExpressionCollection ) );
        }

        public override Expression ToExpression ( )
        {
            return System.Linq.Expressions.Expression.Invoke ( Expression.ToExpression ( ), Arguments.GetExpressions ( ) );
        }
    }
}