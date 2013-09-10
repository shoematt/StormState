using System;

namespace Orca.Domain.Commands
{
    public class ExceptionOccuredCommand
    {

        public Exception Exception { get; set; }


        /// <summary>
        /// Initializes a new instance of the ExceptionOccuredCommand class.
        /// </summary>
        public ExceptionOccuredCommand(Exception exception)
        {
            Exception = exception;
        }

    }
}
