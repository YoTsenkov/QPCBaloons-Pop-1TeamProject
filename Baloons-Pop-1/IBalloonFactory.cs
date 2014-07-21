namespace BalloonsPopsGame
{
    public interface IBalloonFactory
    {
        Balloon GetBalloon(BalloonType key);
    }
}
