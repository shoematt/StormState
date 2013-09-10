using System;
using System.Collections.Generic;
using System.ComponentModel;
using Orca.Core.Domain;
using Orca.Core.Extensions;
using Orca.Domain.DynamicProperties;
using Orca.Domain.Exceptions;
using Orca.Domain.Objects.BaseObjects;

namespace Orca.Domain.Objects
{


    [Serializable]
    public class DisplayFilter : DomainObject, IDomainObject
    {
        protected readonly PropertiesImplementation<DefaultPropertyValueReference> DefaultPropertyValueContainer;// = new PropertiesImplementation<DefaultPropertyValueReference>( );

        //     IList<DefaultPropertyValueReference> _defaultPropertyValueReferences = new List<DefaultPropertyValueReference>( );

        protected DisplayFilter( )
        {
            DefaultPropertyValueContainer = new PropertiesImplementation<DefaultPropertyValueReference>( this );
        }

        public DisplayFilter( string name )
            : base( name )
        {

        }

        [Browsable( false )]
        public virtual IList<DefaultPropertyValueReference> DefaultPropertyValueReferences
        {
            get { return DefaultPropertyValueContainer.Properties; }
            set { DefaultPropertyValueContainer.Properties = value; }
        }

        public virtual void AddDefaultPropertyReference( DefaultPropertyValue DefaultPropertyVal )
        {
            var propRef = new DefaultPropertyValueReference( DefaultPropertyVal.Name, DefaultPropertyVal.Id );

            DefaultPropertyValueContainer.AddProperty( propRef );


        }

        public virtual void RemoveDefaultPropertyReference( DefaultPropertyValue DefaultPropertyVal )
        {
            var item = FindItem( DefaultPropertyVal );
            if ( item != null )
            {
                DefaultPropertyValueContainer.Remove( item );
            }
        }


        public virtual bool HasDefaultPropertyReference( DefaultPropertyValue DefaultPropertyVal )
        {
            return DefaultPropertyValueContainer.HasProperty( DefaultPropertyVal.Name );

        }

        public virtual DefaultPropertyValueReference FindItem( DefaultPropertyValue propValue )
        {
            if ( !HasDefaultPropertyReference( propValue ) )
            {
                throw new PropertyDoesNotExistException( propValue.Name );
            }
            return DefaultPropertyValueContainer[propValue.Name].As<DefaultPropertyValueReference>( );

            //  return _defaultPropertyValueReferences.Where( x => x.Name.ToLowerInvariant( ) == propValue.Name.ToLowerInvariant( ) ).FirstOrDefault( );
        }
    }
}
