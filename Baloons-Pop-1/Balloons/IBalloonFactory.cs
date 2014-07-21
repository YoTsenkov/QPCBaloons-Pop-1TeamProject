namespace BalloonsPopsGame.Balloons
{
    public interface IBalloonFactory
    {
        Balloon GetBalloon(BalloonType key);
    }
}
