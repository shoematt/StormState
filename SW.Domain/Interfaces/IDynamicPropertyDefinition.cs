using Orca.Core.Domain;
using Orca.Domain.Objects;

namespace Orca.Domain.Interfaces
{
    public interface IPropertyDefinition : IDynamicProperty, IPublishableDomainObject, IDomainObject
    {
        DefaultPropertyValue CreateDefaultPropertyValue( );
    }
}