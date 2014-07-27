namespace BalloonsPopsGame.UserInterface.Console
{
    using System;    

    /// <summary>
    /// A class for drawing green balloons.
    /// </summary>
    public class GreenBalloonDrawingManager : BalloonDrawingManager
    {
        /// <summary>
        /// Draws green balloon on the Console.
        /// </summary>
        public override void Draw()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(2 + " ");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
