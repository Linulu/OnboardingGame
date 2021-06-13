using SQLite;
using SQLiteNetExtensions;
using SQLiteNetExtensionsAsync.Extensions;
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
            if (!(item is null))
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
            return null;
        }
        public void SaveItemAsync(List<TaskItem> items) {
            if (!(items is null))
            {
                for (int i = 0; i < items.Count; i++)
                {
                    SaveItemAsync(items[i]).Wait();
                }
            }
        }
        public Task<int> DeleteItemAsync(TaskItem item)
        {
            if (!(item is null))
            {
                return _database.DeleteAsync(item);
            }
            return null;
        }

        //ToDoList__________________________________________________________________
        public Task<List<ToDoList>> GetToDoListAsync()
        {
            return _database.GetAllWithChildrenAsync<ToDoList>();
            //return _database.Table<ToDoList>().ToListAsync();
        }
        public Task<ToDoList> GetToDoListAsync(int id)
        {
            bool exist = _database.Table<ToDoList>().Where(i => i.ID == id).FirstOrDefaultAsync().Result != null;

            if (id > 0 && exist)
            {
                return _database.GetWithChildrenAsync<ToDoList>(id);
            }
            return null;
            //return _database.Table<ToDoList>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }
        public Task<int> SaveListAsync(ToDoList item)
        {
            if (!(item is null))
            {
                if (item.ID != 0)
                {
                    _database.UpdateWithChildrenAsync(item).Wait();
                    //return 1;
                }
                else
                {
                    _database.InsertWithChildrenAsync(item).Wait();
                    //return 1;
                }
            }
            return null;
        }
        public void SaveListAsync(List<ToDoList> items) {
            if (!(items is null))
            {
                for (int i = 0; i < items.Count; i++) {
                    SaveListAsync(items[i]);
                }
            }
        }

        public Task<int> DeleteItemAsync(ToDoList item)
        {
            if (!(item is null))
            {
                return _database.DeleteAsync(item);
            }
            return null;
        }

        //PlayerProfile_____________________________________________________________
        public Task<PlayerProfile> GetPlayerProfile()
        {
            return _database.Table<PlayerProfile>().FirstOrDefaultAsync();
        }
        public Task<PlayerProfile> GetPlayerProfile(int id) 
        {
            return _database.Table<PlayerProfile>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }
        public Task<int> SavePlayerAsync(PlayerProfile item) {
            if (!(item is null))
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
            return null;
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
        public Task<List<TaskItem>> GetAllDoneTasks(int id)
        {
            return _database.Table<TaskItem>().Where(i => i.ListID == id && i.status >= 1).ToListAsync();
        }
        public Task<List<TaskItem>> GetAllDoneTasks() {
            return _database.Table<TaskItem>().Where(i => i.status >= 1).ToListAsync();
        }
    }
}
