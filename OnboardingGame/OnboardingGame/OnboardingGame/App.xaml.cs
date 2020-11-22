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

        public enum AchievementType {
            List,
            EXP,
            Date
        }

        public static async Task Update(AchievementType t) {
            /* Use this for creating pop-ups
            await PopupNavigation.Instance.PushAsync(new PopupView());
            */
            switch (t) {
                case AchievementType.List:
                    List<string> listList = await ListUpdate();
                    foreach (string el in listList)
                    {
                        Console.WriteLine(el);
                    }
                    break;
                case AchievementType.EXP:
                    List<string> expList = await EXPUpdate();
                    foreach (string el in expList)
                    {
                        Console.WriteLine(el);
                    }
                    break;
                case AchievementType.Date:
                    List<string> dateList = await DateUpdate();
                    foreach (string el in dateList) {
                        Console.WriteLine(el);
                    }
                    break;
            }
        }
        private static async Task<List<string>> ListUpdate() {
            List<string> rV = new List<string>();
            List<Achievement> aList = await Database.GetAchievementByType(AchievementType.List);
            foreach(Achievement element in aList)
            {
                if (element.TargetID != 0)
                {
                    element.CurrentAmount = await Database.GetAllDoneTasks(element.TargetID);
                }
                else {
                    element.CurrentAmount = await Database.GetAllDoneTasks();
                }
                element.Status = (element.CurrentAmount >= element.RequiredAmount);
                if (element.Status) {
                    rV.Add(element.Name);
                }
                await Database.SaveAchievement(element);
            }
            return rV;
        }
        private static async Task<List<string>> EXPUpdate() {
            List<string> rV = new List<string>();
            List<Achievement> aList = await Database.GetAchievementByType(AchievementType.EXP);

            PlayerProfile pP = await App.Database.GetPlayerProfile();
            List<ToDoList> list = await App.Database.GetToDoListAsync();
            int EXP = 0;
            for (int i = 0; i < list.Count; i++)
            {
                EXP += (await App.Database.GetAllDoneTasks(list[i].ID)) * list[i].EXP;
            }
            pP.EXP = EXP;
            await App.Database.SavePlayerAsync(pP);

            foreach (Achievement element in aList)
            {
                element.CurrentAmount = pP.EXP;
                element.Status = (element.CurrentAmount >= element.RequiredAmount);
                if (element.Status) {
                    rV.Add(element.Name);
                }
                await Database.SaveAchievement(element);
            }
            return rV;
        }
        private static async Task<List<string>> DateUpdate() {
            List<string> rV = new List<string>();
            List<Achievement> aList = await Database.GetAchievementByType(AchievementType.Date);
            foreach (Achievement element in aList)
            {
                element.CurrentAmount = DateTime.Now.Ticks;
                element.Status = (element.CurrentAmount >= element.RequiredAmount);
                if (element.Status) {
                    rV.Add(element.Name);
                }
                await Database.SaveAchievement(element);
            }
            return rV;
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
    }
}
