using System;
using System.Collections.Generic;
using Orca.Domain.Cache;
using Orca.Domain.Objects;
using Orca.Domain.Cache;

namespace Orca.Domain.Interfaces
{
    public interface IInternalItemProviderService : IOrcaService
    {
        void GetItemsByProperties(PropertyValueSearchCriteria searchCriteria, Action<List<Item>> action);

        void GetItemInfoByEpc(List<string> epcList, Action<List<Item>> action);

        bool IsConnected();
    }
}
