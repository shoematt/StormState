using System;
using Orca.Domain.Interfaces;

namespace Orca.Workflow.ComponentModel.Events
{
    [Serializable]
    public class InteractiveEvent : IWorkflowEvent
    {
        #region IWorkflowEvent Members

        /// <summary>
        ///   Gets or sets the display name.
        /// </summary>
        /// <value>The display name.</value>
        /// <remarks>
        /// </remarks>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>The data.</value>
        /// <remarks></remarks>
        public object Data { get; set; }

        #endregion
    }
}