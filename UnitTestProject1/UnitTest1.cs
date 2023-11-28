using MyGame;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var snakeScore = GameManager.Instance.LevelController.Player.snakeScore;
            var actualScore = GameManager.Instance.Score;

            Assert.AreEqual(snakeScore, actualScore);
        }
        [TestMethod]
        public void TestMethod2()
        {
            var bulletPool = GameManager.Instance.LevelController.Player.bulletsPool.itemsAvailable;
            var poolSize = 1;

            Assert.AreEqual(poolSize, bulletPool.Count);
        }
        [TestMethod]
        public void TestMethod3()
        {

        }
    }
}
