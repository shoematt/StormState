#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Expressions
// 
// File:	EditableMemberBinding.cs
// Author:	Matthew Shoemate
// Date:	08/27/2012
// 
// http://www.StormWorksSoftware.com
// 

#endregion

using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;

using SW.Core.Extensions;

namespace SW.Expressions.Initializers
{
    [ Serializable ]
    public abstract class EditableMemberBinding
    {
        private MemberBindingType _bindingType;

        /// <summary>
        ///     Initializes a new instance of the <see cref="EditableElementInit" /> class.
        /// </summary>
        /// <param name="info"> The info. </param>
        /// <param name="context"> The context. </param>
        protected EditableMemberBinding ( SerializationInfo info, StreamingContext context )
        {
            RestoreObjectData ( info, context );
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="EditableMemberBinding" /> class.
        /// </summary>
        protected EditableMemberBinding ( )
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="EditableMemberBinding" /> class.
        /// </summary>
        /// <param name="type"> The type. </param>
        /// <param name="member"> The member. </param>
        protected EditableMemberBinding ( MemberBindingType type, MemberInfo member )
        {
            _bindingType = type;
            Member = member;
        }

        /// <summary>
        ///     Gets or sets the type of the binding.
        /// </summary>
        /// <value> The type of the binding. </value>
        public virtual MemberBindingType BindingType
        {
            get { return _bindingType; }
            set { _bindingType = value; }
        }

        /// <summary>
        ///     Gets or sets the member.
        /// </summary>
        /// <value> The member. </value>
        public MemberInfo Member { get; set; }

        public abstract MemberBinding ToMemberBinding ( );

        /// <summary>
        ///     Creates the editable member binding.
        /// </summary>
        /// <param name="member"> The member. </param>
        /// <returns> </returns>
        public static EditableMemberBinding CreateEditableMemberBinding ( MemberBinding member )
        {
            if ( member is MemberAssignment )
            {
                return new EditableMemberAssignment ( member as MemberAssignment );
            }
            if ( member is MemberListBinding )
            {
                return new EditableMemberListBinding ( member as MemberListBinding );
            }
            if ( member is MemberMemberBinding )
            {
                return new EditableMemberMemberBinding ( member as MemberMemberBinding );
            }
            return null;
        }

        /// <summary>
        ///     Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.
        /// </summary>
        /// <param name="info">
        ///     The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.
        /// </param>
        /// <param name="context">
        ///     The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext" /> ) for this serialization.
        /// </param>
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
        public void GetObjectData ( SerializationInfo info, StreamingContext context )
        {
            info.AddValueWithType ( "BindingType", BindingType );
            info.AddValueWithType ( "Member", Member );

            OnSerialize ( info, context );
        }

        /// <summary>
        ///     Restores the object data.
        /// </summary>
        /// <param name="info"> The info. </param>
        /// <param name="context"> The context. </param>
        private void RestoreObjectData ( SerializationInfo info, StreamingContext context )
        {
            BindingType = ( MemberBindingType ) info.GetValueWithType ( "BindingType" );
            Member = ( MemberInfo ) info.GetValueWithType ( "Member" );

            OnDeserialize ( info, context );
        }

        /// <summary>
        ///     Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.
        /// </summary>
        /// <param name="info"> The info. </param>
        /// <param name="context"> The context. </param>
        protected virtual void OnSerialize ( SerializationInfo info, StreamingContext context )
        {
        }

        /// <summary>
        ///     Populates an object with data from a  <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed.
        /// </summary>
        /// <param name="info"> The info. </param>
        /// <param name="context"> The context. </param>
        protected virtual void OnDeserialize ( SerializationInfo info, StreamingContext context )
        {
        }
    }
}