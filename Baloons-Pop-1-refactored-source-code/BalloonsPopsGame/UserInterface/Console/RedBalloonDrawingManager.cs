namespace BalloonsPopsGame.UserInterface.Console
{
    using System;    

    public class RedBalloonDrawingManager : BalloonDrawingManager
    {
        public override void Draw()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(1 + " ");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
