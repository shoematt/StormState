#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Expressions
// 
// File:	EditableMemberBindingCollection.cs
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

using SW.Expressions.Initializers;

namespace SW.Expressions.Collections
{
    [ Serializable ]
    public class EditableMemberBindingCollection : List<EditableMemberBinding>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="EditableMemberBindingCollection" /> class.
        /// </summary>
        public EditableMemberBindingCollection ( )
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="EditableMemberBindingCollection" /> class.
        /// </summary>
        /// <param name="source"> The source. </param>
        public EditableMemberBindingCollection ( IEnumerable<EditableMemberBinding> source ) : base ( source )
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="EditableMemberBindingCollection" /> class.
        /// </summary>
        /// <param name="source"> The source. </param>
        public EditableMemberBindingCollection ( IEnumerable<MemberBinding> source )
        {
            foreach ( MemberBinding ex in source )
            {
                Add ( EditableMemberBinding.CreateEditableMemberBinding ( ex ) );
            }
        }

        /// <summary>
        ///     Gets the member bindings.
        /// </summary>
        /// <returns> </returns>
        public IEnumerable<MemberBinding> GetMemberBindings ( )
        {
            return this.Select ( editEx => editEx.ToMemberBinding ( ) );
        }
    }
}