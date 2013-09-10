using System;

namespace Orca.Domain.Exceptions
{
    public class WorkTypeNotFoundException : Exception
    {
        public WorkTypeNotFoundException()
        {

        }
        public WorkTypeNotFoundException(string message)
            : base(message)
        {
            
        }
        public WorkTypeNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
            
        }
        protected WorkTypeNotFoundException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
            
        }
    }
}
