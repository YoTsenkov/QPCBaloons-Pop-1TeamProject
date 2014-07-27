namespace BalloonsPopsGame.UserInterface.Console
{
    using System;    

    /// <summary>
    /// A class for drawing yellow balloons.
    /// </summary>
    public class YellowBalloonDrawingManager : BalloonDrawingManager
    {
        /// <summary>
        /// Draws yellow balloon on the console.
        /// </summary>
        public override void Draw()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(4 + " ");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
