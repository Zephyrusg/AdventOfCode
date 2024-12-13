using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class Y2024D13
    {
        public static string[] Lines = File.ReadAllLines(".\\2024\\Input\\inputDay13.txt");
        static readonly List<(int ax ,int ay,int bx ,int by,long px,long py)> machines = [];
        static readonly ConcurrentBag<(int id, long P1Tokens, long P2Tokens)> Tokens = [];
        internal static readonly string[] separator = ["+", ","];
        internal static readonly string[] separator1 = ["=", ","];

        public static void SolveMachine(int ax, int ay, int bx, int by, long px, long py, int id)
        {
            long P1Tokens = 0;
            long P2Tokens = 0;
            var B = (py * ax - px * ay) / (by * ax - bx * ay);
            var A = (px - B * bx) / ax;

            if (A >= 0 && B >= 0 && B * bx + A * ax == px && B * by + A * ay == py) P1Tokens = A * 3 + B;
                   
 
            px += 10000000000000;
            py += 10000000000000;
            B = (py * ax - px * ay) / (by * ax - bx * ay);
            A = (px - B * bx) / ax;
            if (A >= 0 && B >= 0 && B * bx + A * ax == px && B * by + A * ay == py) P2Tokens = A * 3 + B;

            Tokens.Add((id, P1Tokens, P2Tokens));
        }

        public static long Part1()
        {
            long answer = 0;

            for (int i = 0; i < Lines.Length; i += 4)
            {
                if (string.IsNullOrWhiteSpace(Lines[i])) continue;

                var buttonA = Lines[i].Split(separator, StringSplitOptions.RemoveEmptyEntries);
                var ax = int.Parse(buttonA[1]);
                var ay = int.Parse(buttonA[3]);

                var buttonB = Lines[i + 1].Split(separator, StringSplitOptions.RemoveEmptyEntries);
                var bx = int.Parse(buttonB[1]);
                var by = int.Parse(buttonB[3]);

                var prize = Lines[i + 2].Split(separator1, StringSplitOptions.RemoveEmptyEntries);
                var px = long.Parse(prize[1]);
                var py = long.Parse(prize[3]);

                machines.Add((ax, ay, bx, by, px, py));
            }

            Parallel.For(0,machines.Count,id=>  
            {
                var (ax, ay, bx, by, px, py) = machines[id];
                SolveMachine(ax, ay, bx, by, px, py,id);   
            });

            answer = Tokens.Sum(t => t.P1Tokens);
            return answer;
        }

        public static long Part2()
        {
            long answer = 0;

            answer = Tokens.Sum(t => t.P2Tokens);
            return answer;
        }
    }
}