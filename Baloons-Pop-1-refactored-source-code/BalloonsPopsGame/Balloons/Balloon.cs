namespace BalloonsPopsGame.Balloons
{
    using System;

    public class Balloon : ICloneable
    {
        public Balloon(BalloonType type)
        {
            this.Type = type;
        }

        public BalloonType Type { get; private set; }

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
            Balloon balloon = obj as Balloon;

            if (balloon == null)
            {
                return false;
            }

            if (this.Type != balloon.Type)
            {
                return false;
            }

            return true;
        }

        public Balloon Clone()
        {
            return (Balloon)this.MemberwiseClone();
        }

        public override int GetHashCode()
        {
            return 17 ^ this.Type.GetHashCode();
        }

        object ICloneable.Clone()
        {
            return this.Clone();
        }
    }
}