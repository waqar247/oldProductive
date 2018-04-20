using IAmProductive.ViewModels;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IAmProductive.Views.HelpPage
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HelpPage : ContentPage
	{
        public HelpViewModel helpViewModel;
        public HelpPage ()
		{
			InitializeComponent ();
            helpViewModel = new HelpViewModel();
            BindingContext = helpViewModel;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (CrossConnectivity.Current.IsConnected)
            {
                helpViewModel.IsEnabledAds = true;
            }
            else
            {
                helpViewModel.IsEnabledAds = false;
            }
        }
    }
}