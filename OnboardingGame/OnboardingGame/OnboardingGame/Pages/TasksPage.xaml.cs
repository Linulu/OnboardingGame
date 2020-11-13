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
        private int ListID { get; set; }

        public List<TaskGroup> ListSource { get; private set; }

        public TasksPage(int listID)
        {
            InitializeComponent();
            ListID = listID;
            Title = App.Database.GetToDoListAsync(ListID).Result.Name;
        }

        protected override async void OnAppearing() {
            base.OnAppearing();

            GroupedList();

            await App.Update();
        }

        private async void GroupedList() { 
            List<TaskItem> tasks = await App.Database.GetTasksFromListAsync(ListID);
            List<Catagory> catagories = await App.Database.GetCatagories();

            ListSource = new List<TaskGroup>();

            for (int j = -1; j < catagories.Count; j++)
            {
                List<TaskItem> subTask = new List<TaskItem>();

                for (int i = 0; i < tasks.Count; i++)
                {
                    if (j == -1)
                    {
                        if (tasks[i].CatagoryID == 0)
                        {
                            subTask.Add(tasks[i]);
                        }
                    }
                    else if (tasks[i].CatagoryID == catagories[j].ID)
                    {
                        subTask.Add(tasks[i]);
                    }
                }

                if (subTask.Count > 0) {
                    if (j == -1)
                    {
                        ListSource.Add(new TaskGroup("Tasks", subTask));
                    }
                    else
                    {
                        ListSource.Add(new TaskGroup(catagories[j].Name , subTask));
                    }
                }
            }

            listView.ItemsSource = ListSource;
        }

        async void OnListItemTapped(object sender, SelectionChangedEventArgs e) {
            if (e.CurrentSelection != null)
            {
                await Navigation.PushAsync(new TaskContent()
                {
                    Title = App.Database.GetToDoListAsync(ListID).Result.Name,
                    BindingContext = (e.CurrentSelection.FirstOrDefault() as TaskItem)
                });
            }
        }
    }
}