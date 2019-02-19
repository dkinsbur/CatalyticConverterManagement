using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalyticConverterManagement
{

    public enum ConverterCategory
    {
        ForeignSmall = 1,
        ForeignLarge = 2,
        ForeignPre = 3,
        DomesticSmall = 4,
        DomesticLarge = 5,
        StainlessPre = 6,
        Aftermarket = 7,
        Beads = 8,
    }

    public class Analysis
    {
        public UInt32 ID { get; set; }
        public UInt32 AnalysisNum { get; set; } // parts per milion - grams per kilo
        public UInt32 Platinum { get; set; } // parts per milion - grams per kilo

        public UInt32 Palladium { get; set; }

        public UInt32 Rhodium { get; set; }

        public Double Weight { get; set; } // . weight - kilo

        public UInt32 NumOfSamples { get; set; }
    }

    public class Converter
    {
        public UInt32 ID { get; set; }
        public String Company { get; set; }
        public String Model { get; set; } // Description
        public ConverterCategory Category { get; set; }
        public List<Analysis> Analysis { get; set; }

        public String ImagePath;

    }
}
