namespace BalloonsPopsGame
{
    using System;
    using Balloons;
    using BalloonsPopsGame.UserInterface.Console;

    class Program
    {
        static void Main(string[] args)
        {
            var balloonsContainer = new BalloonsContainer(new BalloonFactory(), new StandardRandomNumberProvider());
            var scoreBoard = new ScoreBoard();
            var consoleUIHandler = new ConsoleUIHandler(balloonsContainer);
            Game game = new Game(balloonsContainer, scoreBoard, consoleUIHandler);
            game.Start();
        }
    }
}
