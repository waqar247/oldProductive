
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using IAmProductive.Droid.DependencyServices;
using IAmProductive.Interfaces;
using SQLite;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(DatabaseConnection_Android))]
namespace IAmProductive.Droid.DependencyServices
{
   public class DatabaseConnection_Android : IDatabaseConnection
    {
        public SQLiteConnection DbConnection()
        {
            var dbName = "IAmProductiveDB.db3";
            var path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), dbName);
            if (!File.Exists(path))
            {
             
            }
         
            return new SQLiteConnection(path);
        }
    }
}