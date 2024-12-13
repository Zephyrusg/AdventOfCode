using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class Y2024D13
    {
        public static string[] Lines = File.ReadAllLines(".\\2024\\Input\\inputDay13.txt");


        public static int SolveMachine(int ax, int ay, int bx, int by, int px, int py)
        {
            int minTokens = int.MaxValue;

          
            for (int aPresses = 0; aPresses <= 100; aPresses++)
            {
                for (int bPresses = 0; bPresses <= 100; bPresses++)
                {
                 
                    int x = aPresses * ax + bPresses * bx;
                    int y = aPresses * ay + bPresses * by;

                    if (x == px && y == py)
                    {
                        
                        int cost = aPresses * 3 + bPresses * 1;

                       
                        minTokens = Math.Min(minTokens, cost);
                    }
                }
            }

            return minTokens == int.MaxValue ? -1 : minTokens;
        }


        public int Part1() 
        {
            int answer = 0;

            
            var machines = new List<(int ax, int ay, int bx, int by, int px, int py)>();


            for (int i = 0; i < Lines.Length; i += 4)
            {
                if (string.IsNullOrWhiteSpace(Lines[i])) continue;

                var buttonA = Lines[i].Split(new[] { "+", "," }, StringSplitOptions.RemoveEmptyEntries);
                var ax = int.Parse(buttonA[1]);
                var ay = int.Parse(buttonA[3]);

                var buttonB = Lines[i + 1].Split(new[] { "+", "," }, StringSplitOptions.RemoveEmptyEntries);
                var bx = int.Parse(buttonB[1]);
                var by = int.Parse(buttonB[3]);

                var prize = Lines[i + 2].Split(new[] { "=", "," }, StringSplitOptions.RemoveEmptyEntries);
                var px = int.Parse(prize[1]);
                var py = int.Parse(prize[3]);

                machines.Add((ax, ay, bx, by, px, py));
            }

            int totalTokens = 0;
            int prizesWon = 0;
           
            foreach (var machine in machines)
            {
                int tokens = SolveMachine(machine.ax, machine.ay, machine.bx, machine.by, machine.px, machine.py);

                if (tokens > 0)
                {
                    
                    prizesWon++;
                    totalTokens += tokens;
                }
                
            }

         
            answer = totalTokens;

            

            return answer;
        }

        public int Part2()
        {
            int answer = 0;




            return answer;
        }

    }
}
