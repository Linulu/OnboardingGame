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

        public Profile()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            pP = await App.Database.GetPlayerProfile();

            List<ToDoList> list = await App.Database.GetToDoListAsync();

            int EXP = 0;

            for (int i = 0; i < list.Count; i++) {
               EXP += (await App.Database.GetAllDoneTasks(list[i].ID)) * list[i].EXP;
            }
            pP.EXP = EXP;
            int level = 1 + (int)Math.Log(1 + ((double)EXP / 3));
            int toNextLVL = (int)(3 * Math.Pow(Math.E, level) - 3);

            await App.Database.SavePlayerAsync(pP);

            Lvl.Text = "Level: " + level;
            Exp.Text = "Points: " + EXP + "/" + toNextLVL;
            ExpBar.Progress = (double)EXP / toNextLVL;

            this.BindingContext = pP;

            Date.Text = "Start Date: " + pP.StartDate.Month + "/" + pP.StartDate.Day + "/" + pP.StartDate.Year;
            
        }
    }
}