namespace TestBalloonsPopsGame
{
    using BalloonsPopsGame;
    using BalloonsPopsGame.Balloons;
    using BalloonsPopsGame.Exceptions;
    using BalloonsPopsGame.Score;
    using BalloonsPopsGame.UserInterface;
    using BalloonsPopsGame.UserInterface.Console;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using Telerik.JustMock;

    [TestClass]
    public class GameTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestSettingInvalidContainer()
        {
            var container = new BalloonsContainer();
            var game = new Game(null, new Scoreboard(), new ConsoleUIHandler(container));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestSettingInvalidScoreboard()
        {
            var container = new BalloonsContainer();
            var game = new Game(container, null, new ConsoleUIHandler(container));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestsettingInvalidUIHandler()
        {
            var container = new BalloonsContainer();
            var game = new Game(container, new Scoreboard(), null);
        }       

        [TestMethod]
        public void TestExecuteExitCommand()
        {
            var balloonsContainer = Mock.Create<IBalloonsContainer>();
            var scoreBoard = Mock.Create<IScoreboard>();
            var uiHandler = Mock.Create<UIHandler>();
            var game = new Game(balloonsContainer, scoreBoard, uiHandler);
            Mock.Arrange(() => uiHandler.DisplayMessage(UIMessages.GoodBye)).OccursOnce();
            game.ExecuteCommand(UIMessages.Exit);

            Mock.Assert(uiHandler);
            Assert.IsTrue(game.IsGameOver, "Exit command doesn't finish the game!");
        }

        [TestMethod]
        public void TestExecuteRestartCommand()
        {
            var balloonsContainer = Mock.Create<IBalloonsContainer>();
            var scoreBoard = Mock.Create<IScoreboard>();
            var uiHandler = Mock.Create<UIHandler>();
            var game = new Game(balloonsContainer, scoreBoard, uiHandler);
            Mock.Arrange(() => balloonsContainer.Empty()).InOrder();
            Mock.Arrange(() => balloonsContainer.Fill()).InOrder();
            Mock.Arrange(() => uiHandler.DisplayMessage(UIMessages.EnterRowAndColumn)).OccursOnce();
            game.ExecuteCommand(UIMessages.Restart);

            Mock.Assert(balloonsContainer);
            Mock.Assert(uiHandler);
            Assert.AreEqual(0, game.NumberOfTurn, "Restarting doesn't make NumberOfTurn to zero!");
        }

        [TestMethod]
        public void TestExecuteTopCommand()
        {
            var balloonsContainer = Mock.Create<IBalloonsContainer>();
            var scoreBoard = Mock.Create<IScoreboard>();
            var uiHandler = Mock.Create<UIHandler>();
            var game = new Game(balloonsContainer, scoreBoard, uiHandler);
            Mock.Arrange(() => uiHandler.DisplayScoreboard(scoreBoard)).OccursOnce();

            game.ExecuteCommand(UIMessages.Top);
            Mock.Assert(uiHandler);
        }

        [TestMethod]
        public void TestPerformBalloonsPoppingWithInvalidLengthCommand()
        {
            var balloonsContainer = Mock.Create<IBalloonsContainer>();
            var scoreBoard = Mock.Create<IScoreboard>();
            var uiHandler = Mock.Create<UIHandler>();
            var game = new Game(balloonsContainer, scoreBoard, uiHandler);
            Mock.Arrange(() => uiHandler.DisplayMessage(UIMessages.InvalidMove)).OccursOnce();

            game.PerformBalloonsPopping("12 3 2");
            Mock.Assert(uiHandler);
        }

        [TestMethod]
        public void TestPerformBalloonsPoppingWithInvalidRowAndColumnCommand()
        {
            var balloonsContainer = Mock.Create<IBalloonsContainer>();
            var scoreBoard = Mock.Create<IScoreboard>();
            var uiHandler = Mock.Create<UIHandler>();
            var game = new Game(balloonsContainer, scoreBoard, uiHandler);
            Mock.Arrange(() => uiHandler.DisplayMessage(UIMessages.InvalidMove)).OccursOnce();

            game.PerformBalloonsPopping("ala bala");
            Mock.Assert(uiHandler);
        }

        [TestMethod]
        public void TestPerformBalloonsPoppingValidCommand()
        {
            var balloonsContainer = Mock.Create<IBalloonsContainer>();
            var scoreBoard = Mock.Create<IScoreboard>();
            var uiHandler = Mock.Create<UIHandler>();
            var game = new Game(balloonsContainer, scoreBoard, uiHandler);
            Mock.Arrange(() => balloonsContainer.IsEmpty()).Returns(false);
            Mock.Arrange(() => balloonsContainer.PopBaloons(Arg.IsAny<int>(), Arg.IsAny<int>())).OccursOnce();
            Mock.Arrange(() => uiHandler.DisplayMessage(UIMessages.EnterRowAndColumn)).OccursOnce();
            var oldNumberOfTurn = game.NumberOfTurn;

            game.PerformBalloonsPopping("2 3");
            Mock.Assert(balloonsContainer);
            Mock.Assert(uiHandler);
            Assert.AreEqual(oldNumberOfTurn + 1, game.NumberOfTurn, "Balloons popping doesn't increase the player number of turn!");
        }

        [TestMethod]
        public void TestPerformBallo0nsPoppingWithOutOfRangeRowAndColumn()
        {
            var balloonsContainer = Mock.Create<IBalloonsContainer>();
            var scoreBoard = Mock.Create<IScoreboard>();
            var uiHandler = Mock.Create<UIHandler>();
            var game = new Game(balloonsContainer, scoreBoard, uiHandler);
            Mock.Arrange(() => balloonsContainer.PopBaloons(Arg.IsAny<int>(), Arg.IsAny<int>())).Throws(new InvalidRowOrColumnException());
            Mock.Arrange(() => uiHandler.DisplayMessage(UIMessages.InvalidMove)).OccursOnce();

            game.PerformBalloonsPopping("5 3");
            Mock.Assert(uiHandler);
        }

        [TestMethod]
        public void TestPerformBalloonsPoppingWithMissingBalloon()
        {
            var balloonsContainer = Mock.Create<IBalloonsContainer>();
            var scoreBoard = Mock.Create<IScoreboard>();
            var uiHandler = Mock.Create<UIHandler>();
            var game = new Game(balloonsContainer, scoreBoard, uiHandler);
            Mock.Arrange(() => balloonsContainer.PopBaloons(Arg.IsAny<int>(), Arg.IsAny<int>())).Throws(new MissingBalloonException());
            Mock.Arrange(() => uiHandler.DisplayMessage(UIMessages.MissingBalloon)).OccursOnce();

            game.PerformBalloonsPopping("5 3");
            Mock.Assert(uiHandler);
        }

        [TestMethod]
        public void TestPerformBalloonsPoppingWithRestart()
        {
            var balloonsContainer = Mock.Create<IBalloonsContainer>();
            var scoreBoard = Mock.Create<IScoreboard>();
            var uiHandler = Mock.Create<UIHandler>();
            var game = new Game(balloonsContainer, scoreBoard, uiHandler);
            Mock.Arrange(() => balloonsContainer.PopBaloons(Arg.IsAny<int>(), Arg.IsAny<int>())).DoNothing();
            Mock.Arrange(() => balloonsContainer.IsEmpty()).Returns(true);
            Mock.Arrange(() => uiHandler.DisplayMessage(UIMessages.PoppedAllBaloons, Arg.IsAny<int>())).InOrder();
            Mock.Arrange(() => uiHandler.DisplayMessage(UIMessages.AskForName)).InOrder();
            Mock.Arrange(() => scoreBoard.Update(Arg.IsAny<string>(), Arg.IsAny<int>())).OccursOnce();
            Mock.Arrange(() => balloonsContainer.Empty()).InOrder();
            Mock.Arrange(() => balloonsContainer.Fill()).InOrder();
            Mock.Arrange(() => uiHandler.DisplayMessage(UIMessages.EnterRowAndColumn)).InOrder();

            game.PerformBalloonsPopping("2 2");
            Mock.Assert(balloonsContainer);
            Mock.Assert(uiHandler);
            Mock.Assert(scoreBoard);
            Assert.AreEqual(0, game.NumberOfTurn, "Restarting doesn't make NumberOfTurn to zero!");
        }
    }
}
