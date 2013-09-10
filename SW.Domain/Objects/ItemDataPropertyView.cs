using System;

namespace Orca.Domain.Objects
{
    public class ItemDataPropertyView
    {
        public string ItemProperty { get; set; }
        public string Epc { get; set; }
        public string ItemStatus { get; set; }
        public string ItemPropertyValue { get; set; }
        public Guid ItemId { get; set; }


    }
}
