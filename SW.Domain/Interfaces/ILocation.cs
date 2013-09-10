using System.Collections.Generic;
using Orca.Core.Domain;

namespace Orca.Domain.Interfaces
{
    /// <summary>
    ///   Represents a physical location
    /// </summary>
    public interface ILocation : IDomainObject
    {
        /// <summary>
        ///   Gets or sets the SGLN.
        /// </summary>
        /// <value>The SGLN.</value>
        string SGLN { get; set; }

        /// <summary>
        ///   Gets or sets the type of the location.
        /// </summary>
        /// <value>The type of the location.</value>
        List<ILocationType> LocationType { get; set; }
    }
}