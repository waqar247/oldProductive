using System;
using IAmProductive.Database;


using Xamarin.Forms;

namespace IAmProductive
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            DatabaseHelper.GetInstance().CreateDatabase();
            // MainPage = new NavigationPage(new ChartViewPage());
            MainPage = new NavigationPage(new IAmProductivePage());
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
