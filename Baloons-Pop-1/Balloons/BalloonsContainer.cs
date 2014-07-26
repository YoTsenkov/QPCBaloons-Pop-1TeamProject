namespace BalloonsPopsGame.Balloons
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Exceptions;

    public class BalloonsContainer : IBalloonsContainer, IEnumerable<Balloon>
    {
        public const int NumberOfRows = 5;
        public const int NumberOfColumns = 10;
        public const int NumberOfBalloonColors = 4;

        private Balloon[,] balloons;
        private IBalloonFactory factory;
        private IRandomNumbersProvider randomNumberProvider;

        public BalloonsContainer(IBalloonFactory factory, IRandomNumbersProvider randomNumberProvider)
        {
            this.Balloons = new Balloon[NumberOfRows, NumberOfColumns];
            this.Factory = factory;
            this.RandomNumberProvider = randomNumberProvider;
        }

        public event EventHandler ContainerChanged;

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

        private Balloon[,] Balloons
        {
            get
            {
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

        public void PopBaloons(int row, int column)
        {
            if (row > NumberOfRows || column > NumberOfColumns)
            {
                throw new InvalidRowOrColumnException();
            }
            else if (this.Balloons[row, column] == this.Factory.GetBalloon(BalloonType.Popped))
            {
                throw new MissingBalloonException();
            }

            var state = this.Balloons[row, column];
            int top = FindTopPoppingRange(row, column);
            int bottom = FindBottomPoppingRange(row, column);
            int left = FindLeftPoppingRange(row, column);
            int right = FindRightPoppingRange(row, column);
            
            for (int currentCol = left; currentCol <= right; currentCol++)
            {
                if (row == 1)
                {
                    this.Balloons[row, currentCol] = this.Factory.GetBalloon(BalloonType.Popped);
                }
                else
                {
                    for (int currentRow = row; currentRow > 0; currentRow--)
                    {
                        this.Balloons[currentRow, currentCol] = this.Balloons[currentRow - 1, currentCol];
                        this.Balloons[currentRow - 1, currentCol] = this.Factory.GetBalloon(BalloonType.Popped);
                    }
                }
            }

            if (top != bottom)
            {
                for (int i = top; i > 0; --i)
                {
                    this.Balloons[i + bottom - top, column] = this.Balloons[i, column];
                    this.Balloons[i, column] = this.Factory.GetBalloon(BalloonType.Popped);
                }

                if (bottom - top > top - 1)
                {
                    for (int i = top; i <= bottom; i++)
                    {
                        if (this.Balloons[i, column] == state)
                        {
                            this.Balloons[i, column] = this.Factory.GetBalloon(BalloonType.Popped);
                        }
                    }
                }
            }

            this.OnContainerChanged();
        }

        public void Fill()
        {
            for (int i = 0; i < NumberOfRows; i++)
            {
                for (int j = 0; j < NumberOfColumns; j++)
                {
                    Balloon newBalloon = this.Factory.GetBalloon((BalloonType)this.RandomNumberProvider.GetRandomNumber(0, NumberOfBalloonColors - 1));
                    this.Balloons[i, j] = newBalloon;
                }
            }

            this.OnContainerChanged();
        }

        public void Empty()
        {
            for (int row = 0; row < NumberOfRows; row++)
            {
                for (int col = 0; col < NumberOfColumns; col++)
                {
                    this.Balloons[row, col] = this.Factory.GetBalloon(BalloonType.Popped);
                }
            }
        }

        public IEnumerator<Balloon> GetEnumerator()
        {
            for (int row = 0; row < NumberOfRows; row++)
            {
                for (int col = 0; col < NumberOfColumns; col++)
                {
                    yield return this.Balloons[row, col].Clone();
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        protected void OnContainerChanged()
        {
            if (this.ContainerChanged != null)
            {
                this.ContainerChanged(this, new EventArgs());
            }
        }

        private int FindTopPoppingRange(int row, int column)
        {
            var state = this.Balloons[row, column];
            int top = row;

            while (top > 0 && (this.Balloons[top - 1, column] == state))
            {
                top--;
            }

            return top;
        }

        private int FindBottomPoppingRange(int row, int column)
        {
            var state = this.Balloons[row, column];
            int bottom = row;

            while (bottom < (NumberOfRows - 1) && this.Balloons[bottom + 1, column] == state)
            {
                bottom++;
            }

            return bottom;
        }

        private int FindRightPoppingRange(int row, int column)
        {
            var state = this.Balloons[row, column];
            int right = column;

            while (right < (NumberOfColumns - 1) && this.Balloons[row, right + 1] == state)
            {
                right++;
            }

            return right;
        }

        private int FindLeftPoppingRange(int row, int column)
        {
            var state = this.Balloons[row, column];
            int left = column;

            while (left > 0 && this.Balloons[row, left - 1] == state)
            {
                left--;
            }

            return left;
        }        
    }
}
