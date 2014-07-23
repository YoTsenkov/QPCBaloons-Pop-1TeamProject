namespace BalloonsPopsGame.UserInterface.Console
{
    using System;
    using Balloons;

    public class ConsoleColorFactory
    {
        public static ConsoleColor GetConsoleColor(Balloon balloon)
        {
            ConsoleColor result;
            if (balloon is RedBalloon)
            {
                result = ConsoleColor.Red;
            }
            else if (balloon is BlueBalloon)
            {
                result = ConsoleColor.Blue;
            }
            else if (balloon is GreenBalloon)
            {
                result = ConsoleColor.Green;
            }
            else if (balloon is YellowBalloon)
            {
                result = ConsoleColor.Yellow;
            }
            else
            {
                result = ConsoleColor.White;
            }

            return result;
        }
    }
}
