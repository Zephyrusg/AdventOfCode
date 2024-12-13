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
        static List<(int ax ,int ay,int bx ,int by,long px,long py)> machines = new();
        public static long SolveMachine(int ax, int ay, int bx, int by, long px, long py, bool Part2)
        {
            var B = (py * ax - px * ay) / (by * ax - bx * ay);
            var A = (px - B * bx) / ax;
            if (!Part2)
            {
                
                if (A >= 0 && B >= 0 && B * bx + A * ax == px && B * by + A * ay == py)
                {
                    return  A * 3 + B;
                    
                }
                else
                {
                    return -1;
                }
            }
            if (Part2)
            {
                px += 10000000000000;
                py += 10000000000000;
                B = (py * ax - px * ay) / (by * ax - bx * ay);
                A = (px - B * bx) / ax;
                if (A >= 0 && B >= 0 && B * bx + A * ax == px && B * by + A * ay == py) return A * 3 + B;

                return -1;
            }
            return -1;
        }

        public long Part1()
        {
            long answer = 0;
            

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
                var px = long.Parse(prize[1]);
                var py = long.Parse(prize[3]);

                machines.Add((ax, ay, bx, by, px, py));
            }

            long totalTokens = 0;
            int prizesWon = 0;

            foreach (var machine in machines)
            {
                long tokens = SolveMachine(machine.ax, machine.ay, machine.bx, machine.by, machine.px, machine.py, false);

                if (tokens > 0)
                {
                    prizesWon++;
                    totalTokens += tokens;
                }
            }

            answer = totalTokens;
            return answer;
        }

        public long Part2()
        {
            long answer = 0;
            

            long totalTokens = 0;
            int prizesWon = 0;

            foreach (var machine in machines)
            {
                long tokens = SolveMachine(machine.ax, machine.ay, machine.bx, machine.by, machine.px, machine.py, true);

                if (tokens > 0)
                {
                    prizesWon++;
                    totalTokens += tokens;

                }
               
            }

            Console.WriteLine($"Prizes won: {prizesWon}");
            Console.WriteLine($"Total tokens spent: {totalTokens}");

            answer = totalTokens;
            return answer;
        }
    }
}