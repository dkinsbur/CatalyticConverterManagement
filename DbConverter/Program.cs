using CsvHelper;
using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConverter
{
    public class OldDataBaseItem
    {

        [Index(0)]
        public string Company { get; set; }

        [Index(1)]
        public string Description { get; set; }

        [Default(0)]
        [Index(2)]
        public int NumOfSamples { get; set; }

        [Default(0)]
        [Index(3)]
        public int Category { get; set; }

        [Default(0)]
        [Index(4)]
        public double Pt { get; set; }

        [Default(0)]
        [Index(5)]
        public double Pd { get; set; }

        [Default(0)]
        [Index(6)]
        public double Rh { get; set; }

        [Default(0)]
        [Index(7)]
        public double Weight { get; set; }

        [Index(8)]
        public string AnalysisNum { get; set; }
    }
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

    class Program
    {
        static void Main(string[] args)
        {
            List<ConverterDataBaseItem> newConverters = new List<ConverterDataBaseItem>();
            List<AnalysisDataBaseItem> newAnalysis = new List<AnalysisDataBaseItem>();
            using (var reader = new StreamReader(@"db.csv"))
            using (var csv = new CsvReader(reader))
            {
                csv.Configuration.HasHeaderRecord = true;
                //csv.Configuration.Comment = '#';
                csv.Configuration.AllowComments = true;
                try
                {
                    var records = csv.GetRecords<OldDataBaseItem>();
                    int convId = 0;
                    int analysisId = 0;
                    foreach (var rec in records)
                    {
                        var conv = new ConverterDataBaseItem()
                        {
                            Company = rec.Company.Trim(),
                            Model = rec.Description.Trim(),
                            Category = rec.Category,
                        };

                        var analysis = new AnalysisDataBaseItem()
                        {
                             AnalysisNum = rec.AnalysisNum.Trim(),
                              NumOfSamples = rec.NumOfSamples,
                               Pd = rec.Pd,
                               Pt = rec.Pt,
                               Rh = rec.Rh,
                               Weight = rec.Weight,
                        };

                        var prevConv = newConverters.Find(x => x.Company == conv.Company && x.Model == conv.Model);
                        var prevAnalysisv = newAnalysis.Find(x => x.AnalysisNum == analysis.AnalysisNum);
                        if (prevAnalysisv != null)
                        {
                            analysis.AnalysisNum += " _dup_";
                        }

                        if (prevConv != null)
                        {
                            conv = prevConv;
                        }
                        else
                        {
                            convId++;
                            conv.Id = convId;
                            newConverters.Add(conv);
                        }

                        analysisId++;
                        analysis.Id = analysisId;
                        analysis.ConverterId = conv.Id;
                        newAnalysis.Add(analysis);
                    }
                    
                }
                catch (Exception e)
                {
                    Console.WriteLine(string.Format("Error:{1}. line:{0} ", csv.Context.Row, e.Message));
                    return;
                }

            }

            DataBase db = new DataBase("converters.csv", "analysis.csv");

            db.SaveConverters(newConverters);
            db.SaveAnalysis(newAnalysis);
        }
    }

    public class DataBase
    {
        private string analysisDbPath;
        private string converterDbPath;

        public DataBase(string ConverterDbPath, string AnalysisDbPath)
        {
            converterDbPath = ConverterDbPath;
            analysisDbPath = AnalysisDbPath;
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

        public void SaveConverters(List<ConverterDataBaseItem> converters)
        {
            using (var writer = new StreamWriter(converterDbPath))
            using (var csv = new CsvWriter(writer))
            {
                csv.Configuration.HasHeaderRecord = true;
                csv.Configuration.AllowComments = true;
                csv.Configuration.SanitizeForInjection = false;

                csv.WriteRecords<ConverterDataBaseItem>(converters);
            }
        }

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
        public void SaveAnalysis(List<AnalysisDataBaseItem> analysis)
        {
            using (var writer = new StreamWriter(analysisDbPath))
            using (var csv = new CsvWriter(writer))
            {
                csv.Configuration.HasHeaderRecord = true;
                csv.Configuration.AllowComments = true;
                csv.Configuration.SanitizeForInjection = false;

                csv.WriteRecords<AnalysisDataBaseItem>(analysis);
            }
        }
    }
}

