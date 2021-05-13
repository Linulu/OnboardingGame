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
using OnboardingGame.Interfaces;

namespace OnboardingGame.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TasksTab : TabbedPage, IObserver
    {
        private List<ToDoList> lists;
        private PlayerProfile player;

        public TasksTab() {
            InitializeComponent();

            lists = App.Database.GetToDoListAsync().Result;
            player = App.Database.GetPlayerProfile().Result;

            App.Attach(this);

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

        public void Update()
        {
            Title = "🌟 Collected: " + App.Database.GetPlayerProfile(player.ID).Result.EXP;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            App.ObserverUpdate();

            if (Data.Settings.FirstRun)
            {
                await DisplayAlert("Welcome Aboard!","If you ever need a run down of how this app works. Navigate yourself to the settings menu and tap the About This App buttons.","Got it!");
                Data.Settings.FirstRun = false;
            }
        }
    }
}