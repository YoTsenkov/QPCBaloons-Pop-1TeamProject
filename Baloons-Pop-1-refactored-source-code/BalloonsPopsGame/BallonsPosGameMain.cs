namespace BalloonsPopsGame
{
    using System;
    using Balloons;
    using BalloonsPopsGame.UserInterface.Console;
    using Score;

    public class BallonsPosGameMain
    {
        public static void Main(string[] args)
        {
            var balloonsContainer = new BalloonsContainer();
            var scoreBoard = new Scoreboard();
            var consoleUIHandler = new ConsoleUIHandler(balloonsContainer);
            Game game = new Game(balloonsContainer, scoreBoard, consoleUIHandler);
            game.Start();
        }
    }
}
