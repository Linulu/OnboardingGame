using OnboardingGame.Models;
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
    public partial class TasksPage : ContentPage
    {
        private string name;
        private List<TaskItem> list;

        public TasksPage(ToDoList list)
        {
            InitializeComponent();
            this.name = list.Name;
            this.list = list.TaskItem;

            Title = name;
        }

        protected override void OnAppearing() {
            base.OnAppearing();

            listView.ItemsSource = new List<TaskGroup> { new TaskGroup(name, list) };
        }

        async void OnListItemTapped(object sender, SelectionChangedEventArgs e) {
            if (e.CurrentSelection != null)
            {
                await Navigation.PushAsync(new TaskContent(e.CurrentSelection.FirstOrDefault() as TaskItem));
            }
        }
    }
}