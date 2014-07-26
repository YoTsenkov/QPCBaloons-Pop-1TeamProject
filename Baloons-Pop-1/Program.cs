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
            Balloon[,] one = new Balloon[,]
            {
                {new RedBalloon(), new BlueBalloon()},
            };

            Balloon[,] two = (Balloon[,])one.Clone();
            two[0, 0] = new YellowBalloon();

            for (int i = 0; i < one.GetLength(0); i++)
            {
                for (int j = 0; j < one.GetLength(1); j++)
                {
                    Console.WriteLine(two[i, j]);
                }
            }

            var balloonsContainer = new BalloonsContainer(new BalloonFactory(), StandardRandomNumbersProvider.Instance);
            var scoreBoard = new ScoreBoard(new List<Tuple<string, int>>());
            var consoleUIHandler = new ConsoleUIHandler(balloonsContainer);
            Game game = new Game(balloonsContainer, scoreBoard, consoleUIHandler);
            game.Start();
        }
    }
}
