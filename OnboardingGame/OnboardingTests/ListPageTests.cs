using OnboardingGame.Pages;
using OnboardingGame.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Reflection;
using System;
using Xamarin.Forms.Internals;
using Xamarin.Forms;
using FakeItEasy;

using OnboardingGame;

namespace OnboardingTests
{
    [TestClass]
    public class ListPageTests
    {
        [TestMethod]
        public void ListPage_Constructor_Test() {
            List<TaskItem> list = new List<TaskItem>();

            ToDoList item = new ToDoList("Billy", list);

            TasksPage page = new TasksPage(item);

            FieldInfo type = typeof(TasksPage).GetField("name", BindingFlags.NonPublic | BindingFlags.Instance);

            Assert.AreEqual("Billy", (string)type.GetValue(page));

            type = typeof(TasksPage).GetField("list", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.AreEqual(list, type.GetValue(page));
        }
    }
}
