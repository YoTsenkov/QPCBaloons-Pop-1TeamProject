namespace BalloonsPopsGame.UserInterface.Console
{
    using System;
    using Balloons;

    public class YellowBalloonDrawingManager : BalloonDrawingManager
    {
        public override void Draw(Balloon balloon)
        {
            Console.ForegroundColor = ConsoleColorFactory.GetConsoleColor(balloon);
            Console.Write(4 + " ");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
