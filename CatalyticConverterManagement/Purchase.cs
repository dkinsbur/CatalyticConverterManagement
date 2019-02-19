using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalyticConverterManagement
{
    
    class PurchaseEntry
    {
        private UInt32 ID { get; set; }


    }

    public class Purchase
    {
        private UInt32 ID { get; set; }

        public UInt32 PlatinumPrice { get; set; }

        public UInt32 PalladiumPrice { get; set; }

        public UInt32 RhodiumPrice { get; set; }

        public Double DollarToShekel { get; set; }

        private  List<PurchaseEntry> Entries { get; set; }

        


    }
}
