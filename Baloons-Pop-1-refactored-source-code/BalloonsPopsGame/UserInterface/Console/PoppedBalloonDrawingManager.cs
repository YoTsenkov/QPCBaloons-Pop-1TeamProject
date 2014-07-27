namespace BalloonsPopsGame.UserInterface.Console
{
    using System;    

    public class PoppedBalloonDrawingManager : BalloonDrawingManager
    {
        public override void Draw()
        {
            Console.Write('-' + " ");
        }
    }
}
