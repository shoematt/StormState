using System;
using System.Collections.Generic;
using System.Linq;
using Orca.Domain.Exceptions;
using Orca.Domain.Interfaces;
using Orca.Domain.Objects;

namespace Orca.Domain.DynamicProperties
{
    /// <summary>
    ///   Helper class to handle the <see cref = "ISupportDefaultPropertyValues" /> and <see cref = "ISupportPropertyValues" />
    /// </summary>
    /// <typeparam name = "T"></typeparam>
    [Serializable]
    public class PropertiesImplementation<T> where T : PropertyBase
    {
        private readonly Dictionary<Guid, T> internalCache = new Dictionary<Guid, T>( );

        private readonly Dictionary<string, Guid> propertyNames = new Dictionary<string, Guid>( StringComparer.CurrentCultureIgnoreCase );

        private object _domainObjectOwner;

        private PropertiesImplementation( )
        {


        }

        public PropertiesImplementation( object domainObjectOwner )
        {
            _domainObjectOwner = domainObjectOwner;
        }

        //     public object DomainObjectOwner { get; set; }

        public virtual IList<T> Properties
        {
            get { return internalCache.Values.ToList( ); }
            set
            {
                ClearInternalCache( );
                foreach ( T item in value )
                {
                    AddProperty( item );
                }
            }
        }


        private void ClearInternalCache( )
        {
            internalCache.Clear( );
            propertyNames.Clear( );
        }

        public virtual object this[Guid StaticInstanceID]
        {
            get
            {
                if ( internalCache.ContainsKey( StaticInstanceID ) )
                {
                    return internalCache[StaticInstanceID];
                }

                throw new PropertyDoesNotExistException( string.Format( "The property with the StaticInstanceID {0} does not exist for this worktype", StaticInstanceID ) );
            }

            set
            {
                if ( internalCache.ContainsKey( StaticInstanceID ) )
                {
                    internalCache[StaticInstanceID].Value = value;
                }
                else
                {
                    throw new PropertyDoesNotExistException( string.Format( "The property with the StaticInstanceID {0} does not exist for this worktype", StaticInstanceID ) );
                }
            }
        }

        public virtual object this[string PropertyName]
        {
            get
            {
                if ( !HasProperty( PropertyName ) )
                {
                    throw new PropertyDoesNotExistException( string.Format( "The property with the Name {0} does not exist for this worktype", PropertyName ) );
                }

                return this[propertyNames[PropertyName]];
            }

            set
            {
                if ( !HasProperty( PropertyName ) )
                {
                    throw new PropertyDoesNotExistException( string.Format( "The property with the Name {0} does not exist for this worktype", PropertyName ) );
                }

                this[propertyNames[PropertyName]] = value;
            }
        }

        public virtual void AddProperty( T PropertyObject )
        {
            //if ( PropertyObject is PropertyValue )
            //{

            //}
            //else if ( PropertyObject is DefaultPropertyValue )
            //{

            //}


            if ( internalCache.ContainsKey( PropertyObject.StaticInstanceID ) )
            {
                internalCache[PropertyObject.StaticInstanceID] = PropertyObject;
            }
            else
            {
                internalCache.Add( PropertyObject.StaticInstanceID, PropertyObject );
                propertyNames.Add( PropertyObject.Name, PropertyObject.StaticInstanceID );
            }
        }


        public virtual void Remove( T PropertyObject )
        {
            if ( internalCache.ContainsKey( PropertyObject.StaticInstanceID ) )
            {
                internalCache.Remove( PropertyObject.StaticInstanceID );
                propertyNames.Remove( PropertyObject.Name );
            }
        }

        public virtual bool HasProperty( Guid StaticInstanceID )
        {
            return (internalCache.ContainsKey( StaticInstanceID ));
        }

        public virtual bool HasProperty( string PropertyName )
        {
            return (propertyNames.ContainsKey( PropertyName ));
        }
    }
}