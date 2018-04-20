using System;
using System.Collections.Generic;
using DLToolkit.Forms.Controls;
using IAmProductive.Constants;
using IAmProductive.Models;
using IAmProductive.ViewModels.AddTasksViewModel;
using IAmProductive.ViewModels.AddTaskVM;
using IAmProductive.Views.AddTasksViews;
using IAmProductive.Views.TaskStatisticsPage;
using Plugin.Connectivity;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;
using Xamvvm;

namespace IAmProductive.Views.AddTasks
{
    public partial class TaskPage : ContentPage, IBasePage<TaskViewModel>
    {
        TaskViewModel taskViewModel;
        public TaskPage()
        {
            InitializeComponent();
            taskViewModel = new TaskViewModel();
            BindingContext = taskViewModel;
            taskViewModel.OpenCalenderForStatsHandler += ShowCalenderPage;
            taskViewModel.OpenAddTaskPopupViewHandler += OpenAddTaskPopupPageHandlerAsync;
        }
        private async void OpenAddTaskPopupPageHandlerAsync(object sender, EventArgs e)
        {
            Taskk selectedTask = sender as Taskk;
            await Navigation.PushPopupAsync(new AddAndEditTasksPopUpViewPage(selectedTask.TaskTrackId));
        }
        #region Methods
        private void ShowCalenderPage(object sender, EventArgs e)
        {
            var selectedTask = (Taskk)sender;
            Navigation.PushAsync(new CalenderPageView(selectedTask));
        }
        /// <summary>
        /// Edit task button handler
        /// Edit the particular task from 'Add task' page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void EditGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var selectedItemTrackId = ((Image)sender).ClassId.ToString();
            await Navigation.PushPopupAsync(new AddAndEditTasksPopUpViewPage(selectedItemTrackId, true));
        }
        /// <summary>
        /// add custom task buttons handling
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void AddCustomTask_Tapped(object sender, EventArgs e)
        {
            var val = ((StackLayout)sender).ClassId.ToString();
            if (val.Equals(AppConstant.UnProductiveTaskType))
            { //add productive task

                await Navigation.PushPopupAsync(new AddAndEditTasksPopUpViewPage(AppConstant.addCustomProductiveTaskTrackId));
            }
            else if (val.Equals(AppConstant.MiscellaneousTaskType))
            { //add un-productive task
                await Navigation.PushPopupAsync(new AddAndEditTasksPopUpViewPage(AppConstant.addCustomUnProductiveTaskTrackId));
            }
            else if (val.Equals(AppConstant.DummyTaskType))
            {//add Miscellaneous task
                await Navigation.PushPopupAsync(new AddAndEditTasksPopUpViewPage(AppConstant.addCustomMiscellaneousTaskTrackId));
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            SetUpMessageCenters();
            if (CrossConnectivity.Current.IsConnected)
            {
                taskViewModel.IsEnabledAds = true;
            }
            else
            {
                taskViewModel.IsEnabledAds = false;
            }
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<AddTaskkPopUpViewModel, bool>(this, "UpdateTask");
            MessagingCenter.Unsubscribe<AddTaskkPopUpViewModel, bool>(this, "InsertedTask");
            MessagingCenter.Unsubscribe<AddTaskkPopUpViewModel, bool>(this, "Can'tInserTask");
        }

        private void SetUpMessageCenters()
        {
            MessagingCenter.Subscribe<AddTaskkPopUpViewModel, bool>(this, "UpdateTask", (sender, arg) =>
            {
                taskViewModel.SortedAndSetTheTaskList();
                taskViewModel.HighlightTaskAfterNewTaskInserted();

            });
            MessagingCenter.Subscribe<AddTaskkPopUpViewModel, bool>(this, "InsertedTask", (sender, arg) =>
            {
                taskViewModel.SortedAndSetTheTaskList();
                taskViewModel.HighlightTaskAfterNewTaskInserted();
            });
        }
        #endregion


    }
}
