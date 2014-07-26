namespace BalloonsPopsGame.UserInterface.Console
{
    using System;
    using Balloons;
    using System.Text;

    public class ConsoleUIHandler : UIHandler
    {
        public ConsoleUIHandler(IBalloonsContainer container)
            : base(container)
        {
        }

        public override string ReadInput()
        {
            var input = Console.ReadLine();
            return input;
        }

        public override void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }

        public override void DisplayMessage(string message, object placeholder)
        {
            Console.WriteLine(message, placeholder);
        }

        public override void DisplayMessage(string message, object firstPlaceholder, object secondPlaceholder)
        {
            Console.WriteLine(message, firstPlaceholder, secondPlaceholder);
        }

        public override void DisplayScoreboard(IScoreBoard scoreboard)
        {
            if (scoreboard.Players.Count == 0)
            {
                this.DisplayMessage(UIMessages.EmptyScoreBoardMessage);
            }
            else
            {
                this.DisplayMessage(UIMessages.ScoreboardMessage);

                foreach (var player in scoreboard.Players)
                {
                    this.DisplayMessage(UIMessages.PlayerMovesMessage, player.Item1, player.Item2);
                }
            }
        }

        protected override void DisplayBalloons()
        {
            this.DrawUpperBorder();
            int counter = 0;
            foreach (var balloon in this.Container)
            {
                if (counter % BalloonsContainer.NumberOfColumns == 0)
                {
                    Console.Write((counter / BalloonsContainer.NumberOfColumns).ToString() + " | ");
                }

                var balloonDrawingManager = BalloonDrawingManagerFactory.GetBalloonDrawingManager(balloon);
                balloonDrawingManager.Draw(balloon);

                if (counter % BalloonsContainer.NumberOfColumns == BalloonsContainer.NumberOfColumns - 1)
                {
                    Console.WriteLine("| ");
                }

                counter++;
            }

            this.DrawBottomBorder();
        }

        protected override void ContainerChanged(object sender, EventArgs e)
        {
            this.DisplayBalloons();
        }

        private void DrawUpperBorder()
        {
            StringBuilder border = new StringBuilder();
            border.AppendLine();
            border.Append(new String(' ', 4));
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

            border.Append(new String(' ', 4));
            border.Append(new String('-', BalloonsContainer.NumberOfColumns * 2));
            Console.WriteLine(border);
        }

        private void DrawBottomBorder()
        {
            StringBuilder border = new StringBuilder();
            border.Append(new String(' ', 4));
            border.AppendLine(new String('-', BalloonsContainer.NumberOfColumns * 2));
            Console.WriteLine(border);
        }
    }
}