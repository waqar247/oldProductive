using IAmProductive.ViewModels.StataticsViewModels;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IAmProductive.Views.TaskStatisticsPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StaticsCalenderViewPage : ContentPage
    {
        private DayTasksViewModel dayTasksViewModel;
        public StaticsCalenderViewPage()
        {
            InitializeComponent();
            calender.MaxDate = DateTime.Now.AddDays(30);
           // calender.MinDate = DateTime.Now.AddDays(-1);
            calender.Padding = new Thickness(5, Device.RuntimePlatform == Device.iOS ? 25 : 5, 5, 5);
            dayTasksViewModel = new DayTasksViewModel();
            BindingContext = dayTasksViewModel;
            dayTasksViewModel.OpenTaskStatsViewPageHandler += OpenTaskStatsViewPage;
        }
        /// <summary>
        /// Event Handler implementation which belongs to View Model
        /// open the Task stats page 
        /// </summary>
        /// <param name="sender"> date selected from calender</param>
        /// <param name="e"></param>
        private void OpenTaskStatsViewPage(object sender, EventArgs e)
        {
            string d = sender as string; //date
            
             Navigation.PushAsync(new DaytasksPageView(d));
            
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (CrossConnectivity.Current.IsConnected)
            {
                dayTasksViewModel.IsEnabledAds = true;
            }
            else
            {
                dayTasksViewModel.IsEnabledAds = false;
            }
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
          
        }
        private void calender_DateClicked(object sender, XamForms.Controls.DateTimeEventArgs e)
        {
            
                Navigation.PushAsync(new DaytasksPageView(dayTasksViewModel.CurrentSelectedDate.ToString("yyyy-MM-dd")));
           
        }
    }
}