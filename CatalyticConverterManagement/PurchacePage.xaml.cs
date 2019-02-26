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
    /// Interaction logic for PurchacePage.xaml
    /// </summary>
    public partial class PurchacePage : Page
    {
        public PurchacePage()
        {
            var db = new TempDataBase(@"db.csv");
            db.Load();
            //DisplayMemberPath="FullName"

            InitializeComponent();

            DataContext = new PurchacePageViewModel(db.GetConverters());

        }
    }

    class PurchacePageViewModel : INotifyPropertyChanged
    {
        private ICollectionView _converters;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        public PurchacePageViewModel(List<Converter> converters)
        {
            _converters = CollectionViewSource.GetDefaultView(converters);
        }

        public ICollectionView Converters { get { return _converters; } }
    }
}
