using OnboardingGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using OnboardingGame.Data;

namespace OnboardingGame.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TasksTab : TabbedPage
    {
        public List<ToDoList> Lists { get; private set; }

        public TasksTab()
        {
            InitializeComponent();

            Lists = App.Database.GetToDoListAsync().Result;

            for(int i = 0; i < Lists.Count; i++) {
                Children.Add(new TasksPage(Lists[i].ID));
            }
        }
    }
}