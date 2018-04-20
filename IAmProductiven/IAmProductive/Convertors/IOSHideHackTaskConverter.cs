using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
/// <summary>
/// Use for IOS side (Task Page)
/// </summary>
namespace IAmProductive.Convertors
{
    public class IOSHideHackTaskConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                string val = (string)value;
                //Hide hack item
                if (val.Equals(Constants.AppConstant.DummyTaskItem))
                {
                    return false;
                }
                // "parameter" pass here just to identify this call for header hiding
                //hide hack item header
                else if (val.Equals(Constants.AppConstant.DummyTaskType) && parameter != null)
                {
                    return false;
                }
                else
                    return true;
            }
            else
                 return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
