using System;
using System.Collections.Generic;
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
            this.Content = new PurchacePage();

        }
    }
}
