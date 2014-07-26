namespace BalloonsPopsGame.Score
{
    using System;
    using System.Collections.Generic;

    public interface IScoreboard
    {
        IList<Tuple<string, int>> Players { get; }

        void Update(string playerName, int numberOfTurns);
    }
}
