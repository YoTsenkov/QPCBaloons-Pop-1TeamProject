namespace BalloonsPopsGame
{
    public interface IGame
    {
        int NumberOfTurn { get; }

        bool IsGameOver { get; }

        void Start();

        void ExecuteCommand(string command);

        void PerformBalloonsPopping(string command);

        void Restart();
    }
}