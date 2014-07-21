namespace BalloonsPopsGame
{
    public class PoppedBalloonCreator : BalloonCreator
    {
        public override Balloon CreateBalloon()
        {
            var poppedBalloon = new PoppedBalloon();
            return poppedBalloon;
        }
    }
}
