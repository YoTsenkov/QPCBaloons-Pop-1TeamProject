namespace TestBalloonsPopsGame
{
    using System;
    using BalloonsPopsGame.Balloons;
    using BalloonsPopsGame.RandomProvider;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Telerik.JustMock;

    [TestClass]
    public class BalloonsContainerTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestSettingNullFactory()
        {
            var container = new BalloonsContainer(null, StandardRandomNumbersProvider.Instance);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestSettingNullIRandomNumbersProvider()
        {
            var container = new BalloonsContainer(new BalloonFactory(), null);
        }

        [TestMethod]
        public void TestFillingWithRedBalloons()
        {
            var container = ArrangeContainer(BalloonType.Red);
            Assert.IsTrue(ContainerContainsOnly(container, BalloonType.Red), "Filling with red ballons doesn't work!");
        }

        [TestMethod]
        public void TestFillingWithBlueBalloons()
        {
            var container = ArrangeContainer(BalloonType.Blue);
            Assert.IsTrue(ContainerContainsOnly(container, BalloonType.Blue), "Filling with blue ballons doesn't work!");
        }


        [TestMethod]
        public void TestFillingWithYellowBalloons()
        {
            var container = ArrangeContainer(BalloonType.Yellow);
            Assert.IsTrue(ContainerContainsOnly(container, BalloonType.Yellow), "Filling with yellow ballons doesn't work!");
        }

        [TestMethod]
        public void TestFillingWithGreenBalloons()
        {
            var container = ArrangeContainer(BalloonType.Green);
            Assert.IsTrue(ContainerContainsOnly(container, BalloonType.Green), "Filling with green ballons doesn't work!");
        }

        [TestMethod]
        public void TestNormalFilling()
        {
            var container = new BalloonsContainer();
            container.Fill();
            Assert.IsTrue(ContainerContainsOnly(container, BalloonType.Green, BalloonType.Blue, BalloonType.Red, BalloonType.Yellow), "Normal filling doesn't work!");
        }

        [TestMethod]
        public void TestEmptying()
        {
            var container = new BalloonsContainer();
            container.Fill();
            container.Empty();
            Assert.IsTrue(ContainerContainsOnly(container, BalloonType.Popped), "Container emptying doesn't work!");
        }

        [TestMethod]
        public void TestGetEnumerator()
        {
            var container = new BalloonsContainer();
            container.Fill();
            Balloon[,] result = new Balloon[BalloonsContainer.NumberOfRows, BalloonsContainer.NumberOfColumns];
            var enumerator = container.GetEnumerator();

            for (int i = 0; i < result.GetLength(0); i++)
            {
                for (int j = 0; j < result.GetLength(1); j++)
                {
                    enumerator.MoveNext();
                    result[i, j] = enumerator.Current;                    
                }
            }

            Assert.IsTrue(AreMatrixesEqual(result, container.Balloons), "GetEnumerator doesn't work correctly!");
        }

        private BalloonsContainer ArrangeContainer(BalloonType type)
        {
            var randomProvider = Mock.Create<IRandomNumbersProvider>();
            Mock.Arrange(() => randomProvider.GetRandomNumber(Arg.IsAny<int>(), Arg.IsAny<int>())).Returns((int)type);
            var container = new BalloonsContainer(new BalloonFactory(), randomProvider);
            container.Fill();
            return container;
        }

        private bool ContainerContainsOnly(BalloonsContainer container, BalloonType type)
        {
            var balloons = container.Balloons;

            for (int i = 0; i < balloons.GetLength(0); i++)
            {
                for (int j = 0; j < balloons.GetLength(1); j++)
                {
                    if (balloons[i, j].Type != type)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private bool ContainerContainsOnly(BalloonsContainer container, params BalloonType[] types)
        {
            var balloons = container.Balloons;

            for (int i = 0; i < balloons.GetLength(0); i++)
            {
                for (int j = 0; j < balloons.GetLength(1); j++)
                {
                    var containsWantedBalloon = false;
                    for (int l = 0; l < types.Length; l++)
                    {
                        if (balloons[i, j].Type == types[l])
                        {
                            containsWantedBalloon = true;
                        }
                    }

                    if (!containsWantedBalloon)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private bool AreMatrixesEqual(Balloon[,] firstMatrix, Balloon[,] secondMatrix)
        {
            if (firstMatrix.GetLength(0) != secondMatrix.GetLength(0) ||
                firstMatrix.GetLength(1) != secondMatrix.GetLength(1))
            {
                return false;
            }

            for (int i = 0; i < firstMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < firstMatrix.GetLength(1); j++)
                {
                    if (firstMatrix[i, j] != secondMatrix[i, j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
