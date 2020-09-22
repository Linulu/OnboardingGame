using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using OnboardingGame.Views;
using OnboardingGame.Data;
using System.IO;
using OnboardingGame.Models;
using System.Reflection;
using System.Diagnostics;
using OnboardingGame.Pages;
using System.Text.RegularExpressions;

namespace OnboardingGame
{
    public partial class App : Application
    {
        static Database database;

        public static Database Database {
            get {
                if (database == null) {
                    database = new Database(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Data.db3"));
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }

        //Initialize the Database here
        protected override async void OnStart() {

            if (!File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Data.db3"))) {
                string line;

                var assembly = IntrospectionExtensions.GetTypeInfo(typeof(TasksTab)).Assembly;
                Stream stream = assembly.GetManifestResourceStream("OnboardingGame.Onboarding.txt");

                StreamReader file = new StreamReader(stream);
                while ((line = file.ReadLine()) != null) {
                    if (line.Contains("\t"))
                    {
                        line = line.Replace("\t", "");
                        await Database.SaveItemAsync(new TaskItem()
                        {
                            ListID = Database.GetLatestSavedList().Result.ID,
                            Description = line,
                            Status = -1
                        });
                    }
                    else {
                        await Database.SaveItemAsync(new ToDoList()
                        {
                            Name = line
                        });
                    }
                }
            }
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
