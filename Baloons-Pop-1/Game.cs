namespace BalloonsPopsGame
{
    using Balloons;
    using Exceptions;
    using System;
    using UserInterface;

    class Game
    {
        private IBalloonsContainer balloons;
        private int numberOfTurn;
        private UIHandler uiHandler;
        private IScoreBoard scoreboard;

        public Game(IBalloonsContainer balloons, IScoreBoard scoreboard, UIHandler uiHandler)
        {
            this.NumberOfTurn = 0;
            this.IsGameOver = false;
            this.Balloons = balloons;
            this.Scoreboard = scoreboard;
            this.UIHandler = uiHandler;
        }

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

        private IScoreBoard Scoreboard
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

        private bool IsGameOver { get; set; }

        public void Start()
        {
            this.UIHandler.DisplayMessage(UIMessages.WelcomeMessage);
            this.UIHandler.DisplayMessage(UIMessages.InstructionsMessage);
            this.Balloons.Fill();
            this.UIHandler.DisplayMessage(UIMessages.EnterRowAndColumnMessage);

            while (!this.IsGameOver)
            {
                this.ExecuteCommand(this.UIHandler.ReadInput());
            }
        }

        private void ExecuteCommand(string command)
        {
            command = command.Trim().ToLower();

            if (command == "exit")
            {
                this.IsGameOver = true;
                this.UIHandler.DisplayMessage(UIMessages.GoodByeMessage);
            }
            else if (command == "restart")
            {
                this.Restart();
            }
            else if (command == "top")
            {
                this.UIHandler.DisplayScoreboard(this.Scoreboard);                
            }
            else
            {
                this.PerformBallonsPopping(command);
            }
        }

        private void PerformBallonsPopping(string command)
        {
            string[] rowsAndCols = command.Split();
            if (rowsAndCols.Length != 2)
            {
                this.UIHandler.DisplayMessage(UIMessages.InvalidMoveMessage);
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
                    this.UIHandler.DisplayMessage(UIMessages.EnterRowAndColumnMessage);
                }
                catch (InvalidRowOrColumnException)
                {
                    this.UIHandler.DisplayMessage(UIMessages.InvalidMoveMessage);
                }
                catch (MissingBalloonException)
                {
                    this.UIHandler.DisplayMessage(UIMessages.MissingBalloonMessage);
                }

                if (shouldRestart)
                {
                    this.UIHandler.DisplayMessage(UIMessages.PoppedAllBaloonsMessage, this.numberOfTurn);
                    this.UIHandler.DisplayMessage(UIMessages.AskForNameMessage);
                    this.Scoreboard.Update(this.UIHandler.ReadInput(), this.NumberOfTurn);
                    this.Restart();
                }
            }
            else
            {
                this.UIHandler.DisplayMessage(UIMessages.InvalidMoveMessage);
            }
        }

        private void Restart()
        {
            this.Balloons.Empty();
            this.Balloons.Fill();
            this.NumberOfTurn = 0;
            this.UIHandler.DisplayMessage(UIMessages.EnterRowAndColumnMessage);
        }
    }
}
