using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
/// <summary>
/// Use for Android side (Task Page)
/// </summary>
namespace IAmProductive.Convertors
{
    class HideShowAddTaskBtnConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                string val = (string)value;
                //hide the task which is added with the aim to introducing the new task type (hack entry)
                //this entry insrted at the bottom of db list at the app installation time, 
                //hack entry hided here
                //header which were showing against this enrty ;also hided using this same converter
                //only thing which we want to achieve from the hack entry is to showing "add custom task" for miscellaneous tasks 
                if (val.Equals(Constants.AppConstant.DummyTaskItem) || val.Equals(Constants.AppConstant.addCustomProductiveTaskTextTitle))
                {
                    return false;
                }
                //hide show the add custom task button base on task type
                else if (val.Equals(Constants.AppConstant.ProductiveTaskType) && parameter == null)
                { //hide the top most button which is going to show against the productive task  
                    return false;
                }
                //hide the Dummy task header
                //send labe as heak to determine weather it is header labe call or add custom task button call
                else if (val.Equals(Constants.AppConstant.DummyTaskType) && parameter != null)
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
