using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Orca.Domain.Interfaces;
using Orca.Domain.Objects;

namespace Orca.Domain.DynamicProperties
{
    public class PropertyTypeDescriptor : CustomTypeDescriptor
    {
        private readonly List<PropertyDescriptor> _dynamicPropertyDescriptors = new List<PropertyDescriptor>( );

        private readonly PropertyDescriptorCollection _propertyCollections;

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyTypeDescriptor"/> class.
        /// </summary>
        /// <param name="defaultDescriptor">The default descriptor.</param>
        /// <param name="instance">The instance.</param>
        /// 
        public PropertyTypeDescriptor( ICustomTypeDescriptor defaultDescriptor, object instance )
        //public PropertyTypeDescriptor( ICustomTypeDescriptor defaultDescriptor, object instance, DynamicPropertyManager dynamicPropertyManager )
        {
            _propertyCollections = defaultDescriptor.GetProperties( );

            _dynamicPropertyDescriptors.AddRange( _propertyCollections.Cast<PropertyDescriptor>( ) ); //add the properties found to the collection 

            if ( instance is ISupportPropertyValues )
            {
                ISupportPropertyValues domainObject = instance as ISupportPropertyValues;


                foreach ( PropertyValue item in domainObject.PropertyValues )
                {
                    var found = _dynamicPropertyDescriptors.FirstOrDefault( x => x.Name == item.Name );
                    if ( found == null )
                    {
                        _dynamicPropertyDescriptors.Add( new PropertyValueDescriptor( item ) );
                    }
                }

                _propertyCollections = new PropertyDescriptorCollection( _dynamicPropertyDescriptors.ToArray( ) );


            }

            if ( instance is ISupportDefaultPropertyValues )
            {
                ISupportDefaultPropertyValues domainObject = instance as ISupportDefaultPropertyValues;

                foreach ( DefaultPropertyValue item in domainObject.DefaultPropertyValues )
                {
                    var found = _dynamicPropertyDescriptors.FirstOrDefault( x => x.Name == item.Name );
                    if ( found == null )
                    {
                        _dynamicPropertyDescriptors.Add( new DefaultPropertyValueDescriptor( item ) );
                    }
                }

                _propertyCollections = new PropertyDescriptorCollection( _dynamicPropertyDescriptors.ToArray( ) );
            }
        }

        /// <summary>
        /// Returns a filtered collection of property descriptors for the object represented by this type descriptor.
        /// </summary>
        /// <param name="attributes">An array of attributes to use as a filter. This can be null.</param>
        /// <returns>
        /// A <see cref="T:System.ComponentModel.PropertyDescriptorCollection"/> containing the property descriptions for the object represented by this type descriptor. The default is <see cref="F:System.ComponentModel.PropertyDescriptorCollection.Empty"/>.
        /// </returns>
        public override sealed PropertyDescriptorCollection GetProperties( Attribute[] attributes )
        {
            return _propertyCollections;
        }

        /// <summary>
        /// Returns a collection of property descriptors for the object represented by this type descriptor.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.ComponentModel.PropertyDescriptorCollection"/> containing the property descriptions for the object represented by this type descriptor. The default is <see cref="F:System.ComponentModel.PropertyDescriptorCollection.Empty"/>.
        /// </returns>
        public override sealed PropertyDescriptorCollection GetProperties( )
        {
            return _propertyCollections;
        }
    }
}