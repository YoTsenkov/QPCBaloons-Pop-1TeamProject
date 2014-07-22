namespace BalloonsPopsGame.Balloons.Creator
{    
    public class RedBalloonCreator : BalloonCreator
    {
        public override Balloon CreateBalloon()
        {
            var redBalloon = new RedBalloon();
            return redBalloon;
        }
    }
}
