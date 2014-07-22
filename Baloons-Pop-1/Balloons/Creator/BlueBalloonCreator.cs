namespace BalloonsPopsGame.Balloons.Creator
{
    public class BlueBalloonCreator : BalloonCreator
    {
        public override Balloon CreateBalloon()
        {
            var blueBalloon = new BlueBalloon(BalloonType.Blue);
            return blueBalloon;
        }
    }
}
