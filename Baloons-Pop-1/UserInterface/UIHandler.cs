namespace BalloonsPopsGame.UserInterface
{
    using System;
    using Balloons;

    public abstract class UIHandler
    {
        public const string GoodByeMessage = "Good bye!";
        public const string InvalidMoveMessage = "Invalid move or command!";
        public const string EmptyScoreBoardMessage = "The scoreboard is empty.";
        public const string MissingBalloonMessage = "Illegal move: cannot pop missing ballon!";
        public const string EnterRowAndColumnMessage = "Enter a row and column:";
        public const string EnterYourNameMessage = "Please enter your name for the top scoreboard:";
        public const string PoppedAllBaloonsMessage = "Congratulations! You popped all baloons in {0} moves.";
        public const string WelcomeMessage = "Welcome to “Balloons Pops” game. Please try to pop the balloons.";
        public const string InstructionsMessage = "Use 'top' to view the top scoreboard, 'restart' to start a new game and 'exit' to quit the game.";

        private IBalloonsContainer container;

        public UIHandler(IBalloonsContainer container)
        {
            this.Container = container;
            this.Container.ContainerChanged += new EventHandler(ContainerChanged);
        }

        public abstract void DisplayMessage(string message);

        public abstract void DisplayMessage(string message, int moves);

        public abstract string ReadCommand();

        protected abstract void ContainerChanged(object sender, EventArgs e);

        protected IBalloonsContainer Container
        {
            get
            {
                return this.container;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("BalloonsContainer", "BalloonsContainer cannot be null!");
                }

                this.container = value;
            }
        }

        protected abstract void DisplayBalloons();
    }
}
