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
        private static Dictionary<string, int> status;

        public App()
        {
            InitializeComponent();
            
            connection = new RestConnection("https://onboardingxperience.azurewebsites.net");
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

        //Initialize the Database here
        public static async void InitializeDatabase(string username, string password) {

            List<JSON_Data> data = await connection.GetDataAsync(username, password);
            List<ToDoList> lists = new List<ToDoList>();

            for (int i = 0; i < data.Count; i++) {
                List<TaskItem> task = new List<TaskItem>();
                for(int j = 0; j < data[i].tasks.Count; j++)
                {
                    task.Add(new TaskItem
                    {
                        title = data[i].tasks[j].title,
                        description = data[i].tasks[j].description,
                        points = data[i].tasks[j].points,
                        status = status[data[i].tasks[j].status]
                    });
                }
                lists.Add(new ToDoList(data[i].name, task));
            }
        }
        public static void DeleteDatabase() {
            Database.DeleteDatabase();
            database = null;
            File.Delete(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Data.db3"));
            Application.Current.MainPage = new AppShell();
        }
    }
}