namespace BalloonsPopsGame
{
    using System;
    using Balloons;
    using BalloonsPopsGame.UserInterface.Console;
    using Score;

    /// <summary>
    /// The class contains the Main method and the entry point
    /// of the program.
    /// </summary>
    public class BallonsPosGameMain
    {
        /// <summary>
        /// The entry point of the program.
        /// </summary>       
        public static void Main()
        {
            var balloonsContainer = new BalloonsContainer();
            var scoreBoard = new Scoreboard();
            var consoleUIHandler = new ConsoleUIHandler(balloonsContainer);
            Game game = new Game(balloonsContainer, scoreBoard, consoleUIHandler);
            game.Start();
        }
    }
}
