using IAmProductive.Constants;
using IAmProductive.Models;
using IAmProductive.ViewModels.StataticsViewModels;
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
    public partial class CalenderPageView : ContentPage
    {
        //private Taskk SelectedTask;
        private DayTasksViewModel dayTasksViewModel;
        public CalenderPageView(Taskk selectedTask)
        {
            InitializeComponent();
            try
            {
                calender.MaxDate = DateTime.Now.AddDays(0);
                calender.MinDate = DateTime.Now.AddDays(-1);
                calender.Padding = new Thickness(5, Device.RuntimePlatform == Device.iOS ? 25 : 5, 5, 5);
                dayTasksViewModel = new DayTasksViewModel();
                BindingContext = dayTasksViewModel;
                dayTasksViewModel.CurrentSelectedTaskk = selectedTask;
                dayTasksViewModel.OpenTaskStatsViewPageHandler += OpenTaskStatsViewPage; 
            }catch(Exception ex)
            {
                MessagingCenter.Send((App)Xamarin.Forms.Application.Current, AppConstant.ErrorEvent, ex.ToString());  
            }

        }
        /// <summary>
        /// open the Task stats page 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenTaskStatsViewPage(object sender, EventArgs e)
        {
            string d = sender as string;
            if (d != null)
            {
                Navigation.PushAsync(new DaytasksPageView(d));
                Navigation.RemovePage(this);
            }
        }
       

        private async void calender_DateClicked(object sender, XamForms.Controls.DateTimeEventArgs e)
        {
            //todo
           await DisplayAlert("Current date", ""+calender.SelectedDate, "cancel");
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
           
        }
    }
}