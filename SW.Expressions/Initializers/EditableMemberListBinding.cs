#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Expressions
// 
// File:	EditableMemberListBinding.cs
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
    public class EditableMemberListBinding : EditableMemberBinding
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="EditableMemberListBinding" /> class.
        /// </summary>
        /// <param name="info"> The info. </param>
        /// <param name="context"> The context. </param>
        public EditableMemberListBinding ( SerializationInfo info, StreamingContext context ) : base ( info, context )
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="EditableMemberListBinding" /> class.
        /// </summary>
        public EditableMemberListBinding ( )
        {
            Initializers = new EditableElementInitCollection ( );
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="EditableMemberListBinding" /> class.
        /// </summary>
        /// <param name="member"> The member. </param>
        public EditableMemberListBinding ( MemberListBinding member ) : base ( member.BindingType, member.Member )
        {
            Initializers = new EditableElementInitCollection ( );
            foreach ( ElementInit e in member.Initializers )
            {
                Initializers.Add ( new EditableElementInit ( e ) );
            }
        }

        /// <summary>
        ///     Gets or sets the initializers.
        /// </summary>
        /// <value> The initializers. </value>
        public EditableElementInitCollection Initializers { get; set; }

        /// <summary>
        ///     Gets or sets the type of the binding.
        /// </summary>
        /// <value> The type of the binding. </value>
        public override MemberBindingType BindingType
        {
            get { return MemberBindingType.ListBinding; }
        }

        /// <summary>
        ///     Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.
        /// </summary>
        /// <param name="info"> The info. </param>
        /// <param name="context"> The context. </param>
        protected override void OnSerialize ( SerializationInfo info, StreamingContext context )
        {
            base.OnSerialize ( info, context );

            info.AddValueWithType ( "Initializers", Initializers );
        }

        /// <summary>
        ///     Populates an object with data from a  <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed.
        /// </summary>
        /// <param name="info"> The info. </param>
        /// <param name="context"> The context. </param>
        protected override void OnDeserialize ( SerializationInfo info, StreamingContext context )
        {
            base.OnDeserialize ( info, context );

            Initializers = ( EditableElementInitCollection ) info.GetValueWithType ( "Initializers" );
        }

        public override MemberBinding ToMemberBinding ( )
        {
            return Expression.ListBind ( Member, Initializers.GetElementsInit ( ) );
        }
    }
}