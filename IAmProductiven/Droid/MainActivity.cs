using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms;
using CarouselView.FormsPlugin.Android;
using SegmentedControl.FormsPlugin.Android;
using Syncfusion.SfChart.XForms.Droid;
using System.Net.Mail;
using IAmProductive.Constants;
using System.Text;
using System.IO;
using Plugin.Connectivity;
using Messier16.Forms.Controls.Droid;

namespace IAmProductive.Droid
{
    [Activity(Label = "IAmProductive", Icon = "@drawable/ic_launcher", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private static string _lastError;
        App app;
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            global::Xamarin.Forms.Forms.Init(this, bundle);
            XamForms.Controls.Droid.Calendar.Init();
            CarouselViewRenderer.Init();
            new SfChartRenderer();
            Rg.Plugins.Popup.Popup.Init(this, bundle);
            SegmentedControlRenderer.Init();
            SendAnyPreviousCrash(); //read log file and send crash report if available
            app = new App();
            MessagingCenter.Subscribe<App, string>(app, AppConstant.ErrorEvent, (s, error) =>
            {
                app.MainPage.DisplayAlert("Error", AppConstant.ErrorText, "Close");
                SendErrorEmail(error);
            });
            PlatformTabbedPageRenderer.Init();
            LoadApplication(app);
          //  Window.SetStatusBarColor(Android.Graphics.Color.Argb(15, 0, 0, 0));
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
            MessagingCenter.Unsubscribe<App, string>(app, AppConstant.ErrorEvent);
        }

        private static void SendErrorEmail(string errorMessage)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                if (errorMessage == _lastError)
                {
                    return;
                }
                _lastError = errorMessage;

                try
                {
                    var client = new SmtpClient
                    {
                        Port = AppConstant.ErrorEmailPort,
                        Host = AppConstant.ErrorEmailHost,
                        EnableSsl = true,
                        Timeout = AppConstant.ErrorEmailTimeout,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new System.Net.NetworkCredential(AppConstant.ErrorEmailAddress, AppConstant.ErrorEmailPassword)
                    };

                    var mailAddress = new MailAddress(AppConstant.ErrorEmailAddress, "Android App");
                    var mail = new MailMessage(mailAddress, mailAddress);
                    mail.Subject = "IAmProductive|Android|Crash";
                    mail.Body = "Exception Stack Trace: " + errorMessage;
                    mail.BodyEncoding = UTF8Encoding.UTF8;
                    mail.IsBodyHtml = true;
                    mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                    client.Send(mail);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(string.Format("Error email not working {0}", ex.StackTrace));
                }  
            }
            else
            {
                LogUnhandledException(errorMessage);  
            }

        }

        internal static void LogUnhandledException(string exception)
        {
            try
            {
                const string errorFileName = "Fatal.log";
                var libraryPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                var errorFilePath = Path.Combine(libraryPath, errorFileName);
                var errorMessage = String.Format("Time: {0}\r\nError: Unhandled Exception\r\n{1}", DateTime.Now, exception.ToString());
                File.WriteAllText(errorFilePath, errorMessage);
            }
            catch
            {
                // just suppress any error logging exceptions
            }
        }
        private static void SendAnyPreviousCrash()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                const string errorFilename = "Fatal.log";
                var libraryPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Resources);
                var errorFilePath = Path.Combine(libraryPath, errorFilename);

                if (!File.Exists(errorFilePath))
                {
                    return;
                }
                var errorText = File.ReadAllText(errorFilePath);
                SendErrorEmail(errorText);
                File.Delete(errorFilePath);
            }
        }

    }
}
