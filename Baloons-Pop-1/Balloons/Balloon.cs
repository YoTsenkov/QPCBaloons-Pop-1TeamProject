namespace BalloonsPopsGame.Balloons
{
    public abstract class Balloon
    {
        public static bool operator ==(Balloon firstBalloon, Balloon secondBalloon)
        {
            return Balloon.Equals(firstBalloon, secondBalloon);
        }

        public static bool operator !=(Balloon firstBalloon, Balloon secondBalloon)
        {
            return !(Balloon.Equals(firstBalloon, secondBalloon));
        }

        public override bool Equals(object obj)
        {
            if (this.GetType().Name == obj.GetType().Name)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
