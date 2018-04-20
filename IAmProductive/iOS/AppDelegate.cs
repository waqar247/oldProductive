using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using CarouselView.FormsPlugin.iOS;
using Foundation;
using IAmProductive.Constants;
using IAmProductive.iOS.Helpers;
using Plugin.Connectivity;
using Plugin.Toasts;
using Rg.Plugins.Popup.IOS;
using SegmentedControl.FormsPlugin.iOS;
using Syncfusion.SfChart.iOS;
using Syncfusion.SfChart.XForms;
using Syncfusion.SfChart.XForms.iOS.Renderers;
using UIKit;
using UserNotifications;
using Xamarin.Forms;

namespace IAmProductive.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        private static string _lastError;
        App mainApplication;
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            
            global::Xamarin.Forms.Forms.Init();
            DependencyService.Register<ToastNotification>();
            ToastNotification.Init();
            XamForms.Controls.iOS.Calendar.Init();
            CarouselViewRenderer.Init();
            SegmentedControlRenderer.Init();
            new SfChartRenderer();

            SendAnyPreviousCrash(); //read log file and send crash report if available
            mainApplication = new App();
            MessagingCenter.Subscribe<App, string>(mainApplication, AppConstant.ErrorEvent, (s, error) =>
            {
                mainApplication.MainPage.DisplayAlert("Error", AppConstant.ErrorText, "Close");
                SendErrorEmail(error);
            });
           
            LoadApplication(mainApplication);

            #region Pie Chart Title Customization
            Xamarin.Forms.Forms.ViewInitialized += (object sender, Xamarin.Forms.ViewInitializedEventArgs e) =>
               {
                   if (e.NativeView is SFChart)
                   {
                       (e.NativeView as SFChart).Delegate = new ChartCustomDelegate(e.View as SfChart);
                   }
               }; 
            #endregion
            if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
            {
                // Request Permissions
                UNUserNotificationCenter.Current.RequestAuthorization(UNAuthorizationOptions.Alert | UNAuthorizationOptions.Badge | UNAuthorizationOptions.Sound, (granted, error) =>
                {
                    // Do something if needed
                });
            }
            else if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
            {
                var notificationSettings = UIUserNotificationSettings.GetSettingsForTypes(
                 UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound, null
                    );

                app.RegisterUserNotificationSettings(notificationSettings);
            }

            return base.FinishedLaunching(app, options);
        }
        public override void WillTerminate(UIApplication uiApplication)
        {
            base.WillTerminate(uiApplication);
            MessagingCenter.Unsubscribe<App, string>(mainApplication, AppConstant.ErrorEvent);
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

                    var mailAddress = new MailAddress(AppConstant.ErrorEmailAddress, "iOS App");
                    var mail = new MailMessage(mailAddress, mailAddress);
                    mail.Subject = "IAmProductive|IOS|Crash";
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
                var libraryPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Resources);
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
                var libraryPath = Environment.GetFolderPath(Environment.SpecialFolder.Resources);
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
