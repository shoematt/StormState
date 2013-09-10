using Orca.Workflow.Transitions;

namespace Orca.Workflow.ComponentModel.Logic
{
    public interface IStateTransitionProxy
    {
        /// <summary>
        /// Gets or sets the transition.
        /// </summary>
        /// <value>
        /// The transition.
        /// </value>
        IStateTransition Transition { get; set; }

        /// <summary>
        /// Executes the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        void Execute( IStateTransitionContext context );
    }
}