using Orca.Core.Domain;

namespace Orca.Domain.Interfaces
{
    public interface IDecisionPoint : IDomainObject
    {
        /// <summary>
        ///   Gets or sets the process to execute when the decision is false.
        /// </summary>
        /// <value>The false process.</value>
        IVEProcessFlow FalseProcess { get; set; }

        /// <summary>
        ///   Gets or sets the process to execute when the decision is true.
        /// </summary>
        /// <value>The true process.</value>
        IVEProcessFlow TrueProcess { get; set; }
    }
}