using System.Collections.Generic;
using Orca.Core.Persistence;
using Orca.Domain.Objects;

namespace Orca.Domain.Interfaces
{
    public interface IItemFactory
    {
        //      Item CreateItem( string EPC );

        List<Item> AddOrUpdateItems( List<ItemData> itemDataEnumeration, OperationReport report );
        //    List<Item> CreateItems( List<ItemData> itemData, OperationReport report );
        bool ItemAlreadyExist( string EPC );
        void UpdateItem( Item item, ItemData itemData, OperationReport report );
        //void UpdateItem( ItemData itemData, OperationReport report );
    }
}
