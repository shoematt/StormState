#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Expressions
// 
// File:	EditableMemberMemberBinding.cs
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

namespace SW.Expressions.Initializers
{
    [ Serializable ]
    public class EditableMemberMemberBinding : EditableMemberBinding
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="EditableMemberMemberBinding" /> class.
        /// </summary>
        /// <param name="info"> The info. </param>
        /// <param name="context"> The context. </param>
        public EditableMemberMemberBinding ( SerializationInfo info, StreamingContext context ) : base ( info, context )
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="EditableMemberMemberBinding" /> class.
        /// </summary>
        public EditableMemberMemberBinding ( )
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="EditableMemberMemberBinding" /> class.
        /// </summary>
        /// <param name="member"> The member. </param>
        public EditableMemberMemberBinding ( MemberMemberBinding member ) : base ( member.BindingType, member.Member )
        {
            Bindings = new EditableMemberBindingCollection ( member.Bindings );
        }

        /// <summary>
        ///     Gets or sets the bindings.
        /// </summary>
        /// <value> The bindings. </value>
        public EditableMemberBindingCollection Bindings { get; set; }

        /// <summary>
        ///     Gets or sets the type of the binding.
        /// </summary>
        /// <value> The type of the binding. </value>
        public override MemberBindingType BindingType
        {
            get { return MemberBindingType.MemberBinding; }
        }

        /// <summary>
        ///     Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.
        /// </summary>
        /// <param name="info"> The info. </param>
        /// <param name="context"> The context. </param>
        protected override void OnSerialize ( SerializationInfo info, StreamingContext context )
        {
            base.OnSerialize ( info, context );

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

            Bindings = ( EditableMemberBindingCollection ) info.GetValueWithType ( "Bindings" );
        }

        public override MemberBinding ToMemberBinding ( )
        {
            return Expression.MemberBind ( Member, Bindings.GetMemberBindings ( ) );
        }
    }
}