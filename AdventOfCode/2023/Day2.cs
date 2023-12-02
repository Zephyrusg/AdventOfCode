using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace AdventOfCode
{
    
    
    internal class Y2023D2
    {
        public static int TotalRed = 12;
        public static int TotalGreen = 13;
        public static int TotalBlue = 14;

        public static List<Game> Games = new List<Game>();
        public class Set
        {

            public int RedCubes;
            public int BlueCubes;
            public int GreenCubes;

            public Set(int Red, int Blue, int Green)
            {
                this.RedCubes = Red;
                this.BlueCubes = Blue;
                this.GreenCubes = Green;
            }
        }

        public class Game
        {
            public List<Set> Sets = new List<Set>();
            public int Id;
            public int MinimumRed = 0;
            public int MinimumGreen = 0;
            public int MinimumBlue = 0;

            public Game(int Id) { this.Id = Id;}

            public bool ValidGame() {
                foreach (Set set in Sets)
                {
                    if (set.RedCubes > TotalRed) {
                        //Console.WriteLine("GameId: " + this.Id + " ToomanyReds");
                        return false;
                        
                    }
                    if(set.BlueCubes > TotalBlue)
                    {
                        //Console.WriteLine("GameId: " + this.Id + " ToomanyBlues");
                        return false;
                    }
                    if(set.GreenCubes > TotalGreen) {
                        //Console.WriteLine("GameId: " + this.Id + " TooManyGreens");
                        return false;
                    
                    }
                }
                
                return true;

            }

            public void GetMinimumCubes() { 
                
                this.MinimumRed = this.Sets.Max(x => x.RedCubes);
                this.MinimumGreen = this.Sets.Max(y => y.GreenCubes);
                this.MinimumBlue = this.Sets.Max(z => z.BlueCubes);
            }

            public int GetMutiplyMinimums() {
                return this.MinimumBlue * this.MinimumRed * this.MinimumGreen;
            }



        }
        public static int Part1() 
        {
            string[] lines = File.ReadAllLines(".\\2023\\Input\\inputDay2.txt");
            int answer = 0;

            foreach (string line in lines) { 
                string[] GameParts = line.Split(": ");
                int GameId = int.Parse(GameParts[0].Split(' ')[1]);
                Game Game = new Game(GameId);

                string[] SetsParts = GameParts[1].Split("; ");
                foreach (string SetPart in SetsParts) 
                {
                    int red = 0;
                    int blue = 0;
                    int green = 0;

                    string[]Subset = SetPart.Split(", ");
                    foreach (string ColorCubes in Subset) {
                        string PatternColor = "[a-z]+";
                        string PatternCount = "\\d+";
                        string Color = Regex.Match(ColorCubes, PatternColor).Value;
                        int count = int.Parse(Regex.Match(ColorCubes,PatternCount).Value);

                        switch(Color)
                        {
                            case "red":
                                red = count;
                                break;
                            case "green":
                                green = count;
                                break;
                            case "blue":
                                blue = count;
                                break;
                        }
                    }

                    Set Set = new(red,blue,green);
                    Game.Sets.Add(Set);
                }
                Games.Add(Game);
            }

            foreach(Game Game in Games)
            {
                if (Game.ValidGame())
                {
                    //Console.WriteLine("Game: " + Game.Id + " Possible, Adding GameId: " + Game.Id);
                    answer += Game.Id;
                }
            }

       


            return answer;
        }

        public static int Part2()
        {

            //string[] lines = File.ReadAllLines(".\\2023\\Input\\inputDay2.txt"); 
            int answer = 0;

            foreach (Game Game in Games)
            {
                Game.GetMinimumCubes();
                answer += Game.GetMutiplyMinimums();
            }

            return answer;
        }

    }
}
