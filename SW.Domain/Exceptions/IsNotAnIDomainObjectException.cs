using System;
using System.Runtime.Serialization;

namespace Orca.Domain.Exceptions
{
    public class IsNotAnIDomainObjectException : Exception
    {
        /// <summary>
        ///   Initializes a new instance of the PropertyNameAlreadyDefinedException class.
        /// </summary>
        public IsNotAnIDomainObjectException( string message )
            : base( message )
        {
        }

        public IsNotAnIDomainObjectException( )
            : base( "The object is not an IDomain Object" )
        {
        }

        public IsNotAnIDomainObjectException( string message, Exception innerException )
            : base( message, innerException )
        {
        }

        protected IsNotAnIDomainObjectException( SerializationInfo info, StreamingContext context )
            : base( info, context )
        {
        }
    }
}