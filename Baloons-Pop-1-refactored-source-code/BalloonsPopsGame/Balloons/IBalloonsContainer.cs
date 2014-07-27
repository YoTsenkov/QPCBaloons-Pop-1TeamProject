namespace BalloonsPopsGame.Balloons
{
    using System;
    using System.Collections.Generic;

    public interface IBalloonsContainer : IEnumerable<Balloon>
    {
        event EventHandler ContainerChanged;

        Balloon[,] Balloons { get; }

        bool IsEmpty();

        void PopBaloons(int row, int column);

        void PopBaloons(Balloon[,] balloons, int row, int column);

        void Fill();

        void Empty();
    }
}