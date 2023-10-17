using ShareMarketData.Command;
using ShareMarketData.Logic;
using ShareMarketData.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;


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

        private IList<StocksResultModel> _stocksList;

        public DataLogic dataLogic { get; set; }

        private bool _isSortbyName;

        public MainWindowViewModel()
        {

            dataLogic = new DataLogic();
            LodaSMDataCommand = new RelayCommand(LoadData, CanLoadData);
            IsSortbyNameCommand = new RelayCommand(SortbyName, CanSortbyName);
            DayDiffCommand = new RelayCommand(DayDiff, CanDayDiff);

            _stocksList = dataLogic.GetAllStockResult().Where(e => e.SctySrs == "EQ").ToList();
            _totoalCount = _stocksList.Count();
            CompareItemFrom = dataLogic.GetCompareItem();
            CompareItemTo = dataLogic.GetCompareItem();
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

       
        private void DayDiff(object obj)
        {
            if (!string.IsNullOrEmpty(SelectedItemFrom) && !string.IsNullOrEmpty(SelectedItemTo))
            {
                foreach (StocksResultModel _userCq in _stocksList)
                {
                    _userCq.dummy = _userCq.PrvsClsgPric - _userCq.ClsPric;
                }

                UsersCq = _stocksList.OrderBy(x => x.dummy).ToList();
            }
        }

        private void SortbyName(object obj)
        {
            if (_isSortbyName) UsersCq.OrderBy(x => x.TckrSymb);
            else
                UsersCq = _stocksList;
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
        public IList<StocksResultModel> UsersCq
        {
            get
            {
                return _stocksList;
            }
            set
            {
                _stocksList = value;
                this.NotifyPropertyChanged("UsersCq");
            }
        }

        
        private void LoadData(object value)
        {
            if (_valueFrom > 0 && _valueTo > 0)
            {
                UsersCq = dataLogic.GetAllStockResult().Where(e => e.SctySrs == "EQ" && e.OpnPric >= _valueFrom && e.OpnPric <= _valueTo).ToList();
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
         

    }

   

  
}
