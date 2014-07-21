using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalloonsPopsGame
{
    class StandardRandomNumberProvider : IRandomNumberProvider
    {
        private Random randomGenerator;

        public StandardRandomNumberProvider()
        {
            this.RandomGenerator = new Random();
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
