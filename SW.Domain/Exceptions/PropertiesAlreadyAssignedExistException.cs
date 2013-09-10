using System;
using System.Runtime.Serialization;

namespace Orca.Domain.Exceptions
{
    public class PropertiesAlreadyAssignedExistException : Exception
    {
        /// <summary>
        ///   Initializes a new instance of the PropertyNameAlreadyDefinedException class.
        /// </summary>
        public PropertiesAlreadyAssignedExistException(string objectName)
            : base(string.Format("Default Properties have already been applied to Object {0}", objectName))
        {
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref = "PropertyDoesNotExistException" /> class.
        /// </summary>
        public PropertiesAlreadyAssignedExistException()
        {
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref = "PropertyDoesNotExistException" /> class.
        /// </summary>
        /// <param name = "message">The message.</param>
        /// <param name = "innerException">The inner exception.</param>
        public PropertiesAlreadyAssignedExistException(string message, Exception innerException)
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
        protected PropertiesAlreadyAssignedExistException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
