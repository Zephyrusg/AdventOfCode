using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class Y2023D6
    {
        public static string[] lines = File.ReadAllLines(".\\2023\\Input\\inputDay6.txt");
        public int Part1() 
        {
            int answer = 1;
            string pattern = "\\d+";
            int[] Times = Regex.Matches(lines[0], pattern).Select(x=>int.Parse(x.Value)).ToArray();
            int[] Distances = Regex.Matches(lines[1], pattern).Select(x => int.Parse(x.Value)).ToArray();

            for(int t= 0; t < Times.Count(); t++) { 
                
                int RaceTime = Times[t];
                int Distance = Distances[t];
                int WinCounter = 0;
                for(int x=0;x <= RaceTime;x++)
                {
                    if (x * (RaceTime - x) > Distance) { 
                        WinCounter++;
                    } 
                }
                answer *= WinCounter;
            }

            return answer;
        }

        public int Part2()
        {
            int answer = 0;

            string pattern = "\\d+";
            var Matches = Regex.Matches(lines[0], pattern);
            string TimeString = "";
            foreach(var Mat in Matches)
            {
                TimeString += Mat.ToString(); 
            }
            long RaceTime = long.Parse(TimeString);
            Matches = Regex.Matches(lines[1], pattern);
            string DistanceString = "";
            foreach (var Mat in Matches)
            {
                DistanceString += Mat.ToString();
            }
            long Distance = long.Parse(DistanceString);

            int WinCounter = 0;
            for (long x = 0; x <= RaceTime; x++)
            {
                if (x * (RaceTime - x) > Distance)
                {
                    WinCounter++;
                }
            }
            answer = WinCounter;

            return answer;
        }

    }
}
