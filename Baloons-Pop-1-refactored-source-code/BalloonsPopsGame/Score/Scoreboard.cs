namespace BalloonsPopsGame.Score
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A class for high score storage.
    /// </summary>
    public class Scoreboard : IScoreboard
    {
        public const int MaximumNumberOfResults = 5;
        private IList<Tuple<string, int>> players;

        /// <summary>
        /// Initializes a new instance of the <see cref="Scoreboard"/> class.
        /// </summary>
        public Scoreboard()
            : this(new List<Tuple<string, int>>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Scoreboard"/> class.
        /// </summary>
        /// <param name="players">A list of players in the high score table.</param>
        public Scoreboard(IList<Tuple<string, int>> players)
        {
            this.Players = players;
        }

        /// <summary>
        /// Gets the players in the high score.
        /// </summary>
        /// <value>Gets or sets the value of the players filed.</value>
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

        /// <summary>
        /// Updates the scoreboard by given player name and his number of turns.
        /// </summary>
        /// <param name="playerName">The player name.</param>
        /// <param name="numberOfTurns">The number of turns.</param>
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

        /// <summary>
        /// Sorts the high score table by the players number of turns.
        /// </summary>
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

        /// <summary>
        /// Adds a player to the high score table.
        /// </summary>
        /// <param name="playerName">The player name.</param>
        /// <param name="numberOfTurns">The player number of turns.</param>
        private void AddPlayer(string playerName, int numberOfTurns)
        {
            Tuple<string, int> a = Tuple.Create<string, int>(playerName, numberOfTurns);
            this.players.Add(a);
        }
    }
}
