namespace TestBalloonsPopsGame
{
    using System;
    using BalloonsPopsGame.Balloons;
    using BalloonsPopsGame.UserInterface.Console;    
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class BalloonDrawingManagerFactoryTest
    {
        [TestMethod]
        public void TestCreatingBlueBalloonDrawingManager()
        {
            var drawingManager = BalloonDrawingManagerFactory.GetBalloonDrawingManager(BalloonType.Blue);
            Assert.IsTrue(drawingManager is BlueBalloonDrawingManager, "BalloonDrawingManagerFactory doesn't return BlueBalloonDrawwingManager!");
        }

        [TestMethod]
        public void TestCreatingGreenBalloonDrawingManager()
        {
            var drawingManager = BalloonDrawingManagerFactory.GetBalloonDrawingManager(BalloonType.Green);
            Assert.IsTrue(drawingManager is GreenBalloonDrawingManager, "BalloonDrawingManagerFactory doesn't return GreenBalloonDrawwingManager!");
        }

        [TestMethod]
        public void TestCreatingRedBalloonDrawingManager()
        {
            var drawingManager = BalloonDrawingManagerFactory.GetBalloonDrawingManager(BalloonType.Red);
            Assert.IsTrue(drawingManager is RedBalloonDrawingManager, "BalloonDrawingManagerFactory doesn't return RedBalloonDrawwingManager!");
        }

        [TestMethod]
        public void TestCreatingYellowBalloonDrawingManager()
        {
            var drawingManager = BalloonDrawingManagerFactory.GetBalloonDrawingManager(BalloonType.Yellow);
            Assert.IsTrue(drawingManager is YellowBalloonDrawingManager, "BalloonDrawingManagerFactory doesn't return YellowBalloonDrawwingManager!");
        }

        [TestMethod]
        public void TestCreatingPoppedBalloonDrawingManager()
        {
            var drawingManager = BalloonDrawingManagerFactory.GetBalloonDrawingManager(BalloonType.Popped);
            Assert.IsTrue(drawingManager is PoppedBalloonDrawingManager, "BalloonDrawingManagerFactory doesn't return PoppedBalloonDrawwingManager!");
        }
    }
}
