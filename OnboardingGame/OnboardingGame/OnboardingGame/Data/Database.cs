using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using OnboardingGame.Models;
using System.Threading.Tasks;
using System.Data.SqlTypes;

namespace OnboardingGame.Data
{
    public class Database
    {
        readonly SQLiteAsyncConnection _database;

        public Database(string dbPath) {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<TaskItem>().Wait();
            _database.CreateTableAsync<ToDoList>().Wait();
            _database.CreateTableAsync<PlayerProfile>().Wait();
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
            return _database.Table<TaskItem>().Where(i => i.ID == id).FirstOrDefaultAsync();
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
        public Task<int> SaveItemAsync(ToDoList item)
        {
            return _database.InsertAsync(item);
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
    }
}
