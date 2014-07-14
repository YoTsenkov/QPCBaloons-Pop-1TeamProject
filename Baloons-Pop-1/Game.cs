namespace BalloonsPopsGame
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class Game
    {
        BalloonsContainer balloons;
        List<Tuple<string, int>> scoreboard;

        public Game()
        {
            balloons = new BalloonsContainer();
            scoreboard = new List<Tuple<string, int>>();
        }

        void displayScoreboard()
        {
            if (scoreboard.Count == 0)
            {
                Console.WriteLine("The scoreboard is empty");
            }
            else
            {
                Console.WriteLine("Top performers:");
                Action<Tuple<string, int>> print = (elem) =>
                {
                    Console.WriteLine(elem.Item1 + "  " + elem.Item2.ToString() + " turns ");
                };

                scoreboard.ForEach(print);
            }
        }

        public void executeCommand(string s)
        {
            if (s == "exit")
            {
                Console.WriteLine("Thanks for playing!!");
                Environment.Exit(0);
            }
            else
                if (s == "restart")
                {
                    Restart();
                }
                else
                {
                    if (s.Length == 3)
                    {
                        if (s == "top")
                        {
                            displayScoreboard();
                        }
                        else
                        {//check input validation
                            int fst, snd;
                            bool first = Int32.TryParse(s.Remove(1), out fst);
                            bool second = Int32.TryParse(s.Remove(0, 1), out snd);
                            if (first && second)
                            {
                                sendCommand(fst, snd);//sends command to the baloonsState game holder
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Unknown Command");
                    }
                }
        }

        private void sendCommand(int fst, int snd)
        {
            bool end = false;
            if (fst > 5)
            {
                Console.WriteLine("Indexes too big");
            }

            else
            {
                end = balloons.PopBaloons(fst + 1, snd + 1);//if this turn ends the game, try to update the scoreboard
            }

            if (end)
            {
                Console.WriteLine("Congratulations!!You popped all the baloons in" + balloons.NumberOfTurn + "moves!");
            }

            updateScoreboard();
            Restart();
        }

        private void updateScoreboard()
        {
            Action<int> add = count =>//function to get the player name and add a tuple to the scoreboard
            {
                Console.WriteLine("Enter Name:");
                string s = Console.ReadLine();
                Tuple<string, int> a = Tuple.Create<string, int>(s, count);
                scoreboard.Add(a);
            };

            if (scoreboard.Count < 5)
            {
                add(balloons.NumberOfTurn);
                return;
            }
            else
            {
                if (scoreboard.ElementAt<Tuple<string, int>>(4).Item2 >= balloons.NumberOfTurn)
                {
                    add(balloons.NumberOfTurn);
                    scoreboard.RemoveRange(4, 1);//if the new name replaces one of the old ones, remove the old one
                }
            }

            scoreboard.Sort(delegate(Tuple<string, int> p1, Tuple<string, int> p2)//re-sort the list
                      {
                          return p1.Item2.CompareTo(p2.Item2);
                      });

            balloons = new BalloonsContainer();
        }

        private void Restart()
        {
            balloons = new BalloonsContainer();
        }
    }
}

