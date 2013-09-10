using System;
using System.ComponentModel;

namespace Orca.Domain.DynamicProperties
{
    public class PropertyTypeDescriptorProvider : TypeDescriptionProvider
    {

        public PropertyTypeDescriptorProvider( TypeDescriptionProvider parent )
            : base( parent )
        {

        }



        public override ICustomTypeDescriptor GetTypeDescriptor( Type objectType, object instance )
        {
            ICustomTypeDescriptor defaultDescriptor = base.GetTypeDescriptor( objectType, instance );

            if ( instance == null )
            {
                return defaultDescriptor;

            }
            else
            {
                return new PropertyTypeDescriptor( defaultDescriptor, instance );
            }
        }



        public static void Add( Type type )
        {
            TypeDescriptionProvider parent = TypeDescriptor.GetProvider( type ); //get the default typedescripter.

            if ( parent != null && parent is PropertyTypeDescriptorProvider )
            {
                return;
        }

            TypeDescriptor.AddProvider( new PropertyTypeDescriptorProvider( parent ), type );

            //  typeDescriptorCache.Store( type.ToString(), dynamicProperties );
        }
    }
}