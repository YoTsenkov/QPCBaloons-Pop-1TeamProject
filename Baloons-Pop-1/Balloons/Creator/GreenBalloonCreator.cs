namespace BalloonsPopsGame.Balloons.Creator
{
    public class GreenBalloonCreator : BalloonCreator
    {
        public override Balloon CreateBalloon()
        {
            var greenBalloon = new GreenBalloon();
            return greenBalloon;
        }
    }
}
