using CarouselView.FormsPlugin.Abstractions;
using IAmProductive.Constants;
using IAmProductive.Helpers;
using IAmProductive.Models;
using IAmProductive.ViewModels.AddTasksViewModel;
using IAmProductive.ViewModels.AddTaskVM;
using IAmProductive.ViewModels.ChartsViewModel;
using IAmProductive.ViewModels.StataticsViewModels;
using IAmProductive.Views.AddTasks;
using IAmProductive.Views.TaskStatisticsPage.ChartsViewPages;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
/// <summary>
/// there is croucel view on this page
/// show the task of a particular day
/// </summary>
namespace IAmProductive.Views.TaskStatisticsPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DaytasksPageView : ContentPage
    {
        private DayTasksViewModel dayTasksViewModel;
        private CarocelModel CurrentlyOpenCarocelPage;
        bool isAnyItemSelected;
        string insertedPosition;
        DayTask selectedDayTask;
        string currentDate;
        int CurrentCarocelPageIndex;
        public DaytasksPageView(string d)
        {

            InitializeComponent();
            dayTasksViewModel = new DayTasksViewModel();
            BindingContext = dayTasksViewModel;
            dayTasksViewModel.CurrentSelectedDate = Convert.ToDateTime(d);
            currentDate = d; // date for currently open carocel page
            isAnyItemSelected = false;
            insertedPosition = "Up"; //default value
            dayTasksViewModel.GetAndSetDayForMonth(currentDate);
            carousel.Position = (int)dayTasksViewModel.CurrentSelectedDate.Day - 1;
            
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            //Update task changes notification subscriber
            MessagingCenter.Subscribe<AddDayTaskPopupViewModel, bool>(this, "UpdateTask", (sender, arg) =>
            {
                isAnyItemSelected = false;
                dayTasksViewModel.GetAndSetDayForSingleDay(currentDate, CurrentCarocelPageIndex);

            });
            // new day task inserting failure Notification subscriber
            MessagingCenter.Subscribe<AddDayTaskPopupViewModel, bool>(this, "Can'tInserTask", (sender, arg) =>
            {
                //todo  
            });
            // show changes on UI list ; show the newly inserted item in appropriate position
            MessagingCenter.Subscribe<AddDayTaskPopupViewModel, string>(this, "InsertedTask", (sender, arg) =>
            {
                isAnyItemSelected = false;
                dayTasksViewModel.GetAndSetDayForSingleDay(currentDate, CurrentCarocelPageIndex);
               
            });

        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<AddDayTaskPopupViewModel, string>(this, "InsertedTask");
            MessagingCenter.Unsubscribe<AddDayTaskPopupViewModel, bool>(this, "Can'tInserTask");
            MessagingCenter.Unsubscribe<AddDayTaskPopupViewModel, bool>(this, "UpdateTask");
        }
        /// <summary>
        /// control the carousel page positions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Handle_PositionSelected(object sender, CarouselView.FormsPlugin.Abstractions.PositionSelectedEventArgs e)
        {
            try
            {
                CurrentCarocelPageIndex = e.NewValue;
                CurrentlyOpenCarocelPage = dayTasksViewModel.TaskForDayList[e.NewValue];
                ToolbarItems.Remove(addToolbarItem); // firstly romove the Add toolbar item
                string dateForCurrentPage = CurrentlyOpenCarocelPage.CurrentDateForCurrentCarocel.ToString("d MMMM yyyy");
                currentDate = CurrentlyOpenCarocelPage.CurrentDateForCurrentCarocel.ToString("yyyy-MM-dd");
                if (Convert.ToDateTime(Convert.ToDateTime(dateForCurrentPage).ToString("d MMMM yyyy")).CompareTo(Convert.ToDateTime(DateTime.Now.ToString("d MMMM yyyy"))) <= 0)
                { // if page date is same as current date or previous than the current date then show the add button on toolbar
                    var s = dayTasksViewModel.GetAllDayTasksBaseOnDayMonth(dateForCurrentPage);
                    if (s.Count > 0)
                    {
                        ToolbarItems.Add(addToolbarItem);
                    }

                } 
            }catch(Exception ex)
            {
                MessagingCenter.Send((App)Xamarin.Forms.Application.Current, AppConstant.ErrorEvent, ex.ToString());
            }


        }
        /// <summary>
        /// control the carocel page scrolling
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Handle_Scrolled(object sender, CarouselView.FormsPlugin.Abstractions.ScrolledEventArgs e)
        {
            //todo
        }
        /// <summary>
        /// toolbar Add (button to add new day task) button control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void AddToolbarItem_Activated(object sender, EventArgs e)
        {
            try
            {
                if (isAnyItemSelected)
                {   //alert to ask user where he want to add new task (top/below of selected day task)
                    var actionSheet = await DisplayActionSheet(AppConstant.AtWhichPositionYouWantAddTask, "Cancel", null, "Up", "Down");
                    switch (actionSheet)
                    {
                        case "Cancel":
                            //todo
                            break;
                        case "Up":

                            insertedPosition = "Up";
                            //isAnyItemSelected = false;
                            await Navigation.PushPopupAsync(new AddDayTaskPopupPageView(selectedDayTask, insertedPosition));

                            break;
                        case "Down":
                            insertedPosition = "Down";
                            //isAnyItemSelected = false;
                            await Navigation.PushPopupAsync(new AddDayTaskPopupPageView(selectedDayTask, insertedPosition));
                            break;
                    }

                }
                else
                {
                    await DisplayAlert(AppConstant.SelectePositionToAddTaskTitleText, AppConstant.SelectePositionToAddTaskMessageBodyText, "OK");
                }
            }catch(Exception ex)
            {
                MessagingCenter.Send((App)Xamarin.Forms.Application.Current, AppConstant.ErrorEvent, ex.ToString());
            }

        }
        /// <summary>
        /// toolbar Stats button to show Graphs handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GraphsToolbarItem_Activated(object sender, EventArgs e)
        {
           // isAnyItemSelected = false;
            Navigation.PushAsync(new ChartViewPage(currentDate));

        }
        /// <summary>
        /// List item selected handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DayTaskList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                isAnyItemSelected = false;
                return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
            }
            else
            {
                isAnyItemSelected = true;
                var selectedTask = ((ListView)sender).SelectedItem;
                selectedDayTask = (DayTask)selectedTask;
                

            }
        }
        /// <summary>
        /// contextual list item click handler to update task
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void EditTaskContextual_Clicked(object sender, EventArgs e)
        {
            var item = ((MenuItem)sender);
            var id = item.CommandParameter.ToString();
            await Navigation.PushPopupAsync(new AddDayTaskPopupPageView(id, true));

        }
        /// <summary>
        /// contextual list item click handler to delete task
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void DeleteTaskContextual_Clicked(object sender, EventArgs e)
        {
            var item = ((MenuItem)sender);
            string id = (string)item.CommandParameter;
            int countOfDeletedRecords = 0;
            bool actionResult = await Application.Current.MainPage.DisplayAlert(AppConstant.DeleteAlertTitle, AppConstant.DeleteConformationText, "Yes", "No");
            if (actionResult)
            {

                countOfDeletedRecords = dayTasksViewModel.DeleteTaskBaseOnId(id);
                isAnyItemSelected = false;
                dayTasksViewModel.GetAndSetDayForSingleDay(currentDate, CurrentCarocelPageIndex);
                if (countOfDeletedRecords > 0)
                { // success invoke  >0 means selected task has been deleted
                    if (Settings.LatestInsertedDayTaskTrackId != null)
                        if (Settings.LatestInsertedDayTaskTrackId.Equals(id))
                        {
                           Settings.LatestInsertedDayTaskTrackId = null;
                        }
                }
                else
                {
                    //todo
                }
            }
            else
            {
                //todo else block of choose of action
            }

        }
    }
}