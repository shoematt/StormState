using System;
using System.Collections.Generic;
using Orca.Domain.Objects;

namespace Orca.Domain.Commands
{
    [Serializable]                 
    public class UpdateItemCommand
    {
        List<Item> items = new List<Item>( );

        public UpdateItemCommand( Item item )
        {
            items.Add( item );
        }

        public UpdateItemCommand( List<Item> itemsValue )
        {
            this.items.AddRange( itemsValue );
        }

        public UpdateItemCommand( )
        {

        }

        public List<Item> Items
        {
            get
            {
                return items;
            }
        }

    }
}
