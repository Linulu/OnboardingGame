using OnboardingGame.Data;
using OnboardingGame.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;


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
        public void GetList_Based_On_Different_ID_Values_Test() {
            ToDoList item = new ToDoList();
            item.ID = 100;
            database.SaveListAsync(item);

            Assert.AreEqual(null, database.GetToDoListAsync(-1).Result);
            Assert.AreEqual(null, database.GetToDoListAsync(20).Result);
            Assert.AreEqual(item, database.GetToDoListAsync(100).Result);
        }

        [TestMethod]
        public void GetTask_Based_On_Different_ID_Values_Test()
        {
            Assert.AreEqual(null, database.GetTaskItem(-1));
            Assert.AreEqual(null, database.GetTaskItem(20));
        }

        [TestMethod]
        public void SaveList_With_Different_List_Parameters() {
            
        }

        [TestMethod]
        public void SaveTask_With_Different_List_Parameters()
        {

        }

        [TestMethod]
        public void SaveUser_With_Different_List_Parameters()
        {

        }

        [TestMethod]
        public void DeleteList_With_Different_List_Parameters()
        {

        }

        [TestMethod]
        public void DeleteTask_With_Different_List_Parameters()
        {

        }
    }
}
