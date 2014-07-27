namespace TestBalloonsPopsGame
{
    using System;
    using BalloonsPopsGame.Balloons;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class BalloonFactoryTest
    {
        private BalloonFactory factory;

        [TestInitialize]
        public void CreateFactory()
        {
            this.factory = new BalloonFactory();
        }

        [TestMethod]
        public void TestCreatingRedBalloon()
        {
            var redBalloon = this.factory.GetBalloon(BalloonType.Red);
            Assert.IsTrue(redBalloon.Type == BalloonType.Red, "Creation of red balloons doesn't work!");
        }

        [TestMethod]
        public void TestCreatingGreenBalloon()
        {
            var greenBalloon = this.factory.GetBalloon(BalloonType.Green);
            Assert.IsTrue(greenBalloon.Type == BalloonType.Green, "Creation of green balloons doesn't work!");
        }

        [TestMethod]
        public void TestCreatingYellowBalloon()
        {
            var yellowBalloon = this.factory.GetBalloon(BalloonType.Yellow);
            Assert.IsTrue(yellowBalloon.Type == BalloonType.Yellow, "Creation of yellow balloons doesn't work!");
        }

        [TestMethod]
        public void TestCreatingBlueBalloon()
        {
            var blueBalloon = this.factory.GetBalloon(BalloonType.Blue);
            Assert.IsTrue(blueBalloon.Type == BalloonType.Blue, "Creation of blue balloons doesn't work!");
        }

        [TestMethod]
        public void TestCreatingPoppedBalloon()
        {
            var poppedBalloon = this.factory.GetBalloon(BalloonType.Popped);
            Assert.IsTrue(poppedBalloon.Type == BalloonType.Popped, "Creation of popped balloons doesn't work!");
        }

        [TestMethod]
        public void TestFlyweightImplementation()
        {
            var firstBalloon = this.factory.GetBalloon(BalloonType.Red);
            var secondBalloon = this.factory.GetBalloon(BalloonType.Red);
            Assert.IsTrue(object.ReferenceEquals(firstBalloon, secondBalloon), "Flyweight implementation doesn't work!");
        }
    }
}
