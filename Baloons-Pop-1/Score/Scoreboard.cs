namespace BalloonsPopsGame.Score
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public class Scoreboard : IScoreboard
    {
        private IList<Tuple<string, int>> players;

        public Scoreboard()
            : this(new List<Tuple<string, int>>())
        { }

        public Scoreboard(IList<Tuple<string, int>> players)
        {
            this.Players = players;
        }

        public IList<Tuple<string, int>> Players
        {
            get
            {
                return new List<Tuple<string, int>>(this.players);
            }

            private set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Players", "Players cannot be null!");
                }

                this.players = value;
            }
        }

        public void Update(string playerName, int numberOfTurns)
        {
            if (this.Players.Count < 5)
            {
                this.AddPlayer(playerName, numberOfTurns);
                return;
            }
            else
            {
                if (this.Players.ElementAt<Tuple<string, int>>(this.Players.Count - 1).Item2 >= numberOfTurns)
                {
                    this.Players.RemoveAt(this.Players.Count - 1);
                    this.AddPlayer(playerName, numberOfTurns);
                }
            }

            this.Players.OrderBy(tuple => tuple.Item2);
        }

        private void AddPlayer(string playerName, int numberOfTurns)
        {
            Tuple<string, int> a = Tuple.Create<string, int>(playerName, numberOfTurns);
            this.players.Add(a);
        }
    }
}
