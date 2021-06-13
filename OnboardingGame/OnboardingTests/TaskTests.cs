using OnboardingGame.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OnboardingTests
{
    [TestClass]
    public class TaskTests
    {

        [TestMethod]
        public void Task_Constructor_Test() {
            TaskItem item = new TaskItem("Name", "Description", 15);

            Assert.AreEqual("Description", item.description);
            Assert.AreEqual("Name", item.title);
            Assert.AreEqual(15, item.points);
            Assert.AreEqual(-1, item.status);

            TaskItem item2 = new TaskItem();

            Assert.AreEqual("", item2.description);
            Assert.AreEqual("", item2.title);
            Assert.AreEqual(0, item2.points);
            Assert.AreEqual(-1, item2.status);
        }

        [TestMethod]
        public void Get_Method_Tests() {
            TaskItem item = new TaskItem("Billy", "Is a child", 9);

            Assert.AreEqual("Is a child", item.description);
            Assert.AreEqual("Billy", item.title);
            Assert.AreEqual(9, item.points);
            Assert.AreEqual(-1, item.status);
        }

        [TestMethod]
        public void UpdateStatus_Test() {
            TaskItem item = new TaskItem("Name", "Desc", 9);

            item.UpdateStatus(0);
            Assert.AreEqual(0, item.status);

            item.UpdateStatus(-564165161);
            Assert.AreEqual(-1, item.status);

            item.UpdateStatus(651651);
            Assert.AreEqual(1, item.status);
        }
    }
}
