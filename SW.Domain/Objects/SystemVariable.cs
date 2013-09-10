using System;
using System.ComponentModel;
using Orca.Core;
using Orca.Domain.Objects.BaseObjects;

namespace Orca.Domain.Objects
{
    [ Serializable ]
    public sealed class SystemVariable : DomainObject
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref = "SystemVariable" /> class.
        /// </summary>
        public SystemVariable( )
        {
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref = "SystemVariable" /> class.
        /// </summary>
        /// <param name = "type">The type.</param>
        public SystemVariable( Type type )
        {
            SystemType = TypeEnumerator.GetQualifiedNamesFor( type );
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [ExpressionVisible(false)]
        public override string Name
        {
            get
            {
                return base.Name;
            }
            set
            {
                base.Name = value;
            }
        }

        /// <summary>
        ///   Gets the type of the system.
        /// </summary>
        /// <value>The type of the system.</value>
        [ Browsable( true ) ]
        [ ExpressionVisible( false ) ]
        public string SystemType { get; internal set; }

        /// <summary>
        ///   Gets the type.
        /// </summary>
        /// <value>The type.</value>
        [ Browsable( false ) ]
        [ ExpressionVisible( false ) ]
        public Type Type
        {
            get { return Type.GetType( SystemType ); }
        }
    }
}