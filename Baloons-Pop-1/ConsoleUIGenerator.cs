namespace BalloonsPopsGame
{
    using System;
    using Balloons;

    public class ConsoleUIGenerator : UIGenerator
    {
        public ConsoleUIGenerator(BalloonsContainer container) : base(container)
        {
        }

        protected override void DisplayBalloons()
        {
            Console.WriteLine();
            Console.WriteLine("     0   1   2   3   4   5   6   7   8   9");
            Console.WriteLine(new String(' ',4) + new String('-',40));

            int counter = 0;
            foreach (var balloon in this.Container)
            {
                if (counter % BalloonsContainer.NumberOfColumns == 0)
                {
                    Console.Write((counter / BalloonsContainer.NumberOfColumns).ToString() + " | ");
                }
                Console.BackgroundColor = this.MatchColor(balloon.Color);
                Console.Write(" " + ConvertBalloonToChar(balloon) + " ");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write(' ');

                if (counter % BalloonsContainer.NumberOfColumns == BalloonsContainer.NumberOfColumns - 1)
                {
                    Console.WriteLine("|");
                    Console.WriteLine();
                }

                counter++;
            }

            Console.WriteLine(new String(' ', 4) + new String('-', 40));
            Console.WriteLine("Insert row and column or other command");
        }

        protected override void ContainerContainerChanged(object sender, EventArgs e)
        {
            this.DisplayBalloons();
        }

        private char ConvertBalloonToChar(Balloon balloon)
        {
            switch (balloon.GetType().Name)
            {
                case "RedBalloon":
                    return '1';
                case "GreenBalloon":
                    return '2';
                case "BlueBalloon":
                    return '3';
                case "YellowBalloon":
                    return '4';
                default:
                    return '-';
            }
        }

        private ConsoleColor MatchColor(BalloonType color)
        {
            ConsoleColor matchColor = ConsoleColor.Black;
            switch (color)
            {
                case BalloonType.Red:
                    matchColor = ConsoleColor.Red;
                    break;
                case BalloonType.Green:
                    matchColor = ConsoleColor.Green;
                    break;
                case BalloonType.Blue:
                    matchColor = ConsoleColor.Blue;
                    break;
                case BalloonType.Yellow:
                    matchColor = ConsoleColor.Yellow;
                    break;
                case BalloonType.Black:
                    matchColor = ConsoleColor.Black;
                    break;
                default:
                    throw new ArgumentException("Unknown colour type.");
            }

            return matchColor;
        }
    }
}