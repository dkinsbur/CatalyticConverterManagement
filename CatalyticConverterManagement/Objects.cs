using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalyticConverterManagement
{
    public class PurchaseEntry
    {
        public Converter Converter { get; set; }
        public int Count { get; set; }
        public int HalfCount { get; set; }
        public bool ManualPriceSet { get; set; }
        public double ManualPrice { get; set; }
        public double Factor { get; set; }
    }

    public class Purchase
    {
        public UInt32 PlatinumPrice { get; set; }

        public UInt32 PalladiumPrice { get; set; }

        public UInt32 RhodiumPrice { get; set; }

        public Double DollarToShekel { get; set; }

        public Double EuroToShekel { get; set; }

        private List<PurchaseEntry> Entries { get; set; }

    }


    public class DataBaseItem
    {
        public UInt32 ID { get; set; }
    }

    public class Analysis
    {
        public string AnalysisNum
        {
            get { return _dbAnlysis.AnalysisNum; }
            private set { _dbAnlysis.AnalysisNum = value; }
        }
        public double Platinum // parts per milion - grams per kilo
        {
            get { return _dbAnlysis.Pt; }
            private set { _dbAnlysis.Pt = value; }
        }
        public double Palladium
        {
            get { return _dbAnlysis.Pd; }
            private set { _dbAnlysis.Pd = value; }
        }
        public double Rhodium
        {
            get { return _dbAnlysis.Rh; }
            private set { _dbAnlysis.Rh = value; }
        }
        public double Weight // . weight - kilo
        {
            get { return _dbAnlysis.Weight; }
            private set { _dbAnlysis.Weight = value; }
        }
        public int NumOfSamples
        {
            get { return _dbAnlysis.NumOfSamples; }
            private set { _dbAnlysis.NumOfSamples = value; }
        }
        public Converter Converter { get; private set; }
        private AnalysisDataBaseItem _dbAnlysis;

        public Analysis(AnalysisDataBaseItem dbAnl, Converter converter = null)
        {
            _dbAnlysis = dbAnl;
            Converter = converter;
        }
    }

    public class ConverterImage
    {
        private ImagePathDataBaseItem _img;

        public Converter Converter { get ; private set; }
        public string ImagePath
        {
            get { return _img.ImagePath; }
            private set { _img.ImagePath = value; }
        }

        public ConverterImage(ImagePathDataBaseItem img, Converter converter = null)
        {
            _img = img;
            Converter = converter;
        }
    }

    public class Converter 
    {
        public String Company
        {
            get
            {
                return _dbConv.Company;
            }
            private set
            {
                _dbConv.Company = value;
            }
        }
        public String Model
        {
            get { return _dbConv.Model; }
            private set { _dbConv.Model = value; }
        }
        public ConverterCategory Category
        {
            get { return (ConverterCategory) _dbConv.Category; }
            private set { _dbConv.Category = (int) value; }
        }
        public List<Analysis> Analysis { get; private set; }

        public List<ConverterImage> Images { get; private set; }

        private ConverterDataBaseItem _dbConv;

        public string FullName
        {
            get
            {
                return string.Format("{0} - {1}", Company, Model);
            }
        }

        public Converter(ConverterDataBaseItem dbConv)
        {
            _dbConv = dbConv;
            Analysis = new List<Analysis>();
            Images = new List<CatalyticConverterManagement.ConverterImage>();
        }
    }

    public class DataBaseWrapper
    {
        private DataBase _db;
        Dictionary<int, Converter> _converters;
        Dictionary<int, Analysis> _analysis;
        Dictionary<int, ConverterImage> _images;

        public DataBaseWrapper(DataBase db)
        {
            _db = db;
            _converters = new Dictionary<int, CatalyticConverterManagement.Converter>();
            _analysis = new Dictionary<int, CatalyticConverterManagement.Analysis>();
        }

        public void Load()
        {
            _converters = new Dictionary<int, Converter>();

            foreach (var conv in _db.LoadConverters())
            {
                if(_converters.ContainsKey(conv.Id))
                {
                    throw new Exception(string.Format("Duplicate converter ID: {0}", conv.Id));
                }

                _converters[conv.Id] = new Converter(conv);
            }

            _analysis = new Dictionary<int, Analysis>();

            foreach (var anl in _db.LoadAnalysis())
            {
                if (_analysis.ContainsKey(anl.Id))
                {
                    throw new Exception(string.Format("Duplicate analysis ID: {0}", anl.Id));
                }

                _analysis[anl.Id] = new Analysis(anl, _converters[anl.ConverterId]);
                _converters[anl.ConverterId].Analysis.Add(_analysis[anl.Id]);
            }

            _images = new Dictionary<int, ConverterImage>();
            foreach (var img in _db.LoadImages())
            {
                if (_images.ContainsKey(img.Id))
                {
                    throw new Exception(string.Format("Duplicate image ID: {0}", img.Id));
                }
                _images[img.Id] = new ConverterImage(img, _converters[img.ConverterId]);
                _converters[img.ConverterId].Images.Add(_images[img.Id]);
            }
        }

        public Dictionary<int, Converter> Converters { get { return _converters; } }

        public Dictionary<int, Analysis> Analysis { get { return _analysis; } }

        public Dictionary<int, ConverterImage> Images { get { return _images; } }

    }
}