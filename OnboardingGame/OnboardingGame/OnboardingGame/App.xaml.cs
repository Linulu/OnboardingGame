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
using System.Threading.Tasks;
using Rg.Plugins.Popup.Services;

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

        public static async Task Update() {
            /* Use this for creating pop-ups
            await PopupNavigation.Instance.PushAsync(new PopupView());
            */
            Console.WriteLine("Updated");
        }

        //Initialize the Database here
        public static async Task InitializeDatabase(bool carBenefits) {
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(TasksTab)).Assembly;
            Stream stream;
            if (carBenefits)
            {
                stream = assembly.GetManifestResourceStream("OnboardingGame.Onboarding.json");
            }
            else {
                stream = assembly.GetManifestResourceStream("OnboardingGame.Onboarding_NoCar.json");
            }

            StreamReader file = new StreamReader(stream);
            string line = file.ReadToEnd();

            JSON_Data list = JsonConvert.DeserializeObject<JSON_Data>(line);

            for (int i = 0; i < list.Catagories.Count; i++) {
                await Database.SaveCatagoryAsync(list.Catagories[i]);
            }

            for (int i = 0; i < list.ListItems.Count; i++)
            {
                await Database.InsertListAsync(list.ListItems[i]);

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

            List<Achievement> aList = await AchievmentList.GetAchievementsAsync();
            foreach (Achievement a in aList) {
                await Database.SaveAchievement(a);
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
