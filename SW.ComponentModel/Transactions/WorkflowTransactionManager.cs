using System.Web;
using Orca.Core.Commands;
using Orca.Domain.Client;

namespace Orca.Workflow.ComponentModel.Transactions
{
    public abstract class WorkflowTransactionManager<T, TU> : ICommandHandler<SystemCommand>
        where TU : IWorkflowTransactionProvider
        where T : IWorkflowTransaction
    {
        private readonly TU _viewerTransactionProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkflowTransactionManager&lt;T, TU&gt;"/> class.
        /// </summary>
        /// <param name="viewerTransactionProvider">The transaction provider.</param>
        protected WorkflowTransactionManager(TU viewerTransactionProvider)
        {
            _viewerTransactionProvider = viewerTransactionProvider;
        }

        #region ICommandHandler<SystemCommand> Members

        /// <summary>
        /// Gets the transaction provider.
        /// </summary>
        public TU ViewerTransactionProvider
        {
            get { return _viewerTransactionProvider; }
        }

        /// <summary>
        /// Handles the specified in application system message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Handle( SystemCommand message )
        {
            OnHandle( message );
        }

        #endregion

        /// <summary>
        /// called when a system message is recieved allowing child objects to handle the behavior
        /// </summary>
        /// <param name="message">The message.</param>
        protected abstract void OnHandle( SystemCommand message );
    }
}