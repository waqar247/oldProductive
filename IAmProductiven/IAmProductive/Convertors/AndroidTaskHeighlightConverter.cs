using IAmProductive.Constants;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace IAmProductive.Convertors
{
    public class AndroidTaskHeighlightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                var val = value as string;
                var hackyBindedKeyLabel = parameter as Label;
                if (hackyBindedKeyLabel != null)
                {
                     bool selectedState = (bool)value;
                    string taskType = hackyBindedKeyLabel.Text;
                    if (taskType.Equals(AppConstant.ProductiveTaskType))
                    {
                        if (selectedState)
                        {
                            return AppConstant.ProductiveSelectiveTaskColor;
                        }
                        else
                        {
                            return AppConstant.ProductiveTaskColor;
                         }
                    }
                    else if (taskType.Equals(AppConstant.UnProductiveTaskType))
                    {
                        if (selectedState)
                        {
                            return AppConstant.UnProductiveSelectiveTaskColor;
                        }
                        else
                        {
                            return AppConstant.UnProductiveTaskColor;
                        }
                    }
                    else if (taskType.Equals(AppConstant.MiscellaneousTaskType))
                    {
                        if (selectedState)
                        {
                            return AppConstant.MiscellaneousSelectiveTaskColor;
                        }
                        else
                        {
                            return AppConstant.MiscellaneousTaskColor;
                         }
                    }


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
