using System;
namespace BalloonsPopsGame.Balloons.Creator
{    
    public class RedBalloonCreator : BalloonCreator
    {
        public override Balloon CreateBalloon()
        {
            var redBalloon = new RedBalloon(BalloonType.Red);
            return redBalloon;
        }
    }
}
