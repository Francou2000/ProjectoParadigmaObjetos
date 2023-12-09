using MyGame;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CheckScore()
        {
            int snakeScore = GameManager.Instance.LevelController.Player.snakeScore;
            var playerScore = snakeScore;

            int actualScore = GameManager.Instance.Score;
            var gameManagerScore = actualScore;

            Assert.AreEqual(playerScore, gameManagerScore);
        }

        [TestMethod]
        public void CheckBulletPoolSize()
        {
            var bulletPool = GameManager.Instance.LevelController.Player.bulletsPool.itemsAvailable;

            int poolSize = 1;
            var size = poolSize;

            Assert.AreEqual(size, bulletPool.Count);
        }
        
        [TestMethod]
        public void CheckBulletVelocity()
        {
            int bulletVelocity = GameManager.Instance.LevelController.Player.bulletSpeed;
            var bulletInicialVelocity = bulletVelocity;

            int bulletSetVelocity = 300;
            var expectedVelocity = bulletSetVelocity;

            Assert.AreEqual(bulletSetVelocity, bulletInicialVelocity);
        }
    }
}
