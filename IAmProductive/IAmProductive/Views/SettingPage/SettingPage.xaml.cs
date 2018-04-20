using IAmProductive.Database;
using IAmProductive.Interfaces;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IAmProductive.Views.SettingPage
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SettingPage : ContentPage
	{
        private SQLiteConnection database;
        private static object collisionLock = new object();
        public DatabaseHelper databaseHelper;
        public SettingPage ()
		{
			InitializeComponent ();
          
            databaseHelper = DatabaseHelper.GetInstance();
            database = DependencyService.Get<IDatabaseConnection>().DbConnection();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            databaseHelper.ABC();
        }
    }
}