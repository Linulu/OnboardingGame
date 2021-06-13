using OnboardingGame.Models;
using OnboardingGame.PopupPages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OnboardingGame.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaskContent : ContentPage
    {
        public Models.TaskItem task;

        public TaskContent(Models.TaskItem item)
        {
            InitializeComponent();

            task = item;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Title_Label.Text = task.title + " (" + task.points + ": Hearts)";

            Description_Label.Text = task.description;

            if (task.status < 0)
            {
                StatusBar.Color = Color.Red;
            }
            else if (task.status == 0)
            {
                StatusBar.Color = Color.Yellow;
            }
            else {
                StatusBar.Color = Color.Green;
            }
        }

        async void OnStartButtonClicked(object sender, EventArgs e) {

            if (task.status < 0) {
                bool answer = await DisplayAlert("Attention", "Once a task has been started it can not be undone. Do you wish to continue?", "Yes", "No");
                if (answer) {
                    task.UpdateStatus(0);
                    await App.Database.SaveItemAsync(task);
                    await Navigation.PopAsync();
                }
            }
            else if (task.status > 0) {
                await DisplayAlert("Finished","You've already finished this task","Return");
            }
        }

        async void OnFinishButtonClicked(object sender, EventArgs e)
        {
            if (task.status <= 0) {
                bool answer = await DisplayAlert("Attention", "Once a task has been finished it can not be undone. Do you whish to continue?", "Yes", "No");
                if (answer) {
                    task.UpdateStatus(1);
                    PlayerProfile pp = await App.Database.GetPlayerProfile();
                    pp.AddPoints(task.points);
                    await App.Database.SavePlayerAsync(pp);
                    await App.Database.SaveItemAsync(task);
                    await Navigation.PopAsync();
                    await PopupNavigation.Instance.PushAsync(new TaskCompleted(task));
                }
            }
            else if (task.status > 0)
            {
                await DisplayAlert("Finished", "You've already comepleted this task", "Return");
            }
        }
    }
}