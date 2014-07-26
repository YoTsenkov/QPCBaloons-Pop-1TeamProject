namespace BalloonsPopsGame.RandomProvider
{
    using System;

    class StandardRandomNumbersProvider : IRandomNumbersProvider
    {
        private static StandardRandomNumbersProvider instance;
        private Random randomGenerator;

        private StandardRandomNumbersProvider()
        {
            this.RandomGenerator = new Random();
        }

        public static StandardRandomNumbersProvider Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new StandardRandomNumbersProvider();
                }

                return instance;
            }
        }

        private Random RandomGenerator
        {
            get
            {
                return this.randomGenerator;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("RandomGenerator", "Random generator cannot be null!");
                }

                this.randomGenerator = value;
            }
        }

        public int GetRandomNumber(int minValue, int maxValue)
        {
            return this.RandomGenerator.Next(minValue, maxValue + 1);
        }
    }
}
