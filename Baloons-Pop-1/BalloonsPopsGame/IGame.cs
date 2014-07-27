namespace BalloonsPopsGame
{
    public interface IGame
    {
        int NumberOfTurn { get; set; }

        bool IsGameOver { get; set; }

        void Start();

        void ExecuteCommand(string command);

        void PerformBalloonsPopping(string command);

        void Restart();
    }
}