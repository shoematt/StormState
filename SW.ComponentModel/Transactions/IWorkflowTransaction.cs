namespace Orca.Workflow.ComponentModel.Transactions
{
    public interface IWorkflowTransaction
    {
        /// <summary>
        /// Begins the transaction.
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// Ends the transaction.
        /// </summary>
        void EndTransaction();

        /// <summary>
        /// Processes the specified transaction context.
        /// </summary>
        /// <param name="transactionContext">The transaction context.</param>
        void Process( TransactionContext transactionContext );
    }
}