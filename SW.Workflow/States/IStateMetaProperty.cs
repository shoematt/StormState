#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Workflow
// 
// File:	IStateMetaProperty.cs
// Author:	Matthew Shoemate
// Date:	08/27/2012
// 
// http://www.StormWorksSoftware.com
// 

#endregion

using System;

namespace SW.Workflow.States
{
    public interface IStateMetaProperty
    {
        /// <summary>
        ///     Gets the ID.
        /// </summary>
        Guid ID { get; }

        /// <summary>
        ///     Gets the instance ID.
        /// </summary>
        Guid InstanceID { get; }

        /// <summary>
        ///     Gets the name.
        /// </summary>
        string Name { get; }

        /// <summary>
        ///     Gets or sets the value.
        /// </summary>
        /// <value> The value. </value>
        object Default { get; set; }

        /// <summary>
        ///     Gets or sets the type.
        /// </summary>
        /// <value> The type. </value>
        Type Type { get; set; }
    }
}