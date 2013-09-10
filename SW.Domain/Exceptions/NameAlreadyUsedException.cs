using System;
using System.Runtime.Serialization;

namespace Orca.Domain.Exceptions
{
    public class NameAlreadyUsedException : Exception
    {
        /// <summary>
        ///   Initializes a new instance of the NameAlreadyUsedException class.
        /// </summary>
        public NameAlreadyUsedException( string name, Type objectType )
            : this( string.Format( "The name {0} for Type {1} already exist", name, objectType.Name ) )
        {
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref = "NameAlreadyUsedException" /> class.
        /// </summary>
        public NameAlreadyUsedException( )
        {
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref = "NameAlreadyUsedException" /> class.
        /// </summary>
        /// <param name = "message">The message.</param>
        public NameAlreadyUsedException( string message )
            : base( message )
        {
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref = "NameAlreadyUsedException" /> class.
        /// </summary>
        /// <param name = "message">The message.</param>
        /// <param name = "innerException">The inner exception.</param>
        public NameAlreadyUsedException( string message, Exception innerException )
            : base( message, innerException )
        {
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref = "NameAlreadyUsedException" /> class.
        /// </summary>
        /// <param name = "info">The <see cref = "T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name = "context">The <see cref = "T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        /// <exception cref = "T:System.ArgumentNullException">
        ///   The <paramref name = "info" /> parameter is null.
        /// </exception>
        /// <exception cref = "T:System.Runtime.Serialization.SerializationException">
        ///   The class name is null or <see cref = "P:System.Exception.HResult" /> is zero (0).
        /// </exception>
        protected NameAlreadyUsedException( SerializationInfo info, StreamingContext context )
            : base( info, context )
        {
        }
    }

    public class NameStillInUseException : Exception
    {
        /// <summary>
        ///   Initializes a new instance of the NameStillInUseException class.
        /// </summary>
        public NameStillInUseException( string name, Type objectType )
            : this( string.Format( "The name {0} for Type {1} is still being used", name, objectType.Name ) )
        {
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref = "NameStillInUseException" /> class.
        /// </summary>
        public NameStillInUseException( )
        {
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref = "NameStillInUseException" /> class.
        /// </summary>
        /// <param name = "message">The message.</param>
        public NameStillInUseException( string message )
            : base( message )
        {
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref = "NameStillInUseException" /> class.
        /// </summary>
        /// <param name = "message">The message.</param>
        /// <param name = "innerException">The inner exception.</param>
        public NameStillInUseException( string message, Exception innerException )
            : base( message, innerException )
        {
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref = "NameStillInUseException" /> class.
        /// </summary>
        /// <param name = "info">The <see cref = "T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name = "context">The <see cref = "T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        /// <exception cref = "T:System.ArgumentNullException">
        ///   The <paramref name = "info" /> parameter is null.
        /// </exception>
        /// <exception cref = "T:System.Runtime.Serialization.SerializationException">
        ///   The class name is null or <see cref = "P:System.Exception.HResult" /> is zero (0).
        /// </exception>
        protected NameStillInUseException( SerializationInfo info, StreamingContext context )
            : base( info, context )
        {
        }
    }


    public class NameNotRegisterdException : Exception
    {
        /// <summary>
        ///   Initializes a new instance of the NameNotRegisterdException class.
        /// </summary>
        public NameNotRegisterdException( string name, Type objectType )
            : this( string.Format( "The name {0} for Type {1} does not exist", name, objectType.Name ) )
        {
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref = "NameNotRegisterdException" /> class.
        /// </summary>
        public NameNotRegisterdException( )
        {
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref = "NameNotRegisterdException" /> class.
        /// </summary>
        /// <param name = "message">The message.</param>
        public NameNotRegisterdException( string message )
            : base( message )
        {
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref = "NameNotRegisterdException" /> class.
        /// </summary>
        /// <param name = "message">The message.</param>
        /// <param name = "innerException">The inner exception.</param>
        public NameNotRegisterdException( string message, Exception innerException )
            : base( message, innerException )
        {
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref = "NameNotRegisterdException" /> class.
        /// </summary>
        /// <param name = "info">The <see cref = "T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name = "context">The <see cref = "T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        /// <exception cref = "T:System.ArgumentNullException">
        ///   The <paramref name = "info" /> parameter is null.
        /// </exception>
        /// <exception cref = "T:System.Runtime.Serialization.SerializationException">
        ///   The class name is null or <see cref = "P:System.Exception.HResult" /> is zero (0).
        /// </exception>
        protected NameNotRegisterdException( SerializationInfo info, StreamingContext context )
            : base( info, context )
        {
        }
    }
}