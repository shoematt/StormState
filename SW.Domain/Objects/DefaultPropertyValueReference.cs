using System;
using Orca.Core.Domain;

namespace Orca.Domain.Objects
{
    [Serializable]
    public class DefaultPropertyValueReference : PropertyBase, IDomainObject
    {

        public DefaultPropertyValueReference( )
        {

        }

        public DefaultPropertyValueReference( string name, Guid id )
        {
            Name = name;
            DefaultPropertyValueId = id;
        }


        public virtual DisplayFilter DisplayFilter { get; set; }


      

        /// <summary>
        /// Gets or sets the default property value id.
        /// </summary>
        /// <value>
        /// The default property value id.
        /// </value>
        public virtual Guid DefaultPropertyValueId { get; set; }


    }
}
