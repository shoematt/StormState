#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Workflow
// 
// File:	StateEventProxyBase.cs
// Author:	Matthew Shoemate
// Date:	08/27/2012
// 
// http://www.StormWorksSoftware.com
// 

#endregion

using System;
using System.ComponentModel;
using System.Xml.Serialization;

using SW.Workflow.Logic;

using StructureMap.Attributes;

using IContainer = StructureMap.IContainer;

namespace SW.Workflow.Behavior
{
    [ Serializable ]
    public abstract class StateEventProxyBase : IStateEventProxy
    {
        [ NonSerialized ] [ XmlIgnore ] private IContainer _container;

        /// <summary>
        ///     Gets or sets the container.
        /// </summary>
        /// <value> The container. </value>
        [ XmlIgnore ]
        [ SetterProperty ]
        [ Browsable ( false ) ]
        public virtual IContainer Container
        {
            get { return _container; }
            set
            {
                _container = value;

                OnContainerApplied ( );
            }
        }

        /// <summary>
        ///     Gets the name.
        /// </summary>
        [ Browsable ( false ) ]
        public string Name
        {
            get
            {
                if ( Attribute.IsDefined ( GetType ( ), typeof ( DisplayNameAttribute ) ) )
                {
                    DisplayNameAttribute attr = ( DisplayNameAttribute ) Attribute.GetCustomAttribute ( GetType ( ), typeof ( DisplayNameAttribute ) );

                    return attr.DisplayName;
                }

                return GetType ( )
                    .Name;
            }
        }

        /// <summary>
        ///     Gets or sets the text.
        /// </summary>
        /// <value> The text. </value>
        public virtual string Text { get; set; }

        /// <summary>
        ///     Called when [container applied].
        /// </summary>
        protected virtual void OnContainerApplied ( )
        {
        }

        /// <summary>
        ///     Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        ///     A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString ( )
        {
            return Name;
        }

        #region Nested type: MetaCategoryClassifications

        public static class MetaCategoryClassifications
        {
            public const string InterfaceAction = "Interface Action";
            public const string LogicalAction = "Logical Action";
            public const string DataAction = "Data Action";
            public const string DebugAction = "Debug Action";

            public const string ContainerAction = "Container Action";
        }

        #endregion
    }
}