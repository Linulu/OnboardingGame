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

namespace OnboardingGame.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public Command LoginCommand { get; }
        public String Username { get; set; }
        public String Password { get; set; }

        public LoginPage()
        {
            InitializeComponent();

            Title = "Login";
            LoginCommand = new Command(OnLoginClicked);

            this.BindingContext = this;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            if (!(File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Data.db3"))))
            {
                bool answer = await DisplayAlert("No Database found", "There seems to be no Database in the app currently.\nWould you like to set it up now","Yes","No");
                if (answer) {
                    ProfileSetUp();
                }
            }
        }

        private async void OnLoginClicked(object obj)
        {
            if(!string.IsNullOrWhiteSpace(Username) || !string.IsNullOrWhiteSpace(Password))
            {
                if (App.Database.GetPlayerProfile().Result.Name.CompareTo(Username) == 0 && App.Database.GetPlayerProfile().Result.Password.CompareTo(Password) == 0)
                {
                    await Shell.Current.GoToAsync($"//{nameof(TasksTab)}");
                }
                else {
                    await DisplayAlert("Error", "Wrong Username or Password", "Try Agian");
                }
            }
            else {
                await DisplayAlert("Error","No Username or Password","Try Agian");
            }
        }

        private async void ProfileSetUp() {
            string name = await DisplayPromptAsync("Username", "Choose a Username?");
            if (!string.IsNullOrWhiteSpace(name))
            {
                string password = await DisplayPromptAsync("Password", "Choose a password");
                if (!string.IsNullOrWhiteSpace(password))
                {
                    await App.InitializeDatabase();
                    await App.Database.SavePlayerAsync(new PlayerProfile()
                    {
                        Name = name,
                        Password = password
                    });
                    await Shell.Current.GoToAsync($"//{nameof(TasksTab)}");
                }
            }
        }
    }
}