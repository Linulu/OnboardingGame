using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using OnboardingGame.Views;
using OnboardingGame.Data;
using System.IO;
using OnboardingGame.Models;

namespace OnboardingGame
{
    public partial class App : Application
    {

        static Database database;

        public static Database Database {
            get {
                if (database == null) {
                    database = new Database(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Data.db3"));

                    ToDoList listT = new ToDoList();
                    TaskItem itemT = new TaskItem();

                    listT.Name = "ListItem";
                    itemT.Description = "TaskItem";

                    Database.SaveItemAsync(listT).Wait();

                    itemT.ListID = Database.GetToDoListAsync().Result[0].ID;

                    Database.SaveItemAsync(itemT).Wait();
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
