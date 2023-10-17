using ShareMarketData.Command;
using ShareMarketData.DataReader;
using ShareMarketData.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace ShareMarketData.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public int _valueFrom { get; set; }

        public int _totoalCount { get; set; }
        public int _valueTo { get; set; }
        public static MainWindowViewModel Instance { get; set; }
        public ICommand LodaSMDataCommand { get; set; }
        public ICommand IsSortbyNameCommand { get; set; }

        public ICommand DayDiffCommand { get; set; }

        public ICommand CheckCommand { get; set; }

        private IList<QueryResultModel> _usersCq;
        private IList<QueryResultModel> _100Price;
        
        private bool _isSortbyName;

        public MainWindowViewModel()
        {

            _100Price = GetAllQueryResult().Where(e => e.SctySrs == "EQ" && e.OpnPric <= 100).ToList();
           
            LodaSMDataCommand = new RelayCommand(LoadData, CanLoadData);
            IsSortbyNameCommand = new RelayCommand(SortbyName, CanSortbyName);
            DayDiffCommand = new RelayCommand(DayDiff, CanDayDiff);

            _usersCq = GetAllQueryResult().Where(e => e.SctySrs == "EQ").ToList();
            _totoalCount = _usersCq.Count();
            CompareItemFrom = GetCompareItem();
            CompareItemTo = GetCompareItem();
        }

        private string _selectedItemFrom;
        public string SelectedItemFrom
        {
            get
            {
                return _selectedItemFrom;
            }
            set
            {
                _selectedItemFrom = value;
                NotifyPropertyChanged("SelectedItemFrom");
            }
        }

        private string _selectedItemTo;
        public string SelectedItemTo
        {
            get
            {
                return _selectedItemTo;
            }
            set
            {
                _selectedItemTo = value;
                NotifyPropertyChanged("SelectedItemTo");
            }
        }

        private ObservableCollection<string> _compareItemFrom;
        public ObservableCollection<string> CompareItemFrom
        {
            get
            {
                return _compareItemFrom;
            }
            set
            {
                _compareItemFrom = value;
                NotifyPropertyChanged("CompareItemFrom");
            }
        }

        private ObservableCollection<string> _compareItemTo;
        public ObservableCollection<string> CompareItemTo
        {
            get
            {
                return _compareItemTo;
            }
            set
            {
                _compareItemTo = value;
                NotifyPropertyChanged("CompareItemTo");
            }
        }

        private bool CanDayDiff(object arg)
        {
            return true;
        }

        private ObservableCollection<string> GetCompareItem()
        {
            ObservableCollection<string> items = new ObservableCollection<string>();
            items.Add("Open Price");
            items.Add("High Price");
            items.Add("Low Price");
            items.Add("Close Price");
            items.Add("Last Price");
            items.Add("Pre close Price");
            return items;

        }
        private void DayDiff(object obj)
        {
            if (!string.IsNullOrEmpty(SelectedItemFrom) && !string.IsNullOrEmpty(SelectedItemTo))
            {
                foreach (QueryResultModel _userCq in _usersCq)
                {
                    _userCq.dummy = _userCq.PrvsClsgPric - _userCq.ClsPric;
                }

                UsersCq = _usersCq.OrderBy(x => x.dummy).ToList();
            }
        }

        private void SortbyName(object obj)
        {
            if (_isSortbyName) UsersCq.OrderBy(x => x.TckrSymb);
            else
                UsersCq = _usersCq;
        }

        private bool CanSortbyName(object arg)
        {
            return true;
        }

        public int TotoalCount
        {
            get { return _totoalCount; }
            set {
                _totoalCount = value;
                this.NotifyPropertyChanged("TotoalCount");
            }
        }

        public int ValueTo
        {
            get { return _valueTo; }
            set { _valueTo = value; }
        }
        public int ValueFrom
        {
            get { return _valueFrom; }
            set { _valueFrom = value; }
        }
        public IList<QueryResultModel> UsersCq
        {
            get
            {
                return _usersCq;
            }
            set
            {
                _usersCq = value;
                this.NotifyPropertyChanged("UsersCq");
            }
        }

        
        private void LoadData(object value)
        {
            if (_valueFrom > 0 && _valueTo > 0)
            {
                UsersCq = GetAllQueryResult().Where(e => e.SctySrs == "EQ" && e.OpnPric >= _valueFrom && e.OpnPric <= _valueTo).ToList();
                TotoalCount = UsersCq.Count();
            }
               
           
        }

        private bool CanLoadData(object value)
        {
            if (_valueFrom > 0 && _valueTo > 0)
                return true;
            else
                return false;
        }

        public bool IsSortbyName
        {
            get { return _isSortbyName; }
            set { _isSortbyName = value; }
        }



       
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public ObservableCollection<QueryResultModel> GetAllQueryResult()
        {
            string path = $"DataReader/09092023.csv";
            StreamReader sr = new StreamReader(path);
            ObservableCollection<QueryResultModel> importingData = new ObservableCollection<QueryResultModel>();

            var source = File.ReadAllLines(path)
                  .Skip(1)
                  .Where(l => l.Length > 1);
            foreach (var line in source)
            {

                var columns = line.Split(',');

                importingData.Add(new QueryResultModel
                {
                    ISIN = columns[0],
                    TckrSymb = columns[1],
                    SctySrs = columns[2],
                    OpnPric = Convert.ToDecimal(columns[3]),
                    HghPric = Convert.ToDecimal(columns[4]),
                    LwPric = Convert.ToDecimal(columns[5]),
                    ClsPric = Convert.ToDecimal(columns[6]),
                    LastPric = Convert.ToDecimal(columns[7]),
                    PrvsClsgPric = Convert.ToDecimal(columns[8]),
                    TtlTradgVol = Convert.ToInt32(columns[9]),
                    TtlTrfVal = Convert.ToDecimal(columns[10]),
                    TradDt = columns[11],
                    TtlNbOfTxsExctd = Convert.ToInt32(columns[12]),
                });
            }

            return importingData;
        }

       

    }

   

  
}
