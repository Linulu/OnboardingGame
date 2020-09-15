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
        public List<TaskItem> List { get; private set; }

        public string Name { get; private set; }

        public TasksPage(int listID)
        {   
            InitializeComponent();

            Name = App.Database.GetToDoListAsync(listID).Result.Name;
            List = App.Database.GetTasksFromListAsync(listID).Result;

            BindingContext = this;
        }
    }
}