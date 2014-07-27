namespace BalloonsPopsGame.Balloons
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A factory implementing the Flyweight pattern.
    /// </summary>
    public class BalloonFactory : IBalloonFactory
    {
        /// <summary>
        /// A dictionary where already created balloons are stored.
        /// </summary>
        private readonly IDictionary<BalloonType, Balloon> balloons = new Dictionary<BalloonType, Balloon>();

        /// <summary>
        /// Return a Balloon instance by given BalloonType.
        /// </summary>
        /// <param name="key">The given BalloonType.</param>
        /// <returns>The instance.</returns>
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
