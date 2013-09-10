using System;
using Orca.Core.Persistence;

namespace Orca.Domain.Exceptions
{
    public class OrcaArgumentNullException : Exception
    {

        public OrcaArgumentNullException(OperationReport report)
            : base(report.GetNotifications())
        {



        }
    }
}
