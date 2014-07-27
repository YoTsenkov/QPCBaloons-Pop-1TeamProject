namespace BalloonsPopsGame.UserInterface.Console
{
    using System;
    using Balloons;

    public class PoppedBalloonDrawingManager : BalloonDrawingManager
    {
        public override void Draw(Balloon balloon)
        {
            Console.Write('-' + " ");
        }
    }
}
