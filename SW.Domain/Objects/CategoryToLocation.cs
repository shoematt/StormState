using System;
using Orca.Domain.Objects.BaseObjects;

namespace Orca.Domain.Objects
{
    [Serializable]
    public class CategoryToLocation : DomainObject
    {
        public virtual Guid LocationCategory_id { get; set; }
        public virtual Guid Location_id { get; set; }
    }
}