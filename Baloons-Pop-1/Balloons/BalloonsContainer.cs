namespace BalloonsPopsGame.Balloons
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class BalloonsContainer : IBalloonsContainer, IEnumerable<Balloon>
    {
        public const int NumberOfRows = 5;
        public const int NumberOfColumns = 10;
        public const int NumberOfBalloonColors = 4;

        private Balloon[,] balloons;
        private IBalloonFactory factory;
        private IRandomNumberProvider randomNumberProvider;

        public BalloonsContainer(IBalloonFactory factory, IRandomNumberProvider randomNumberProvider)
        {
            this.Balloons = new Balloon[NumberOfRows, NumberOfColumns];
            this.Factory = factory;
            this.RandomNumberProvider = randomNumberProvider;            
        }

        public event EventHandler ContainerChanged;

        private IRandomNumberProvider RandomNumberProvider
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

                this.OnContainerChanged();
            }
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
            throw new NotImplementedException();
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
    }
}
