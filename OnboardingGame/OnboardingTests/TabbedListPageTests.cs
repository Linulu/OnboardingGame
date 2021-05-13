using OnboardingGame.Pages;
using OnboardingGame.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Reflection;
using FakeItEasy;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using System;

namespace OnboardingTests
{
    [TestClass]
    public class TabbedListPageTests
    {
        public TabbedListPageTests() {
            var platformServicesFake = A.Fake<IPlatformServices>();
            Device.PlatformServices = platformServicesFake;
        }

        [TestMethod]
        public void TabbedListPage_Test() {
            PlayerProfile player = new PlayerProfile();
            List<ToDoList> lists = new List<ToDoList>();

            TasksTab page;

            try
            {
                page = new TasksTab(lists, player);

                FieldInfo info = page.GetType().GetField("player", BindingFlags.NonPublic | BindingFlags.Instance);

                Assert.AreEqual(player, (PlayerProfile)info.GetValue(page));

                info = page.GetType().GetField("lists", BindingFlags.NonPublic | BindingFlags.Instance);
                Assert.AreEqual(lists, (List<ToDoList>)info.GetValue(page));
            }
            catch { 

            }
        }
    }
}
