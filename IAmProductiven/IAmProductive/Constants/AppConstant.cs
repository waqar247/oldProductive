using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAmProductive.Constants
{
    public static class  AppConstant
    {

        #region email
        public const int ErrorEmailPort = 587;
        public const int ErrorEmailTimeout = 10000;
        public const string ErrorEmailHost = "smtp.gmail.com";
        public static string ErrorEmailAddress = "claritycrashreports@gmail.com"; 
        public static string ErrorEmailPassword = "Test4321!";
        #endregion


        //App Ui constants
        public static string RoutineTaskText = "Task";
        public static string ConformationMessagetoAddTask = "Are you sure you want to add this task?";
        public static string CancelDayAlertBodyText = "All tasks will be deleted";
        public static string DeleteAlertTitle = "Delete";
        public static string DeleteConformationText = "Do you really want to delete it?";
        public static string CurrentTaskIsInProgressText = "Invalid Time";
        public static string SelectePositionToAddTaskMessageBodyText = "Please select a position where you want to add a task.";
        public static string SelectePositionToAddTaskTitleText = "Oops!";
        #region Toast Constant
        public static string SuccessMessageTitleForToast = "Success";
        public static string FailureMessageTitleForToast = "Error";
        public static string DeletionFailureMessageBody = "Delete operation is failed.Please try again.";
        public static string UpdateFailureMessageBody = "Update operation is failed.Please try again.";
        public const string ErrorEvent = "ErrorOccurred";
        public const string ErrorText = "An error has occurred and has been logged for review. We apologize for the inconvenience.";
        #endregion
        public static string AtWhichPositionYouWantAddTask = "Where to add task from selected position?";
        public  static string AtBottomOfSelectedTask = "Bottom";
        public static string AtAboveOfSelectedTask = "Above";
        public static string addCustomProductiveTaskTextTitle = "Add Custom Task";
        public static string addCustomUnProductiveTaskTextTitle = "Add Custom Task";
        public static string addCustomMiscellaneousTaskTextTitle = "Add Custom Task";
        public static string AlreadyTaskExist = "Task Already Exist";
        public static string addCustomProductiveTaskTrackId = "AddProductive100";
        public static string addCustomUnProductiveTaskTrackId = "UnAddProductive101";
        public static string addCustomMiscellaneousTaskTrackId = "Miscellaneous103";
        public static string ProductiveTaskType = "Productive Tasks";
        public static string UnProductiveTaskType = "Unproductive Tasks";
        public static string MiscellaneousTaskType = "Miscellaneous Tasks";
        public static string DummyTaskType = "Dummy Tasks";
        public static string DummyTaskItem = "Hack0001 Task";

        public static string ProductiveChartTitle = "Productive";
        public static string UnProductiveChartTitle = "Unproductive";
        public static string MiscellaneousChartTitle = "Miscellaneous";
        public static string ProductiveChartTitle_C = "PRODUCTIVE";
        public static string UnProductiveChartTitle_C = "UNPRODUCTIVE";
        public static string MiscellaneousChartTitle_C = "MISCELLANEOUS";
        //TaskType Colors
        public static string ProductiveTaskColor = "#8371AD";
        public static string ProductiveSelectiveTaskColor = "#9C27B0";
        public static string UnProductiveTaskColor = "#eb1923";
        public static string UnProductiveSelectiveTaskColor = "#990d1b";
        public static string MiscellaneousTaskColor = "#808080"; //like gray
        public static string MiscellaneousSelectiveTaskColor = "#9e9e9e";// like dim gray
        public static string MonthlyChartLable = "Monthly Progress";
        public static string WeekelyChartLable = "Weekly Progress";
        public static string DailyChartLable = "Day Progress";
        //Chart Types
        public static string PieChartTypeLable = "Pie View";
        public static string BarChartTypeLable = "Bar View";
        public static string DoughuntChartTypeLable = "Doughunt View";
        public static string PayramidChartTypeLable = "Pyramid View";
        //Task stats page
        public static string StatsToolbarAddNewTaskButton = "Add";
        public static string StatsToolbargraphsButton = "Stats";

    }
}
