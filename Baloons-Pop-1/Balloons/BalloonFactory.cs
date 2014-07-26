namespace BalloonsPopsGame.Balloons
{
    using System;
    using System.Collections.Generic;

    public class BalloonFactory : IBalloonFactory
    {
        private static readonly Balloon BalloonInstance = new Balloon();
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
                balloon = BalloonInstance.Clone();
                switch (key)
                {
                    case BalloonType.Red:
                        balloon.Type = BalloonType.Red;
                        break;
                    case BalloonType.Green:
                        balloon.Type = BalloonType.Green;
                        break;
                    case BalloonType.Blue:
                        balloon.Type = BalloonType.Blue;
                        break;
                    case BalloonType.Yellow:
                        balloon.Type = BalloonType.Yellow;
                        break;
                    case BalloonType.Popped:
                        balloon.Type = BalloonType.Popped;
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
