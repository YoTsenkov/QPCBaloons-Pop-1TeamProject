namespace BalloonsPopsGame
{
    using System;

    public class BalloonsContainer
    {
        private const int NumberOfRows = 5;
        private const int NumberOfColumns = 10;
        private const int NumberOfBalloonColors = 4;

        private Balloon[,] balloons;
        private IBalloonFactory factory;

        public BalloonsContainer(IBalloonFactory factory)
        {
            this.Balloons = new Balloon[NumberOfRows, NumberOfColumns];
            this.Factory = factory;
            this.Fill();
            this.Display();
        }

        public IBalloonFactory Factory
        {
            get
            {
                return this.factory;
            }

            private set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("factory", "Factory mustn't be null!");
                }

                this.factory = value;
            }
        }

        public Balloon[,] Balloons
        {
            private get
            {
                // TODO: Deside if we should use return (int[,])this.balloons.Clone();
                return this.balloons;
            }

            set
            {
                if (value.GetLength(0) != NumberOfRows)
                {
                    throw new ArgumentException("Invalid number of rows for container!");
                }

                if (value.GetLength(1) != NumberOfColumns)
                {
                    throw new ArgumentException("Invalid number of columns for container!");
                }

                this.balloons = value;
            }
        }

        public void Display()
        {
            Console.WriteLine("    0 1 2 3 4 5 6 7 8 9");
            Console.WriteLine("    --------------------");
            for (int i = 0; i < NumberOfRows; i++)
            {
                Console.Write(i.ToString() + " | ");
                for (int j = 0; j < NumberOfColumns; j++)
                {
                    Console.Write(ConvertBalloonToChar(Balloons[i, j]) + " ");
                }

                Console.WriteLine("| ");
            }

            Console.WriteLine("    --------------------");
            Console.WriteLine("Insert row and column or other command");
        }

        public bool IsContainerEmpty()
        {
            foreach (var balloon in this.Balloons)
            {
                if (balloon != null)
                {
                    return false;
                }
            }

            return true;
        }

        public void PopBaloons(int row, int column)
        {
            if (row > NumberOfRows || column > NumberOfColumns)
            {
                Console.WriteLine("Invalid balloon position!");
            }
            else if (this.Balloons[row - 1, column - 1] == this.Factory.GetBalloon(BalloonType.Popped))
            {
                Console.WriteLine("Invalid Move! Can not pop a baloon at that place!!");
            }
            else
            {
                var state = this.Balloons[row - 1, column - 1];
                int top = row - 1;
                int bottom = row - 1;
                int left = column - 1;
                int right = column - 1;

                while (top > 0 && (this.Balloons[top - 1, column - 1] == state))
                {
                    top--;
                }

                while (bottom < (NumberOfRows - 1) && this.Balloons[bottom + 1, column - 1] == state)
                {
                    bottom++;
                }

                while (left > 0 && this.Balloons[row - 1, left - 1] == state)
                {
                    left--;
                }

                while (right < (NumberOfColumns - 1) && this.Balloons[row - 1, right + 1] == state)
                {
                    right++;
                }

                for (int currentCol = left; currentCol <= right; currentCol++)
                {
                    //first remove the elements on the same row and float the elemnts above down
                    if (row == 1)
                    {
                        this.Balloons[row - 1, currentCol] = this.Factory.GetBalloon(BalloonType.Popped);
                    }
                    else
                    {
                        for (int currentRow = row - 1; currentRow > 0; currentRow--)
                        {
                            this.Balloons[currentRow, currentCol] = this.Balloons[currentRow - 1, currentCol];
                            this.Balloons[currentRow - 1, currentCol] = this.Factory.GetBalloon(BalloonType.Popped);
                        }
                    }
                }

                if (top != bottom)
                {
                    //fix the problematic column as well
                    for (int i = top; i > 0; --i)
                    {
                        //first float the elements above down and replace them
                        this.Balloons[i + bottom - top, column - 1] = this.Balloons[i, column - 1];
                        this.Balloons[i, column - 1] = this.Factory.GetBalloon(BalloonType.Popped);
                    }

                    if (bottom - top > top - 1)
                    {
                        //is there are more baloons to pop in the column than elements above them, need to pop them as well
                        for (int i = top; i <= bottom; i++)
                        {
                            if (this.Balloons[i, column - 1] == state)
                            {
                                this.Balloons[i, column - 1] = this.Factory.GetBalloon(BalloonType.Popped);
                            }
                        }
                    }
                }

                Console.WriteLine();
                this.Display();
                Console.WriteLine();
            }
        }

        private void Fill()
        {
            Random rnd = new Random();

            for (int i = 0; i < NumberOfRows; i++)
            {
                for (int j = 0; j < NumberOfColumns; j++)
                {
                    Balloon newBalloon = this.Factory.GetBalloon((BalloonType)rnd.Next(0, NumberOfBalloonColors));

                    this.Balloons[i, j] = newBalloon;
                }
            }
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
