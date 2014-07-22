namespace BalloonsPopsGame
{
    using System;
    using System.Linq;
    using Balloons;
    using UserInterface;
    using Exceptions;

    class Game
    {
        private static Game instance;
        private BalloonsContainer balloons;
        private int numberOfTurn;
        private UIGenerator uiGenerator;
        private ScoreBoard scoreBoard;

        private Game()
        {
            this.NumberOfTurn = 0;
            this.Balloons = new BalloonsContainer(new BalloonFactory(), new StandardRandomNumberProvider());
            this.UIGenerator = new ConsoleUIGenerator(this.Balloons);
            this.Scoreboard = new ScoreBoard();
        }

        private BalloonsContainer Balloons
        {
            get
            {
                return this.balloons;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Balloons", "Balloons cannot be null!");
                }

                this.balloons = value;
            }
        }

        private UIGenerator UIGenerator
        {
            get
            {
                return this.uiGenerator;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("UIGenerator", "UIGenerator cannot be null!");
                }

                this.uiGenerator = value;
            }
        }

        public static Game Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Game();
                }

                return instance;
            }
        }

        private ScoreBoard Scoreboard
        {
            get
            {
                return this.scoreBoard;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("ScoreBoard", "ScoreBoard cannot be null!");
                }

                this.scoreBoard = value;
            }
        }

        private int NumberOfTurn
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

        public void Start()
        {
            this.Balloons.Fill();
            this.UIGenerator.DisplayMessage(UIGenerator.EnterRowAndColumnMessage);
            while (true)
            {
                this.ExecuteCommand(Console.ReadLine());
            }
        }

        private void ExecuteCommand(string command)
        {
            if (command == "exit")
            {
                this.UIGenerator.DisplayMessage(UIGenerator.GoodByeMessage);
                Environment.Exit(0);
            }
            else if (command == "restart")
            {
                this.Restart();
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
                    this.UIGenerator.DisplayMessage(UIGenerator.InvalidMoveMessage);
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
                        this.UIGenerator.DisplayMessage(UIGenerator.InvalidMoveMessage);
                    }
                }
            }
        }

        private void PopBallons(int row, int column)
        {
            bool isGameOver = false;

            try
            {
                this.balloons.PopBaloons(row + 1, column + 1);
                this.UIGenerator.DisplayMessage(UIGenerator.EnterRowAndColumnMessage);
            }
            catch (InvalidRowOrColumnException)
            {
                this.UIGenerator.DisplayMessage(UIGenerator.InvalidMoveMessage);
            }
            catch (MissingBalloonException)
            {
                this.UIGenerator.DisplayMessage(UIGenerator.MissingBalloonMessage);
            }

            isGameOver = this.balloons.IsContainerEmpty();
            if (isGameOver)
            {
                this.UIGenerator.DisplayMessage(UIGenerator.PoppedAllBaloonsMessage, this.numberOfTurn);
                this.Scoreboard.Update(this.NumberOfTurn);
                this.Restart();
            }
        }

        private void Restart()
        {
            this.Balloons.Empty();
            this.Balloons.Fill();
            this.NumberOfTurn = 0;
            this.UIGenerator.DisplayMessage(UIGenerator.EnterRowAndColumnMessage);
        }
    }
}
