using System;

namespace Orca.Domain.Exceptions
{
    public class PropertyValueException : Exception
    {

        public PropertyValueException()
        {

        }
        public PropertyValueException(string message)
            : base(message)
        {
            
        }
        public PropertyValueException(string message, Exception innerException)
            : base(message, innerException)
        {
            
        }
        protected PropertyValueException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
            
        }
    }
}
