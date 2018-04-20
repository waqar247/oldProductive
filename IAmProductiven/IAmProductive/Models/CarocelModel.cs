using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAmProductive.Models
{
    public class CarocelModel  : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private ObservableCollection<DayTask> _taskForDayList;
        private bool _isShowError;
        private bool _isShowList;
        private bool _isEnableCell;
        private string _monthAndDate;
        private DateTime _monthAndDateForCarocelPage;
        public CarocelModel()
        {
            _taskForDayList = new ObservableCollection<DayTask>();
        }
        public ObservableCollection<DayTask> ModelTaskForDayList
        {
            get { return _taskForDayList; }
            set { _taskForDayList = value; OnPropertyChanged("ModelTaskForDayList"); 
            }
        }
        public bool IsShowError
        {
            get { return _isShowError; }
            set
            {
                _isShowError = value; OnPropertyChanged("IsShowError");
            }

        }
        public bool IsShowList
        {
            get { return _isShowList; }
            set
            {
                _isShowList = value; OnPropertyChanged("IsShowList");
            }

        }
        public bool IsEnableCell
        {
            get { return _isEnableCell; }
            set
            {
                _isEnableCell = value; OnPropertyChanged("IsShowList");
            }

        }

        public string MonthAndDate
        {
            get { return _monthAndDate; }
            set
            {
                _monthAndDate = value; OnPropertyChanged("MonthAndDate");
            }
        }
        /// <summary>
        /// use to show hide the add task button on toolbar
        /// </summary>
        public DateTime CurrentDateForCurrentCarocel
        {
            get { return _monthAndDateForCarocelPage; }
            set
            {
                _monthAndDateForCarocelPage = value; OnPropertyChanged("CurrentDateForCurrentCarocel");
            }
        }
        public void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this,
              new PropertyChangedEventArgs(propertyName));
        }
    }
}
