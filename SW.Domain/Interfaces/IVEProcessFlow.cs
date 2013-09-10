using Orca.Core.Domain;

namespace Orca.Domain.Interfaces
{
    public interface IVEProcessFlow : IDomainObject
    {
        /// <summary>
        ///   Gets or sets the next process.
        /// </summary>
        /// <value>The next process.</value>
        IVEProcessFlow NextProcess { get; set; }

        /// <summary>
        ///   Gets or sets the prev process.
        /// </summary>
        /// <value>The prev process.</value>
        IVEProcessFlow PrevProcess { get; set; }
    }
}