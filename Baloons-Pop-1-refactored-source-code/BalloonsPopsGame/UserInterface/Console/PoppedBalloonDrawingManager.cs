namespace BalloonsPopsGame.UserInterface.Console
{
    using System;    

    /// <summary>
    /// A class for drawing popped balloons.
    /// </summary>
    public class PoppedBalloonDrawingManager : BalloonDrawingManager
    {
        /// <summary>
        /// Draws popped balloon on the Console.
        /// </summary>
        public override void Draw()
        {
            Console.Write('-' + " ");
        }
    }
}
