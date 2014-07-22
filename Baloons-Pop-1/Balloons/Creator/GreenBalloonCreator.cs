namespace BalloonsPopsGame.Balloons.Creator
{
    public class GreenBalloonCreator : BalloonCreator
    {
        public override Balloon CreateBalloon()
        {
            var greenBalloon = new GreenBalloon(BalloonType.Green);
            return greenBalloon;
        }
    }
}
