namespace Orca.Workflow.ComponentModel.Transactions
{
    public interface IWorkflowTransactionProvider
    {
        /// <summary>
        /// Gets a new transaction instance from a custom
        /// provider implementation
        /// </summary>
        /// <returns></returns>
        IWorkflowTransaction GetTransaction();
    }
}