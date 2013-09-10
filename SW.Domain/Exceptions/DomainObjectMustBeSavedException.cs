using System;
using System.Runtime.Serialization;

namespace Orca.Domain.Exceptions
{
    public class DomainObjectMustBeSavedException : Exception
    {
        private new static readonly string Message = "The object {0} must be saved";

        public DomainObjectMustBeSavedException( )
        {
        }

        public DomainObjectMustBeSavedException( string ObjectName ) :
            base( string.Format( Message, ObjectName ) )
        {
        }

        public DomainObjectMustBeSavedException( string ObjectName, string message ) :
            base( string.Format( Message, ObjectName ) + ";" + message )
        {
        }


        public DomainObjectMustBeSavedException( string ObjectName, Exception innerException )
            : base( string.Format( Message, ObjectName ), innerException )
        {
        }

        protected DomainObjectMustBeSavedException( SerializationInfo info, StreamingContext context )
            : base( info, context )
        {
        }
    }
}