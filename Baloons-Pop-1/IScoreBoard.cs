namespace BalloonsPopsGame
{
    using System;
    using System.Collections.Generic;

    public interface IScoreBoard
    {
        IList<Tuple<string, int>> Players { get; }

        void Update(string playerName, int numberOfTurns);
    }
}
