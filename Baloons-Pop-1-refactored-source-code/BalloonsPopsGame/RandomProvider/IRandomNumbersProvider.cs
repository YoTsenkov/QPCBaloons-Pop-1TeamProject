namespace BalloonsPopsGame.RandomProvider
{
    public interface IRandomNumbersProvider
    {
        int GetRandomNumber(int minValue, int maxValue);
    }
}
