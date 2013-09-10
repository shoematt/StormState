namespace Orca.Domain.Interfaces
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
