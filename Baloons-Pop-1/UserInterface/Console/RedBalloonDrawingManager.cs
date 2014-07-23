namespace BalloonsPopsGame.UserInterface.Console
{
    using System;
    using Balloons;

    public class RedBalloonDrawingManager : BalloonDrawingManager
    {
        public override void Draw(Balloon balloon)
        {
            Console.ForegroundColor = ConsoleColorFactory.GetConsoleColor(balloon);
            Console.Write(1 + " ");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
