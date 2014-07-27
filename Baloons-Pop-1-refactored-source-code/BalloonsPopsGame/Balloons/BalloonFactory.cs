namespace BalloonsPopsGame.Balloons
{
    using System;
    using System.Collections.Generic;

    public class BalloonFactory : IBalloonFactory
    {
        private readonly IDictionary<BalloonType, Balloon> balloons = new Dictionary<BalloonType, Balloon>();

        public Balloon GetBalloon(BalloonType key)
        {
            Balloon balloon = null;
            if (this.balloons.ContainsKey(key))
            {
                balloon = this.balloons[key];
            }
            else
            {
                balloon = null;
                switch (key)
                {
                    case BalloonType.Red:
                        balloon = new Balloon(BalloonType.Red);
                        break;
                    case BalloonType.Green:
                        balloon = new Balloon(BalloonType.Green);
                        break;
                    case BalloonType.Blue:
                        balloon = new Balloon(BalloonType.Blue);
                        break;
                    case BalloonType.Yellow:
                        balloon = new Balloon(BalloonType.Yellow);
                        break;
                    case BalloonType.Popped:
                        balloon = new Balloon(BalloonType.Popped);
                        break;
                    default:
                        throw new ArgumentException("No such balloon color!");
                }

                this.balloons.Add(key, balloon);
            }

            return balloon;
        }
    }
}
