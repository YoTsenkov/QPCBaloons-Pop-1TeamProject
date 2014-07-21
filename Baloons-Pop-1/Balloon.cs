using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalloonsPopsGame
{
    public abstract class Balloon
    {
        public override int GetHashCode()
        {
            unchecked
            {
                return 17;
            }
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

        public static bool operator ==(Balloon firstBalloon, Balloon secondBalloon)                       
        {
            return Balloon.Equals(firstBalloon, secondBalloon);
        }

        public static bool operator !=(Balloon firstBalloon, Balloon secondBalloon)
        {
            return !(Balloon.Equals(firstBalloon, secondBalloon));
        }
    }
}
