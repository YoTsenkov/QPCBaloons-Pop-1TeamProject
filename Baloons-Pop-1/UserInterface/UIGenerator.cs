namespace BalloonsPopsGame.UserInterface
{
    using System;
    using Balloons;

    public abstract class UIGenerator
    {
        public const string GoodByeMessage = "Good bye!";
        public const string InvalidMoveMessage = "Invalid move or command!";
        public const string EmptyScoreBoardMessage = "The scoreboard is empty.";
        public const string MissingBalloonMessage = "Illegal move: cannot pop missing ballon!";
        public const string EnterRowAndColumnMessage = "Enter a row and column:";
        public const string EnterYourNameMessage = "Please enter your name for the top scoreboard:";
        public const string PoppedAllBaloonsMessage = "Congratulations! You popped all baloons in {0} moves.";

        private BalloonsContainer container;

        public UIGenerator(BalloonsContainer container)
        {
            this.Container = container;
            this.Container.ContainerChanged += new EventHandler(ContainerChanged);
        }

        public abstract void DisplayMessage(string message);

        public abstract void DisplayMessage(string message, int moves);

        protected abstract void ContainerChanged(object sender, EventArgs e);

        protected BalloonsContainer Container
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
