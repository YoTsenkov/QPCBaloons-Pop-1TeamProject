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

        public override string ReadCommand()
        {
            var command = Console.ReadLine();
            return command;
        }

        public override void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }

        public override void DisplayMessage(string message, int moves)
        {
            Console.WriteLine(message, moves);
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