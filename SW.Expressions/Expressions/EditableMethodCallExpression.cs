#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Expressions
// 
// File:	EditableMethodCallExpression.cs
// Author:	Matthew Shoemate
// Date:	08/27/2012
// 
// http://www.StormWorksSoftware.com
// 

#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;

using SW.Core.Extensions;
using SW.Expressions.Collections;

namespace SW.Expressions.Expressions
{
    [ Serializable ]
    public class EditableMethodCallExpression : EditableExpression
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="EditableMethodCallExpression" /> class.
        /// </summary>
        /// <param name="info"> The info. </param>
        /// <param name="context"> The context. </param>
        public EditableMethodCallExpression ( SerializationInfo info, StreamingContext context ) : base ( info, context )
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="EditableMethodCallExpression" /> class.
        /// </summary>
        public EditableMethodCallExpression ( )
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="EditableMethodCallExpression" /> class.
        /// </summary>
        /// <param name="arguments"> The arguments. </param>
        /// <param name="method"> The method. </param>
        /// <param name="theObject"> The object. </param>
        /// <param name="nodeType"> Type of the node. </param>
        public EditableMethodCallExpression ( EditableExpressionCollection arguments, MethodInfo method, EditableExpression theObject, ExpressionType nodeType )
        {
            Arguments = arguments;
            Method = method;
            Object = theObject;
            NodeType = nodeType;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="EditableMethodCallExpression" /> class.
        /// </summary>
        /// <param name="arguments"> The arguments. </param>
        /// <param name="method"> The method. </param>
        /// <param name="theObject"> The object. </param>
        /// <param name="nodeType"> Type of the node. </param>
        public EditableMethodCallExpression ( IEnumerable<EditableExpression> arguments, MethodInfo method, Expression theObject, ExpressionType nodeType )
        {
            Arguments = new EditableExpressionCollection ( arguments );
            Method = method;
            Object = CreateEditableExpression ( theObject );
            NodeType = nodeType;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="EditableMethodCallExpression" /> class.
        /// </summary>
        /// <param name="callEx"> The call ex. </param>
        public EditableMethodCallExpression ( MethodCallExpression callEx )
        {
            Arguments = new EditableExpressionCollection ( callEx.Arguments );
            Method = callEx.Method;
            Object = CreateEditableExpression ( callEx.Object );
            NodeType = callEx.NodeType;
        }

        /// <summary>
        ///     Gets or sets the arguments.
        /// </summary>
        /// <value> The arguments. </value>
        public EditableExpressionCollection Arguments { get; set; }

        /// <summary>
        ///     Gets or sets the method.
        /// </summary>
        /// <value> The method. </value>
        public MethodInfo Method { get; set; }

        /// <summary>
        ///     Gets or sets the object.
        /// </summary>
        /// <value> The object. </value>
        public EditableExpression Object { get; set; }

        /// <summary>
        ///     Gets or sets the type of the node.
        /// </summary>
        /// <value> The type of the node. </value>
        public override sealed ExpressionType NodeType { get; set; }

        /// <summary>
        ///     Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.
        /// </summary>
        /// <param name="info"> The info. </param>
        /// <param name="context"> The context. </param>
        protected override void OnSerialize ( SerializationInfo info, StreamingContext context )
        {
            base.OnSerialize ( info, context );

            info.AddValueWithType ( "Method", Method );
            info.AddValueWithType ( "Object", Object );
            info.AddValueWithType ( "Arguments", Arguments );
        }

        /// <summary>
        ///     Populates an object with data from a  <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed.
        /// </summary>
        /// <param name="info"> The info. </param>
        /// <param name="context"> The context. </param>
        protected override void OnDeserialize ( SerializationInfo info, StreamingContext context )
        {
            base.OnDeserialize ( info, context );

            Method = ( MethodInfo ) info.GetValueWithType ( "Method" );
            Object = ( EditableExpression ) info.GetValueWithType ( "Object" );
            Arguments = ( EditableExpressionCollection ) info.GetValueWithType ( "Arguments" );
        }

        public override Expression ToExpression ( )
        {
            Expression instanceExpression = null;
            if ( Object != null )
            {
                instanceExpression = Object.ToExpression ( );
            }

            return Expression.Call ( instanceExpression, Method, Arguments.GetExpressions ( )
                                                                          .ToArray ( ) );
        }
    }
}