namespace BalloonsPopsGame
{
    using System;

    class BalloonsContainer
    {
        private const int NUMBER_OF_ROWS = 5;
        private const int NUMBER_OF_COLUMNS = 10;
        private const int NUMBER_OF_BALLOON_COLORS = 4;

        private int[,] balloons;
        private int numberOfTurn;
        
        public BalloonsContainer()
        {
            this.NumberOfTurn = 0;
            this.balloons = new int[NUMBER_OF_ROWS, NUMBER_OF_COLUMNS];
            this.Fill();
            this.Display();
        }

        public int NumberOfTurn
        {
            get
            {
                return this.numberOfTurn;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Number of turn must be bigger than zero");
                }

                this.numberOfTurn = value;
            }
        }

        private void Fill()
        {
            Random rnd = new Random();

            for (int i = 0; i < NUMBER_OF_ROWS; i++)
            {
                for (int j = 0; j < NUMBER_OF_COLUMNS; j++)
                {
                    balloons[i, j] = rnd.Next(1, NUMBER_OF_BALLOON_COLORS + 1);
                }
            }
        }

        private char ConvertBalloonToChar(int ballon)
        {
            switch (ballon)
            {
                case 1:
                    return '1';
                case 2:
                    return '2';
                case 3:
                    return '3';
                case 4:
                    return '4';
                default:
                    return '-';
            }
        }

        public bool PopBaloons(int row, int column)
        {
            //changes the game state and returns boolean,indicating wheater the game is over
            if (balloons[row - 1, column - 1] == 0)
            {
                Console.WriteLine("Invalid Move! Can not pop a baloon at that place!!");
                return false;
            }
            else
            {
                NumberOfTurn++;
                int state = balloons[row - 1, column - 1];
                int top = row - 1;
                int bottom = row - 1;
                int left = column - 1;
                int right = column - 1;

                while (top > 0 && (balloons[top - 1, column - 1] == state))
                {
                    top--;
                }

                while (bottom < (NUMBER_OF_ROWS - 1) && balloons[bottom + 1, column - 1] == state)
                {
                    bottom++;
                }

                while (left > 0 && balloons[row - 1, left - 1] == state)
                {
                    left--;
                }

                while (right < (NUMBER_OF_COLUMNS - 1) && balloons[row - 1, right + 1] == state)
                {
                    right++;
                }

                for (int i = left; i <= right; i++)
                {

                    //first remove the elements on the same row and float the elemnts above down
                    if (row == 1)
                    {
                        balloons[row - 1, i] = 0;
                    }
                    else
                    {
                        for (int j = row - 1; j > 0; j--)
                        {
                            balloons[j, i] = balloons[j - 1, i];
                            balloons[j - 1, i] = 0;
                        }
                    }
                }

                //if that's enough,just stop
                if (top == bottom)
                {
                    Console.WriteLine();
                    this.Display();
                    Console.WriteLine();
                    return IsGameOver();
                }
                else
                {   //otherwise fix the problematic column as well
                    for (int i = top; i > 0; --i)
                    {//first float the elements above down and replace them
                        balloons[i + bottom - top, column - 1] = balloons[i, column - 1];
                        balloons[i, column - 1] = 0;
                    }

                    if (bottom - top > top - 1)
                    {   //is there are more baloons to pop in the column than elements above them, need to pop them as well
                        for (int i = top; i <= bottom; i++)
                        {
                            if (balloons[i, column - 1] == state)
                                balloons[i, column - 1] = 0;
                        }
                    }
                }

                Console.WriteLine();
                this.Display();
                Console.WriteLine();
                return IsGameOver();
            }
        }

        private bool IsGameOver()
        {
            foreach (var balloon in balloons)
            {
                if (balloon != 0)
                {
                    return false;
                }
            }

            return true;
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
                    Console.Write(ConvertBalloonToChar(balloons[i, j]) + " ");
                }

                Console.WriteLine("| ");
            }

            Console.WriteLine("    --------------------");
            Console.WriteLine("Insert row and column or other command");
        }
    }
}

