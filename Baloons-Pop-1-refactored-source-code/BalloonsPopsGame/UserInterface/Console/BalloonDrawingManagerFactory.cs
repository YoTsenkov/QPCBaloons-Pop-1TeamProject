namespace BalloonsPopsGame.UserInterface.Console
{
    using System;
    using Balloons;

    /// <summary>
    /// Simple Factory for creating balloons by given <see cref="BallonType"/>.
    /// </summary>
    public class BalloonDrawingManagerFactory
    {
        /// <summary>
        /// Returns concrete implementation of the <see cref="BalloonDrawingManager"/> class by given <see cref="BalloonType"/>.
        /// </summary>
        /// <param name="balloonType">The type of the balloon.</param>
        /// <returns>A implementation of BalloonDrawingManager.</returns>
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
