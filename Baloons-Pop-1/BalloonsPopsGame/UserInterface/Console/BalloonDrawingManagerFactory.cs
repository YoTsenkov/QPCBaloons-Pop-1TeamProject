namespace BalloonsPopsGame.UserInterface.Console
{
    using System;
    using Balloons;

    public class BalloonDrawingManagerFactory
    {
        public static BalloonDrawingManager GetBalloonDrawingManager(BalloonType balloonType)
        {
            BalloonDrawingManager result;

            switch (balloonType)
            {
                case BalloonType.Red:
                    result = new RedBalloonDrawingManager();
                    break;
                case BalloonType.Green:
                    result = new GreenBalloonDrawingManager();
                    break;
                case BalloonType.Blue:
                    result = new BlueBalloonDrawingManager();
                    break;
                case BalloonType.Yellow:
                    result = new YellowBalloonDrawingManager();
                    break;
                case BalloonType.Popped:
                    result = new PoppedBalloonDrawingManager();
                    break;
                default:
                    throw new ArgumentException("No such BalloonDrawingManager");
            }

            return result;
        }
    }
}
