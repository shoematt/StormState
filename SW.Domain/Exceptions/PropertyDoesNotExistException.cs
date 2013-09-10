using System;
using System.Runtime.Serialization;

namespace Orca.Domain.Exceptions
{
    public class PropertyDoesNotExistException : Exception
    {
        /// <summary>
        ///   Initializes a new instance of the PropertyNameAlreadyDefinedException class.
        /// </summary>
        public PropertyDoesNotExistException(string propertyName)
            : base(string.Format("The property {0} does not exist in the system", propertyName))
        {
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref = "PropertyDoesNotExistException" /> class.
        /// </summary>
        public PropertyDoesNotExistException()
        {
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref = "PropertyDoesNotExistException" /> class.
        /// </summary>
        /// <param name = "message">The message.</param>
        /// <param name = "innerException">The inner exception.</param>
        public PropertyDoesNotExistException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref = "PropertyDoesNotExistException" /> class.
        /// </summary>
        /// <param name = "info">The <see cref = "T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name = "context">The <see cref = "T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        /// <exception cref = "T:System.ArgumentNullException">
        ///   The <paramref name = "info" /> parameter is null.
        /// </exception>
        /// <exception cref = "T:System.Runtime.Serialization.SerializationException">
        ///   The class name is null or <see cref = "P:System.Exception.HResult" /> is zero (0).
        /// </exception>
        protected PropertyDoesNotExistException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}