namespace BalloonsPopsGame.RandomProvider
{
    using System;

    /// <summary>
    /// A class for providing random numbers.
    /// </summary>
    public class StandardRandomNumbersProvider : IRandomNumbersProvider
    {
        private static StandardRandomNumbersProvider instance;
        private Random randomGenerator;

        /// <summary>
        /// Prevents a default instance of the <see cref="StandardRandomNumbersProvider"/> class from being created.
        /// </summary>
        private StandardRandomNumbersProvider()
        {
            this.RandomGenerator = new Random();
        }

        /// <summary>
        /// Gets the singleton instance of the <see cref="StandardRandomNumbersProvider"/> class.
        /// </summary>
        /// <value>Gets the value of the static instance field.</value>
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

        /// <summary>
        /// Gets or sets the reference to the Random class.
        /// </summary>
        /// <value>Gets or sets the value of the randomGenerator field.</value>
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

        /// <summary>
        /// Returns random number by given range.
        /// </summary>
        /// <param name="minValue">The lowest part of the range.</param>
        /// <param name="maxValue">The highest part of the range.</param>
        /// <returns>The generated random number.</returns>
        public int GetRandomNumber(int minValue, int maxValue)
        {
            return this.RandomGenerator.Next(minValue, maxValue + 1);
        }
    }
}
