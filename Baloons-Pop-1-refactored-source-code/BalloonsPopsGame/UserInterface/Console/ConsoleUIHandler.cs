namespace BalloonsPopsGame.UserInterface.Console
{
    using System;
    using System.Text;
    using Balloons;
    using Score;    

    /// <summary>
    /// Works with the Console to create user interface.
    /// </summary>
    public class ConsoleUIHandler : UIHandler
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleUIHandler"/> class.
        /// </summary>
        /// <param name="container">The balloons container which will be displayed later on.</param>
        public ConsoleUIHandler(IBalloonsContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Reads and input string from the console.
        /// </summary>
        /// <returns>The read input string.</returns>
        public override string ReadInput()
        {
            var input = Console.ReadLine();
            return input;
        }

        /// <summary>
        /// Displays a given message on the console.
        /// </summary>
        /// <param name="message">The message to display.</param>
        public override void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }

        /// <summary>
        /// Displays a given message to the console replacing the placeholder with the argument.
        /// </summary>
        /// <param name="message">The message to display.</param>
        /// <param name="argument">The argument to replace the placeholder.</param>
        public override void DisplayMessage(string message, object argument)
        {
            Console.WriteLine(message, argument);
        }

        /// <summary>
        /// Displays a given message to the console replacing the placeholders with the arguments.
        /// </summary>
        /// <param name="message">The message to display.</param>
        /// <param name="firstArgument">The first argument to replace the first placeholder.</param>
        /// <param name="secondArgument">The second argument to replace the second placeholder.</param>
        public override void DisplayMessage(string message, object firstArgument, object secondArgument)
        {
            Console.WriteLine(message, firstArgument, secondArgument);
        }

        /// <summary>
        /// Display all the players results by a given scoreboard.
        /// </summary>
        /// <param name="scoreboard">The given scoreboard.</param>
        public override void DisplayScoreboard(IScoreboard scoreboard)
        {
            if (scoreboard.Players.Count == 0)
            {
                this.DisplayMessage(UIMessages.EmptyScoreBoard);
            }
            else
            {
                this.DisplayMessage(UIMessages.Scoreboard);

                foreach (var player in scoreboard.Players)
                {
                    this.DisplayMessage(UIMessages.PlayerMoves, player.Item1, player.Item2);
                }
            }
        }

        /// <summary>
        /// Displays the balloons from the container.
        /// </summary>
        public override void DisplayBalloons()
        {
            this.DrawUpperBorder();
            int counter = 0;
            foreach (var balloon in this.Container)
            {
                if (counter % BalloonsContainer.NumberOfColumns == 0)
                {
                    Console.Write((counter / BalloonsContainer.NumberOfColumns).ToString() + " | ");
                }

                var balloonDrawingManager = BalloonDrawingManagerFactory.GetBalloonDrawingManager(balloon.Type);
                balloonDrawingManager.Draw();

                if (counter % BalloonsContainer.NumberOfColumns == BalloonsContainer.NumberOfColumns - 1)
                {
                    Console.WriteLine("| ");
                }

                counter++;
            }

            this.DrawBottomBorder();
        }

        /// <summary>
        /// The method that is called when the event is fired.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">Event arguments.</param>
        protected override void ContainerChanged(object sender, EventArgs e)
        {
            this.DisplayBalloons();
        }

        /// <summary>
        /// Draws the upper border of the container.
        /// </summary>
        private void DrawUpperBorder()
        {
            StringBuilder border = new StringBuilder();
            border.AppendLine();
            border.Append(new string(' ', 4));
            for (int i = 0; i < BalloonsContainer.NumberOfColumns; i++)
            {
                if (i != BalloonsContainer.NumberOfColumns - 1)
                {
                    border.Append(i + " ");
                }
                else
                {
                    border.AppendLine(i.ToString());
                }
            }

            border.Append(new string(' ', 4));
            border.Append(new string('-', BalloonsContainer.NumberOfColumns * 2));
            Console.WriteLine(border);
        }

        /// <summary>
        /// Draws the bottom border of the container.
        /// </summary>
        private void DrawBottomBorder()
        {
            StringBuilder border = new StringBuilder();
            border.Append(new string(' ', 4));
            border.AppendLine(new string('-', BalloonsContainer.NumberOfColumns * 2));
            Console.WriteLine(border);
        }
    }
}
