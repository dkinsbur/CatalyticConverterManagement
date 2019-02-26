using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalyticConverterManagement
{
    public class PurchaseEntry
    {
        public Converter Converter{ get; set; }
        public int Count{ get; set; }
        public int HalfCount{ get; set; }
        public bool ManualPriceSet{ get; set; }
        public double ManualPrice { get; set; }
        public double Factor{ get; set; }
    }
    
    public class Purchase : DataBaseItem
    {
        public UInt32 PlatinumPrice { get; set; }

        public UInt32 PalladiumPrice { get; set; }

        public UInt32 RhodiumPrice { get; set; }

        public Double DollarToShekel { get; set; }

        public Double EuroToShekel { get; set; }

        private List<PurchaseEntry> Entries { get; set; }

    }
}
