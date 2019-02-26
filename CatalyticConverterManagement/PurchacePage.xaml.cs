using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

            PurchaseList = new ObservableCollection<PurchaseEntry>();
            lstPurchase.ItemsSource = PurchaseList;
            tbSearch.Focus();

        }

        public ObservableCollection<PurchaseEntry> PurchaseList;
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            (DataContext as PurchacePageViewModel).UpdateFilter((sender as TextBox).Text);
            list.SelectedIndex = 0;
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                if (waitForEnterRelease)
                {
                    waitForEnterRelease = false;
                }
                else
                {
                    if (list.SelectedItem != null)
                    {
                        tbWhole.Focus();
                    }
                }
            }
            else if (e.Key == Key.Up)
            {
                list.SelectedIndex -= 1;
            }
            else if (e.Key == Key.Down)
            {
                list.SelectedIndex += 1;
            }
            else if (e.Key == Key.Down)
            {
                list.SelectedIndex += 1;
            }



        }

        private void tbWhole_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (waitForEnterRelease)
                {
                    waitForEnterRelease = false;
                }
                else
                {
                    tbHalf.Focus();
                }
            }
        }

        private void tbHalf_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (waitForEnterRelease)
                {
                    waitForEnterRelease = false;
                }
                else
                {
                    btnAdd.Focus();
                }
            }
        }

        private void list_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                tbWhole.Focus();
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var conv = (ctrlConv.DataContext as Converter);
            if (conv == null)
            {
                tbSearch.Focus();

                return;
            }

            int whole = 0;
            int half = 0;
            var txtWhole = tbWhole.Text.Trim();
            var txtHalf = tbHalf.Text.Trim();
            if (txtWhole != "")
            {
                if(!int.TryParse(txtWhole, out whole))
                {
                    MessageBox.Show(string.Format("Bad Whole Value '{0}' ", txtWhole), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    tbWhole.Focus();
                    return;
                }
            }
            if (txtHalf != "")
            {
                if (!int.TryParse(txtHalf, out half))
                {
                    MessageBox.Show(string.Format("Bad Whole Value '{0}' ", txtHalf), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    tbHalf.Focus();
                    return;
                }
            }
            PurchaseList.Insert(0,new PurchaseEntry() { Converter = conv, ManualPriceSet = false, Count = whole, HalfCount = half });
            tbSearch.Text = "";
            tbWhole.Text = "";
            tbHalf.Text = "";
            list.SelectedItem = null;
            tbSearch.Focus();
        }

        bool waitForEnterRelease = false;

        private void GotFocus(object sender, RoutedEventArgs e)
        {
            if(Keyboard.IsKeyDown( Key.Enter))
            {
                waitForEnterRelease = true;
            }
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

        public void UpdateFilter(string filter)
        {
            _filter = filter;
            _converters.Refresh();
        }
        string _filter="";

        public PurchacePageViewModel(List<Converter> converters)
        {
            _converters = CollectionViewSource.GetDefaultView(converters);
            _converters.Filter = ConverterListFilter;
        }

        bool ConverterListFilter(object item)
        {
            var conv = (item as Converter);
            var filtArgs = _filter.Trim().ToUpper().Split(null);
            foreach (var filt in filtArgs)
            {
                if (!conv.FullName.ToUpper().Contains(filt))
                {
                    return false;
                }
            }

            return true;
        }
        public ICollectionView Converters { get { return _converters; } }
    }
}
