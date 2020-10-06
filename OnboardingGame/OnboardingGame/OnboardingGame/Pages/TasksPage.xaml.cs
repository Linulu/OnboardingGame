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
    public partial class TasksPage : ContentPage
    {
        private int ListID { get; set; }

        public TasksPage(int listID)
        {   
            InitializeComponent();
            ListID = listID;
            Title = App.Database.GetToDoListAsync(ListID).Result.Name;
        }

        protected override async void OnAppearing() {
            base.OnAppearing();
            
            listView.ItemsSource = await App.Database.GetTasksFromListAsync(ListID);
        }

        async void OnListViewItemTapped(object sender, ItemTappedEventArgs e) {

            if ((e.Item as TaskItem).Status < 0)
            {
                bool response = await DisplayAlert((e.Item as TaskItem).Title, (e.Item as TaskItem).Description, "Start this task", "Cancel");
                (e.Item as TaskItem).Status = response.CompareTo(false) - 1;
                await App.Database.SaveItemAsync(e.Item as TaskItem);
                listView.ItemsSource = await App.Database.GetTasksFromListAsync(ListID);
            }
            else if ((e.Item as TaskItem).Status == 0)
            {
                bool response = await DisplayAlert((e.Item as TaskItem).Title, (e.Item as TaskItem).Description, "Finish this task", "Cancel");
                (e.Item as TaskItem).Status = response.CompareTo(false);
                await App.Database.SaveItemAsync(e.Item as TaskItem);
                listView.ItemsSource = await App.Database.GetTasksFromListAsync(ListID);
            }
            else {
                await DisplayAlert("Completed", "Congratulations you've completed this task!", "Next Task");
            }
        }
    }
}