using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace IAmProductive.Convertors
{
    class IsValidTaskTypeSelectedTriggerConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool flage = false;
            if (value != null)
            {
               
                string text = (string)value;
                if (text.Equals("Select Task Type"))
                {
                    flage = false;
                }
                else if (text.Equals("Select Task"))
                {
                    flage = false;
                }
                else if (text.Equals("Select day") || text.Equals("Select Week") || text.Equals("Select Month"))
                {
                    flage = false;
                }
                else
                {
                    flage = true;
                }
               
            }
            return flage;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
