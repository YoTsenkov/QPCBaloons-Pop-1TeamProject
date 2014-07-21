namespace BalloonsPopsGame
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
