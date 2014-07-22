namespace BalloonsPopsGame.Balloons
{
    using System;

    public interface IBalloonsContainer
    {
        bool IsContainerEmpty();

        void PopBaloons(int row, int column);

        void Fill();

        void Empty();
    }
}