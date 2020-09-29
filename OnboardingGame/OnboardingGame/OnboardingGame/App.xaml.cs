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
using System.Runtime.InteropServices;
using Newtonsoft.Json;

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
                Stream stream = assembly.GetManifestResourceStream("OnboardingGame.Onboarding.json");

                StreamReader file = new StreamReader(stream);
                line = file.ReadToEnd();

                var list = JsonConvert.DeserializeObject<JSON_Data>(line);

                for (int i = 0; i < list.ListItems.Count; i++) {
                    Database.SaveItemAsync(list.ListItems[i]);

                    for (int j = 0; j < list.TaskItems.Count; j++) {
                        if (list.TaskItems[j].ListID == i) {
                            list.TaskItems[j].ListID = Database.GetLatestSavedList().Result.ID;
                            await Database.SaveItemAsync(list.TaskItems[j]);
                        }
                    }
                }

                /*while ((line = file.ReadLine()) != null) {
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
                            Name = line,
                            EXP = 10
                        });
                    }
                }*/
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
