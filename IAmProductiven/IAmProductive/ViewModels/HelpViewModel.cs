using IAmProductive.ViewModels.BaseViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAmProductive.ViewModels
{
    public class HelpViewModel : BaseViewModel
    {
        private bool _isEnabledAds;
        public HelpViewModel()
        {

        }
        public bool IsEnabledAds { get { return _isEnabledAds; } set { this._isEnabledAds = value; OnPropertyChanged("IsEnabledAds"); } }
    }
}
