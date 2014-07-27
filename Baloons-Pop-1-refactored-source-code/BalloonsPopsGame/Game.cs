namespace BalloonsPopsGame
{
    using System;
    using Balloons;
    using Exceptions;
    using Score;    
    using UserInterface;

    public class Game : IGame
    {
        private IBalloonsContainer balloons;
        private int numberOfTurn;
        private UIHandler uiHandler;
        private IScoreboard scoreboard;

        public Game(IBalloonsContainer balloons, IScoreboard scoreboard, UIHandler uiHandler)
        {
            this.NumberOfTurn = 0;
            this.IsGameOver = false;
            this.Balloons = balloons;
            this.Scoreboard = scoreboard;
            this.UIHandler = uiHandler;
        }        
        
        public int NumberOfTurn
        {
            get
            {
                return this.numberOfTurn;
            }

            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Number of turn must be bigger than zero");
                }

                this.numberOfTurn = value;
            }
        }

        public bool IsGameOver { get;  private set; }

        private IBalloonsContainer Balloons
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

        private UIHandler UIHandler
        {
            get
            {
                return this.uiHandler;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("UIGenerator", "UIGenerator cannot be null!");
                }

                this.uiHandler = value;
            }
        }

        private IScoreboard Scoreboard
        {
            get
            {
                return this.scoreboard;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("ScoreBoard", "ScoreBoard cannot be null!");
                }

                this.scoreboard = value;
            }
        }

        public void Start()
        {
            this.UIHandler.DisplayMessage(UIMessages.Welcome);
            this.UIHandler.DisplayMessage(UIMessages.Instructions);
            this.Balloons.Fill();
            this.UIHandler.DisplayMessage(UIMessages.EnterRowAndColumn);

            while (!this.IsGameOver)
            {
                this.ExecuteCommand(this.UIHandler.ReadInput());
            }
        }

        public void ExecuteCommand(string command)
        {
            command = command.Trim().ToLower();

            if (command == UIMessages.Exit)
            {
                this.IsGameOver = true;
                this.UIHandler.DisplayMessage(UIMessages.GoodBye);
            }
            else if (command == UIMessages.Restart)
            {
                this.Restart();
            }
            else if (command == UIMessages.Top)
            {
                this.UIHandler.DisplayScoreboard(this.Scoreboard);
            }
            else
            {
                this.PerformBalloonsPopping(command);
            }
        }

        public void PerformBalloonsPopping(string command)
        {
            string[] rowsAndCols = command.Split();
            if (rowsAndCols.Length != 2)
            {
                this.UIHandler.DisplayMessage(UIMessages.InvalidMove);
                return;
            }

            int row, column;
            bool isValidRow = int.TryParse(rowsAndCols[0], out row);
            bool isValidColumn = int.TryParse(rowsAndCols[1], out column);
            bool shouldRestart = false;

            if (isValidRow && isValidColumn)
            {
                try
                {
                    this.balloons.PopBaloons(row, column);
                    this.NumberOfTurn++;
                    shouldRestart = this.balloons.IsEmpty();                    
                }
                catch (InvalidRowOrColumnException)
                {
                    this.UIHandler.DisplayMessage(UIMessages.InvalidMove);
                }
                catch (MissingBalloonException)
                {
                    this.UIHandler.DisplayMessage(UIMessages.MissingBalloon);
                }

                if (shouldRestart)
                {
                    this.UIHandler.DisplayMessage(UIMessages.PoppedAllBaloons, this.numberOfTurn);
                    this.UIHandler.DisplayMessage(UIMessages.AskForName);
                    this.Scoreboard.Update(this.UIHandler.ReadInput(), this.NumberOfTurn);
                    this.Restart();
                }
                else
                {
                    this.UIHandler.DisplayMessage(UIMessages.EnterRowAndColumn);
                }
            }
            else
            {
                this.UIHandler.DisplayMessage(UIMessages.InvalidMove);
            }
        }

        public void Restart()
        {
            this.Balloons.Empty();
            this.Balloons.Fill();
            this.NumberOfTurn = 0;
            this.UIHandler.DisplayMessage(UIMessages.EnterRowAndColumn);
        }
    }
}
