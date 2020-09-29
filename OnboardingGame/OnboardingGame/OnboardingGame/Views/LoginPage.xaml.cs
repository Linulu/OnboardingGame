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
            if (await App.Database.GetPlayerProfile() == null)
            {
                string result = await DisplayPromptAsync("Profile Setup", "What's your name?");
                if (result != null) {
                    await App.Database.SavePlayerAsync(new PlayerProfile()
                    {
                        Name = result
                    });
                    await Shell.Current.GoToAsync($"//{nameof(TasksTab)}");
                }
            }
            else {
                // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
                await Shell.Current.GoToAsync($"//{nameof(TasksTab)}");
            }
        }
    }
}