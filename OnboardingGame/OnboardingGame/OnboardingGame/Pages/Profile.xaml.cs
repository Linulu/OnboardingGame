using OnboardingGame.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OnboardingGame.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Profile : ContentPage
    {
        PlayerProfile pP;

        int EXP = 0;
        int level = 0;
        int toNextLVL = 0;

        public Profile()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await App.Update();

            pP = await App.Database.GetPlayerProfile();

            EXP = pP.EXP;
            level = 1 + (int)Math.Log(1 + ((double)EXP / 3));
            toNextLVL = (int)(3 * Math.Pow(Math.E, level) - 3);

            Lvl.Text = "Level: " + level;
            NextLevel.Text = "Amount of hearts needed for next level " + (1 + (toNextLVL - EXP));
            ExpBar.Progress = (double)EXP / toNextLVL;

            this.BindingContext = pP;

            //Date.Text = "Start Date: " + pP.StartDate.Date.ToString("MMMM/dd/yyyy");
            ExpSize();

            Achievements.ItemsSource = await App.Database.GetAchievement();
            Date.Text = "Start date: " + pP.StartDate.ToString("MMMM/dd/yyyy");
        }

        async void OnAchievementTapped(object sender, SelectionChangedEventArgs e) {
            if (e.CurrentSelection != null) {
                Achievement a = e.CurrentSelection.FirstOrDefault() as Achievement;
                await DisplayAlert(a.Name,"Description:\n"+a.Description,"Return");
            }
        }

        private void ExpSize()
        {
            int length = Exp.Text.Length;
            if (length > 11)
            {
                Exp.FontSize = Device.GetNamedSize(NamedSize.Micro, Exp);
            }
            else if (length > 8)
            {
                Exp.FontSize = Device.GetNamedSize(NamedSize.Small, Exp);
            }
            else if (length > 6)
            {
                Exp.FontSize = Device.GetNamedSize(NamedSize.Medium, Exp);
            }
            else if (length > 0) {
                Exp.FontSize = Device.GetNamedSize(NamedSize.Large, Exp);
            }
        }
    }
}