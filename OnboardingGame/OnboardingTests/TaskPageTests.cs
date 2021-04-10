using OnboardingGame.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnboardingGame.Models;
using FakeItEasy;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace OnboardingTests
{
    [TestClass]
    public class TaskPageTests
    {
        public TaskPageTests() {
            var platformServicesFake = A.Fake<IPlatformServices>();
            Device.PlatformServices = platformServicesFake;
        }

        [TestMethod]
        public void TaskPage_Constructor_Test() {
            TaskItem item = new TaskItem("Billy", "Desc", 0);

            TaskContent page = new TaskContent(item);

            Assert.AreEqual(item, page.task);
        }
    }
}
