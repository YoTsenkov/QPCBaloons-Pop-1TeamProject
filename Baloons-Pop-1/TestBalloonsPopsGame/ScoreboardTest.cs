namespace TestBalloonsPopsGame
{
    using BalloonsPopsGame.Score;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    [TestClass]
    public class ScoreboardTest
    {
        [TestMethod]
        public void UpdatingEmptyScoreboardShouldIncreasePlayersCount()
        {
            Scoreboard scoreboard = new Scoreboard();
            scoreboard.Update("pesho", 15);
            Assert.AreEqual(1, scoreboard.Players.Count, "Updating empty scoreboard doesn't add the player!");
        }

        [TestMethod]
        public void UpdatingNotFullScoreboardShouldIncreasePlayersCount()
        {
            Scoreboard scoreboard = new Scoreboard();
            scoreboard.Update("pesho", 15);
            scoreboard.Update("gosho", 10);
            scoreboard.Update("bai ivan", 13);
            Assert.AreEqual(3, scoreboard.Players.Count, "Updating not full scoreboard doesn't add the player!");
        }

        [TestMethod]
        public void UpdatingFullScoreboardShouldShouldNotIncreasePlayersCount()
        {
            Scoreboard scoreboard = new Scoreboard();
            scoreboard.Update("pesho", 15);
            scoreboard.Update("gosho", 10);
            scoreboard.Update("bai ivan", 13);
            scoreboard.Update("stamat", 15);
            scoreboard.Update("stancho", 4);
            scoreboard.Update("maria", 8);
            Assert.AreEqual(5, scoreboard.Players.Count, "Updating full scoreboard doesn't keep the players count!");
        }

        [TestMethod]
        public void UpdatingFullscoreboardShouldRemoveWeakestPlayer()
        {
            Scoreboard scoreboard = new Scoreboard();
            scoreboard.Update("pesho", 15);
            scoreboard.Update("gosho", 10);
            scoreboard.Update("bai ivan", 13);
            scoreboard.Update("stamat", 14);
            scoreboard.Update("stancho", 4);
            scoreboard.Update("maria", 8);
            Assert.IsFalse(ScoreboardContainsPlayer(scoreboard, new Tuple<string, int>("pesho", 15)), "Updating full scoreboard doesn't remove weakest player!");
        }

        [TestMethod]
        public void ScoreboardShouldBeSorted()
        {
            Scoreboard scoreboard = new Scoreboard();
            scoreboard.Update("pesho", 15);
            scoreboard.Update("gosho", 10);
            scoreboard.Update("bai ivan", 13);
            Assert.IsTrue(IsScoreboardSorted(scoreboard), "Scoreboard sorting doesn't work!");
        }

        private bool IsScoreboardSorted(Scoreboard scoreboard)
        {
            var players = scoreboard.Players;
            for (int i = 0; i < players.Count - 1; i++)
            {
                if (players[i].Item2 > players[i + 1].Item2)
                {
                    return false;
                }
            }

            return true;
        }

        private bool ScoreboardContainsPlayer(Scoreboard scoreboard, Tuple<string, int> player)
        {
            var players = scoreboard.Players;
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].Item1 == player.Item1 && players[i].Item2 == player.Item2)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
