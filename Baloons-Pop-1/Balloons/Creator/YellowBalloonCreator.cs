﻿namespace BalloonsPopsGame.Balloons.Creator
{
    public class YellowBalloonCreator : BalloonCreator
    {
        public override Balloon CreateBalloon()
        {
            var yellowBalloon = new YellowBalloon(BalloonType.Yellow);
            return yellowBalloon;
        }
    }
}
