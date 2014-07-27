namespace BalloonsPopsGame.Score
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Scoreboard : IScoreboard
    {
        public const int MaximumNumberOfResults = 5;
        private IList<Tuple<string, int>> players;

        public Scoreboard()
            : this(new List<Tuple<string, int>>())
        {
        }

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
            if (this.Players.Count < MaximumNumberOfResults)
            {
                this.AddPlayer(playerName, numberOfTurns);
            }
            else if (this.Players.ElementAt<Tuple<string, int>>(this.players.Count - 1).Item2 >= numberOfTurns)
            {
                this.players.RemoveAt(this.players.Count - 1);
                this.AddPlayer(playerName, numberOfTurns);
            }

            this.Sort();
        }

        private void Sort()
        {
            int smallestElementIndex = -1;
            for (int i = 0; i < this.players.Count; i++)
            {
                smallestElementIndex = i;
                for (int j = i + 1; j < this.players.Count; j++)
                {
                    if (this.players[smallestElementIndex].Item2 > this.players[j].Item2)
                    {
                        smallestElementIndex = j;
                    }
                }

                var oldElement = this.players[i];
                this.players[i] = this.players[smallestElementIndex];
                this.players[smallestElementIndex] = oldElement;
            }
        }

        private void AddPlayer(string playerName, int numberOfTurns)
        {
            Tuple<string, int> a = Tuple.Create<string, int>(playerName, numberOfTurns);
            this.players.Add(a);
        }
    }
}
