namespace BalloonsPopsGame.UserInterface.Console
{
    using System;    

    /// <summary>
    /// A class for drawing blue balloons.
    /// </summary>
    public class BlueBalloonDrawingManager : BalloonDrawingManager
    {
        /// <summary>
        /// Draws blue balloon on the Console.
        /// </summary>
        public override void Draw()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(3 + " ");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
