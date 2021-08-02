using OnboardingGame.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.Text;
using OnboardingGame.REST_Data;
using OnboardingGame.Models;

namespace OnboardingTests
{
    [TestClass]
    public class RESTConnectionTests
    {
        static string value = "https://onboarding_xperience.phoniro.se";
        RestConnection connection = new RestConnection(value);

        [TestMethod]
        public void Constructor_Test() {
            FieldInfo info = connection.GetType().GetField("baseUri", BindingFlags.NonPublic | BindingFlags.Instance);

            Assert.AreEqual(value, (string)info.GetValue(connection));
        }

        [TestMethod]
        public void Validate_User_Test() {
            Assert.IsFalse(connection.ValidateUserAsync("", "").Result);
            Assert.IsTrue(connection.ValidateUserAsync("test", "test").Result);
        }

        [TestMethod]
        public void GETData_Test() {
            Assert.IsNull(connection.GetDataAsync("", "").Result);
            Assert.IsNotNull(connection.GetDataAsync("test", "test").Result);
        }

        [TestMethod]
        public void PUTData_Test() {
            Assert.IsFalse(connection.UpdateStatusAsync("", "", null).Result);

            List<JSON_Data> data = connection.GetDataAsync("test", "test").Result;

            Task task = data[0].tasks[0];

            task.status = "Active";

            Assert.IsTrue(connection.UpdateStatusAsync("test", "test", task).Result);

            data = connection.GetDataAsync("test", "test").Result;
            Assert.AreEqual(task.status, data[0].tasks[0].status);

            task.status = "ToDo";
            connection.UpdateStatusAsync("test", "test", task).Wait();
        }
    }
}
