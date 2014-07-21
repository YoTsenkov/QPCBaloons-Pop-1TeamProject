namespace BalloonsPopsGame
{
    using System;    

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to “Balloons Pops” game. Please try to pop the balloons.");
            Console.WriteLine(" Use 'top' to view the top scoreboard, 'restart' to start a new game and 'exit' to quit the game.");
            Game game = Game.Instance;

            while (true)
            {
                game.ExecuteCommand(Console.ReadLine());
            }
        }
    }
}
