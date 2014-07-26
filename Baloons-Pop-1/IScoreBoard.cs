namespace BalloonsPopsGame
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public interface IScoreBoard
    {
        void Display();

        void Update(int moves);
    }
}