using Orca.Core.Persistence;

namespace Orca.Domain.Commands
{
    public class MethodFailureMessageCommand
    {
        public OperationReport OperationResult { get; set; }

        public string ClassName { get; set; }

        public string MethodName { get; set; }


        /// <summary>
        /// Initializes a new instance of the MethodFailureMessageCommand class.
        /// </summary>
        public MethodFailureMessageCommand(OperationReport operationResult)
        {

            ClassName = string.Empty;
            MethodName = string.Empty;
            OperationResult = operationResult;
        }

        /// <summary>
        /// Initializes a new instance of the MethodFailureMessageCommand class.
        /// </summary>
        public MethodFailureMessageCommand(OperationReport operationResult, string className, string methodName)
            : this(operationResult)
        {
            ClassName = className;
            MethodName = methodName;
        }

    }

}
