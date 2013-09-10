using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Orca.Domain.DomainServices;

namespace Orca.Domain
{
    public interface ITypeService
    {
        void RegisterTemmplateFor<ObjectType>( ISupportDefaultPropertyValues template );

        HashSet<PropertyDescriptor> GetPropertyDescriptorsForTemplate( Guid templateId );

        IEnumerable<PropertyDescriptor> GetPropertyDescriptors( ISupportPropertyValues domainObject );
    }
}

