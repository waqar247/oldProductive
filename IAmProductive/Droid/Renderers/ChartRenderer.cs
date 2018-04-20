using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Com.Syncfusion.Charts;
using IAmProductive.Constants;
using IAmProductive.Droid.Renderers;
using IAmProductive.Helpers;
using IAmProductive.Models;
using Syncfusion.SfChart.XForms.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(IAmProductive.Helpers.ChartExt), typeof(ChartRenderer))]
namespace IAmProductive.Droid.Renderers
{
    public class ChartRenderer : SfChartRenderer
    {
        internal static ChartExt Chart { get; set; }

        public ChartRenderer()
        {

        }
        protected override void OnElementChanged(ElementChangedEventArgs<Syncfusion.SfChart.XForms.SfChart> e)
        {
            base.OnElementChanged(e);
            Chart = e.NewElement as ChartExt;
            if (Control != null)
            {
                try{
                    for (int i = 0; i < Control.Series.Count; i++)
                    {
                        if (Control.Series[i].GetType() == typeof(PieSeries))
                        {
                            (Control.Series[i] as ChartSeries).DataMarkerLabelCreated += CustomRenderer_DataMarkerLabelCreated;
                        }
                    }
                }catch(Exception ex)
                {
                    MessagingCenter.Send((App)Xamarin.Forms.Application.Current, AppConstant.ErrorEvent, ex.ToString());
                }

            }
        }
        List<ChartModel> data;
        private void CustomRenderer_DataMarkerLabelCreated(object sender, ChartSeries.DataMarkerLabelCreatedEventArgs e)
        {
            try{
                data = Chart.Series[0].ItemsSource as List<ChartModel>;
                e.P0.Label = e.P0.Label.ToString() + " (" + (data[e.P0.Index] as ChartModel).Name + ")";

            }catch(Exception ex)
            {
                MessagingCenter.Send((App)Xamarin.Forms.Application.Current, AppConstant.ErrorEvent, ex.ToString());
            }
        }

    }
}