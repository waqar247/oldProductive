using IAmProductive.Models;
using IAmProductive.ViewModels.AddTaskVM;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

/// <summary>
/// From Add task page:
/// 1)add new Tasks
/// 2)update tha tasks
/// </summary>
namespace IAmProductive.Views.AddTasksViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddAndEditTasksPopUpViewPage : PopupPage
    {
        AddTaskkPopUpViewModel addTaskkPopUpViewModel;
        public AddAndEditTasksPopUpViewPage(string taskTrackId, bool isEditRequest = false)
        {
            InitializeComponent();
            addTaskkPopUpViewModel = new AddTaskkPopUpViewModel(Navigation);
            BindingContext = addTaskkPopUpViewModel;
            if (isEditRequest)
            { //Edit request
                addTaskkPopUpViewModel.Task = addTaskkPopUpViewModel.GetTaskBaseOnTrackID(taskTrackId);
                addTaskkPopUpViewModel.Task.UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt");
                RequestTitle.Text = "Edit Name";
            }
            else
            {    //add new task
                addTaskkPopUpViewModel.Task = addTaskkPopUpViewModel.GetTaskBaseOnTrackID(taskTrackId);
                addTaskkPopUpViewModel.Task.Title = null;
                RequestTitle.Text = "Add Custom Task";
            }

        }

        private void Save_Clicked(object sender, EventArgs e)
        {
            addTaskkPopUpViewModel.SaveTask();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        protected override bool OnBackButtonPressed()
        {
            // Prevent hide popup
            return base.OnBackButtonPressed();
            // return true;
        }

        // Invoced when background is clicked
        protected override bool OnBackgroundClicked()
        {
            CloseAllPopup();
            return false;
        }

        //  Invoced when close button tapped close all popup,s
        private void OnCloseButton_Tapped(object sender, EventArgs e)
        {
            CloseAllPopup();
        }


        // close all open popup,s
        private async void CloseAllPopup()
        {
            await Navigation.PopAllPopupAsync();
        }

        private void CancelBtn_Clicked(object sender, EventArgs e)
        {
            CloseAllPopup();
        }
    }
}