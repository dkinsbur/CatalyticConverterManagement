﻿using System;
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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CatalyticConverterManagement
{
    /// <summary>
    /// Interaction logic for ConverterControl.xaml
    /// </summary>
    public partial class ConverterControl : UserControl
    {
        public ConverterControl()
        {
            InitializeComponent();
        }
    }

    public class EnumToItemsSource : MarkupExtension
    {
        private readonly Type _type;

        public EnumToItemsSource(Type type)
        {
            _type = type;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Enum.GetValues(_type)
                .Cast<object>()
                .Select(e => new { Value = e, DisplayName = e.ToString() });
        }
    }

    class ConverterControlViewModel
    {
        private Converter _converter;
        private bool _editable;

        public ConverterControlViewModel(Converter converter, bool editable)
        {
            _converter = converter;
            _editable = editable;
        }


        public bool CanEdit { get { return _editable; } }
        public String Company { get { return _converter.Company; } set { } }
        public String Model { get { return _converter.Model; } set { } }
        public ConverterCategory Category { get { return _converter.Category; } set { } }

        public String ImagePath
        {
            get
            {
                var img = _converter.Images.FirstOrDefault();
                if (img == null)
                {
                    return null;
                }

                return img.ImagePath;
            }
            set { }
        }
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
                if (_index != value)
                {
                    _index = value;
                    OnPropertyChanged("AnalysisIndex");
                }
            }

        }
    }

}
