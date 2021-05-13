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

            Assert.AreEqual("Name", item.name);
            Assert.AreEqual(list, item.tasks);

            ToDoList item2 = new ToDoList();

            Assert.AreEqual("", item2.name);
            Assert.AreEqual(null, item2.tasks);
        }

        public void List_Get_Method_Test() {
            ToDoList item = new ToDoList("Name", new List<TaskItem>());

            Assert.AreEqual("Name", item.name);
            Assert.IsNotNull(item.tasks);
        }
    }
}
