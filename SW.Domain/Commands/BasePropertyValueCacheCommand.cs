using System;
using System.Collections.Generic;

namespace Orca.Domain.Commands
{
    [Serializable]
    public class BasePropertyValueCacheCommand<T> where T : class
    {


        List<T> _objects = new List<T>( );

        public BasePropertyValueCacheCommand( )
        {

        }

        public BasePropertyValueCacheCommand( T objectValue )
        {
            this._objects.Add( objectValue );
        }


        public BasePropertyValueCacheCommand( List<T> objects )
        {
            _objects.AddRange( objects );
        }




        public void AddObject( T objectToAdd )
        {
            _objects.Add( objectToAdd );
        }


        public void AddObjects( List<T> objects )
        {
            objects.AddRange( objects );
        }

        public List<T> Objects
        {
            get
            {
                return new List<T>( _objects );
            }
        }


    }
}
