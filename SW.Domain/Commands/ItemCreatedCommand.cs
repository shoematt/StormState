using System;
using System.Collections.Generic;
using Orca.Domain.Objects;

namespace Orca.Domain.Commands
{
    [Serializable]
    public class ItemCreatedCommand
    {

        List<Item> items = new List<Item>( );


        /// <summary>
        /// Initializes a new instance of the <see cref="ItemComplete"/> class.
        /// </summary>
        public ItemCreatedCommand( )
        {
        }


        public ItemCreatedCommand( List<Item> itemsValue )
            : this( )
        {

            this.items.AddRange( itemsValue );
        }

        public ItemCreatedCommand( Item item )
        {
            items.Add( item );
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
