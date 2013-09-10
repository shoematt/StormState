#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Expressions
// 
// File:	EditableParameterExpression.cs
// Author:	Matthew Shoemate
// Date:	08/27/2012
// 
// http://www.StormWorksSoftware.com
// 

#endregion

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.Serialization;

namespace SW.Expressions.Expressions
{
    [ Serializable ]
    public class EditableParameterExpression : EditableExpression
    {
        private static readonly Dictionary<string, ParameterExpression> UsableParameters = new Dictionary<string, ParameterExpression> ( );

        /// <summary>
        ///     Initializes a new instance of the <see cref="EditableParameterExpression" /> class.
        /// </summary>
        /// <param name="info"> The info. </param>
        /// <param name="context"> The context. </param>
        public EditableParameterExpression ( SerializationInfo info, StreamingContext context ) : base ( info, context )
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="EditableParameterExpression" /> class.
        /// </summary>
        public EditableParameterExpression ( )
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="EditableParameterExpression" /> class.
        /// </summary>
        /// <param name="parmEx">
        ///     The <see cref="ParameterExpression" /> ex.
        /// </param>
        public EditableParameterExpression ( ParameterExpression parmEx ) : this ( parmEx.Type, parmEx.Name )
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="EditableParameterExpression" /> class.
        /// </summary>
        /// <param name="type"> The type. </param>
        /// <param name="name"> The name. </param>
        public EditableParameterExpression ( Type type, string name ) : base ( type )
        {
            Name = name;
        }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value> The name. </value>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the type of the node.
        /// </summary>
        /// <value> The type of the node. </value>
        public override ExpressionType NodeType
        {
            get { return ExpressionType.Parameter; }
        }

        /// <summary>
        ///     Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.
        /// </summary>
        /// <param name="info"> The info. </param>
        /// <param name="context"> The context. </param>
        protected override void OnSerialize ( SerializationInfo info, StreamingContext context )
        {
            base.OnSerialize ( info, context );

            info.AddValue ( "Name", Name );
        }

        /// <summary>
        ///     Populates an object with data from a  <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed.
        /// </summary>
        /// <param name="info"> The info. </param>
        /// <param name="context"> The context. </param>
        protected override void OnDeserialize ( SerializationInfo info, StreamingContext context )
        {
            base.OnDeserialize ( info, context );

            Name = info.GetString ( "Name" );
        }

        /// <summary>
        ///     Creates the parameter.
        /// </summary>
        /// <param name="type"> The type. </param>
        /// <param name="name"> The name. </param>
        /// <returns> </returns>
        public static ParameterExpression CreateParameter ( Type type, string name )
        {
            ParameterExpression parameter;
            string key = type.AssemblyQualifiedName + Environment.NewLine + name;
            if ( UsableParameters.ContainsKey ( key ) )
            {
                parameter = UsableParameters [key];
            }
            else
            {
                parameter = Expression.Parameter ( type, name );
                UsableParameters.Add ( key, parameter );
            }
            return parameter;
        }

        public override Expression ToExpression ( )
        {
            return CreateParameter ( Type, Name );
        }
    }
}