using OnboardingGame.Models;
using System;
using System.Collections.Generic;
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

            await App.Database.SavePlayerAsync(pP);

            Lvl.Text = "Level: " + (40+40*(pP.EXP/40))/pP.EXP;

            this.BindingContext = pP;
        }
    }
}