using System;
using Orca.Core.Domain;

namespace Orca.Domain.Interfaces
{
    public interface ISupportDynamicProperties : IDomainObject
    {

        object this[Guid PropertyValueID] { get; set; }


        object this[string PropertyName] { get; set; }

        bool HasProperty( Guid PropertyDefinitionValueID );


        bool HasProperty( string PropertyName );
    }
}