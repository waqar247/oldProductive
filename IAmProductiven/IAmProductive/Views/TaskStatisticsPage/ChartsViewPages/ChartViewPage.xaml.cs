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
using System.Collections.ObjectModel;
using IAmProductive.Models;
using Plugin.Connectivity;

namespace IAmProductive.Views.TaskStatisticsPage.ChartsViewPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChartViewPage : ContentPage
    {
        ChartViewModel charViewModel;
        string currentOpnChart = " ";//daily , weekly,monthly
        string currentDate;
        bool flag, isFirstTimeOpenPage;
        string d;
        public ChartViewPage(string date)
        {
            InitializeComponent();
            currentDate = date;
            charViewModel = new ChartViewModel(date);
            BindingContext = charViewModel;
            flag = true;
            isFirstTimeOpenPage = true;
            d = date;
            DailyBarChart.SecondaryAxis.ActualRangeChanged += SecondaryAxis_ActualRangeChanged;
            WeekelyBarChart.SecondaryAxis.ActualRangeChanged += SecondaryAxis_WeeklyRangeChaged;
        }
        private void SecondaryAxis_WeeklyRangeChaged(object sender, ActualRangeChangedEventArgs e)
        {
            e.VisibleMaximum = Convert.ToDouble(e.ActualMaximum);
        }
        private void SecondaryAxis_ActualRangeChanged(object sender, ActualRangeChangedEventArgs e)
        {
            e.VisibleMaximum = Convert.ToDouble(e.ActualMaximum);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (CrossConnectivity.Current.IsConnected)
            {
                charViewModel.IsEnabledAds = true;
            }
            else
            {
                charViewModel.IsEnabledAds = false;
            }
        }

        private void SegControl_ValueChanged(object sender, SegmentedControl.FormsPlugin.Abstractions.ValueChangedEventArgs e)
        {
            switch (e.NewValue)
            {
                case 0: //daily
                    if (isFirstTimeOpenPage)
                    {
                        charViewModel.ChartDataPopulation(d);
                        isFirstTimeOpenPage = false;
                    }
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
            else
            {
                ShowDailyChart();
            }
        }
        public void ShowMonthlyChart()
        {
            currentOpnChart = "monthly";
            if (charViewModel.SelectedChartType.Equals(AppConstant.BarChartTypeLable))
            { //bar
                Title = "Stats(Bar)";
                DailyBarChart.IsVisible = true;
                PieChart.IsVisible = false;
                WeekelyBarChart.IsVisible = false;
                WeeklyPieChart.IsVisible = false;
                DoughnutChart.IsVisible = false;
                WeeklyDoughuntChart.IsVisible = false;
                PayramidChart.IsVisible = false;
                WeeklyPayramidChart.IsVisible = false;
                DailyBarChart.Title.Text = charViewModel.CurrentMonth;
                DailyBarChart.PrimaryAxis.Title.Text = charViewModel.MonthChartLabel;
                DailyBarSeries.ItemsSource = charViewModel.MonthlyBarChart;
            }
            else if (charViewModel.SelectedChartType.Equals(AppConstant.PieChartTypeLable))
            {//pie
                Title = "Stats(Pie)";
                DailyBarChart.IsVisible = false;
                WeekelyBarChart.IsVisible = false;
                WeeklyPieChart.IsVisible = false;
                PieChart.IsVisible = true;
                PieChart.Title.Text = charViewModel.CurrentMonth;
                PieChartSeries.ItemsSource = charViewModel.MonthlyBarChart; ;
                DoughnutChart.IsVisible = false;
                WeeklyDoughuntChart.IsVisible = false;
                PayramidChart.IsVisible = false;
                WeeklyPayramidChart.IsVisible = false;
            }
            else if (charViewModel.SelectedChartType.Equals(AppConstant.DoughuntChartTypeLable))
            {//doughunt
                Title = "Stats(Doughunt)";
                DailyBarChart.IsVisible = false;
                PieChart.IsVisible = false;
                WeekelyBarChart.IsVisible = false;
                WeeklyPieChart.IsVisible = false;
                WeeklyDoughuntChart.IsVisible = false;
                DoughnutChart.IsVisible = true;
                DoughnutChart.Title.Text = charViewModel.CurrentMonth;
                DoughuntSeries.ItemsSource = charViewModel.MonthlyBarChart;
                PayramidChart.IsVisible = false;
                WeeklyPayramidChart.IsVisible = false;
            }
            else
            { //payramid
                Title = "Stats(Pyramid)";
                DailyBarChart.IsVisible = false;
                PieChart.IsVisible = false;
                WeekelyBarChart.IsVisible = false;
                WeeklyPieChart.IsVisible = false;
                WeeklyDoughuntChart.IsVisible = false;
                DoughnutChart.IsVisible = false;
                PayramidChart.IsVisible = true;
                PyramidSeries.ItemsSource = charViewModel.MonthlyBarChart;
                PayramidChart.Title.Text = charViewModel.CurrentMonth;
                WeeklyPayramidChart.IsVisible = false;
            }

        }
        public void ShowWeeklyChart()
        {

            currentOpnChart = "weekly";
            if (charViewModel.SelectedChartType.Equals(AppConstant.BarChartTypeLable))
            {//bar
                Title = "Stats(Bar)";
                WeekelyBarChart.IsVisible = true;
                WeeklyPieChart.IsVisible = false;
                DailyBarChart.IsVisible = false;
                PieChart.IsVisible = false;
                WeeklyDoughuntChart.IsVisible = false;
                DoughnutChart.IsVisible = false;
                PayramidChart.IsVisible = false;
                WeeklyPayramidChart.IsVisible = false;
            }
            else if (charViewModel.SelectedChartType.Equals(AppConstant.PieChartTypeLable))
            {//pie
                Title = "Stats(Pie)";
                WeeklyPieChart.IsVisible = true;
                WeekelyBarChart.IsVisible = false;
                DailyBarChart.IsVisible = false;
                PieChart.IsVisible = false;
                WeeklyDoughuntChart.IsVisible = false;
                DoughnutChart.IsVisible = false;
                PayramidChart.IsVisible = false;
                WeeklyPayramidChart.IsVisible = false;
            }
            else if (charViewModel.SelectedChartType.Equals(AppConstant.DoughuntChartTypeLable))
            {//doughunt
                Title = "Stats(Doughunt)";
                WeeklyPieChart.IsVisible = false;
                WeekelyBarChart.IsVisible = false;
                DailyBarChart.IsVisible = false;
                PieChart.IsVisible = false;
                WeeklyDoughuntChart.IsVisible = true;
                DoughnutChart.IsVisible = false;
                PayramidChart.IsVisible = false;
                WeeklyPayramidChart.IsVisible = false;
            }
            else
            {//payramid
                Title = "Stats(Pyramid)";
                WeeklyPieChart.IsVisible = false;
                WeekelyBarChart.IsVisible = false;
                DailyBarChart.IsVisible = false;
                PieChart.IsVisible = false;
                WeeklyDoughuntChart.IsVisible = false;
                DoughnutChart.IsVisible = false;
                PayramidChart.IsVisible = false;
                WeeklyPayramidChart.IsVisible = true;
            }
        }
        public void ShowDailyChart()
        {
            if (!flag)
            {
                currentOpnChart = "daily";
                if (charViewModel.SelectedChartType.Equals(AppConstant.BarChartTypeLable))
                { //bar
                    Title = "Stats(Bar)";
                    DailyBarChart.IsVisible = true;
                    PieChart.IsVisible = false;
                    WeekelyBarChart.IsVisible = false;
                    WeeklyPieChart.IsVisible = false;
                    DoughnutChart.IsVisible = false;
                    WeeklyDoughuntChart.IsVisible = false;
                    PayramidChart.IsVisible = false;
                    WeeklyPayramidChart.IsVisible = false;
                    DailyBarChart.PrimaryAxis.Title.Text = charViewModel.DailyChartLabel;
                    DailyBarChart.Title.Text = charViewModel.CurrentDayDate;
                    DailyBarSeries.ItemsSource = charViewModel.DailyBarChart;

                }
                else if (charViewModel.SelectedChartType.Equals(AppConstant.PieChartTypeLable))
                {//pie
                    Title = "Stats(Pie)";
                    PieChart.IsVisible = true;
                    DailyBarChart.IsVisible = false;
                    WeekelyBarChart.IsVisible = false;
                    WeeklyPieChart.IsVisible = false;
                    DoughnutChart.IsVisible = false;
                    WeeklyDoughuntChart.IsVisible = false;
                    PayramidChart.IsVisible = false;
                    WeeklyPayramidChart.IsVisible = false;
                    PieChart.Title.Text = charViewModel.CurrentDayDate;
                    PieChartSeries.ItemsSource = charViewModel.DailyBarChart; ;
                }
                else if (charViewModel.SelectedChartType.Equals(AppConstant.DoughuntChartTypeLable))
                { //doughunt
                    Title = "Stats(Doughunt)";
                    PieChart.IsVisible = false;
                    DailyBarChart.IsVisible = false;
                    WeekelyBarChart.IsVisible = false;
                    WeeklyPieChart.IsVisible = false;
                    DoughnutChart.IsVisible = true;
                    WeeklyDoughuntChart.IsVisible = false;
                    DoughnutChart.Title.Text = charViewModel.CurrentDayDate;
                    DoughuntSeries.ItemsSource = charViewModel.DailyBarChart; 
                    PayramidChart.IsVisible = false;
                    WeeklyPayramidChart.IsVisible = false;
                }
                else
                {//payramid
                    Title = "Stats(Pyramid)";
                    PieChart.IsVisible = false;
                    DailyBarChart.IsVisible = false;
                    WeekelyBarChart.IsVisible = false;
                    WeeklyPieChart.IsVisible = false;
                    DoughnutChart.IsVisible = false;
                    WeeklyDoughuntChart.IsVisible = false;
                    PayramidChart.IsVisible = true;
                    PayramidChart.Title.Text = charViewModel.CurrentDayDate;
                    PyramidSeries.ItemsSource = charViewModel.DailyBarChart; 
                    WeeklyPayramidChart.IsVisible = false;
                }
            }
            else
            {//bar -invoke when fist time app constructor get called 
                flag = false;
                Title = "Stats(Bar)";
                currentOpnChart = "daily";
                DailyBarChart.IsVisible = true;
                PieChart.IsVisible = false;
                WeekelyBarChart.IsVisible = false;
                DoughnutChart.IsVisible = false;
                WeeklyDoughuntChart.IsVisible = false;
                PayramidChart.IsVisible = false;
                WeeklyPayramidChart.IsVisible = false;
            }
        }
        //show out the chart Type picker
        private void ChartTypeToolbarItem_Activated(object sender, EventArgs e)
        {
            ChartTypePicker.Focus();
        }
       private void Week1PieChart_Scroll(object sender, ChartScrollEventArgs e)
        {
          //todo
        }
    }
}