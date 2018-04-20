using IAmProductive.Views.AddTasks;
using IAmProductive.Views.HelpPage;
using IAmProductive.Views.TaskStatisticsPage;
using Messier16.Forms.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace IAmProductive
{
    public class HomeTabbedPage : PlatformTabbedPage
    {
        public HomeTabbedPage()
        {
            BarBackgroundColor = (Color)Application.Current.Resources["ToolBarColor"];
            SelectedColor = (Color)Application.Current.Resources["AddCustomTaskBgColor"];
            BarBackgroundApplyTo = BarBackgroundApplyTo.Android;
            NavigationPage.SetHasNavigationBar(this, false);  // Hide nav bar
            HelpPage helpPage = new HelpPage();
            StaticsCalenderViewPage stat = new StaticsCalenderViewPage();           
            #pragma warning disable CS0618 // Type or member is obsolete
            #pragma warning disable CS0612 // Type or member is obsolete
            if (Device.OS == TargetPlatform.iOS)
            #pragma warning restore CS0612 // Type or member is obsolete
            #pragma warning restore CS0618 // Type or member is obsolete
            {
                TaskPage taskPage = new TaskPage();
                Children.Add(taskPage);
            }
            else
            #pragma warning disable CS0618 // Type or member is obsolete
            #pragma warning disable CS0612 // Type or member is obsolete
            #pragma warning restore CS0612 // Type or member is obsolete
            #pragma warning restore CS0618 // Type or member is obsolete
            {
                TaskPage_Android taskPage_Android = new TaskPage_Android();
                Children.Add(taskPage_Android);
            }
            Children.Add(stat);
            Children.Add(helpPage);
          
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
          
        }

    }
}