using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CatalyticConverterManagement
{
    class ConverterControlViewModel
    {
        private Converter _converter;

        
        public ConverterControlViewModel(Converter converter, bool editable)
        {
            _converter = converter;
        }


        public String Company { get { return _converter.Company; } set { } }
        public String Model { get { return _converter.Model; } set { } }
        public ConverterCategory Category { get { return _converter.Category; } set { } }

        public String ImagePath { get { return _converter.ImagePath; } set { } }
    }

    class MainWindowViewModel : INotifyPropertyChanged
    {
        private ConverterControlViewModel _converter;
        private int _index = 0;
        private List<AnalysisControlViewModel> _analysis;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }


        public MainWindowViewModel(Converter converter, bool editable)
        {
            _converter = new ConverterControlViewModel(converter, editable);
            _analysis = (from analysis in converter.Analysis
                         select new AnalysisControlViewModel(analysis, editable)).ToList();

            PropertyChanged += MainWindowViewModel_PropertyChanged;
        }

        private void MainWindowViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "AnalysisIndex")
                OnPropertyChanged("Analysis");
        }

        public ConverterControlViewModel Converter { get { return _converter; } }
        public AnalysisControlViewModel Analysis
        {
            get
            {
                if (_analysis.Count == 0)
                {
                    return null;
                }

                return _analysis[_index];
            }
        }

        public int AnalysisMax
        {
            get
            {
                return _analysis.Count > 0 ? _analysis.Count - 1 : 0;
            }
        }

        public int AnalysisIndex
        {
            set
            {
                if(_index != value)
                {
                    _index = value;
                    OnPropertyChanged("AnalysisIndex");
                }
            }
            
        }
    }
}
