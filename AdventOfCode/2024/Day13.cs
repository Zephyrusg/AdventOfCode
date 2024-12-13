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

        public static bool HasIntegerSolution(int a, int b, long c)
        {
            int gcd = Gcd(a, b);
            Console.WriteLine($"Checking integer solution: GCD({a}, {b}) = {gcd}, c % GCD = {c % gcd}"); // Debug
            return gcd != 0 && c % gcd == 0;
        }

        public static int Gcd(int a, int b)
        {
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return Math.Abs(a);
        }

        static (long gcd, long x, long y) ExtendedGcd(long a, long b)
        {
            if (b == 0) return (a, 1, 0);

            (long gcd, long x1, long y1) = ExtendedGcd(b, a % b);
            long x = y1;
            long y = x1 - (a / b) * y1;

            return (gcd, x, y);
        }

        public static long SolveMachineV2(int ax, int ay, int bx, int by, long px, long py)
        {
            var B = (py * ax - px * ay) / (by * ax - bx * ay);
            var A = (px - B * bx) / ax;

            px += 10000000000000;
            py += 10000000000000;
            B = (py * ax - px * ay) / (by * ax - bx * ay);
            A = (px - B * bx) / ax;
            if (A >= 0 && B >= 0 && B * bx + A * ax == px && B * by + A * ay == py) return A * 3 + B;

            return -1;
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

        public long Part2()
        {
            long answer = 0;
            var machines = new List<(int ax, int ay, int bx, int by, long px, long py)>();

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
                var px = long.Parse(prize[1]) + 10000000000000L; // Add large offset to X
                var py = long.Parse(prize[3]) + 10000000000000L; // Add large offset to Y

                machines.Add((ax, ay, bx, by, px, py));
            }

            long totalTokens = 0;
            int prizesWon = 0;

            foreach (var machine in machines)
            {
                long tokens = SolveMachineV2(machine.ax, machine.ay, machine.bx, machine.by, machine.px, machine.py);

                if (tokens > 0)
                {
                    prizesWon++;
                    totalTokens += tokens;

                    Console.WriteLine($"Machine: A({machine.ax},{machine.ay}), B({machine.bx},{machine.by}), Prize({machine.px},{machine.py})");
                    Console.WriteLine($"Tokens spent: {tokens}");
                }
                else
                {
                    Console.WriteLine($"Machine: A({machine.ax},{machine.ay}), B({machine.bx},{machine.by}), Prize({machine.px},{machine.py})");
                    Console.WriteLine("No solution found.");
                }
            }

            Console.WriteLine($"Prizes won: {prizesWon}");
            Console.WriteLine($"Total tokens spent: {totalTokens}");

            answer = totalTokens;
            return answer;
        }
    }
}