using OnboardingGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Services;
using OnboardingGame.PopupPages;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using OnboardingGame.Data;

namespace OnboardingGame.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TasksTab : TabbedPage
    {
        private List<ToDoList> lists;
        private PlayerProfile player;

        public TasksTab() {
            InitializeComponent();

            lists = App.Database.GetToDoListAsync().Result;
            player = App.Database.GetPlayerProfile().Result;

            for (int i = 0; i < lists.Count; i++) {
                Children.Add(new TasksPage(lists[i]));
            }
        }

        public TasksTab(List<ToDoList> lists, PlayerProfile player)
        {
            InitializeComponent();

            this.lists = lists;
            this.player = player; 

            for(int i = 0; i < this.lists.Count; i++) {
                Children.Add(new TasksPage(this.lists[i]));
            }
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            Title = "Missons, Points: " + App.Database.GetPlayerProfile(player.ID).Result.EXP;

            if (App.FirstTimeList)
            {
                await PopupNavigation.Instance.PushAsync(new ListInfoPopup());
                App.FirstTimeList = false;
            }
            else
            {
                //await App.Update();
            }
        }
    }
}