using CsvHelper;
using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalyticConverterManagement
{
//    class TempDataBase
//    {
//        private string _fname;
//        private List<TempDataBaseItem> _items;
//
//        class TempDataBaseItem
//        {
//            public int Id;
//            public string Company;
//            public string Description;
//            public int NumOfSamples;
//            public int Category;
//            public int Pt;
//            public int Pd;
//            public int Rh;
//            public double Weight;
//            public int AnalysisNum;
//        }
//
//        public TempDataBase(string fname)
//      {
//          _fname = fname;
//      }
//
//        List<Converter> _converters = new List<CatalyticConverterManagement.Converter>();
//        List<Analysis> _analysis = new List<CatalyticConverterManagement.Analysis>();
//
//        public void Load()
//      {
//          var lines = File.ReadAllLines(_fname);
//
//
//          for (int i = 1; i < lines.Length; i++)
//          {
//              var args = lines[i].Split(',');
//              bool newConv = false;
//              try
//              {
//                  var converter = GetConverterByModel(args[1].Trim(), args[2].Trim());
//                  var analysis = new Analysis();
//                  if (converter == null)
//                  {
//                      newConv = true;
//                      converter = new Converter();
//                      converter.Company = args[1].Trim();
//                      converter.Model = args[2].Trim();
//                      int catagory;
//                      int.TryParse(args[4], out catagory);
//                      converter.Category = (ConverterCategory)catagory;
//                  }
//
//                  int.TryParse(args[3], out analysis.NumOfSamples);
//                  int.TryParse(args[5], out analysis.Palladium);
//                  int.TryParse(args[6], out analysis.Platinum);
//                  int.TryParse(args[7], out analysis.Rhodium);
//                  double.TryParse(args[8], out analysis.Weight);
//                  int.TryParse(args[9], out analysis.AnalysisNum);
//
//                  analysis.Converter = converter;
//                  converter.Analysis.Add(analysis);
//
//                  if (newConv)
//                  {
//                      _converters.Add(converter);
//                  }
//                  _analysis.Add(analysis);
//
//              }
//              catch
//              {
//                  Debug.WriteLine("{0}: {1}", i, lines[i]);
//                  throw;
//              }
//
//
//          }
//
//      }
//        public Converter GetConverterByModel(string Company, string Model)
//      {
//          return _converters.Find(c => c.Company == Company.Trim() && c.Model == Model.Trim());
//      }
//
//        public List<Converter> GetConverters()
//      {
//          return _converters;
//      }
//
//        public List<Analysis> GetAnalysis()
//      {
//          return _analysis;
//      }
//
//
//
//    }

    public class AnalysisDataBaseItem
    {
        [Name("Id")]
        public int Id { get; set; }

        [Name("Analysis#")]
        public string AnalysisNum { get; set; }

        [Name("Num Of Samples")]
        public int NumOfSamples { get; set; }

        [Name("Pt")]
        public double Pt { get; set; }

        [Name("Pd")]
        public double Pd { get; set; }

        [Name("Rh")]
        public double Rh { get; set; }

        [Name("Weight")]
        public double Weight { get; set; }

        [Name("ConverterId")]
        public int ConverterId { get; set; }
    }
    public class ConverterDataBaseItem
    {
        [Name("Id")]
        public int Id { get; set; }

        [Name("Company")]
        public string Company { get; set; }

        [Name("Model")]
        public string Model { get; set; }

        [Name("Category")]
        public int Category { get; set; }
    }

    public class ImagePathDataBaseItem
    {
        [Name("Id")]
        public int Id { get; set; }

        [Name("ImagePath")]
        public string ImagePath { get; set; }

        [Name("ConverterId")]
        public int ConverterId { get; set; }
    }
    public class DataBase
    {
        private string analysisDbPath;
        private string converterDbPath;
        private string imageDbPath;

        public DataBase(string ConverterDbPath, string AnalysisDbPath, string ImageDbPath)
        {
            converterDbPath = ConverterDbPath;
            analysisDbPath = AnalysisDbPath;
            imageDbPath = ImageDbPath;
        }

        public List<ConverterDataBaseItem> LoadConverters()
        {
            using (var reader = new StreamReader(converterDbPath))
            using (var csv = new CsvReader(reader))
            {
                csv.Configuration.HasHeaderRecord = true;
                csv.Configuration.AllowComments = true;

                return csv.GetRecords<ConverterDataBaseItem>().ToList();
            }
        }

        public List<ImagePathDataBaseItem> LoadImages()
        {
            using (var reader = new StreamReader(imageDbPath))
            using (var csv = new CsvReader(reader))
            {
                csv.Configuration.HasHeaderRecord = true;
                csv.Configuration.AllowComments = true;

                return csv.GetRecords<ImagePathDataBaseItem>().ToList();
            }
        }

        //public void SaveConverters(List<ConverterDataBaseItem> converters)
        //{
        //    using (var writer = new StreamWriter(converterDbPath))
        //    using (var csv = new CsvWriter(writer))
        //    {
        //        csv.Configuration.HasHeaderRecord = true;
        //        csv.Configuration.AllowComments = true;
        //        csv.Configuration.SanitizeForInjection = false;
        //
        //        csv.WriteRecords<ConverterDataBaseItem>(converters);
        //    }
        //}

        public List<AnalysisDataBaseItem> LoadAnalysis()
        {
            using (var reader = new StreamReader(analysisDbPath))
            using (var csv = new CsvReader(reader))
            {
                csv.Configuration.HasHeaderRecord = true;
                csv.Configuration.AllowComments = true;

                return csv.GetRecords<AnalysisDataBaseItem>().ToList();
            }
        }
        //public void SaveAnalysis(List<AnalysisDataBaseItem> analysis)
        //{
        //    using (var writer = new StreamWriter(analysisDbPath))
        //    using (var csv = new CsvWriter(writer))
        //    {
        //        csv.Configuration.HasHeaderRecord = true;
        //        csv.Configuration.AllowComments = true;
        //        csv.Configuration.SanitizeForInjection = false;
        //
        //        csv.WriteRecords<AnalysisDataBaseItem>(analysis);
        //    }
        //}
    }
}
