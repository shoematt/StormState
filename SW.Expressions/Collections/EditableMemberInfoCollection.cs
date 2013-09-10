#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Expressions
// 
// File:	EditableMemberInfoCollection.cs
// Author:	Matthew Shoemate
// Date:	08/27/2012
// 
// http://www.StormWorksSoftware.com
// 

#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SW.Expressions.Collections
{
    [ Serializable ]
    public class EditableMemberInfoCollection : List<MemberInfo>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="EditableMemberInfoCollection" /> class.
        /// </summary>
        public EditableMemberInfoCollection ( )
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="EditableMemberInfoCollection" /> class.
        /// </summary>
        /// <param name="source"> The source. </param>
        public EditableMemberInfoCollection ( IEnumerable<MemberInfo> source )
        {
            if ( source == null )
            {
                return;
            }

            foreach ( MemberInfo ex in source )
            {
                Add ( ex );
            }
        }

        /// <summary>
        ///     Gets the members.
        /// </summary>
        /// <returns> </returns>
        public IEnumerable<MemberInfo> GetMembers ( )
        {
            MemberInfo member = null;
            return this.Select ( editEx => member );
        }
    }
}