using CsvHelper;
using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// <summary>
    /// Interaction logic for PurchacePage.xaml
    /// </summary>
    public partial class PurchacePage : Page
    {
        public PurchacePage()
        {
            //var db = new TempDataBase(@"db.csv");
            //db.Load();

            var db = new DataBaseWrapper(new DataBase("converters.csv", "analysis.csv", "images.csv"));
            db.Load();

            InitializeComponent();
            var purchase = new Purchase();
            DataContext = new PurchacePageViewModel(db.Converters.Values.ToList(), purchase);

            //PurchaseList = new ObservableCollection<PurchaseEntry>();
            tbSearch.Focus();

        }

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
            var vm = DataContext as PurchacePageViewModel;
            var conv = (ctrlConv.DataContext as Converter);
            if (conv == null)
            {
                tbSearch.Focus();

                return;
            }

            uint whole = 0;
            uint half = 0;
            var txtWhole = tbWhole.Text.Trim();
            var txtHalf = tbHalf.Text.Trim();
            if (txtWhole != "")
            {
                if(!uint.TryParse(txtWhole, out whole))
                {
                    MessageBox.Show(string.Format("Bad Whole Value '{0}' ", txtWhole), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    tbWhole.Focus();
                    return;
                }
            }
            if (txtHalf != "")
            {
                if (!uint.TryParse(txtHalf, out half))
                {
                    MessageBox.Show(string.Format("Bad Whole Value '{0}' ", txtHalf), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    tbHalf.Focus();
                    return;
                }
            }
            vm.AddCurrentPurchaseEntry();

            //PurchaseList.Insert(0,new PurchaseEntry() { Converter = conv, ManualPriceSet = false, WholeCount = whole, HalfCount = half });
            tbSearch.Text = "";
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
            if (sender is TextBox)
            {
                var tb = (sender as TextBox);
                tb.Select(0, tb.Text.Length);
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {

        }

    }

    class PurchacePageViewModel : INotifyPropertyChanged
    {
        private ICollectionView _converters;
        private Purchase _purchase;

        
        public void UpdateFilter(string filter)
        {
            _filter = filter;
            _converters.Refresh();
        }
        string _filter="";

        public PurchacePageViewModel(List<Converter> converters, Purchase purchase)
        {
            _converters = CollectionViewSource.GetDefaultView(converters);
            _converters.Filter = ConverterListFilter;
            _purchase = purchase;
            PurchaseList = new ObservableCollection<PurchaseEntry>(purchase.Entries);
            _currPurchaseEntry = new CatalyticConverterManagement.PurchaseEntry();
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

        internal void AddCurrentPurchaseEntry()
        {
            PurchaseList.Insert(0, _currPurchaseEntry);
            CurretPurchaseEntry = new CatalyticConverterManagement.PurchaseEntry();
        }

        public PurchaseEntry CurretPurchaseEntry
        {
            get
            {
                return _currPurchaseEntry;
            }
            set
            {
                if(value != _currPurchaseEntry)
                {
                    _currPurchaseEntry = value;
                    OnPropertyChanged("CurretPurchaseEntry");
                }
            }
        }

        public Converter CurretPurchaseEntryConveretr
        {
            get
            {
                return _currPurchaseEntry.Converter;
            }
            set
            {
                if (value != _currPurchaseEntry.Converter)
                {
                    _currPurchaseEntry.Converter = value;
                    OnPropertyChanged("CurretPurchaseEntryConveretr");
                }
            }
        }



        public ICollectionView Converters { get { return _converters; } }

        private DateTime date = DateTime.Today;

        private PurchaseEntry _currPurchaseEntry;
        public int WholeCount
        {
            get { return _currPurchaseEntry.WholeCount; }
            set { _currPurchaseEntry.WholeCount = value; }
        }

        public int HalfCount
        {
            get { return _currPurchaseEntry.HalfCount; }
            set { _currPurchaseEntry.HalfCount = value; }
        }

        public DateTime Date
        {
            get
            {
                return date;
            }

            set
            {
                date = value;
            }
        }

        public double Pt
        {
            get { return (double)_purchase.PlatinumPrice; }
            set { _purchase.PlatinumPrice = value; }
        }

        public double Pd
        {
            get { return (double)_purchase.PalladiumPrice; }
            set { _purchase.PalladiumPrice = value; }
        }

        public double Rh
        {
            get { return (double)_purchase.RhodiumPrice; }
            set { _purchase.RhodiumPrice = value; }
        }

        public double DollarExchange
        {
            get { return (double)_purchase.DollarToShekel; }
            set { _purchase.DollarToShekel = value; }
        }

        public double EuroExchange
        {
            get { return (double)_purchase.EuroToShekel; }
            set { _purchase.EuroToShekel = value; }
        }

        public ObservableCollection<PurchaseEntry> PurchaseList { get; set; }

        #region Property change
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        #endregion

    }
}
