using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnboardingGame.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using OnboardingGame.Pages;
using OnboardingGame.Models;
using System.IO;
using System.Diagnostics;
using System.Windows.Input;

namespace OnboardingGame.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public Command LoginCommand { get; }
        public string Username { get; set; }
        public string Password { get; set; }

        public LoginPage()
        {
            InitializeComponent();

            Title = "Login";
            LoginCommand = new Command(OnLoginClicked);

            this.BindingContext = this;
        }

        protected override void OnAppearing() {
            base.OnAppearing();
        }

        private async void OnLoginClicked(object obj)
        {
            if (await App.LoginUser(Username, Password))
            {
                App.DeleteDatabase();

                await App.InitializeDatabase(Username, Password);
                await Shell.Current.GoToAsync($"//{nameof(TasksTab)}");
                return;
            }

            await DisplayAlert("Wrong username or password", "Maybe your finger slipped when you tried to login?", "Try Again");
        }

        private async void ProfileSetUp() {
            if (!(File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Data.db3"))))
            {
                bool answer = await DisplayAlert("No Profile found", "There seems to be no Profile in the app currently.\nWould you like to set it up now?", "Yes", "No");
                if (answer)
                {
                    await Navigation.PushAsync(new LoginSetupPage());
                }
            }
            else
            {
                await DisplayAlert("Error", "There already exist a profile on this device", "Return");
            }
        }
    }
}