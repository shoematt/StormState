#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Expressions
// 
// File:	EditableNewExpression.cs
// Author:	Matthew Shoemate
// Date:	08/27/2012
// 
// http://www.StormWorksSoftware.com
// 

#endregion

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;

using SW.Core.Extensions;
using SW.Expressions.Collections;

namespace SW.Expressions.Expressions
{
    [ Serializable ]
    public class EditableNewExpression : EditableExpression
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="EditableNewExpression" /> class.
        /// </summary>
        /// <param name="info"> The info. </param>
        /// <param name="context"> The context. </param>
        public EditableNewExpression ( SerializationInfo info, StreamingContext context ) : base ( info, context )
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="EditableNewExpression" /> class.
        /// </summary>
        public EditableNewExpression ( )
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="EditableNewExpression" /> class.
        /// </summary>
        /// <param name="newEx"> The new ex. </param>
        public EditableNewExpression ( NewExpression newEx ) : this ( newEx.Constructor, new EditableExpressionCollection ( newEx.Arguments ), newEx.Members, newEx.Type )
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="EditableNewExpression" /> class.
        /// </summary>
        /// <param name="constructor"> The constructor. </param>
        /// <param name="arguments"> The arguments. </param>
        /// <param name="members"> The members. </param>
        /// <param name="type"> The type. </param>
        public EditableNewExpression ( ConstructorInfo constructor, IEnumerable<EditableExpression> arguments, IEnumerable<MemberInfo> members, Type type ) : this ( constructor, new EditableExpressionCollection ( arguments ), members, type )
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="EditableNewExpression" /> class.
        /// </summary>
        /// <param name="constructor"> The constructor. </param>
        /// <param name="arguments"> The arguments. </param>
        /// <param name="members"> The members. </param>
        /// <param name="type"> The type. </param>
        public EditableNewExpression ( ConstructorInfo constructor, EditableExpressionCollection arguments, IEnumerable<MemberInfo> members, Type type ) : base ( type )
        {
            Arguments = arguments;
            Constructor = constructor;
            Members = new EditableMemberInfoCollection ( members );
        }

        /// <summary>
        ///     Gets or sets the constructor.
        /// </summary>
        /// <value> The constructor. </value>
        public ConstructorInfo Constructor { get; set; }

        /// <summary>
        ///     Gets or sets the arguments.
        /// </summary>
        /// <value> The arguments. </value>
        public EditableExpressionCollection Arguments { get; set; }

        /// <summary>
        ///     Gets or sets the members.
        /// </summary>
        /// <value> The members. </value>
        public EditableMemberInfoCollection Members { get; set; }

        /// <summary>
        ///     Gets or sets the type of the node.
        /// </summary>
        /// <value> The type of the node. </value>
        public override ExpressionType NodeType
        {
            get { return ExpressionType.New; }
        }

        /// <summary>
        ///     Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.
        /// </summary>
        /// <param name="info"> The info. </param>
        /// <param name="context"> The context. </param>
        protected override void OnSerialize ( SerializationInfo info, StreamingContext context )
        {
            base.OnSerialize ( info, context );

            info.AddValueWithType ( "Constructor", Constructor );
            info.AddValueWithType ( "Members", Members );
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

            Constructor = ( ConstructorInfo ) info.GetValueWithType ( "Constructor" );
            Members = ( EditableMemberInfoCollection ) info.GetValueWithType ( "Members" );
            Arguments = ( EditableExpressionCollection ) info.GetValueWithType ( "Arguments" );
        }

        public override Expression ToExpression ( )
        {
            return Constructor != null ? Expression.New ( Constructor, Arguments.GetExpressions ( ) ) : Expression.New ( Type );
        }
    }
}