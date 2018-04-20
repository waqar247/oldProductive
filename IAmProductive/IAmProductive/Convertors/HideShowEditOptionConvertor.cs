using System;
using System.Globalization;
using Xamarin.Forms;

namespace IAmProductive.Convertors
{
    public class HideShowEditOptionConvertor: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                string taskTitle = (string)value;
                if (taskTitle.Equals(Constants.AppConstant.addCustomProductiveTaskTextTitle))
                {
                    return false;
                }
                else if (taskTitle.Equals(Constants.AppConstant.addCustomUnProductiveTaskTextTitle))
                {
                    return false;
                }
                else if (taskTitle.Equals(Constants.AppConstant.addCustomMiscellaneousTaskTextTitle))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            return false;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}


