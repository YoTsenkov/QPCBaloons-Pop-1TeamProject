namespace BalloonsPopsGame.UserInterface.Console
{
    using System;    

    /// <summary>
    /// A class for drawing red balloons.
    /// </summary>
    public class RedBalloonDrawingManager : BalloonDrawingManager
    {
        /// <summary>
        /// Draws red balloon on the Console.
        /// </summary>
        public override void Draw()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(1 + " ");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
