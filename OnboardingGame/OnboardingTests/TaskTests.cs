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

            Assert.AreEqual("Description", item.Description);
            Assert.AreEqual("Name", item.Title);
            Assert.AreEqual(15, item.Points);
            Assert.AreEqual(-1, item.Status);

            TaskItem item2 = new TaskItem();

            Assert.AreEqual("", item2.Description);
            Assert.AreEqual("", item2.Title);
            Assert.AreEqual(0, item2.Points);
            Assert.AreEqual(-1, item2.Status);
        }

        [TestMethod]
        public void Get_Method_Tests() {
            TaskItem item = new TaskItem("Billy", "Is a child", 9);

            Assert.AreEqual("Is a child", item.Description);
            Assert.AreEqual("Billy", item.Title);
            Assert.AreEqual(9, item.Points);
            Assert.AreEqual(-1, item.Status);
        }

        [TestMethod]
        public void UpdateStatus_Test() {
            TaskItem item = new TaskItem("Name", "Desc", 9);

            item.UpdateStatus(0);
            Assert.AreEqual(0, item.Status);

            item.UpdateStatus(-564165161);
            Assert.AreEqual(-1, item.Status);

            item.UpdateStatus(651651);
            Assert.AreEqual(1, item.Status);
        }
    }
}
