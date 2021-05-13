using OnboardingGame.Data;
using OnboardingGame.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Collections.Generic;
using System;

namespace OnboardingTests
{
    [TestClass]
    public class DatabaseTests
    {
        Database database = new Database("C:/Users/lcg/Source/Repos/OnboardingGame/OnboardingGame/OnboardingTests/Data.db3");

        [TestMethod]
        public void Database_Constructor_Check()
        {
            Assert.IsNotNull(database.GetToDoListAsync());
            Assert.IsNotNull(database.GetTaskItem());
            Assert.IsNotNull(database.GetPlayerProfile());
        }

        [TestMethod]
        public void GetList_Based_On_Different_ID_Values_Test() 
        {
            Assert.AreEqual(null, database.GetToDoListAsync(-1));
            Assert.AreEqual(null, database.GetToDoListAsync(2000));
        }

        [TestMethod]
        public void GetTask_Based_On_Different_ID_Values_Test()
        {
            Assert.AreEqual(null, database.GetTaskItem(-1).Result);
            Assert.AreEqual(null, database.GetTaskItem(2000).Result);
        }

        [TestMethod]
        public void SaveList_With_Different_List_Parameters() 
        {
            Assert.AreEqual(null, database.SaveListAsync((ToDoList)null));
            database.SaveListAsync((List<ToDoList>)null);
            Assert.AreEqual(0, database.GetToDoListAsync().Result.Count);

            List<TaskItem> items = new List<TaskItem> { new TaskItem("1", "2", 2), new TaskItem("2", "3", 4) };

            ToDoList item = new ToDoList ("Name", items);
            database.SaveListAsync(item);
            Assert.AreEqual(item.ID, database.GetToDoListAsync(item.ID).Result.ID);
            Assert.AreEqual(item.name, database.GetToDoListAsync(database.GetTaskItem().Result[0].ListID).Result.name);
            Assert.AreEqual(item.name, database.GetToDoListAsync(database.GetTaskItem().Result[1].ListID).Result.name);
            Assert.AreEqual(items[0].title, database.GetToDoListAsync(database.GetTaskItem().Result[0].ListID).Result.tasks[0].title);
            Assert.AreEqual(items[0].title, database.GetToDoListAsync().Result[0].tasks[0].title);
            Assert.AreEqual(2, database.GetToDoListAsync().Result[0].tasks.Count);

            item.name = "Billy";
            database.SaveListAsync(item);
            Assert.AreEqual(item.name, database.GetToDoListAsync(item.ID).Result.name);

            database.SaveListAsync(new List<ToDoList> { new ToDoList("Name", new List<TaskItem>()), new ToDoList("Name", new List<TaskItem>()) });
            Assert.AreEqual(3, database.GetToDoListAsync().Result.Count);
        }

        [TestMethod]
        public void SaveTask_With_Different_List_Parameters()
        {
            Assert.AreEqual(null, database.SaveItemAsync((TaskItem)null));
            database.SaveItemAsync((List<TaskItem>)null);
            Assert.AreEqual(2, database.GetTaskItem().Result.Count);

            TaskItem item = new TaskItem("1", "Desc", 1);
            database.SaveItemAsync(item).Wait();
            Assert.AreEqual(item.ID, database.GetTaskItem(item.ID).Result.ID);

            TaskItem billy = new TaskItem("Billy", "", 0);
            database.SaveItemAsync(billy).Wait();
            Assert.AreEqual(billy.title, database.GetTaskItem(billy.ID).Result.title);

            billy.title = "Steve";
            database.SaveItemAsync(billy).Wait();
            Assert.AreEqual(billy.title, database.GetTaskItem(billy.ID).Result.title);

            database.SaveItemAsync(new List<TaskItem> { new TaskItem("1", "Desc", 1), new TaskItem("1", "Desc", 1) });
            Assert.AreEqual(6, database.GetTaskItem().Result.Count);
        }

        [TestMethod]
        public void SaveUser_With_Different_List_Parameters()
        {
            Assert.AreEqual(null, database.SavePlayerAsync(null));

            PlayerProfile item = new PlayerProfile();
            database.SavePlayerAsync(item).Wait();
            Assert.AreEqual(item.ID, database.GetPlayerProfile(item.ID).Result.ID);

            item.Name = "Billy";
            database.SavePlayerAsync(item).Wait();
            Assert.AreEqual(item.Name, database.GetPlayerProfile(item.ID).Result.Name);
        }

        [TestMethod]
        public void DeleteList_With_Different_List_Parameters()
        {
            Assert.AreEqual(null, database.DeleteItemAsync((ToDoList)null));

            ToDoList item = new ToDoList("Name", new List<TaskItem>());
            Assert.AreEqual(0, database.DeleteItemAsync(item).Result);

            database.SaveListAsync(item);

            Assert.AreEqual(1, database.DeleteItemAsync(item).Result);
        }

        [TestMethod]
        public void DeleteTask_With_Different_List_Parameters()
        {
            Assert.AreEqual(null, database.DeleteItemAsync((TaskItem)null));

            TaskItem item = new TaskItem("Name", "Desc", 145);
            Assert.AreEqual(0, database.DeleteItemAsync(item).Result);

            database.SaveItemAsync(item).Wait();
            Assert.AreEqual(1, database.DeleteItemAsync(item).Result);
        }

        [TestMethod]
        public void ZZZZZZZLastTest()
        {
            database.DeleteDatabase();
        }
    }
}
