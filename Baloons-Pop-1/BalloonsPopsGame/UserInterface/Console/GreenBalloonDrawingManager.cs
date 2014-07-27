namespace BalloonsPopsGame.UserInterface.Console
{
    using System;
    using Balloons;

    public class GreenBalloonDrawingManager : BalloonDrawingManager
    {
        public override void Draw(Balloon balloon)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(2 + " ");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
