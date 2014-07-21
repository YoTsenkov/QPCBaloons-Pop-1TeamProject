namespace BalloonsPopsGame
{
    public class YellowBalloonCreator : BalloonCreator
    {
        public override Balloon CreateBalloon()
        {
            var yellowBalloon = new YellowBalloon();
            return yellowBalloon;
        }
    }
}
