namespace BalloonsPopsGame.Balloons
{
    using System;

    /// <summary>
    /// The class which represents the balloon objects.
    /// </summary>
    public class Balloon : ICloneable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Balloon"/> class.
        /// </summary>
        /// <param name="type">The type of the balloon.</param>
        public Balloon(BalloonType type)
        {
            this.Type = type;
        }

        /// <summary>
        /// Gets the type of the balloon.
        /// </summary>
        /// <value>Gets or sets the value of the hidden field.</value>
        public BalloonType Type { get; private set; }

        /// <summary>
        /// Compares two balloons one to another.
        /// </summary>
        /// <param name="firstBalloon">The first balloon.</param>
        /// <param name="secondBalloon">The second balloon.</param>
        /// <returns>Returns true if the balloons are equal.</returns>
        public static bool operator ==(Balloon firstBalloon, Balloon secondBalloon)
        {
            return Balloon.Equals(firstBalloon, secondBalloon);
        }

        /// <summary>
        /// Compares two balloons one to another.
        /// </summary>
        /// <param name="firstBalloon">The first balloon.</param>
        /// <param name="secondBalloon">The second balloon.</param>
        /// <returns>Returns true if the balloons are different.</returns>
        public static bool operator !=(Balloon firstBalloon, Balloon secondBalloon)
        {
            return !Balloon.Equals(firstBalloon, secondBalloon);
        }

        /// <summary>
        /// Compares the balloon with an object.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>The result from the comparison.</returns>
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

        /// <summary>
        /// Clones the balloon thus implementing the prototype pattern.
        /// </summary>
        /// <returns>Return a new clone.</returns>
        public Balloon Clone()
        {
            return (Balloon)this.MemberwiseClone();
        }

        /// <summary>
        /// Returns a unique integer for the balloon.
        /// </summary>
        /// <returns>The unique integer.</returns>
        public override int GetHashCode()
        {
            return 17 ^ this.Type.GetHashCode();
        }

        /// <summary>
        /// Clones the balloon thus implementing the prototype pattern.
        /// </summary>
        /// <returns>Return a new clone.</returns>
        object ICloneable.Clone()
        {
            return this.Clone();
        }
    }
}