namespace BalloonsPopsGame
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public interface IScoreBoard
    {
        IList<Tuple<string, int>> Players { get; }

        void Update(string playerName, int numberOfTurns);
    }
}