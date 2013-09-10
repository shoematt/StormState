using System.Collections.Generic;
using Orca.Core.Domain;

namespace Orca.Domain.Interfaces
{
    public interface IValidation : IDomainObject
    {
        /// <summary>
        ///   Gets or sets the pass validation event actions.
        /// </summary>
        /// <value>The pass validation event actions.</value>
        List<IEventAction> PassValidationEventActions { get; set; }

        /// <summary>
        ///   Gets or sets the failed validation event actions.
        /// </summary>
        /// <value>The failed validation event actions.</value>
        List<IEventAction> FailedValidationEventActions { get; set; }

        /// <summary>
        ///   Determines whether the specified work instruction is valid.
        /// </summary>
        /// <param name = "WorkInstruction">The work instruction.</param>
        /// <returns>
        ///   <c>true</c> if the specified work instruction is valid; otherwise, <c>false</c>.
        /// </returns>
        bool IsValid( IWorkInstruction WorkInstruction );
    }
}