namespace TestBalloonsPopsGame
{
    using System;
    using BalloonsPopsGame.Balloons;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class BalloonTest
    {
        [TestMethod]
        public void TestTypeProperty()
        {
            var balloon = new Balloon(BalloonType.Red);
            Assert.IsTrue(balloon.Type == BalloonType.Red, "Type property doesn't return the expected value!");
        }

        [TestMethod]
        public void TestEqualsMethod()
        {
            var firstBalloon = new Balloon(BalloonType.Red);
            var secondBalloon = new Balloon(BalloonType.Red);
            Assert.IsTrue(firstBalloon == secondBalloon, "Equals method doesn't work correct!");
        }

        [TestMethod]
        public void TestCloneMethod()
        {
            var firstBalloon = new Balloon(BalloonType.Red);
            var secondBalloon = firstBalloon.Clone();
            Assert.IsTrue(firstBalloon == secondBalloon && !Object.ReferenceEquals(firstBalloon, secondBalloon), "Clone method doesn't work correct!");
        }
    }
}
