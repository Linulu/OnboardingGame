using OnboardingGame.Interfaces;
using OnboardingGame.Models;
using OnboardingGame.PopupPages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OnboardingGame.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TasksPage : ContentPage, IObserver
    {
        private string name;
        private List<TaskItem> list;

        private int index = 0;

        public TasksPage(ToDoList list)
        {
            InitializeComponent();
            this.name = list.name;
            this.list = list.tasks;

            Title = name;

            App.Attach(this);
        }

        public void Update()
        {
            listView.ItemsSource = new List<TaskGroup> { new TaskGroup(name, list) };
            listView.ScrollTo(index, position: ScrollToPosition.MakeVisible, animate: false);
            index = 0;

            if (listView.SelectedItem != null) {
                listView.SelectedItem = null;
            }
        }

        protected override void OnAppearing() {
            base.OnAppearing();

            App.ObserverUpdate();
        }

        async void OnListItemTapped(object sender, SelectionChangedEventArgs e) {
            if (listView.SelectedItem != null && e.CurrentSelection != null)
            {
                index = list.IndexOf(e.CurrentSelection.FirstOrDefault() as Models.TaskItem);
                await PopupNavigation.Instance.PushAsync(new PopupTask(e.CurrentSelection.FirstOrDefault() as Models.TaskItem));
            }
        }
    }
}