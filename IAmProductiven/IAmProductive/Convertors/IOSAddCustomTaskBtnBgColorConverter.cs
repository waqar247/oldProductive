using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace IAmProductive.Convertors
{
    class IOSAddCustomTaskBtnBgColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                string taskTitle = (string)value;
                if (taskTitle.Equals(Constants.AppConstant.addCustomProductiveTaskTextTitle) )
                {
                    return Application.Current.Resources["AddCustomTaskBgColor"];
                }
                //else if (taskTitle.Equals(Constants.AppConstant.addCustomUnProductiveTaskTextTitle))
                //{
                //    return Application.Current.Resources["AddCustomTaskBgColor"];
                //}
                //else if (taskTitle.Equals(Constants.AppConstant.addCustomMiscellaneousTaskTextTitle))
                //{
                //    return Application.Current.Resources["AddCustomTaskBgColor"];
                //}
                else
                {
                    return Color.White;
                }

            }

            return Color.Transparent;
        }
            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
