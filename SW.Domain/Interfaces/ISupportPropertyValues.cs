using System;
using System.Collections.Generic;
using Orca.Core.Domain;
using Orca.Domain.Objects;

namespace Orca.Domain.Interfaces
{
    public interface ISupportPropertyValues : ISupportDynamicProperties, IDomainObject
    {
        Guid TemplateId { get; }


        IList<PropertyValue> PropertyValues { get; }


        PropertyValue GetPropertyValueObject(string propertyName);


        void AddPropertyValue(PropertyValue propertyValue);
    }
}