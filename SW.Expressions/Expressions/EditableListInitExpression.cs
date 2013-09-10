#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Expressions
// 
// File:	EditableListInitExpression.cs
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
using System.Runtime.Serialization;

using SW.Core.Extensions;
using SW.Expressions.Collections;
using SW.Expressions.Initializers;

namespace SW.Expressions.Expressions
{
    [ Serializable ]
    public class EditableListInitExpression : EditableExpression
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="EditableListInitExpression" /> class.
        /// </summary>
        /// <param name="info"> The info. </param>
        /// <param name="context"> The context. </param>
        public EditableListInitExpression ( SerializationInfo info, StreamingContext context ) : base ( info, context )
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="EditableListInitExpression" /> class.
        /// </summary>
        public EditableListInitExpression ( )
        {
            Initializers = new EditableElementInitCollection ( );
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="EditableListInitExpression" /> class.
        /// </summary>
        /// <param name="listInit">
        ///     The <see cref="ListInitExpression" /> .
        /// </param>
        public EditableListInitExpression ( ListInitExpression listInit ) : this ( )
        {
            NewExpression = CreateEditableExpression ( listInit.NewExpression );
            foreach ( ElementInit e in listInit.Initializers )
            {
                Initializers.Add ( new EditableElementInit ( e ) );
            }
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="EditableListInitExpression" /> class.
        /// </summary>
        /// <param name="newEx"> The new ex. </param>
        /// <param name="initializers"> The initializers. </param>
        public EditableListInitExpression ( EditableExpression newEx, IEnumerable<EditableElementInit> initializers )
        {
            Initializers = new EditableElementInitCollection ( initializers );
            NewExpression = newEx;
        }

        /// <summary>
        ///     Gets or sets the new expression.
        /// </summary>
        /// <value> The new expression. </value>
        public EditableExpression NewExpression { get; set; }

        /// <summary>
        ///     Gets or sets the initializers.
        /// </summary>
        /// <value> The initializers. </value>
        public EditableElementInitCollection Initializers { get; set; }

        /// <summary>
        ///     Gets or sets the type of the node.
        /// </summary>
        /// <value> The type of the node. </value>
        public override ExpressionType NodeType
        {
            get { return ExpressionType.ListInit; }
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
            info.AddValue ( "Initializers", Initializers );
        }

        /// <summary>
        ///     Populates an object with data from a  <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed.
        /// </summary>
        /// <param name="info"> The info. </param>
        /// <param name="context"> The context. </param>
        protected override void OnDeserialize ( SerializationInfo info, StreamingContext context )
        {
            base.OnDeserialize ( info, context );

            NewExpression = ( EditableExpression ) info.GetValueWithType ( "NewExpression" );
            Initializers = ( EditableElementInitCollection ) info.GetValue ( "Initializers", typeof ( EditableElementInitCollection ) );
        }

        public override Expression ToExpression ( )
        {
            return Expression.ListInit ( ( NewExpression ) NewExpression.ToExpression ( ), Initializers.GetElementsInit ( )
                                                                                                       .ToArray ( ) );
        }
    }
}