using System;
using System.Xml.Linq;
using Orca.Domain.Objects.BaseObjects;

namespace Orca.Domain.Objects
{
    [Serializable]
    public class StateMapTemplate : DomainObjectWithPropertyValues
    {
        /// <summary>
        ///   Gets or sets the template.
        /// </summary>
        /// <value>The template.</value>
        public virtual XDocument Template { get; set; }

        //    public virtual XDocument Template1 { get; set; }
    }
}