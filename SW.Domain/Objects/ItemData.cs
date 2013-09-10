using System;

namespace Orca.Domain.Objects
{
    public class ItemData : ExternalDataBaseObject
    {
        public virtual string DefaultLocationSgln { get; set; }
        public virtual string CurrentLocationSgln { get; set; }
        public virtual string Epc { get; set; }

        string tostringResult;


        /// <summary>
        /// Initializes a new instance of the ItemData class.
        /// </summary>
        public ItemData()
        {
            DefaultLocationSgln = Guid.Empty.ToString();
            CurrentLocationSgln = Guid.Empty.ToString();
        }

        public ItemData(string EPC)
        {
            this.Epc = EPC;
        }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(tostringResult))
            {
                tostringResult = string.Format("Item Data object values:  Epc = '{0}'; CurrentlocationSlgn = '{1}'; DefaultLocationSgln = '{2}'", Epc, CurrentLocationSgln, DefaultLocationSgln);
            }
            return tostringResult;
        }

    }
}
