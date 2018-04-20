using IAmProductive.Constants;
using System;
using System.Globalization;
using Xamarin.Forms;
/// <summary>
/// highlight the cell text when user select or add new task
/// </summary>
namespace IAmProductive.Convertors
{
    public class HighlightCellConvertor: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                var val = value as string;
                var hackyBindedKeyLabel = parameter as Label;
                if (val.Equals(AppConstant.addCustomProductiveTaskTextTitle))
                {
                    return Application.Current.Resources["ToolBarColor"];
                }
                if (hackyBindedKeyLabel != null)
                {
                   // bool selectedState = (bool)value;
                    string taskType = hackyBindedKeyLabel.Text;
                    if (taskType.Equals(AppConstant.ProductiveTaskType))
                    {
                        //if (selectedState)
                        //{
                        //    return AppConstant.ProductiveSelectiveTaskColor; 
                        //}
                        //else
                        //{
                            return AppConstant.ProductiveTaskColor;
                       // }
                    }
                    else if(taskType.Equals(AppConstant.UnProductiveTaskType))
                    {
                        //if (selectedState)
                        //{
                        //    return AppConstant.UnProductiveSelectiveTaskColor;
                        //}
                        //else
                        //{
                            return AppConstant.UnProductiveTaskColor;
                        //}
                    }
                    else if (taskType.Equals(AppConstant.MiscellaneousTaskType))
                    {
                        //if (selectedState)
                        //{
                        //    return AppConstant.MiscellaneousSelectiveTaskColor;
                        //}
                        //else
                        //{
                            return AppConstant.MiscellaneousTaskColor;
                       // }
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

