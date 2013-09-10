using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Orca.Core;
using Orca.Core.Domain;
using Orca.Domain.DomainServices;

namespace Orca.Domain
{
    public class TypeService : ITypeService
    {
        Dictionary<Guid, HashSet<PropertyDescriptor>> propertiesForTemplateType = new Dictionary<Guid, HashSet<PropertyDescriptor>>( );
        GenericEqualityComparer<PropertyDescriptor> propertyComparer;

        public TypeService( )
        {
            propertyComparer = new GenericEqualityComparer<PropertyDescriptor>( ( a1, a2 ) => String.Compare( a1.Name, a2.Name, true ) == 0, w => w.GetHashCode( ) );
        }

        public void RegisterTemmplate( ISupportDefaultPropertyValues template )
        {

            if ( propertiesForTemplateType.ContainsKey( template.Id ) )
            {
                return;
            }

            Type templateType = template.GetType( );



            TypeDescriptionProvider typeProvider = TypeDescriptor.GetProvider( templateType );

            HashSet<PropertyDescriptor> props = new HashSet<PropertyDescriptor>( propertyComparer );



            var propDescriptors = typeProvider.GetTypeDescriptor( typeof( ObjectType ) ).GetProperties( ).Cast<PropertyDescriptor>( );


            TypeDescriptionProvider templateTypeProvider = TypeDescriptor.GetProvider( templateType );

            var templatePropDescriptors = templateTypeProvider.GetTypeDescriptor( templateType, template ).GetProperties( ).Cast<PropertyDescriptor>( );


            var propertyInfos = templatePropDescriptors.Where( p => !p.IsReadOnly );

            PopulateHashSet( props, propDescriptors );
            PopulateHashSet( props, propertyInfos );

            propertiesForTemplateType.Add( template.Id, props );

        }

        public IEnumerable<PropertyDescriptor> GetPropertyDescriptors( ISupportPropertyValues domainObject )
        {

            if ( !propertiesForTemplateType.ContainsKey( domainObject.TemplateId ) )
            {
                throw new Exception( string.Format( "The template with id {0} has not been registered with the type service", domainObject.Id ) );
            }
            else
            {
                return propertiesForTemplateType[domainObject.TemplateId];
            }
        }


        public HashSet<PropertyDescriptor> GetPropertyDescriptorsForTemplate( Guid templateId )
        {
            if ( !propertiesForTemplateType.ContainsKey( templateId ) )
            {
                return new HashSet<PropertyDescriptor>( );
            }
            return new HashSet<PropertyDescriptor>( propertiesForTemplateType[templateId] );

        }

        public HashSet<PropertyDescriptor> GetSpecifiedPropertyDescriptorsForTemplate( Guid templateId, IEnumerable<string> requestedProperties )
        {
            if ( !propertiesForTemplateType.ContainsKey( templateId ) )
            {
                return new HashSet<PropertyDescriptor>( );
            }

            var existingProperties = propertiesForTemplateType[templateId];

            var result = new HashSet<PropertyDescriptor>( existingProperties.Where( x => requestedProperties.Contains( x.Name ) ) );

            return result;
        }


        private void PopulateHashSet( HashSet<PropertyDescriptor> props, IEnumerable<PropertyDescriptor> propDescriptors )
        {
            foreach ( PropertyDescriptor desc in propDescriptors )
            {
                props.Add( desc );
            }
        }
    }
}
