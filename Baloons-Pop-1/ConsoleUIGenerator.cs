namespace BalloonsPopsGame
{
    using System;
    using Balloons;

    public class ConsoleUIGenerator : UIGenerator
    {
        public ConsoleUIGenerator(BalloonsContainer container)
            : base(container)
        {
        }

        protected override void DisplayBalloons()
        {
            Console.WriteLine();
            Console.WriteLine("    0 1 2 3 4 5 6 7 8 9");
            Console.WriteLine("    --------------------");

            int counter = 0;
            foreach (var balloon in this.Container)
            {
                if (counter % BalloonsContainer.NumberOfColumns == 0)
                {
                    Console.Write((counter / BalloonsContainer.NumberOfColumns).ToString() + " | ");
                }

                Console.Write(ConvertBalloonToChar(balloon) + " ");

                if (counter % BalloonsContainer.NumberOfColumns == BalloonsContainer.NumberOfColumns - 1)
                {
                    Console.WriteLine("| ");
                }

                counter++;
            }

            Console.WriteLine("    --------------------");
            Console.WriteLine();
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
    }
}
