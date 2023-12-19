using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Security;
using System.Diagnostics.Metrics;

namespace AdventOfCode
{
    internal class Y2023D18
    {
        Dictionary<int, string> Direction = new Dictionary<int, string>
        {
            { 3, "U" },
            { 1, "D" },
            { 2, "L" },
            { 0, "R" }
        };

        public static string[] Lines = File.ReadAllLines(".\\2023\\Input\\inputDay18.txt");
        static int Height = Lines.Count();
        static int Width = Lines[0].Length;
        public int Part1() 
        {
            int answer = 0;
            HashSet<(int x, int y)> EdgePoints = new();
            (int x, int y) current = (0, 0);
            List<(int distance, string direction)> Steps = new();

            foreach(var line in Lines)
            {
                string direction = line.Split(' ')[0];
                int meters = int.Parse(line.Split(' ')[1]);
                Steps.Add((meters, direction));
            }
            (int x, int y) lastcorner = current;
            (int x, int y) currentcorner = current;
            string lastdirect = Steps[Steps.Count - 1].direction;
            string ?nextdirection = null;
            for(int s = 0; s < Steps.Count; s++) 
            {
                var Step = Steps[s];

                if (s != 0) { lastdirect = Steps[s - 1].direction; }
                if(s == Steps.Count - 1) { nextdirection = Steps[0].direction; }
                else { nextdirection = Steps[s + 1].direction; }

                switch (Steps[s].direction) {


                    case "U":
                        current = (current.x, current.y - Step.distance);
                        break;
                    case "D":
                        current = (current.x, current.y + Step.distance);
                        break;
                    case "L":
                        if (nextdirection == "D")
                        {

                            currentcorner = (current.x - Step.distance + 1, current.y + 1);
                        }
                        else 
                        {

                            currentcorner = (current.x - Step.distance, current.y + 1); 

                        }
                        current = (current.x - Step.distance, current.y);
                        answer += ((lastcorner.x - currentcorner.x) * currentcorner.y);
                        lastcorner = currentcorner;
                        break;
                    case "R":

                        if (nextdirection == "D")
                        {
                            currentcorner = (current.x + Step.distance + 1, current.y);
                        }
                        else 
                        {
                            currentcorner = (current.x + Step.distance, current.y);
                        }

                        current= (current.x + Step.distance, current.y);
                        answer += ((lastcorner.x - currentcorner.x) * current.y);
                        lastcorner = currentcorner;
                        break;
                }
            }

            return answer;
        }

        public long Part2()
        {
            long answer = 0;

            List<(long distance, string direction)> Steps = new();
            foreach (var line in Lines)
            {
                long distance = long.Parse(((line.Split(' ')[2])[2..7]), NumberStyles.HexNumber);
                string direction = Direction[int.Parse(((line.Split(' ')[2])[7].ToString()))];
                Steps.Add((distance, direction));
            }

            (long x, long y) current = (0, 0);
            
            (long x, long y) lastcorner = current;
            (long x, long y) currentcorner = current;
            string lastdirect = Steps[Steps.Count - 1].direction;
            string? nextdirection = null;
            for (int s = 0; s < Steps.Count; s++)
            {
                var Step = Steps[s];

                if (s != 0) { lastdirect = Steps[s - 1].direction; }
                if (s == Steps.Count - 1) { nextdirection = Steps[0].direction; }
                else { nextdirection = Steps[s + 1].direction; }

                switch (Steps[s].direction)
                {


                    case "U":
                        current = (current.x, current.y - Step.distance);
                        break;
                    case "D":
                        current = (current.x, current.y + Step.distance);
                        break;
                    case "L":
                        if (nextdirection == "D")
                        {
                            currentcorner = (current.x - Step.distance + 1, current.y + 1);
                        }
                        else
                        {
                            currentcorner = (current.x - Step.distance, current.y + 1);
                        }
                        current = (current.x - Step.distance, current.y);
                        answer += ((lastcorner.x - currentcorner.x) * currentcorner.y);
                        lastcorner = currentcorner;
                        break;
                    case "R":

                        if (nextdirection == "D")
                        {
                            currentcorner = (current.x + Step.distance + 1, current.y);   
                        }
                        else
                        {
                            currentcorner = (current.x + Step.distance, current.y);
                        }

                        current = (current.x + Step.distance, current.y);
                        answer += ((lastcorner.x - currentcorner.x) * current.y);
                        lastcorner = currentcorner;
                        break;
                }
            }

            return answer;
        }

    }
}
