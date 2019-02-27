using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for AnalysisControl.xaml
    /// </summary>
    public partial class AnalysisControl : UserControl
    {
        public AnalysisControl()
        {
            InitializeComponent();
        }
    }

    public class AnalysisControlViewModel : INotifyPropertyChanged
    {
        private Analysis _analysis;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        public double Platinum
        {
            get
            {
                return this._analysis.Platinum;
            }
            set
            {
                //this._analysis.Platinum = value;
            }
        }

        public double Palladium
        {
            get
            {
                return this._analysis.Palladium;
            }
            set
            {
                //this._analysis.Palladium = value;
            }
        }

        public double Rhodium
        {
            get
            {
                return this._analysis.Rhodium;
            }
            set
            {
                //this._analysis.Rhodium = value;
            }
        }

        public double Weight
        {
            get
            {
                return this._analysis.Weight;
            }
            set
            {
                //this._analysis.Weight = value;
            }
        }
        public int NumOfSamples
        {
            get
            {
                return this._analysis.NumOfSamples;
            }
            set
            {
                //this._analysis.NumOfSamples = value;
            }
        }


        public AnalysisControlViewModel(Analysis analysis, bool editable)
        {
            this._analysis = analysis;
            this.CanEdit = editable;
        }

        public bool CanEdit { get; set; }
    }
}
