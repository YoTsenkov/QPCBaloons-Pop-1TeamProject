namespace BalloonsPopsGame
{
    using System;
    using Balloons;
    using Exceptions;
    using Score;    
    using UserInterface;

    /// <summary>
    /// Runs the game and uses the other classes to do his work.
    /// </summary>
    public class Game : IGame
    {
        private IBalloonsContainer balloons;
        private int numberOfTurn;
        private UIHandler uiHandler;
        private IScoreboard scoreboard;

        /// <summary>
        /// Creates instance of the Game class.
        /// </summary>
        /// <param name="balloons">the ballons container</param>
        /// <param name="scoreboard">the scoreboard for keeping the high scores</param>
        /// <param name="uiHandler">the instance for working with the user interface</param>
        public Game(IBalloonsContainer balloons, IScoreboard scoreboard, UIHandler uiHandler)
        {
            this.NumberOfTurn = 0;
            this.IsGameOver = false;
            this.Balloons = balloons;
            this.Scoreboard = scoreboard;
            this.UIHandler = uiHandler;
        }        
        
        /// <summary>
        /// Gets information for the number of turns
        /// the player has made into the game.
        /// </summary>
        /// <value>The property gets/sets the value of the integer field numberOfTurn.</value>
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

        /// <summary>
        /// Gets a value indicating whether the game should finish.
        /// </summary>        
        /// <value>Gets/sets boolean value to a hidden field.</value>
        public bool IsGameOver { get;  private set; }

        /// <summary>
        /// Gets or sets the reference for the balloons container.
        /// </summary>
        /// <value>Gets/sets the value of the balloons field.</value>
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

        /// <summary>
        /// Gets or sets the reference for the UIHandler.
        /// </summary>
        /// <value> Gets or sets the value of the uiHandler field.</value>
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

        /// <summary>
        /// Gets or sets the reference for the Scoreboard.
        /// </summary>
        /// <value>Gets or sets the value of the scoreboard field.</value>
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

        /// <summary>
        /// Starts the game and waits for user input.
        /// </summary>
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

        /// <summary>
        /// Parses and executes a command by a given string.
        /// </summary>
        /// <param name="command">The string by which the method decides which command to execute.</param>
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

        /// <summary>
        /// Parses the user input and gives the information
        /// to the container to pop the balloons.
        /// </summary>
        /// <param name="command">The string to parse.</param>
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

        /// <summary>
        /// Rstarts the game.
        /// </summary>
        public void Restart()
        {
            this.Balloons.Empty();
            this.Balloons.Fill();
            this.NumberOfTurn = 0;
            this.UIHandler.DisplayMessage(UIMessages.EnterRowAndColumn);
        }
    }
}
