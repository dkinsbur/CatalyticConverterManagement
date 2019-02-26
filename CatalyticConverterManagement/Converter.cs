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

    public class DataBaseItem
    {
        public UInt32 ID { get; set; }
    }

    public class Analysis : DataBaseItem
    {
        public int AnalysisNum;
        public int Platinum ; // parts per milion - grams per kilo
        public int Palladium ;
        public int Rhodium ;
        public double Weight ; // . weight - kilo
        public int NumOfSamples ;
        public Converter Converter ;
    }

    public class Converter : DataBaseItem
    {
        public String Company { get; set; }
        public String Model { get; set; } // Description
        public ConverterCategory Category { get; set; }
        public List<Analysis> Analysis { get; set; }
        public String ImagePath;

        public string FullName
        {
            get
            {
                return string.Format("{0} - {1}", Company, Model);
            }
        }

        public Converter()
        {
            Analysis = new List<CatalyticConverterManagement.Analysis>();
        }
    }
}
