using Foundation;
using IAmProductive.Constants;
using IAmProductive.Models;
using Syncfusion.SfChart.iOS;
using Syncfusion.SfChart.XForms;
using Syncfusion.SfChart.XForms.iOS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace IAmProductive.iOS.Helpers
{
    class ChartCustomDelegate : ChartDelegate
    {
        public SfChart Chart
        {
            get;
            set;
        }
        public ChartCustomDelegate(SfChart chart) : base(chart)
        {
            this.Chart = chart;
        }
        public override NSString GetFormattedDataMarkerLabel(SFChart chart, NSString label, nint index, SFSeries series)
        {
            try
            {
                int seriesIndex = (int)chart.IndexOfSeries(series);
                IList data = Chart.Series[seriesIndex].ItemsSource as IList;
                //string formattedLabel = label.ToString() + " (" + (data[(int)index] as ChartModel).Name + ")";
                return new NSString(label.ToString());
            }catch(Exception ex)
            {
                MessagingCenter.Send((App)Xamarin.Forms.Application.Current, AppConstant.ErrorEvent, ex.ToString());
            }
            return new NSString("");
        }
    }
}
