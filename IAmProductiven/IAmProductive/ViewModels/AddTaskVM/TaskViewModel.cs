using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using DLToolkit.Forms.Controls;
using IAmProductive.Constants;
using IAmProductive.Database;
using IAmProductive.Helpers;
using IAmProductive.Interfaces;
using IAmProductive.Models;
using SQLite;
using Xamarin.Forms;
using Xamvvm;


namespace IAmProductive.ViewModels.AddTasksViewModel
{
    public class TaskViewModel : BasePageModel
    {

        public EventHandler OpenAddTaskPopupViewHandler;
        public EventHandler OpenCalenderForStatsHandler;
        public ICommand EditTaskCommand { get; set; }
        public ICommand ItemTapCommand { get; set; }

        private SQLiteConnection database;
        private static object collisionLock = new object();
        public DatabaseHelper databaseHelper;
        private bool _isEnabledAds;
        public bool _isSelected;
        Taskk selectedTask;
        public Taskk ask;
        Taskk currentSelectedTask;

        public TaskViewModel()
        {
            databaseHelper = DatabaseHelper.GetInstance();
            database = DependencyService.Get<IDatabaseConnection>().DbConnection();
            // ReloadData();
            SortedAndSetTheTaskList();
            SetUpCommands();
        }

        public ObservableCollection<Grouping<string, Taskk>> Items
        {
            get { return GetField<ObservableCollection<Grouping<string, Taskk>>>(); }
            set { SetField(value); }
        }
        public bool IsEnabledAds { get { return _isEnabledAds; } set { this._isEnabledAds = value; OnPropertyChanged("IsEnabledAds"); } }


        #region Methods

        /// <summary>
        /// Set up all commands for this page
        /// provide implementation for every commands of this page
        /// </summary>
        private void SetUpCommands()
        {
            ItemTapCommand = new Command(async e =>
            {
                try
                {
                    selectedTask = (e as Taskk);
                    if (selectedTask.TaskTrackId.Equals(AppConstant.addCustomProductiveTaskTrackId) ||
                        selectedTask.TaskTrackId.Equals(AppConstant.addCustomUnProductiveTaskTrackId) ||
                        selectedTask.TaskTrackId.Equals(AppConstant.addCustomMiscellaneousTaskTrackId))
                    { // insert new task
                        OpenAddTaskPopupViewHandler(selectedTask, null); // move control to TaskPage.xaml.cs page to show the input dialog
                    }
                    else
                    { // highlight the selected task

                        currentSelectedTask = selectedTask; // to keep track of selected task
                        HighlightSelectedTask();
                        bool actionResult = await Application.Current.MainPage.DisplayAlert(null, AppConstant.ConformationMessagetoAddTask, "Yes", "No");
                        if (actionResult)
                        OpenCalenderForStatsHandler(selectedTask, null);
                    }
                }
                catch (Exception ex)
                {
                    MessagingCenter.Send((App)Xamarin.Forms.Application.Current, AppConstant.ErrorEvent, ex.ToString());
                }
            });
        }

        /// <summary>
        /// get all task from db
        /// grouped the task base on their task type
        /// set task to FlowObservableCollection to display on UI
        /// </summary>
        public void SortedAndSetTheTaskList()
        {
            try
            {
                var s = databaseHelper.GetAllTaskks();
                var sorted = databaseHelper.GetAllTaskks()
                   .GroupBy(item => item.TaskType)
                   .Select(itemGroup => new Grouping<string, Taskk>(itemGroup.Key, itemGroup))
                   .ToList();
                Items = new FlowObservableCollection<Grouping<string, Taskk>>(sorted);
            }
            catch (Exception ex)
            {
                MessagingCenter.Send((App)Xamarin.Forms.Application.Current, AppConstant.ErrorEvent, ex.ToString());
            }

        }


        /// <summary>
        /// after inserting the new task, this method invoke to highlight the previously selected task
        /// </summary>
        public void HighlightTaskAfterNewTaskInserted()
        {
            try
            {
                //firstly look if task were selected from group one
                if (currentSelectedTask != null)
                {
                    Grouping<string, Taskk> item = (Grouping<string, Taskk>)Items[0];
                    var taskk = item.Where(e => e.TaskTrackId.Equals(currentSelectedTask.TaskTrackId)).ToList();
                    if (taskk.Count > 0)
                    {
                        item.Where(e => e.TaskTrackId.Equals(currentSelectedTask.TaskTrackId)).First().IsSelected = true;
                    }
                    else
                    {   //secondly look if task were selected from group two
                        item = (Grouping<string, Taskk>)Items[1];
                        taskk = item.Where(e => e.TaskTrackId.Equals(currentSelectedTask.TaskTrackId)).ToList();
                        if (taskk.Count > 0)
                        {
                            item.Where(e => e.TaskTrackId.Equals(currentSelectedTask.TaskTrackId)).First().IsSelected = true;
                        }
                        else
                        {  //thirdly look if task were selected from group third 

                            item = (Grouping<string, Taskk>)Items[2];
                            taskk = item.Where(e => e.TaskTrackId.Equals(currentSelectedTask.TaskTrackId)).ToList();
                            if (taskk.Count > 0)
                            {
                                item.Where(e => e.TaskTrackId.Equals(currentSelectedTask.TaskTrackId)).First().IsSelected = true;
                            }
                            else
                            {
                                //todo for forthly .... and so on
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessagingCenter.Send((App)Xamarin.Forms.Application.Current, AppConstant.ErrorEvent, ex.ToString());
            }
        }

        /// <summary>
        /// if user select any task without inserting new task
        /// highlight the selected task
        /// /// </summary>
        public void HighlightSelectedTask()
        {
            try
            {

                Grouping<string, Taskk> item = (Grouping<string, Taskk>)Items[0];
                for (int j = 0; j < item.Count; j++)
                {
                    item[j].IsSelected = false;
                }
                item = (Grouping<string, Taskk>)Items[1];
                for (int j = 0; j < item.Count; j++)
                {
                    item[j].IsSelected = false;

                }
                item = (Grouping<string, Taskk>)Items[2];
                for (int j = 0; j < item.Count; j++)
                {
                    item[j].IsSelected = false;

                }
                currentSelectedTask.IsSelected = true;
            }
            catch (Exception ex)
            {
                MessagingCenter.Send((App)Xamarin.Forms.Application.Current, AppConstant.ErrorEvent, ex.ToString());
            }

        } 
        #endregion

    }
}
