#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Expressions
// 
// File:	EditableMemberInitExpression.cs
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
    public class EditableMemberInitExpression : EditableExpression
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="EditableMemberInitExpression" /> class.
        /// </summary>
        /// <param name="info"> The info. </param>
        /// <param name="context"> The context. </param>
        public EditableMemberInitExpression ( SerializationInfo info, StreamingContext context ) : base ( info, context )
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="EditableMemberInitExpression" /> class.
        /// </summary>
        public EditableMemberInitExpression ( )
        {
            Bindings = new EditableMemberBindingCollection ( );
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="EditableMemberInitExpression" /> class.
        /// </summary>
        /// <param name="membInit">
        ///     The <see cref="MemberInitExpression" /> .
        /// </param>
        public EditableMemberInitExpression ( MemberInitExpression membInit ) : this ( CreateEditableExpression ( membInit.NewExpression ) as EditableNewExpression, membInit.Bindings )
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="EditableMemberInitExpression" /> class.
        /// </summary>
        /// <param name="newEx"> The new ex. </param>
        /// <param name="bindings"> The bindings. </param>
        public EditableMemberInitExpression ( EditableNewExpression newEx, IEnumerable<MemberBinding> bindings )
        {
            Bindings = new EditableMemberBindingCollection ( bindings );
            NewExpression = newEx;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="EditableMemberInitExpression" /> class.
        /// </summary>
        /// <param name="newRawEx"> The new raw ex. </param>
        /// <param name="bindings"> The bindings. </param>
        public EditableMemberInitExpression ( NewExpression newRawEx, IEnumerable<MemberBinding> bindings ) : this ( CreateEditableExpression ( newRawEx ) as EditableNewExpression, bindings )
        {
        }

        /// <summary>
        ///     Gets or sets the new expression.
        /// </summary>
        /// <value> The new expression. </value>
        public EditableNewExpression NewExpression { get; set; }

        /// <summary>
        ///     Gets or sets the bindings.
        /// </summary>
        /// <value> The bindings. </value>
        public EditableMemberBindingCollection Bindings { get; set; }

        /// <summary>
        ///     Gets or sets the type of the node.
        /// </summary>
        /// <value> The type of the node. </value>
        public override ExpressionType NodeType
        {
            get { return ExpressionType.MemberInit; }
        }

        /// <summary>
        ///     Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.
        /// </summary>
        /// <param name="info"> The info. </param>
        /// <param name="context"> The context. </param>
        protected override void OnSerialize ( SerializationInfo info, StreamingContext context )
        {
            base.OnSerialize ( info, context );

            info.AddValueWithType ( "NewExpression", NewExpression );
            info.AddValueWithType ( "Bindings", Bindings );
        }

        /// <summary>
        ///     Populates an object with data from a  <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed.
        /// </summary>
        /// <param name="info"> The info. </param>
        /// <param name="context"> The context. </param>
        protected override void OnDeserialize ( SerializationInfo info, StreamingContext context )
        {
            base.OnDeserialize ( info, context );

            NewExpression = ( EditableNewExpression ) info.GetValueWithType ( "NewExpression" );
            Bindings = ( EditableMemberBindingCollection ) info.GetValueWithType ( "Bindings" );
        }

        public override Expression ToExpression ( )
        {
            return Expression.MemberInit ( ( NewExpression ) NewExpression.ToExpression ( ), Bindings.GetMemberBindings ( ) );
        }
    }
}