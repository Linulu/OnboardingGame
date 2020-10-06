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
using System.Collections.Generic;

namespace OnboardingGame
{
    public partial class App : Application
    {
        static Database database;
        public static Database Database {
            get {
                if (database == null)
                {
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
        public static async void InitializeDatabase() {
            string line;

            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(TasksTab)).Assembly;
            Stream stream = assembly.GetManifestResourceStream("OnboardingGame.Onboarding.json");

            StreamReader file = new StreamReader(stream);
            line = file.ReadToEnd();

            JSON_Data list = JsonConvert.DeserializeObject<JSON_Data>(line);

            for (int i = 0; i < list.ListItems.Count; i++)
            {
                await Database.SaveItemAsync(list.ListItems[i]);

                foreach (TaskItem element in list.TaskItems)
                {
                    if (element.ListID == i)
                    {
                        element.ListID = list.ListItems[i].ID;
                        element.Status = -1;
                        await Database.SaveItemAsync(element);
                    }
                }
            }
        }

        public static void DeleteDatabase() {
            Database.DeleteDatabase();
            database = null;
            File.Delete(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Data.db3"));
        }
        
        protected override void OnStart() {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
