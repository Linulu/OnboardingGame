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
using OnboardingGame.PopupPages;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Services;
using OnboardingGame.Interfaces;
using OnboardingGame.REST_Data;
using System.Linq;

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

        private static List<IObserver> observers = new List<IObserver>();

        private static RestConnection connection;
        private static readonly Dictionary<string, int> status = new Dictionary<string, int>();

        public App()
        {
            InitializeComponent();

            connection = new RestConnection("https://onboarding_xperience.phoniro.se");

            status.Add("ToDo", -1);
            status.Add("Active", 0);
            status.Add("Done", 1);

            MainPage = new AppShell();
        }

        public static void Attach(IObserver observer) {
            observers.Add(observer);
        }

        public static void ObserverUpdate() {
            foreach (IObserver o in observers) {
                o.Update();
            }
        }

        public static async Task<bool> LoginUser(string username, string password) {
            return await connection.ValidateUserAsync(username, password);
        }

        public static async System.Threading.Tasks.Task UpdateTask(TaskItem item) {
            REST_Data.Task task = new REST_Data.Task
            {
                title = item.title,
                description = item.description,
                points = item.points,
                status = status.FirstOrDefault(x => x.Value == item.status).Key
            };

            PlayerProfile player = await Database.GetPlayerProfile();
            await connection.UpdateStatusAsync(player.Name, player.Password, task);
        }

        //Initialize the Database here
        public static async System.Threading.Tasks.Task InitializeDatabase(string username, string password) {

            List<JSON_Data> data = await connection.GetDataAsync(username, password);
            List<ToDoList> lists = new List<ToDoList>();
            int pp = 0;

            for (int i = 0; i < data.Count; i++) {
                List<TaskItem> tasks = new List<TaskItem>();
                for(int j = 0; j < data[i].tasks.Count; j++)
                {
                    TaskItem task = new TaskItem
                    {
                        title = data[i].tasks[j].title,
                        description = data[i].tasks[j].description,
                        points = data[i].tasks[j].points,
                        status = status[data[i].tasks[j].status]
                    };

                    if (task.status > 0) {
                        pp += task.points;
                    }

                    tasks.Add(task);
                }
                lists.Add(new ToDoList(data[i].name, tasks));
            }

            PlayerProfile player = new PlayerProfile
            {
                Name = username,
                Password = password
            };
            player.AddPoints(pp);
            await App.Database.SavePlayerAsync(player);

            Database.SaveListAsync(lists);
        }

        public static void DeleteDatabase() {
            Database.DeleteDatabase();
            database = null;
            File.Delete(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Data.db3"));
        }
    }
}