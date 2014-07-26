namespace BalloonsPopsGame
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public class ScoreBoard : IScoreBoard
    {
        private List<Tuple<string, int>> players;

        public ScoreBoard()
        {
            this.Players = new List<Tuple<string, int>>();
        }

        public List<Tuple<string, int>> Players
        {
            get
            {
                return new List<Tuple<string, int>>(this.players);
            }

            private set
            {
                this.players = value;
            }
        }

        public void Display()
        {
            if (this.Players.Count == 0)
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

                this.Players.ForEach(print);
            }
        }

        public void Update(int moves)
        {
            Action<int> addPLayer = (count) =>
            {
                Console.WriteLine("Enter Name:");
                string s = Console.ReadLine();

                Tuple<string, int> a = Tuple.Create<string, int>(s, count);
                this.players.Add(a);
            };

            if (this.Players.Count < 5)
            {
                addPLayer(moves);
                return;
            }
            else
            {
                if (this.Players.ElementAt<Tuple<string, int>>(this.Players.Count - 1).Item2 >= moves)
                {
                    addPLayer(moves);
                    this.Players.RemoveRange(this.Players.Count, 1);//if the new name replaces one of the old ones, remove the old one
                }
            }

            this.Players.Sort(delegate(Tuple<string, int> p1, Tuple<string, int> p2)//re-sort the list
            {
                return p1.Item2.CompareTo(p2.Item2);
            });
        }
    }
}
