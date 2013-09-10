namespace Orca.Domain.Interfaces
{
    public interface IWorkflowEvent
    {
        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>The display name.</value>
        /// <remarks></remarks>
        string DisplayName { get; set; }
    }
}