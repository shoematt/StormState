using System;
using System.Runtime.Serialization;

namespace Orca.Domain.WorkFlows
{
    public class WorkTypeAlreadyExistInWorkFlowException : Exception
    {
        /// <summary>
        ///   Initializes a new instance of the PropertyNameAlreadyDefinedException class.
        /// </summary>
        public WorkTypeAlreadyExistInWorkFlowException( string WorkTypeName )
            : base( string.Format( "The work type {0} has already exist in the work flow", WorkTypeName ) )
        {
        }

        public WorkTypeAlreadyExistInWorkFlowException( )
        {
        }

        public WorkTypeAlreadyExistInWorkFlowException( string message, Exception innerException )
            : base( message, innerException )
        {
        }

        protected WorkTypeAlreadyExistInWorkFlowException( SerializationInfo info, StreamingContext context )
            : base( info, context )
        {
        }
    }
}