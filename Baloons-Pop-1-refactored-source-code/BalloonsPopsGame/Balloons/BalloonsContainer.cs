namespace BalloonsPopsGame.Balloons
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Exceptions;
    using RandomProvider;

    /// <summary>
    /// A class for storing all the balloons in the game.
    /// </summary>
    public class BalloonsContainer : IBalloonsContainer, IEnumerable<Balloon>
    {
        public const int NumberOfRows = 5;
        public const int NumberOfColumns = 10;
        public const int NumberOfBalloonColors = 4;

        private Balloon[,] balloons;
        private IBalloonFactory factory;
        private IRandomNumbersProvider randomNumberProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="BalloonsContainer"/> class.
        /// </summary>
        public BalloonsContainer()
            : this(new BalloonFactory(), StandardRandomNumbersProvider.Instance)
        { 
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BalloonsContainer"/> class.
        /// </summary>
        /// <param name="factory">The Flyweight factory.</param>
        /// <param name="randomNumberProvider">The random number generator.</param>
        public BalloonsContainer(IBalloonFactory factory, IRandomNumbersProvider randomNumberProvider)
        {
            this.Balloons = new Balloon[NumberOfRows, NumberOfColumns];
            this.Factory = factory;
            this.RandomNumberProvider = randomNumberProvider;
        }

        public event EventHandler ContainerChanged;

        /// <summary>
        /// Gets the stored balloons.
        /// </summary>
        /// <value>Gets or sets the value of the balloons filed.</value>
        public Balloon[,] Balloons
        {
            get
            {
                return (Balloon[,])this.balloons.Clone();
            }

            private set
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

        /// <summary>
        /// Gets or sets the random number generator.
        /// </summary>
        /// <value>Gets or sets the value of the randomNumberProvider field.</value>
        private IRandomNumbersProvider RandomNumberProvider
        {
            get
            {
                return this.randomNumberProvider;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("RandomNumberProvider", "RandomNumberProvider cannot be null!");
                }

                this.randomNumberProvider = value;
            }
        }

        /// <summary>
        /// Gets or sets the reference to the Flyweight factory.
        /// </summary>
        /// <value>Gets or sets the value of the factory field.</value>
        private IBalloonFactory Factory
        {
            get
            {
                return this.factory;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("factory", "Factory mustn't be null!");
                }

                this.factory = value;
            }
        }        

        /// <summary>
        /// Gets result whether the container is empty.
        /// </summary>
        /// <returns>The answer.</returns>
        public bool IsEmpty()
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

        /// <summary>
        /// Pop balloons by given row and column.
        /// </summary>
        /// <param name="row">The given row.</param>
        /// <param name="column">The given column.</param>
        public void PopBaloons(int row, int column)
        {
            this.PopBaloons(this.balloons, row, column);
        }

        /// <summary>
        /// Pop balloons by given row and column.
        /// </summary>
        /// <param name="row">The given row.</param>
        /// <param name="column">The given column.</param>
        public void PopBaloons(Balloon[,] balloons, int row, int column)
        {
            if (row > balloons.GetLength(0) - 1 || column > balloons.GetLength(1) - 1)
            {
                throw new InvalidRowOrColumnException();
            }
            else if (balloons[row, column] == this.Factory.GetBalloon(BalloonType.Popped))
            {
                throw new MissingBalloonException();
            }

            var state = balloons[row, column];
            int top = this.FindTopPoppingRange(balloons, row, column);
            int bottom = this.FindBottomPoppingRange(balloons, row, column);
            int left = this.FindLeftPoppingRange(balloons, row, column);
            int right = this.FindRightPoppingRange(balloons, row, column);

            for (int currentCol = left; currentCol <= right; currentCol++)
            {
                if (row == 0)
                {
                    balloons[row, currentCol] = this.Factory.GetBalloon(BalloonType.Popped);
                }
                else
                {
                    for (int currentRow = row; currentRow > 0; currentRow--)
                    {
                        balloons[currentRow, currentCol] = balloons[currentRow - 1, currentCol];
                        balloons[currentRow - 1, currentCol] = this.Factory.GetBalloon(BalloonType.Popped);
                    }
                }
            }

            if (top != bottom)
            {
                for (int i = top; i > 0; --i)
                {
                    balloons[i + bottom - top, column] = balloons[i, column];
                    balloons[i, column] = this.Factory.GetBalloon(BalloonType.Popped);
                }

                if (bottom - top > top - 1)
                {
                    for (int i = top; i <= bottom; i++)
                    {
                        if (balloons[i, column] == state)
                        {
                            balloons[i, column] = this.Factory.GetBalloon(BalloonType.Popped);
                        }
                    }
                }
            }

            this.OnContainerChanged();
        }

        /// <summary>
        /// Fills the container.
        /// </summary>
        public void Fill()
        {
            for (int i = 0; i < NumberOfRows; i++)
            {
                for (int j = 0; j < NumberOfColumns; j++)
                {
                    Balloon newBalloon = this.Factory.GetBalloon((BalloonType)this.RandomNumberProvider.GetRandomNumber(0, NumberOfBalloonColors - 1));
                    this.balloons[i, j] = newBalloon;
                }
            }

            this.OnContainerChanged();
        }

        /// <summary>
        /// Empties the container.
        /// </summary>
        public void Empty()
        {
            for (int row = 0; row < NumberOfRows; row++)
            {
                for (int col = 0; col < NumberOfColumns; col++)
                {
                    this.balloons[row, col] = this.Factory.GetBalloon(BalloonType.Popped);
                }
            }
        }

        /// <summary>
        /// Implements the Iterator pattern for the class.
        /// </summary>
        /// <returns>The current balloon.</returns>
        public IEnumerator<Balloon> GetEnumerator()
        {
            for (int row = 0; row < NumberOfRows; row++)
            {
                for (int col = 0; col < NumberOfColumns; col++)
                {
                    yield return this.balloons[row, col].Clone();
                }
            }
        }

        /// <summary>
        /// Implements the Iterator pattern for the class.
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Method called when the ContainerChanged event fires.
        /// </summary>
        protected void OnContainerChanged()
        {
            if (this.ContainerChanged != null)
            {
                this.ContainerChanged(this, new EventArgs());
            }
        }

        /// <summary>
        /// Finds the top balloon popping range.
        /// </summary>
        /// <param name="balloons">The balloons to pop</param>
        /// <param name="row">The row.</param>
        /// <param name="column">The column</param>
        /// <returns>The range.</returns>
        private int FindTopPoppingRange(Balloon[,] balloons, int row, int column)
        {
            var state = balloons[row, column];
            int top = row;

            while (top > 0 && (balloons[top - 1, column] == state))
            {
                top--;
            }

            return top;
        }

        /// <summary>
        /// Finds the bottom balloon popping range.
        /// </summary>
        /// <param name="balloons">The balloons to pop</param>
        /// <param name="row">The row.</param>
        /// <param name="column">The column</param>
        /// <returns>The range.</returns>
        private int FindBottomPoppingRange(Balloon[,] balloons, int row, int column)
        {
            var state = balloons[row, column];
            int bottom = row;

            while (bottom < balloons.GetLength(0) - 1 && balloons[bottom + 1, column] == state)
            {
                bottom++;
            }

            return bottom;
        }

        /// <summary>
        /// Finds the right balloon popping range.
        /// </summary>
        /// <param name="balloons">The balloons to pop</param>
        /// <param name="row">The row.</param>
        /// <param name="column">The column</param>
        /// <returns>The range.</returns>
        private int FindRightPoppingRange(Balloon[,] balloons, int row, int column)
        {
            var state = balloons[row, column];
            int right = column;

            while (right < balloons.GetLength(1) - 1 && balloons[row, right + 1] == state)
            {
                right++;
            }

            return right;
        }

        /// <summary>
        /// Finds the left balloon popping range.
        /// </summary>
        /// <param name="balloons">The balloons to pop</param>
        /// <param name="row">The row.</param>
        /// <param name="column">The column</param>
        /// <returns>The range.</returns>
        private int FindLeftPoppingRange(Balloon[,] balloons, int row, int column)
        {
            var state = balloons[row, column];
            int left = column;

            while (left > 0 && balloons[row, left - 1] == state)
            {
                left--;
            }

            return left;
        }
    }
}
