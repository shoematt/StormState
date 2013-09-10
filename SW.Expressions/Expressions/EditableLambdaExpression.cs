#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Expressions
// 
// File:	EditableLambdaExpression.cs
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
using SW.Expressions.Extensions;

namespace SW.Expressions.Expressions
{
    [ Serializable ]
    public class EditableLambdaExpression : EditableExpression
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="EditableLambdaExpression" /> class.
        /// </summary>
        /// <param name="info"> The info. </param>
        /// <param name="context"> The context. </param>
        public EditableLambdaExpression ( SerializationInfo info, StreamingContext context ) : base ( info, context )
        {
        }

        // Properties
        /// <summary>
        ///     Initializes a new instance of the <see cref="EditableLambdaExpression" /> class.
        /// </summary>
        public EditableLambdaExpression ( )
        {
            Parameters = new EditableExpressionCollection ( );
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="EditableLambdaExpression" /> class.
        /// </summary>
        /// <param name="lambEx"> The lamb ex. </param>
        public EditableLambdaExpression ( LambdaExpression lambEx ) : base ( lambEx.Type )
        {
            Parameters = new EditableExpressionCollection ( );
            Body = CreateEditableExpression ( lambEx.Body );
            foreach ( ParameterExpression param in lambEx.Parameters )
            {
                Parameters.Add ( CreateEditableExpression ( param ) );
            }
        }

        /// <summary>
        ///     Gets or sets the body.
        /// </summary>
        /// <value> The body. </value>
        public EditableExpression Body { get; set; }

        /// <summary>
        ///     Gets or sets the parameters.
        /// </summary>
        /// <value> The parameters. </value>
        public EditableExpressionCollection Parameters { get; set; }

        /// <summary>
        ///     Gets or sets the type of the node.
        /// </summary>
        /// <value> The type of the node. </value>
        public override ExpressionType NodeType
        {
            get { return ExpressionType.Lambda; }
        }

        /// <summary>
        ///     Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.
        /// </summary>
        /// <param name="info"> The info. </param>
        /// <param name="context"> The context. </param>
        protected override void OnSerialize ( SerializationInfo info, StreamingContext context )
        {
            base.OnSerialize ( info, context );

            info.AddValueWithType ( "Body", Body );
            info.AddValue ( "Parameters", Parameters );
        }

        /// <summary>
        ///     Populates an object with data from a  <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed.
        /// </summary>
        /// <param name="info"> The info. </param>
        /// <param name="context"> The context. </param>
        protected override void OnDeserialize ( SerializationInfo info, StreamingContext context )
        {
            base.OnDeserialize ( info, context );

            Body = ( EditableExpression ) info.GetValueWithType ( "Body" );
            Parameters = ( EditableExpressionCollection ) info.GetValue ( "Parameters", typeof ( EditableExpressionCollection ) );
        }

        public override Expression ToExpression ( )
        {
            Expression body = Body.ToExpression ( );
            List<ParameterExpression> parameters = new List<ParameterExpression> ( Parameters.GetParameterExpressions ( ) );

            var bodyParameters = from edX in body.Nodes ( ) where edX is ParameterExpression select edX;
            for ( int i = 0; i < parameters.Count; i++ )
            {
                int i1 = i;
                var matchingParm = from parm in bodyParameters where ( ( ParameterExpression ) parm ).Name == parameters [i1].Name && ( parm as ParameterExpression ).Type == parameters [i1].Type select parm as ParameterExpression;
                if ( matchingParm.Count ( ) == 1 )
                {
                    parameters [i] = matchingParm.First ( );
                }
            }

            return Expression.Lambda ( Type, body, parameters );
        }
    }
}