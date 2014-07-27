namespace TestBalloonsPopsGame
{
    using System;
    using System.IO;
    using BalloonsPopsGame.Balloons;
    using BalloonsPopsGame.RandomProvider;
    using BalloonsPopsGame.Score;
    using BalloonsPopsGame.UserInterface;
    using BalloonsPopsGame.UserInterface.Console;
    using Microsoft.VisualStudio.TestTools.UnitTesting;    
    using Telerik.JustMock;       

    [TestClass]
    public class ConsoleUIHandlerTest
    {
        [TestMethod]
        public void TestReadInput()
        {
            var container = Mock.Create<IBalloonsContainer>();
            var consoleUIHandler = new ConsoleUIHandler(container);
            var input = "Ivan";
            using (StringReader sr = new StringReader(input))
            {
                Console.SetIn(sr);
                var result = consoleUIHandler.ReadInput();
                Assert.AreEqual(input, result, "ReadInput method doesn't work!");
            }
        }

        [TestMethod]
        public void TestDisplayMessageWithNoPlaceholders()
        {
            var container = Mock.Create<IBalloonsContainer>();
            var consoleUIHandler = new ConsoleUIHandler(container);
            string message = UIMessages.GoodBye;
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                consoleUIHandler.DisplayMessage(message);
                Assert.AreEqual(message + "\r\n", sw.ToString(), "Display message doesn't work!");
            }
        }

        [TestMethod]
        public void TestDisplayMessageWithOnePlaceholder()
        {
            var container = Mock.Create<IBalloonsContainer>();
            var consoleUIHandler = new ConsoleUIHandler(container);
            string message = UIMessages.PoppedAllBaloons;
            int moves = 5;
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                consoleUIHandler.DisplayMessage(message, moves);
                Assert.AreEqual(string.Format(message, moves) + "\r\n", sw.ToString(), "DisplayMessage with one placeholder doesn't work!");
            }
        }

        [TestMethod]
        public void TestDisplayMessageWithTwoPlaceholders()
        {
            var container = Mock.Create<IBalloonsContainer>();
            var consoleUIHandler = new ConsoleUIHandler(container);
            string message = UIMessages.PlayerMoves;
            string playerName = "ivan";
            int moves = 5;
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                consoleUIHandler.DisplayMessage(message, playerName, moves);
                Assert.AreEqual(string.Format(message, playerName, moves) + "\r\n", sw.ToString(), "DisplayMessage with two placeholders doesn't work!");
            }
        }

        [TestMethod]
        public void TestDisplayScoreboard()
        {
            var container = Mock.Create<IBalloonsContainer>();
            var consoleUIHandler = new ConsoleUIHandler(container);
            var scoreboard = new Scoreboard();
            scoreboard.Update("pesho", 10);
            scoreboard.Update("ivan", 12);
            scoreboard.Update("gosho", 3);
            string expectedResult = UIMessages.Scoreboard + "\r\n";
            for (int i = 0; i < scoreboard.Players.Count; i++)
            {
                expectedResult += string.Format(UIMessages.PlayerMoves, scoreboard.Players[i].Item1, scoreboard.Players[i].Item2) + "\r\n";
            }

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                consoleUIHandler.DisplayScoreboard(scoreboard);
                Assert.AreEqual(expectedResult, sw.ToString(), "DisplayScoreBoard doesn't work!");
            }
        }

        [TestMethod]
        public void TestDisplayBalloons()
        { 
            var randomProvider = Mock.Create<IRandomNumbersProvider>();
            Mock.Arrange(() => randomProvider.GetRandomNumber(Arg.IsAny<int>(), Arg.IsAny<int>())).Returns(0);
            var container = new BalloonsContainer(new BalloonFactory(), randomProvider);
            container.Fill();
            var consoleUIHandler = new ConsoleUIHandler(container);

            string[] expectedOutputArr = 
            {
                "\r\n",
                "    0 1 2 3 4 5 6 7 8 9\r\n",
                "    --------------------\r\n",
                "0 | 1 1 1 1 1 1 1 1 1 1 | \r\n",
                "1 | 1 1 1 1 1 1 1 1 1 1 | \r\n",
                "2 | 1 1 1 1 1 1 1 1 1 1 | \r\n",
                "3 | 1 1 1 1 1 1 1 1 1 1 | \r\n",
                "4 | 1 1 1 1 1 1 1 1 1 1 | \r\n",
                "    --------------------\r\n",
                "\r\n" 
            };

            string expectedOutput = string.Empty;
            for (int i = 0; i < expectedOutputArr.Length; i++)
            {
                expectedOutput += expectedOutputArr[i];
            }

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                consoleUIHandler.DisplayBalloons();
                Assert.AreEqual(expectedOutput, sw.ToString(), "DisplayBalloons with two placeholders doesn't work!");
            }
        }
    }
}
