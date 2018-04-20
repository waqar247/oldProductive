using System;
using System.Collections.Generic;
using DLToolkit.Forms.Controls;
using IAmProductive.Models;
using IAmProductive.ViewModels.AddTasksViewModel;
using IAmProductive.ViewModels.AddTaskVM;
using IAmProductive.Views.AddTasksViews;
using IAmProductive.Views.TaskStatisticsPage;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;
using Xamvvm;
/// <summary>
/// 1)Open page For Editting the Particular task
/// 2)Open Page for Adding Custom new Task
/// 3)Open the Calender page to add new Day Task
/// 4)Show the All task (Front page For Add Task Tab)
/// </summary>
namespace IAmProductive.Views.AddTasks
{
    public partial class TaskPage : ContentPage,IBasePage<TaskViewModel>
    {
        TaskViewModel taskViewModel;
        public TaskPage()
        {
            InitializeComponent();
            taskViewModel = new TaskViewModel();
            BindingContext = taskViewModel;
            taskViewModel.OpenAddTaskPopupViewHandler += ShowAddTaskPopUpAsync; //handler to open the add task popup page
            taskViewModel.OpenCalenderForStatsHandler += ShowCalenderPage;
        }

        private void ShowCalenderPage(object sender, EventArgs e)
        {
            var selectedTask =(Taskk) sender;
            Navigation.PushAsync(new CalenderPageView(selectedTask));
        }

        /// <summary>
        /// open the popup page to add new task
        /// pass the Id of Selected task as paprameter to to tell the AddTaskPopup page which type of task it need to insert
        /// </summary>
        /// <param name="sender">Clicked task </param>
        /// <param name="e"></param>
        private async void ShowAddTaskPopUpAsync(object sender, EventArgs e)
        {
            Taskk selectedTask = sender as Taskk;
            await Navigation.PushPopupAsync(new AddAndEditTasksPopUpViewPage(selectedTask.TaskTrackId));
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

        protected override void OnAppearing()
        {
            base.OnAppearing();

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
            MessagingCenter.Subscribe<AddTaskkPopUpViewModel, bool>(this, "Can'tInserTask", (sender, arg) =>
            {
               //todo

            });
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<AddTaskkPopUpViewModel, bool>(this, "UpdateTask");
            MessagingCenter.Unsubscribe<AddTaskkPopUpViewModel, bool>(this, "InsertedTask");
            MessagingCenter.Unsubscribe<AddTaskkPopUpViewModel, bool>(this, "Can'tInserTask");
        }
        
        //private async void flowListView_FlowItemTapped(object sender, ItemTappedEventArgs e)
        //{
            
        //    var selectedTask = (Taskk)e.Item;
        //    if (selectedTask.TaskTrackId.Equals("AddProductive100") || selectedTask.TaskTrackId.Equals("UnAddProductive101"))
        //    {
        //        await Navigation.PushPopupAsync(new AddAndEditTasksPopUpViewPage(selectedTask.TaskTrackId));
        //    }
        //    else
        //    {
        //        taskViewModel.HighlightSelectedTask();
        //        selectedTask.IsSelected = true;
        //    }

        //}
    }
}
