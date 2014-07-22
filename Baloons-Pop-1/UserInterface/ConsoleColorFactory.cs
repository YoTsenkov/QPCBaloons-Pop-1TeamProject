namespace BalloonsPopsGame.UserInterface
{
    using System;
    using Balloons;

    public class ConsoleColorFactory
    {
        public static ConsoleColor GetConsoleColor(Balloon ballon)
        {
            ConsoleColor result = ConsoleColor.White;
            if (ballon is RedBalloon)
            {
                result = ConsoleColor.Red;
            }
            else if (ballon is BlueBalloon)
            {
                result = ConsoleColor.Blue;
            }
            else if (ballon is GreenBalloon)
            {
                result = ConsoleColor.Green;
            }
            else if (ballon is YellowBalloon)
            {
                result = ConsoleColor.Yellow;
            }
            else if (ballon is PoppedBalloon)
            {
                result = ConsoleColor.White;
            }

            return result;
        }
    }
}
