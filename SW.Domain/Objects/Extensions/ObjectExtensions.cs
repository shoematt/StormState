using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orca.Core.Converters;

namespace Orca.Domain.Objects.Extensions
{
    public static class ObjectExtensions
    {
        public static IDictionary<string, object> ConvertToDictionary( this object client, object data )
        {
            return  ObjectToDictionaryRegistry.Convert( data ) ;
        }
    }
}
