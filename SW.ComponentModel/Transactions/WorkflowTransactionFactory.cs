using System;

namespace Orca.Workflow.ComponentModel.Transactions
{
    public class WorkflowTransactionFactory<T> where T: IWorkflowTransaction, new() 
    {
        private readonly IWorkflowTransactionProvider _provider;

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkflowTransactionFactory&lt;T&gt;"/> class.
        /// </summary>
        public WorkflowTransactionFactory(IWorkflowTransactionProvider provider)
        {
            _provider = provider;
        }

        /// <summary>
        /// Creates a new transaction from the underlying provider
        /// </summary>
        /// <returns></returns>
        public T New()
        {
            return (T)_provider.GetTransaction( );
        }

        /// <summary>
        /// Creates a new transaction from the underlying provider, providing
        /// interaction on creation
        /// </summary>
        /// <returns></returns>
        public T New(Func<T, T> createAction)
        {
            return createAction((T)_provider.GetTransaction());
        }
    }
}