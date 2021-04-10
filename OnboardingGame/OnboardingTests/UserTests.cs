using OnboardingGame.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OnboardingTests
{
    [TestClass]
    public class UserTests
    {
        [TestMethod]
        public void User_Constructor_Test() {
            PlayerProfile player = new PlayerProfile();
            Assert.AreEqual(0, player.EXP);
        }

        [TestMethod]
        public void AddPoints_Test() {
            PlayerProfile player = new PlayerProfile();
            player.AddPoints(-4494);

            Assert.AreEqual(0, player.EXP);

            player.AddPoints(5);
            Assert.AreEqual(5, player.EXP);
        }
    }
}
