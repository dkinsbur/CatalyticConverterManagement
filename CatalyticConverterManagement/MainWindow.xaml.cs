using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CatalyticConverterManagement
{

    class TempDataBase
    {
        private string _fname;
        private List<TempDataBaseItem> _items;

        class TempDataBaseItem
        {
            public int Id;
            public string Company;
            public string Description;
            public int NumOfSamples;
            public int Category;
            public int Pt;
            public int Pd;
            public int Rh;
            public double Weight;
            public int AnalysisNum;
        }

        public TempDataBase(string fname)
        {
            _fname = fname;
        }

        List<Converter> _converters = new List<CatalyticConverterManagement.Converter>();
        List<Analysis> _analysis = new List<CatalyticConverterManagement.Analysis>();

        public void Load()
        {
            var lines = File.ReadAllLines(_fname);


            for (int i = 1; i < lines.Length; i++)
            {
                var args = lines[i].Split(',');
                bool newConv = false;
                try
                {
                    var converter = GetConverterByModel(args[1].Trim(), args[2].Trim());
                    var analysis = new Analysis();
                    if (converter == null)
                    {
                        newConv = true;
                        converter = new Converter();
                        converter.Company = args[1].Trim();
                        converter.Model = args[2].Trim();
                        int catagory;
                        int.TryParse(args[4], out catagory);
                        converter.Category = (ConverterCategory)catagory;
                    }

                    int.TryParse(args[3], out analysis.NumOfSamples);
                    int.TryParse(args[5], out analysis.Palladium);
                    int.TryParse(args[6], out analysis.Platinum);
                    int.TryParse(args[7], out analysis.Rhodium);
                    double.TryParse(args[8], out analysis.Weight);
                    int.TryParse(args[9], out analysis.AnalysisNum);

                    analysis.Converter = converter;
                    converter.Analysis.Add(analysis);

                    if (newConv)
                    {
                        _converters.Add(converter);
                    }
                    _analysis.Add(analysis);

                }
                catch
                {
                    Debug.WriteLine("{0}: {1}", i, lines[i]);
                    throw;
                }


            }

        }
        public Converter GetConverterByModel(string Company, string Model)
        {
            return _converters.Find(c => c.Company == Company.Trim() && c.Model == Model.Trim());
        }

        public List<Converter> GetConverters()
        {
            return _converters;
        }

        public List<Analysis> GetAnalysis()
        {
            return _analysis;
        }



    }



    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            //var js = new JsonDatabase("database.json");
            //js.AddAnalysis(new AnalysisDb());



            var b = new Converter() {
                Analysis = new List<Analysis>()
                {
                    new Analysis()
                    {
                        AnalysisNum = 12,
                        NumOfSamples = 2,
                        Palladium = 3,
                        Platinum = 5,
                        Rhodium = 7,
                        Weight = 1.9
                    },
                    new Analysis()
                    {
                        AnalysisNum = 34,
                        NumOfSamples = 1,
                        Palladium = 333,
                        Platinum = 599,
                        Rhodium = 700,
                        Weight = 0.89
                    }
                },
                Category = ConverterCategory.Beads,
                Company = "Com",
                Model = "mod",
                ImagePath = @"C:\Users\dkinsbur\Desktop\tmp.png"
            };

            //this.DataContext = new MainWindowViewModel(b, false);//new AnalysisControlViewModel(a, false);
            InitializeComponent();

            var page = new PurchacePage();
            this.Content = page;


        }
    }
}
