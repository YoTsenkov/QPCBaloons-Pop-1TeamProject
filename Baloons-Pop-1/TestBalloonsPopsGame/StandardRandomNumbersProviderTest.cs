namespace TestBalloonsPopsGame
{
    using BalloonsPopsGame.RandomProvider;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    [TestClass]
    public class StandardRandomNumbersProviderTest
    {
        [TestMethod]
        public void GetRandomNumberShouldReturnNumberInTheDesiredRange()
        {
            StandardRandomNumbersProvider randomProvider = StandardRandomNumbersProvider.Instance;
            var number = randomProvider.GetRandomNumber(0, 5);
            Assert.IsTrue(0 <= number && number <= 5, "StandardRandomNumbersProvider doesn't give random number in the desired range");
        }

        [TestMethod]
        public void GetRandomNumberShouldReturnConcreteNumberWhenMinimumRange()
        {
            StandardRandomNumbersProvider randomProvider = StandardRandomNumbersProvider.Instance;
            var number = randomProvider.GetRandomNumber(2, 2);
            Assert.AreEqual(2, number, "StandardRandomNumbersProvider doesn't give the concrete number when provided minimum range!");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetRandomNumberShouldThrowExceptionWhenProvidedInvalidRange()
        {
            StandardRandomNumbersProvider randomProvider = StandardRandomNumbersProvider.Instance;
            randomProvider.GetRandomNumber(2, -2);
        }

        [TestMethod]
        public void TestSingletonImplementation()
        {
            var firstRandomProvider = StandardRandomNumbersProvider.Instance;
            var secondRandomProvider = StandardRandomNumbersProvider.Instance;
            Assert.IsTrue(firstRandomProvider == secondRandomProvider, "Singleton implementation doesn't work!");
        }
    }
}
