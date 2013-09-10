using System;
using Orca.ClientIntegration;
using Orca.Domain.Interfaces;
using VE.Core.MessageEvents;

namespace Orca.Workflow.ComponentModel.Events
{
    public class ExternalEventContainer : IExternalOrcaEventContainer, IWorkflowEventContainer
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref = "ExternalEventContainer" /> class.
        /// </summary>
        /// <param name = "container">The container.</param>
        /// <remarks>
        /// </remarks>
        public ExternalEventContainer(IExternalOrcaEventContainer container)
        {
            CurrentEventType = container.CurrentEventType;
            CurrentEvent = container.CurrentEvent;
            EventTime = container.EventTime;
            LoadEvent = container.LoadEvent;
            UnLoadEvent = container.UnLoadEvent;
            LocationChangeEvent = container.LocationChangeEvent;
        }

        #region IVEOrcaEventContainer Members

        /// <summary>
        ///   Gets or sets the type of the current event.
        /// </summary>
        /// <value>The type of the current event.</value>
        /// <remarks>
        /// </remarks>
        public MessageEventTypes CurrentEventType { get; set; }

        /// <summary>
        ///   Gets or sets the event time.
        /// </summary>
        /// <value>The event time.</value>
        /// <remarks>
        /// </remarks>
        public DateTime EventTime { get; set; }

        /// <summary>
        ///   Gets or sets the load event.
        /// </summary>
        /// <value>The load event.</value>
        /// <remarks>
        /// </remarks>
        public ExternalOrcaEvent LoadEvent { get; set; }

        /// <summary>
        ///   Gets or sets the location change event.
        /// </summary>
        /// <value>The location change event.</value>
        /// <remarks>
        /// </remarks>
        public ExternalOrcaEvent LocationChangeEvent { get; set; }

        /// <summary>
        ///   Gets or sets the un load event.
        /// </summary>
        /// <value>The un load event.</value>
        /// <remarks>
        /// </remarks>
        public ExternalOrcaEvent UnLoadEvent { get; set; }

        /// <summary>
        ///   Gets or sets the current event.
        /// </summary>
        /// <value>The current event.</value>
        /// <remarks>
        /// </remarks>
        public ExternalOrcaEvent CurrentEvent { get; set; }

        #endregion

        #region IWorkflowEventContainer Members

        /// <summary>
        ///   Gets or sets the event.
        /// </summary>
        /// <value>The event.</value>
        /// <remarks>
        /// </remarks>
        IWorkflowEvent IWorkflowEventContainer.Event { get; set; }

        #endregion
    }
}