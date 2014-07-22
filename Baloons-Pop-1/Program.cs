﻿namespace BalloonsPopsGame
{
    using System;
    using Balloons;

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to “Balloons Pops” game. Please try to pop the balloons.");
            Console.WriteLine(" Use 'top' to view the top scoreboard, 'restart' to start a new game and 'exit' to quit the game.");
            Game game = Game.Instance;
            game.Start();            
        }
    }
}
