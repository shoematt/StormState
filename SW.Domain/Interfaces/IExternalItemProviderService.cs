using System;
using System.Collections.Generic;
using Orca.Domain.Cache;
using Orca.Domain.Objects;
using Orca.Domain.Cache;

namespace Orca.Domain.Interfaces
{
    public interface IExternalItemProviderService : IOrcaService
    {

        void GetItemsByProperties( PropertyValueSearchCriteria searchCriteria, Action<List<Item>> action );

        void GetItemInfoByEpc( HashSet<string> epcList, Action<List<Item>> action );

        bool IsConnected( );
    }




}