namespace BalloonsPopsGame.UserInterface.Console
{
    using System;    

    public class YellowBalloonDrawingManager : BalloonDrawingManager
    {
        public override void Draw()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(4 + " ");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
