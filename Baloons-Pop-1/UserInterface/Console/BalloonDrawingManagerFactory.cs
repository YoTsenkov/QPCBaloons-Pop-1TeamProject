namespace BalloonsPopsGame.UserInterface.Console
{
    using Balloons;

    public class BalloonDrawingManagerFactory
    {
        public static BalloonDrawingManager GetBalloonDrawingManager(Balloon balloon)
        {
            BalloonDrawingManager result;
            if (balloon is RedBalloon)
            {
                result = new RedBalloonDrawingManager();
            }
            else if (balloon is BlueBalloon)
            {
                result = new BlueBalloonDrawingManager();
            }
            else if (balloon is GreenBalloon)
            {
                result = new GreenBalloonDrawingManager();
            }
            else if (balloon is YellowBalloon)
            {
                result = new YellowBalloonDrawingManager();
            }
            else
            {
                result = new PoppedBalloonDrawingManager();
            }

            return result;
        }
    }
}
