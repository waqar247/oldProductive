using IAmProductive.Constants;
using IAmProductive.Models;
using IAmProductive.ViewModels.AddTasksViewModel;
using IAmProductive.ViewModels.AddTaskVM;
using IAmProductive.Views.AddTasksViews;
using IAmProductive.Views.TaskStatisticsPage;
using Plugin.Connectivity;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamvvm;

namespace IAmProductive.Views.AddTasks
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TaskPage_Android : ContentPage, IBasePage<TaskViewModel>
    {
        TaskViewModel taskViewModel;
        public TaskPage_Android ()
		{
			InitializeComponent ();
            taskViewModel = new TaskViewModel();
            BindingContext = taskViewModel;
            taskViewModel.OpenCalenderForStatsHandler += ShowCalenderPage;
            taskViewModel.OpenAddTaskPopupViewHandler += OpenAddTaskPopupPageHandlerAsync;
        }

        /// <summary>
        /// Ios Add Custom task handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Android add custom task button handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void AddCustomTaskContainer_Tapped(object sender, EventArgs e)
        {
            var selectedItem = ((StackLayout)sender).ClassId.ToString();
            if (selectedItem.Equals(AppConstant.UnProductiveTaskType))
            {
                await Navigation.PushPopupAsync(new AddAndEditTasksPopUpViewPage(AppConstant.addCustomProductiveTaskTrackId));
            }
            else if (selectedItem.Equals(AppConstant.MiscellaneousTaskType))
            {
                await Navigation.PushPopupAsync(new AddAndEditTasksPopUpViewPage(AppConstant.addCustomUnProductiveTaskTrackId));
            }
            else
            {
                await Navigation.PushPopupAsync(new AddAndEditTasksPopUpViewPage(AppConstant.addCustomMiscellaneousTaskTrackId));
            }
        }
    }
}