using System;
using System.Reflection;
using MassTransit;
using Orca.Domain.Client;
using log4net;

namespace Orca.Workflow.ComponentModel.Transactions
{
    public class WorkflowTransaction<T> : WorkflowTransaction, IWorkflowTransaction where T : TransactionContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WorkflowTransaction&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="session">The session.</param>
        public WorkflowTransaction( IWorkSession session ) : base( session )
        {
        }

        #region IWorkflowTransaction Members

        /// <summary>
        /// Processes the specified transaction context.
        /// </summary>
        /// <param name="transactionContext">The transaction context.</param>
        void IWorkflowTransaction.Process( TransactionContext transactionContext )
        {
            Process( transactionContext );
        }

        #endregion

        /// <summary>
        /// Processes the specified transaction context.
        /// </summary>
        /// <param name="transactionContext">The transaction context.</param>
        public void Process( T transactionContext )
        {
            base.Process( transactionContext );
        }

        /// <summary>
        /// Called when [process transaction].
        /// </summary>
        /// <param name="transactionContext">The transaction context.</param>
        protected virtual void OnProcessTransaction( T transactionContext )
        {
        }

        /// <summary>
        /// Called when [process transaction].
        /// </summary>
        /// <param name="transactionContext">The transaction context.</param>
        protected override void OnProcessTransaction( TransactionContext transactionContext )
        {
            OnProcessTransaction( ( T ) transactionContext );
        }
    }

    /// <summary>
    /// Manages a transaction for a flow, handling the begin and end states between commands.
    /// </summary>
    public class WorkflowTransaction : IWorkflowTransaction
    {
        protected readonly ILog _log = LogManager.GetLogger( MethodBase.GetCurrentMethod( ).DeclaringType );

        /// <summary>
        /// Unique transaction identifier
        /// </summary>
        public Guid TransactionId = Guid.NewGuid( );

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkflowTransaction"/> class.
        /// </summary>
        /// <param name="session">The session.</param>
        public WorkflowTransaction( IWorkSession session )
        {
            Session = session;
            HasBegun = false;
            IsComplete = false;
        }

        /// <summary>
        /// Gets or sets the session.
        /// </summary>
        /// <value>
        /// The session.
        /// </value>
        public IWorkSession Session { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has begun.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has begun; otherwise, <c>false</c>.
        /// </value>
        protected bool HasBegun { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is complete.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is complete; otherwise, <c>false</c>.
        /// </value>
        protected bool IsComplete { get; set; }

        #region IWorkflowTransaction Members

        /// <summary>
        /// Begins the transaction.
        /// </summary>
        public void BeginTransaction( )
        {
            if (IsComplete) throw new Exception( "The transaction has already completed" );
            if (HasBegun) throw new Exception( "The transaction has already started" );

            HasBegun = true;

            if (_log.IsDebugEnabled)
            {
                _log.DebugFormat( "BeginTransaction Type:{0} Id{1}", GetType( ).ToFriendlyName( ), TransactionId );
            }

            OnBeginTransaction( );
        }

        /// <summary>
        /// Ends the transaction.
        /// </summary>
        public void EndTransaction( )
        {
            if (!HasBegun) throw new Exception( "The transaction has not been started" );
            if (IsComplete) throw new Exception( "The transaction has already completed" );

            IsComplete = true;

            OnEndTransaction( );

            if (_log.IsDebugEnabled)
            {
                _log.DebugFormat( "EndTransaction Type:{0} Id{1}", GetType( ).ToFriendlyName( ), TransactionId );
            }
        }

        /// <summary>
        /// Processes the specified transaction context.
        /// </summary>
        /// <param name="transactionContext">The transaction context.</param>
        public void Process( TransactionContext transactionContext )
        {
            OnProcessTransaction( transactionContext );
        }

        #endregion

        /// <summary>
        /// Called when a transaction has started, before processing has begun.
        /// </summary>
        protected virtual void OnBeginTransaction( )
        {
        }

        /// <summary>
        /// Called when a transaction has been completed, including all processing.
        /// </summary>
        protected virtual void OnEndTransaction( )
        {
        }

        /// <summary>
        /// Called when [process transaction].
        /// </summary>
        /// <param name="transactionContext">The transaction context.</param>
        protected virtual void OnProcessTransaction( TransactionContext transactionContext )
        {
        }
    }

    public class TransactionContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionContext"/> class.
        /// </summary>
        /// <param name="state">The state.</param>
        public TransactionContext( object state )
        {
            State = state;
        }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        public object State { get; set; }
    }
}