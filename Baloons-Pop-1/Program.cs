namespace BalloonsPopsGame
{
    using Balloons;
    using BalloonsPopsGame.UserInterface.Console;
    using System;
    using System.Collections.Generic;

    class Program
    {
        static void Main(string[] args)
        {
            var balloonsContainer = new BalloonsContainer(new BalloonFactory(), StandardRandomNumbersProvider.Instance);
            var scoreBoard = new ScoreBoard(new List<Tuple<string, int>>());
            var consoleUIHandler = new ConsoleUIHandler(balloonsContainer);
            Game game = new Game(balloonsContainer, scoreBoard, consoleUIHandler);
            game.Start();
        }
    }
}
