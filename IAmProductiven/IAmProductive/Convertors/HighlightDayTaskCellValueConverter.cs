using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace IAmProductive.Convertors
{
    public class HighlightDayTaskCellValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                bool val = System.Convert.ToBoolean(value.ToString());
                if (val)
                {
                     return (Color)Application.Current.Resources["ToolBarColor"];
                    //return Color.Red;
                }
                else
                {
                    return Color.White;
                }

            }
            return Color.White;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
