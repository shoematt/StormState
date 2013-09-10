using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orca.Workflow.ComponentModel
{
    public interface IWorkflowEventContainer
    {
        /// <summary>
        /// Gets or sets the event.
        /// </summary>
        /// <value>The event.</value>
        /// <remarks></remarks>
        IWorkflowEvent Event { get; set; }
    }
}
