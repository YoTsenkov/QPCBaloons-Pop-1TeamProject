namespace BalloonsPopsGame.Balloons
{
    using System;

    public abstract class Balloon : ICloneable
    {
        public Balloon(BalloonType color)
        {
            this.Color = color;
        }

        public BalloonType Color { get; set; }

        public static bool operator ==(Balloon firstBalloon, Balloon secondBalloon)
        {
            return Balloon.Equals(firstBalloon, secondBalloon);
        }

        public static bool operator !=(Balloon firstBalloon, Balloon secondBalloon)
        {
            return !Balloon.Equals(firstBalloon, secondBalloon);
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

        public Balloon Clone()
        {
            return (Balloon)this.MemberwiseClone();
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return 17;
            }
        }

        object ICloneable.Clone()
        {
            return this.Clone();
        }
    }
}