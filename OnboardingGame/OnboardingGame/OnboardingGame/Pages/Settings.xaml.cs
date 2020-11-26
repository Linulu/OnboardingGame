using OnboardingGame.Models;
using OnboardingGame.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Services;

namespace OnboardingGame.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Settings : ContentPage
    {
        public Settings()
        {
            InitializeComponent();
        }

        async void OnButtonPress(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Reset", "Do you really wish to reset all the data?", "Yes", "No");

            if (answer)
            {
                App.DeleteDatabase();

                await DisplayAlert("Profile Deleted", "Your profile has been deleted", "Continue");
                await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
            }
        }

        async void OnTitlePress(object sender, EventArgs e) {
            string result = await DisplayPromptAsync("Change title", "New title");
            if (result != null)
            {
                PlayerProfile pP = await App.Database.GetPlayerProfile();
                pP.Title = result;
                await App.Database.SavePlayerAsync(pP);
            }
        }

        async void ProfileButtonPress(object sender, EventArgs e) {
            string result = await DisplayPromptAsync("Change name","New name");
            if (result != null) {
                PlayerProfile pP = await App.Database.GetPlayerProfile();
                pP.Name = result;
                await App.Database.SavePlayerAsync(pP);
            }
        }

        async void AboutButtonPress(object sender, EventArgs e) {
            await PopupNavigation.Instance.PushAsync(new PopupPages.InfoPopup());
        }

        async void HowToButtonPress(object sender, EventArgs e) {
            await PopupNavigation.Instance.PushAsync(new PopupPages.ListInfoPopup());
        }
    }
}