using SQLite;
using SQLiteNetExtensions;
using System;
using System.Collections.Generic;
using System.Text;
using OnboardingGame.Models;
using System.Threading.Tasks;
using System.Data.SqlTypes;
using System.Diagnostics;

namespace OnboardingGame.Data
{
    public class Database
    {
        readonly SQLiteAsyncConnection _database;

        public Database(string dbPath) {
            _database = new SQLiteAsyncConnection(dbPath, true);
            _database.CreateTableAsync<TaskItem>().Wait();
            _database.CreateTableAsync<ToDoList>().Wait();
            _database.CreateTableAsync<PlayerProfile>().Wait();
            _database.CreateTableAsync<Catagory>().Wait();
            _database.CreateTableAsync<Achievement>().Wait();
        }

        public void DeleteDatabase() {
            _database.DropTableAsync<TaskItem>().Wait();
            _database.DropTableAsync<ToDoList>().Wait();
            _database.DropTableAsync<PlayerProfile>().Wait();
            _database.CloseAsync().Wait();
        }

        //TaskItem_________________________________________________________________
        public Task<List<TaskItem>> GetTaskItem()
        {
            return _database.Table<TaskItem>().ToListAsync();
        }
        public Task<TaskItem> GetTaskItem(int id)
        {
            if (id >= 0 && _database.Table<ToDoList>().ToListAsync().Result.Count > id)
            {
                return _database.Table<TaskItem>().Where(i => i.ID == id).FirstOrDefaultAsync();
            }
            return null;
        }

        public Task<int> SaveItemAsync(TaskItem item)
        {
            if (item.ID != 0)
            {
                return _database.UpdateAsync(item);
            }
            else
            {
                return _database.InsertAsync(item);
            }
        }
        public Task<int> DeleteItemAsync(TaskItem item)
        {
            return _database.DeleteAsync(item);
        }

        //ToDoList__________________________________________________________________
        public Task<List<ToDoList>> GetToDoListAsync()
        {
            return _database.Table<ToDoList>().ToListAsync();
        }
        public Task<ToDoList> GetToDoListAsync(int id)
        {
            return _database.Table<ToDoList>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }
        public Task<int> SaveListAsync(ToDoList item)
        {
            if (item.ID != 0)
            {
                return _database.UpdateAsync(item);
            }
            else
            {
                return _database.InsertAsync(item);
            }
        }
        public Task<int> DeleteItemAsync(ToDoList item)
        {
            return _database.DeleteAsync(item);
        }

        //PlayerProfile_____________________________________________________________
        public Task<PlayerProfile> GetPlayerProfile()
        {
            return _database.Table<PlayerProfile>().FirstOrDefaultAsync();
        }
        public Task<int> SavePlayerAsync(PlayerProfile item) {
            if (item.ID != 0)
            {
                return _database.UpdateAsync(item);
            }
            else
            {
                return _database.InsertAsync(item);
            }
        }
        public Task<int> DeletePlayerAsync() {
            return _database.DeleteAllAsync<PlayerProfile>();
        }

        //Catagories________________________________________________________________
        public Task<List<Catagory>> GetCatagories() {
            return _database.Table<Catagory>().ToListAsync();
        }
        public Task<Catagory> GetCatagory(int id) {
            return _database.Table<Catagory>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }
        public Task<int> SaveCatagoryAsync(Catagory item)
        {
            return _database.InsertAsync(item);
        }
        public Task<int> DeleteCatagoryAsync(Catagory item)
        {
            return _database.DeleteAsync(item);
        }

        //Achievements______________________________________________________________
        public Task<List<Achievement>> GetAchievement() {
            return _database.Table<Achievement>().ToListAsync();
        }
        public Task<Achievement> GetAchievement(int id) {
            return _database.Table<Achievement>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }
        public Task<int> SaveAchievement(Achievement item) {
            if (item.ID != 0)
            {
                return _database.UpdateAsync(item);
            }
            else
            {
                return _database.InsertAsync(item);
            }
        }
        public Task<int> DeleteAchievement(Achievement item) {

            return _database.DeleteAsync(item);
        }

        //Return a list of TaskItems who's ListID matches that of the given id 
        //parameter. Use this method to get all the TaskItems from a given ToDoList 
        public Task<List<TaskItem>> GetTasksFromListAsync(int id) {
            return _database.Table<TaskItem>().Where(i => i.ListID == id).ToListAsync();
        }
        public Task<ToDoList> GetLatestSavedList() {
            return _database.Table<ToDoList>().ElementAtAsync(_database.Table<ToDoList>().CountAsync().Result - 1);
        }
        public Task<int> GetAllDoneTasks(int id)
        {
            return _database.Table<TaskItem>().Where(i => i.ListID == id && i.Status >= 1).CountAsync();
        }
        public Task<int> GetAllDoneTasks() {
            return _database.Table<TaskItem>().Where(i => i.Status >= 1).CountAsync();
        }
        public Task<List<Achievement>> GetAchievementByType(App.AchievementType t) {
            return _database.Table<Achievement>().Where(i => i.AchievementType == t && i.Status == false).ToListAsync();
        }
    }
}
