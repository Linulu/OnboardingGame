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

namespace OnboardingGame.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public Command LoginCommand { get; }

        public LoginPage()
        {
            InitializeComponent();

            Title = "Login";
            LoginCommand = new Command(OnLoginClicked);

            this.BindingContext = this;
        }

        private async void OnLoginClicked(object obj)
        {
            if (!(File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Data.db3"))))
            {
                await App.InitializeDatabase();
            }

            if (await App.Database.GetPlayerProfile() == null)
            {
                string name = await DisplayPromptAsync("Profile Setup", "What's your name?");
                if (!string.IsNullOrWhiteSpace(name)) {
                    string password = await DisplayPromptAsync("Password Setup", "Select a password");
                    if (!string.IsNullOrWhiteSpace(password)) {
                        await App.Database.SavePlayerAsync(new PlayerProfile()
                        {
                            Name = name,
                            Password = password
                        });
                        await Shell.Current.GoToAsync($"//{nameof(TasksTab)}");
                    }
                }
            }
            else {
                string result = await DisplayPromptAsync("Current Profile: " + App.Database.GetPlayerProfile().Result.Name, "Password?");
                if (App.Database.GetPlayerProfile().Result.Password.CompareTo(result) == 0) {
                    // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
                    await Shell.Current.GoToAsync($"//{nameof(TasksTab)}");
                }
            }
        }
    }
}