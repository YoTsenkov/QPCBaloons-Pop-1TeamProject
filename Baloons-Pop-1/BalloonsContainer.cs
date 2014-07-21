namespace BalloonsPopsGame
{
    using System;

    class BalloonsContainer
    {
        private const int NUMBER_OF_ROWS = 5;
        private const int NUMBER_OF_COLUMNS = 10;
        private const int NUMBER_OF_BALLOON_COLORS = 4;

        private static RedBalloonCreator redBalloonCreator = new RedBalloonCreator();
        private static BlueBalloonCreator blueBalloonCreator = new BlueBalloonCreator();
        private static GreenBalloonCreator greenBalloonCreator = new GreenBalloonCreator();
        private static YellowBalloonCreator yellowBalloonCreator = new YellowBalloonCreator();
        private static PoppedBalloonCreator poppedBallonCreator = new PoppedBalloonCreator();

        private Balloon[,] balloons;

        public BalloonsContainer()
        {
            this.Balloons = new Balloon[NUMBER_OF_ROWS, NUMBER_OF_COLUMNS];
            this.Fill();
            this.Display();
        }

        public Balloon[,] Balloons
        {
            private get
            {
                return this.balloons;
                //return (int[,])this.balloons.Clone();
            }

            set
            {
                if (value.GetLength(0) != NUMBER_OF_ROWS)
                {
                    throw new ArgumentException("Invalid number of rows for container!");
                }

                if (value.GetLength(1) != NUMBER_OF_COLUMNS)
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
            for (int i = 0; i < NUMBER_OF_ROWS; i++)
            {
                Console.Write(i.ToString() + " | ");
                for (int j = 0; j < NUMBER_OF_COLUMNS; j++)
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
            if (row > NUMBER_OF_ROWS || column > NUMBER_OF_COLUMNS)
            {
                Console.WriteLine("Invalid balloon position!");
            }
            else if (this.Balloons[row - 1, column - 1] == poppedBallonCreator.CreateBalloon())
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

                while (bottom < (NUMBER_OF_ROWS - 1) && this.Balloons[bottom + 1, column - 1] == state)
                {
                    bottom++;
                }

                while (left > 0 && this.Balloons[row - 1, left - 1] == state)
                {
                    left--;
                }

                while (right < (NUMBER_OF_COLUMNS - 1) && this.Balloons[row - 1, right + 1] == state)
                {
                    right++;
                }

                for (int currentCol = left; currentCol <= right; currentCol++)
                {
                    //first remove the elements on the same row and float the elemnts above down
                    if (row == 1)
                    {
                        this.Balloons[row - 1, currentCol] = poppedBallonCreator.CreateBalloon();
                    }
                    else
                    {
                        for (int currentRow = row - 1; currentRow > 0; currentRow--)
                        {
                            this.Balloons[currentRow, currentCol] = this.Balloons[currentRow - 1, currentCol];
                            this.Balloons[currentRow - 1, currentCol] = poppedBallonCreator.CreateBalloon();
                        }
                    }
                }

                if (top != bottom)
                {
                    //fix the problematic column as well
                    for (int i = top; i > 0; --i)
                    {//first float the elements above down and replace them
                        this.Balloons[i + bottom - top, column - 1] = this.Balloons[i, column - 1];
                        this.Balloons[i, column - 1] = poppedBallonCreator.CreateBalloon();
                    }

                    if (bottom - top > top - 1)
                    {   //is there are more baloons to pop in the column than elements above them, need to pop them as well
                        for (int i = top; i <= bottom; i++)
                        {
                            if (this.Balloons[i, column - 1] == state)
                            {
                                this.Balloons[i, column - 1] = poppedBallonCreator.CreateBalloon();
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

            for (int i = 0; i < NUMBER_OF_ROWS; i++)
            {
                for (int j = 0; j < NUMBER_OF_COLUMNS; j++)
                {
                    Balloon newBalloon;

                    switch (rnd.Next(1, NUMBER_OF_BALLOON_COLORS + 1))
                    {
                        case 1:
                            newBalloon = redBalloonCreator.CreateBalloon();
                            break;
                        case 2:
                            newBalloon = greenBalloonCreator.CreateBalloon();
                            break;
                        case 3:
                            newBalloon = blueBalloonCreator.CreateBalloon();
                            break;
                        case 4:
                            newBalloon = yellowBalloonCreator.CreateBalloon();
                            break;
                        default:
                            throw new ArgumentException("Invalid balloon!");
                            break;
                    }

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
