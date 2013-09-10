#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Expressions
// 
// File:	EditableElementInitCollection.cs
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
    public class EditableElementInitCollection : List<EditableElementInit>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="EditableElementInitCollection" /> class.
        /// </summary>
        public EditableElementInitCollection ( )
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="EditableElementInitCollection" /> class.
        /// </summary>
        /// <param name="source"> The source. </param>
        public EditableElementInitCollection ( IEnumerable<EditableElementInit> source ) : base ( source )
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="EditableElementInitCollection" /> class.
        /// </summary>
        /// <param name="source"> The source. </param>
        public EditableElementInitCollection ( IEnumerable<ElementInit> source )
        {
            foreach ( ElementInit ex in source )
            {
                Add ( new EditableElementInit ( ex ) );
            }
        }

        /// <summary>
        ///     Gets the initialized elements.
        /// </summary>
        /// <returns> </returns>
        public IEnumerable<ElementInit> GetElementsInit ( )
        {
            return this.Select ( editEx => editEx.ToElementInit ( ) );
        }
    }
}