namespace BalloonsPopsGame.UserInterface.Console
{
    using System;
    using Balloons;

    public class PoppedBalloonDrawingManager : BalloonDrawingManager
    {
        public override void Draw()
        {
            Console.Write('-' + " ");
        }
    }
}
