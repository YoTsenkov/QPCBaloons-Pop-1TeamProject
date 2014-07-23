namespace BalloonsPopsGame.UserInterface.Console
{
    using System;
    using Balloons;

    public class BlueBalloonDrawingManager : BalloonDrawingManager
    {
        public override void Draw(Balloon balloon)
        {
            Console.ForegroundColor = ConsoleColorFactory.GetConsoleColor(balloon);
            Console.Write(3 + " ");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
