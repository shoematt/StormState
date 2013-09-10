using System;
using System.Runtime.Serialization;

namespace Orca.Domain.Exceptions
{
    public class CanNotDeletePublishedDomainObject : Exception
    {
        /// <summary>
        ///   Initializes a new instance of the PropertyNameAlreadyDefinedException class.
        /// </summary>
        public CanNotDeletePublishedDomainObject( string name )
            : base( string.Format( "Can not delete the published domain object {0} ", name ) )
        {
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref = "CanNotDeletePublishedDomainObject" /> class.
        /// </summary>
        public CanNotDeletePublishedDomainObject( )
        {
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref = "CanNotDeletePublishedDomainObject" /> class.
        /// </summary>
        /// <param name = "message">The message.</param>
        /// <param name = "innerException">The inner exception.</param>
        public CanNotDeletePublishedDomainObject( string message, Exception innerException )
            : base( message, innerException )
        {
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref = "CanNotDeletePublishedDomainObject" /> class.
        /// </summary>
        /// <param name = "info">The <see cref = "T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name = "context">The <see cref = "T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        /// <exception cref = "T:System.ArgumentNullException">
        ///   The <paramref name = "info" /> parameter is null.
        /// </exception>
        /// <exception cref = "T:System.Runtime.Serialization.SerializationException">
        ///   The class name is null or <see cref = "P:System.Exception.HResult" /> is zero (0).
        /// </exception>
        protected CanNotDeletePublishedDomainObject( SerializationInfo info, StreamingContext context )
            : base( info, context )
        {
        }
    }
}