namespace BalloonsPopsGame.UserInterface.Console
{
    using System;    

    public class GreenBalloonDrawingManager : BalloonDrawingManager
    {
        public override void Draw()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(2 + " ");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
