using OnboardingGame.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace OnboardingTests
{
    [TestClass]
    public class ToDoListTests
    {
        [TestMethod]
        public void List_Constructor_Test() {
            List<TaskItem> list = new List<TaskItem>();

            ToDoList item = new ToDoList("Name", list);

            Assert.AreEqual("Name", item.Name);
            Assert.AreEqual(list, item.TaskItem);

            ToDoList item2 = new ToDoList();

            Assert.AreEqual("", item2.Name);
            Assert.AreEqual(null, item2.TaskItem);
        }

        public void List_Get_Method_Test() {
            ToDoList item = new ToDoList("Name", new List<TaskItem>());

            Assert.AreEqual("Name", item.Name);
            Assert.IsNotNull(item.TaskItem);
        }
    }
}
