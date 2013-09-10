#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Expressions
// 
// File:	EditableMemberAssignment.cs
// Author:	Matthew Shoemate
// Date:	08/27/2012
// 
// http://www.StormWorksSoftware.com
// 

#endregion

using System;
using System.Linq.Expressions;

using SW.Expressions.Expressions;

namespace SW.Expressions.Initializers
{
    [ Serializable ]
    public class EditableMemberAssignment : EditableMemberBinding
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="EditableMemberAssignment" /> class.
        /// </summary>
        public EditableMemberAssignment ( )
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="EditableMemberAssignment" /> class.
        /// </summary>
        /// <param name="member"> The member. </param>
        public EditableMemberAssignment ( MemberAssignment member ) : base ( member.BindingType, member.Member )
        {
            Expression = EditableExpression.CreateEditableExpression ( member.Expression );
        }

        /// <summary>
        ///     Gets or sets the expression.
        /// </summary>
        /// <value> The expression. </value>
        public EditableExpression Expression { get; set; }

        /// <summary>
        ///     Gets or sets the type of the binding.
        /// </summary>
        /// <value> The type of the binding. </value>
        public override MemberBindingType BindingType
        {
            get { return MemberBindingType.Assignment; }
            set { }
        }

        public override MemberBinding ToMemberBinding ( )
        {
            return System.Linq.Expressions.Expression.Bind ( Member, Expression.ToExpression ( ) );
        }
    }
}