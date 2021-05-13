﻿using OnboardingGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Services;
using OnboardingGame.PopupPages;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;

namespace OnboardingGame.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginSetupPage : ContentPage
    {
        public LoginSetupPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing() {
            base.OnAppearing();
            await PopupNavigation.Instance.PushAsync(new InfoPopup());
        }

        async void OnFinishClicked(object sender, EventArgs e) {
            
            if (!string.IsNullOrWhiteSpace(Username.Text) && !string.IsNullOrWhiteSpace(Password.Text))
            {
                bool answer = await DisplayAlert("Continue?", "Do you want this username and password?", "Yes", "No");

                if (answer) {
                    await App.Database.SavePlayerAsync(new PlayerProfile()
                    {
                        Name = Username.Text,
                        Password = Password.Text,
                        StartDate = Start_Date.Date
                    });
                    App.InitializeDatabase(CarBenifit.IsChecked);
                    await Navigation.PopAsync();
                    //await Shell.Current.GoToAsync($"Missons?list={await App.Database.GetToDoListAsync()}");
                    await Shell.Current.GoToAsync($"//{nameof(TasksTab)}");
                }
            }
            else {
                await DisplayAlert("","Please type in a valid Username/Password","Return");
            }
        }
    }
}