using System;
using IAmProductive.Database;
using IAmProductive.Helpers;
using IAmProductive.Views;
using Xamarin.Forms;

namespace IAmProductive
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            if (Settings.AppDbVersion == Settings.CurrentDbVersion)
            {
                DatabaseHelper.GetInstance().CreateDatabase();
                Settings.AppDbVersion = 1;
            }
              
            MainPage = new NavigationPage(new HomeTabbedPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }
        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }
        protected override void OnResume()
        {
            // Handle when your app resumes
        }

	}
}
