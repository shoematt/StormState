using System;
using Orca.Domain.Interfaces;

namespace Orca.Workflow.ComponentModel.Events
{
    [Serializable]
    public class InteractiveEventContainer : IWorkflowEventContainer
    {
        public InteractiveEventContainer()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InteractiveEventContainer"/> class.
        /// </summary>
        /// <param name="viewCommand">The view command.</param>
        /// <param name="eventData">The event data.</param>
        /// <remarks></remarks>
        public InteractiveEventContainer(string viewCommand, InteractiveEvent eventData)
        {
            ViewCommand = viewCommand;
            Event = eventData;
        }

        /// <summary>
        /// Gets or sets the view command info.
        /// </summary>
        /// <value>The view command info.</value>
        /// <remarks></remarks>
        public string ViewCommand { get; set; }

        /// <summary>
        ///   Gets or sets the event.
        /// </summary>
        /// <value>The event.</value>
        /// <remarks>
        /// </remarks>
        public InteractiveEvent Event { get; set; }

        #region IWorkflowEventContainer Members

        /// <summary>
        ///   Gets or sets the event.
        /// </summary>
        /// <value>The event.</value>
        /// <remarks>
        /// </remarks>
        IWorkflowEvent IWorkflowEventContainer.Event
        {
            get { return this.Event; }
            set { this.Event = (InteractiveEvent) value; }
        }

        #endregion
    }
}