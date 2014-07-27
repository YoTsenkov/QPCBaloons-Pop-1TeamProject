namespace TestBalloonsPopsGame
{
    using System;
    using System.IO;
    using BalloonsPopsGame.UserInterface.Console;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class BalloonDrawingManagerTest
    {
        [TestMethod]
        public void TestBlueBalloonDrawingManagerTest()
        {
            var balloonDrawingManager = new BlueBalloonDrawingManager();
            var expectedOutput = "3 ";
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                balloonDrawingManager.Draw();
                Assert.AreEqual(expectedOutput, sw.ToString(), "BlueBallonDrawingManager doesn't print correct result!");
            }
        }

        [TestMethod]
        public void TestRedBalloonDrawingManagerTest()
        {
            var balloonDrawingManager = new RedBalloonDrawingManager();
            var expectedOutput = "1 ";
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                balloonDrawingManager.Draw();
                Assert.AreEqual(expectedOutput, sw.ToString(), "RedBallonDrawingManager doesn't print correct result!");
            }
        }

        [TestMethod]
        public void TestYellowBalloonDrawingManagerTest()
        {
            var balloonDrawingManager = new YellowBalloonDrawingManager();
            var expectedOutput = "4 ";
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                balloonDrawingManager.Draw();
                Assert.AreEqual(expectedOutput, sw.ToString(), "YellowBallonDrawingManager doesn't print correct result!");
            }
        }

        [TestMethod]
        public void TestGreenBalloonDrawingManagerTest()
        {
            var balloonDrawingManager = new GreenBalloonDrawingManager();
            var expectedOutput = "2 ";
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                balloonDrawingManager.Draw();
                Assert.AreEqual(expectedOutput, sw.ToString(), "GreenBallonDrawingManager doesn't print correct result!");
            }
        }

        [TestMethod]
        public void TestPoppedBalloonDrawingManagerTest()
        {
            var balloonDrawingManager = new PoppedBalloonDrawingManager();
            var expectedOutput = "- ";
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                balloonDrawingManager.Draw();
                Assert.AreEqual(expectedOutput, sw.ToString(), "PoppedBallonDrawingManager doesn't print correct result!");
            }
        }
    }
}
