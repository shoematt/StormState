using System;
using System.Runtime.Serialization;

namespace Orca.Domain.Exceptions
{
    public class ObjectDoesNotSupportDynamicPropertiesException : Exception
    {
        /// <summary>
        ///   Initializes a new instance of the PropertyNameAlreadyDefinedException class.
        /// </summary>
        public ObjectDoesNotSupportDynamicPropertiesException( string name )
            : base( string.Format( "The Domain object {0} does not support dynamic properties", name ) )
        {
        }

        public ObjectDoesNotSupportDynamicPropertiesException( )
        {
        }

        public ObjectDoesNotSupportDynamicPropertiesException( string message, Exception innerException )
            : base( message, innerException )
        {
        }

        protected ObjectDoesNotSupportDynamicPropertiesException( SerializationInfo info, StreamingContext context )
            : base( info, context )
        {
        }
    }
}