using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using IAmProductive.Constants;
using IAmProductive.ViewModels.ChartsViewModel;
using Syncfusion.SfChart.XForms;

namespace IAmProductive.Views.TaskStatisticsPage.ChartsViewPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChartViewPage : ContentPage
    {
        ChartViewModel charViewModel;
        string currentOpnChart=" ";//daily , weekly,monthly
        string currentDate;
        bool flag;
        public ChartViewPage(string date)
        {
            
            InitializeComponent();
            //charViewModel.IsBusy = true;
            currentDate = date;
            SegControl.TintColor = Color.FromHex(AppConstant.MiscellaneousTaskColor);
            charViewModel = new ChartViewModel(date);
            BindingContext = charViewModel;
          //  CarouselZoos.ItemsSource = charViewModel.observableCollection;
            // WeeklyPieChart.BindingContext = charViewModel;
            //BindingContext = charViewModel;
            //charViewModel.IsBusy = true;
            flag = true;
          
            
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
           // charViewModel.ChartDataPopulation(currentDate);
           // BindingContext = charViewModel;
           //DailyBarChart.BindingContext = charViewModel;
           //DailyPieChart.BindingContext = charViewModel;
           //WeekelyBarChart.BindingContext = charViewModel;
           //MonthlyBarChart.BindingContext = charViewModel;
           //MonthlyPieChart.BindingContext = charViewModel;
           // charViewModel.IsBusy = false;
            
        }
        
        private void SegControl_ValueChanged(object sender, SegmentedControl.FormsPlugin.Abstractions.ValueChangedEventArgs e)
        {
            switch (e.NewValue)
            {
                case 0: //daily
                   
                    ShowDailyChart();
                    break;
                case 1: // weekly
                    ShowWeeklyChart();
                    break;
                case 2: //mothly
                    ShowMonthlyChart();
                    break;
               
            }
        }
        //invoke on selection of any chart type from type picker
        private void TypePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            
                if (currentOpnChart.Equals("monthly"))
                {
                    ShowMonthlyChart();
                }
                else if (currentOpnChart.Equals("weekly"))
                {
                    ShowWeeklyChart();
                }
                else if (currentOpnChart.Equals("daily"))
                {
                    ShowDailyChart();
                }
        

        }
        public void ShowMonthlyChart()
        {
            currentOpnChart = "monthly";
            if (charViewModel.SelectedChartType.Equals(AppConstant.BarChartTypeLable))
            {
                MonthlyBarChart.IsVisible = true;
                DailyBarChart.IsVisible = false;
                DailyPieChart.IsVisible = false;
                WeekelyBarChart.IsVisible = false;
                WeeklyPieChart.IsVisible = false;
                MonthlyPieChart.IsVisible = false;
               
            }
            else
            {
                MonthlyBarChart.IsVisible = false;
                DailyBarChart.IsVisible = false;
                DailyPieChart.IsVisible = false;
                WeekelyBarChart.IsVisible = false;
                WeeklyPieChart.IsVisible = false;
                MonthlyPieChart.IsVisible = true;

               
            }

        }
        public void ShowWeeklyChart()
        {

            currentOpnChart = "weekly";
            if (charViewModel.SelectedChartType.Equals(AppConstant.BarChartTypeLable))
            {//bar
                WeekelyBarChart.IsVisible = true;
                WeeklyPieChart.IsVisible = false;
                DailyBarChart.IsVisible = false;
                DailyPieChart.IsVisible = false;
                MonthlyBarChart.IsVisible = false;
                MonthlyPieChart.IsVisible = false;
            }
            else
            {//pie
                WeeklyPieChart.IsVisible = true;
                WeekelyBarChart.IsVisible = false;
                DailyBarChart.IsVisible = false;
                DailyPieChart.IsVisible = false;
                MonthlyBarChart.IsVisible = false;
                MonthlyPieChart.IsVisible = false;
                //Week1PieChart.IsVisible = true;
                //Week2PieChart.IsVisible = true;
                //Week3PieChart.IsVisible = true;
                //Week4PieChart.IsVisible = true;
                //if (charViewModel.WeekOnePieChart.Count > 0)
                //{
                //    Week1PieChart.IsVisible = true;
                //    if (charViewModel.WeekTwoPieChart.Count == 0 && charViewModel.WeekThreePieChart.Count == 0 && charViewModel.WeekFourPieChart.Count == 0)
                //    {
                //        Week1PieChart.HorizontalOptions = LayoutOptions.FillAndExpand;
                //        Week1PieChart.VerticalOptions = LayoutOptions.FillAndExpand;
                //    }


                //}
                //if (charViewModel.WeekTwoPieChart.Count> 0)
                //{
                //    Week2PieChart.IsVisible = true;
                //    if (charViewModel.WeekOnePieChart.Count == 0 && charViewModel.WeekThreePieChart.Count == 0 && charViewModel.WeekFourPieChart.Count == 0)
                //    {
                //        Week2PieChart.HorizontalOptions = LayoutOptions.FillAndExpand;
                //        Week2PieChart.VerticalOptions = LayoutOptions.FillAndExpand;
                //    }
                //}
                //if (charViewModel.WeekThreePieChart.Count > 0)
                //{
                //    Week3PieChart.IsVisible = true;
                //    if (charViewModel.WeekOnePieChart.Count == 0 && charViewModel.WeekTwoPieChart.Count == 0 && charViewModel.WeekFourPieChart.Count == 0)
                //    {
                //        Week3PieChart.HorizontalOptions = LayoutOptions.FillAndExpand;
                //        Week3PieChart.VerticalOptions = LayoutOptions.FillAndExpand;
                //    }
                //}
                //if (charViewModel.WeekFourPieChart.Count == 0)
                //{
                //    Week4PieChart.IsVisible = true;
                //    if (charViewModel.WeekOnePieChart.Count == 0 && charViewModel.WeekTwoPieChart.Count == 0 && charViewModel.WeekThreePieChart.Count == 0)
                //    {
                //        Week4PieChart.HorizontalOptions = LayoutOptions.FillAndExpand;
                //        Week4PieChart.VerticalOptions = LayoutOptions.FillAndExpand;
                //    }
                //}


            }
           


        }
        public void ShowDailyChart()
        {
            if (!flag)
            {
                currentOpnChart = "daily";
                if (charViewModel.SelectedChartType.Equals(AppConstant.BarChartTypeLable))
                {
                    DailyBarChart.IsVisible = true;
                    DailyPieChart.IsVisible = false;
                    WeekelyBarChart.IsVisible = false;
                    WeeklyPieChart.IsVisible = false;
                    MonthlyBarChart.IsVisible = false;
                    MonthlyPieChart.IsVisible = false;

                }
                else
                {
                    DailyPieChart.IsVisible = true;
                    DailyBarChart.IsVisible = false;
                    WeekelyBarChart.IsVisible = false;
                    WeeklyPieChart.IsVisible = false;
                    MonthlyBarChart.IsVisible = false;
                    MonthlyPieChart.IsVisible = false;

                }
            }
            else
            {
                flag = false;
                
                currentOpnChart = "daily";
                DailyBarChart.IsVisible = true;
                DailyPieChart.IsVisible = false;
                WeekelyBarChart.IsVisible = false;
                MonthlyBarChart.IsVisible = false;
                MonthlyPieChart.IsVisible = false;
            }
           

        }

        //show out the chart Type picker
        private void ChartTypeToolbarItem_Activated(object sender, EventArgs e)
        {
            ChartTypePicker.Focus();
        }

        private void Week1PieChart_Scroll(object sender, ChartScrollEventArgs e)
        {

        }
    }
}