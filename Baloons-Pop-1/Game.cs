namespace BalloonsPopsGame
{
    using System;
    using System.Linq;

    class Game
    {
        private static readonly Game game = new Game();
        private BalloonsContainer balloons;
        private int numberOfTurn;

        private Game()
        {
            this.NumberOfTurn = 0;
            balloons = new BalloonsContainer();
            this.Scoreboard = new ScoreBoard();
        }

        public static Game Instance
        {
            get
            {
                return game;
            }
        }

        public ScoreBoard Scoreboard { get; set; }

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

        public void ExecuteCommand(string command)
        {
            if (command == "exit")
            {
                Console.WriteLine("Thanks for playing!!");
                Environment.Exit(0);
            }
            else if (command == "restart")
            {
                Restart();
            }
            else if (command == "top")
            {
                this.Scoreboard.Display();
            }
            else
            {
                var rowsAndCols = command.Split();

                if (rowsAndCols.Length != 2)
                {
                    Console.WriteLine("Unknown Command");
                }
                else
                {
                    int row, column;
                    bool validRow = int.TryParse(rowsAndCols[0], out row);
                    bool validColumn = int.TryParse(rowsAndCols[1], out column);

                    if (validRow && validColumn)
                    {
                        this.PopBallons(row, column);
                        this.NumberOfTurn++;
                    }
                    else
                    {
                        Console.WriteLine("Unknown Command");
                    }
                }
            }
        }

        private void PopBallons(int row, int column)
        {
            bool isGameOver = false;
            this.balloons.PopBaloons(row + 1, column + 1);
            isGameOver = this.balloons.IsContainerEmpty();

            if (isGameOver)
            {
                Console.WriteLine("Congratulations!!You popped all the baloons in" + this.NumberOfTurn + "moves!");
                this.Scoreboard.Update(this.NumberOfTurn);
                Restart();
            }
        }

        private void Restart()
        {
            balloons = new BalloonsContainer();
        }
    }
}

