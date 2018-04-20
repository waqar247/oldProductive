using IAmProductive.Database;
using IAmProductive.Interfaces;
using IAmProductive.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace IAmProductive.ViewModels.BaseViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private SQLiteConnection database;
        private static object collisionLock = new object();
        public DatabaseHelper databaseHelper;
        public DayTask CurrentSelectedDayTask;
        public BaseViewModel()
        {
            databaseHelper = DatabaseHelper.GetInstance();
            database = DependencyService.Get<IDatabaseConnection>().DbConnection();
            CurrentSelectedDayTask = new DayTask();
        }
        public void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this,
              new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// deleted task base on task track id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteTaskBaseOnId(string id)
        {
            int isDeleteRecord = 0;
            DayTask taskToBeDeleted = databaseHelper.GetDayTaskByTrackId(id);
            List<DayTask> allTasks = databaseHelper.GetAllDayTasksBaseOnDayMonth(taskToBeDeleted.CreatedAt); //date
            int tasksToBeDeletedTaskIndex = allTasks.FindIndex(x => x.DayTaskTrackId == id);
            isDeleteRecord = allTasks.Count;
            if (tasksToBeDeletedTaskIndex == 0)
            { // if task was top most in list
                isDeleteRecord = databaseHelper.DeleteDayTaskByTrackId(id);

            }
            else if (tasksToBeDeletedTaskIndex == allTasks.Count - 1)
            {// if bottom most task is about to delete
                DayTask prevTask = allTasks[tasksToBeDeletedTaskIndex - 1];
                prevTask.SpentTime = null;
                string isUpdated = databaseHelper.AddDayTask(prevTask);
                if (isUpdated != null)
                {
                    isDeleteRecord = databaseHelper.DeleteDayTaskByTrackId(id);
                }
            }
            else
            {// if any inbetween item is about to delete
                DayTask prevTask = allTasks[tasksToBeDeletedTaskIndex - 1];
                DayTask afterTask = allTasks[tasksToBeDeletedTaskIndex + 1];
                prevTask.SpentTime = AppUtil.AppUtil.CalculateSpendedTime(Convert.ToDateTime(prevTask.TaskStartedAt), Convert.ToDateTime(afterTask.TaskStartedAt));
                string isUpdated = databaseHelper.AddDayTask(prevTask);
                if (isUpdated != null)
                {
                    isDeleteRecord = databaseHelper.DeleteDayTaskByTrackId(id);
                }
            }
            return isDeleteRecord;
        }

        /// <summary>
        /// when user menually specify the position for task to add in list then this method invoke to maintain the desire sequence of user
        /// </summary>
        /// <param name="position"></param>
        public void ArrangeTask(string date, string position = "Up")
        {
            DayTask task = CurrentSelectedDayTask; //selected task from the list (to which up/down new task added)
            List<DayTask> allTasks = databaseHelper.GetAllDayTasksBaseOnDayMonth(date);
            int selectedTaskIndex = allTasks.FindIndex(x => x.DayTaskTrackId == CurrentSelectedDayTask.DayTaskTrackId);
            DayTask recentAddedTask = databaseHelper.GetLatestInsertedDayTask();
            int recentTaskIndex = allTasks.FindIndex(x => x.DayTaskTrackId == recentAddedTask.DayTaskTrackId);
            allTasks.RemoveAt(recentTaskIndex);
            if (position.Equals("Up"))
            {
                allTasks.Insert(selectedTaskIndex, recentAddedTask);
            }
            else if (position.Equals("Down"))
            {
                allTasks.Insert(selectedTaskIndex + 1, recentAddedTask);

            }
            else { }
            int r = databaseHelper.DeleteAllDayTasksBaseOnDayMonth(date);
            if (r > 0)
            {
                databaseHelper.InsertedGroupOfDayTaskk(allTasks);

            }
        }

    }
}
