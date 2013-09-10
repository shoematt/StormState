using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Orca.Domain.Memento
{
    public class Memento<T>
    {
        private Dictionary<PropertyDescriptor, object> storedProperties = new Dictionary<PropertyDescriptor, object>( );

        public Memento( T originator )
        {
            TypeDescriptionProvider typeProvider = TypeDescriptor.GetProvider( typeof ( T ) );

            var test =
                typeProvider.GetTypeDescriptor( typeof ( T ), originator ).GetProperties( ).Cast<PropertyDescriptor>( );


            var propertyInfos = test.Where( p => !p.IsReadOnly );


            //var propertyInfos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
            //                             .Where(p => p.CanRead && p.CanWrite);

            foreach ( var property in propertyInfos )
            {
                storedProperties[ property ] = property.GetValue( originator );
            }
        }

        public void Restore( T originator )
        {
            foreach ( var pair in storedProperties )
            {
                pair.Key.SetValue( originator, pair.Value );
            }
        }
    }
}