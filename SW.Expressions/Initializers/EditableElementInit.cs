#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Expressions
// 
// File:	EditableElementInit.cs
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
using SW.Expressions.Collections;
using SW.Expressions.Expressions;

namespace SW.Expressions.Initializers
{
    [ Serializable ]
    public class EditableElementInit : ISerializable
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="EditableElementInit" /> class.
        /// </summary>
        /// <param name="info"> The info. </param>
        /// <param name="context"> The context. </param>
        public EditableElementInit ( SerializationInfo info, StreamingContext context )
        {
            RestoreObjectData ( info, context );
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="EditableElementInit" /> class.
        /// </summary>
        public EditableElementInit ( )
        {
            Arguments = new EditableExpressionCollection ( );
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="EditableElementInit" /> class.
        /// </summary>
        /// <param name="elmInit">
        ///     The <see cref="ElementInit" /> .
        /// </param>
        public EditableElementInit ( ElementInit elmInit ) : this ( )
        {
            AddMethod = elmInit.AddMethod;
            foreach ( Expression ex in elmInit.Arguments )
            {
                Arguments.Add ( EditableExpression.CreateEditableExpression ( ex ) );
            }
        }

        /// <summary>
        ///     Gets or sets the arguments.
        /// </summary>
        /// <value> The arguments. </value>
        public EditableExpressionCollection Arguments { get; set; }

        /// <summary>
        ///     Gets or sets the add method.
        /// </summary>
        /// <value> The add method. </value>
        public MethodInfo AddMethod { get; set; }

        #region ISerializable Members

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
            info.AddValueWithType ( "Type", Arguments );
            info.AddValueWithType ( "AddMethod", AddMethod );

            OnSerialize ( info, context );
        }

        #endregion

        public ElementInit ToElementInit ( )
        {
            return Expression.ElementInit ( AddMethod, Arguments.GetExpressions ( ) );
        }

        /// <summary>
        ///     Restores the object data.
        /// </summary>
        /// <param name="info"> The info. </param>
        /// <param name="context"> The context. </param>
        private void RestoreObjectData ( SerializationInfo info, StreamingContext context )
        {
            Arguments = ( EditableExpressionCollection ) info.GetValueWithType ( "Type" );
            AddMethod = ( MethodInfo ) info.GetValueWithType ( "AddMethod" );

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