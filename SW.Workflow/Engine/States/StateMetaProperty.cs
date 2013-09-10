#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Workflow
// 
// File:	StateMetaProperty.cs
// Author:	Matthew Shoemate
// Date:	08/27/2012
// 
// http://www.StormWorksSoftware.com
// 

#endregion

using System;
using System.Runtime.InteropServices;

using SW.Workflow.States;

namespace SW.Workflow.Engine.States
{
    [ Serializable ]
    public class StateMetaProperty : IStateMetaProperty
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="StateMetaProperty" /> class.
        /// </summary>
        public StateMetaProperty ( )
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="StateMetaProperty" /> class.
        /// </summary>
        /// <param name="id"> The id. </param>
        public StateMetaProperty ( Guid id ) : this ( )
        {
            ID = id;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="StateMetaProperty" /> class.
        /// </summary>
        /// <param name="id"> The id. </param>
        /// <param name="valueType"> Type of the value. </param>
        /// <param name="defaultValue"> The default value. </param>
        public StateMetaProperty ( Guid id, Type valueType, [ Optional ] object defaultValue ) : this ( id )
        {
            InstanceID = Guid.NewGuid ( );

            Type = valueType;
            Default = defaultValue;
        }

        #region IStateMetaProperty Members

        /// <summary>
        ///     Gets the ID.
        /// </summary>
        public Guid ID { get; private set; }

        /// <summary>
        ///     Gets the instance ID.
        /// </summary>
        public Guid InstanceID { get; private set; }

        /// <summary>
        ///     Gets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the value.
        /// </summary>
        /// <value> The value. </value>
        public object Default { get; set; }

        /// <summary>
        ///     Gets or sets the type.
        /// </summary>
        /// <value> The type. </value>
        public Type Type { get; set; }

        #endregion
    }
}