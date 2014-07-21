namespace BalloonsPopsGame
{
    using System;
    using System.Collections.Generic;

    public class BalloonFactory : IBalloonFactory
    {
        private static readonly RedBalloonCreator redBalloonCreator = new RedBalloonCreator();
        private static readonly BlueBalloonCreator blueBalloonCreator = new BlueBalloonCreator();
        private static readonly GreenBalloonCreator greenBalloonCreator = new GreenBalloonCreator();
        private static readonly YellowBalloonCreator yellowBalloonCreator = new YellowBalloonCreator();
        private static readonly PoppedBalloonCreator poppedBallonCreator = new PoppedBalloonCreator();

        private readonly Dictionary<BalloonType, Balloon> balloons = new Dictionary<BalloonType, Balloon>();

        public Balloon GetBalloon(BalloonType key)
        {
            Balloon balloon = null;
            if (this.balloons.ContainsKey(key))
            {
                balloon = this.balloons[key];
            }
            else
            {
                switch (key)
                {
                    case BalloonType.Red:
                        balloon = redBalloonCreator.CreateBalloon();
                        break;
                    case BalloonType.Green:
                        balloon = greenBalloonCreator.CreateBalloon();
                        break;
                    case BalloonType.Blue:
                        balloon = blueBalloonCreator.CreateBalloon();
                        break;
                    case BalloonType.Yellow:
                        balloon = yellowBalloonCreator.CreateBalloon();
                        break;
                    case BalloonType.Popped:
                        balloon = poppedBallonCreator.CreateBalloon();
                        break;
                    default:
                        throw new ArgumentException("No such balloon colour!");
                }

                this.balloons.Add(key, balloon);
            }

            return balloon;
        }
    }
}
