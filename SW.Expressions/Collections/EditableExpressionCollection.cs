#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Expressions
// 
// File:	EditableExpressionCollection.cs
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

using SW.Expressions.Expressions;

namespace SW.Expressions.Collections
{
    [ Serializable ]
    public class EditableExpressionCollection : List<EditableExpression>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="EditableExpressionCollection" /> class.
        /// </summary>
        public EditableExpressionCollection ( )
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="EditableExpressionCollection" /> class.
        /// </summary>
        /// <param name="source"> The source. </param>
        public EditableExpressionCollection ( IEnumerable<EditableExpression> source ) : base ( source )
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="EditableExpressionCollection" /> class.
        /// </summary>
        /// <param name="source"> The source. </param>
        public EditableExpressionCollection ( IEnumerable<Expression> source )
        {
            foreach ( Expression ex in source )
            {
                Add ( EditableExpression.CreateEditableExpression ( ex ) );
            }
        }

        /// <summary>
        ///     Gets the expressions.
        /// </summary>
        /// <returns> </returns>
        public IEnumerable<Expression> GetExpressions ( )
        {
            return this.Select ( editEx => editEx.ToExpression ( ) );
        }

        /// <summary>
        ///     Gets the parameter expressions.
        /// </summary>
        /// <returns> </returns>
        public IEnumerable<ParameterExpression> GetParameterExpressions ( )
        {
            return this.OfType<EditableParameterExpression> ( )
                       .Select ( editEx => editEx )
                       .Select ( parmEx => new EditableParameterExpression ( parmEx.Type, parmEx.Name ).ToExpression ( ) as ParameterExpression );
        }
    }
}