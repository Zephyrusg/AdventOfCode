using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class Y2023D24
    {
        public static string[] Lines = File.ReadAllLines(".\\2023\\Input\\inputDay24.txt");
        static int Height = Lines.Count();
        static int Width = Lines[0].Length;

        List<Hailstone> Hailstones = new List<Hailstone>();
        HashSet<(int A, int B)> Pairs = new HashSet<(int, int)>();
        public class Hailstone
        {
            public double x, y, z, dx, dy , dz;

            public Hailstone(double x, double y, double z, double dx, double dy, double dz)
            {
                this.x = x;
                this.y = y;
                this.z = z;
                this.dx = dx;
                this.dy = dy;
                this.dz = dz;
            }

            public override string? ToString()
            {
                return $"x = {x} y= {y} z= {z} dx = {dx} dy= {dy} dz = {dz}";
            }
        }

        public static (double x, double y)? HailStoneIntersection(Hailstone A, Hailstone B)
        {
            double a1 = A.dy;
            double b1 = (-1)*A.dx;
            double c1 = a1 * (A.x) + b1 * (A.y);

            double a2 = B.dy;
            double b2 = (-1)*B.dx;
            double c2 = a2 * (B.x) + b2 * (B.y);

            double determinant = a1 * b2 - a2 * b1;

            if (determinant == 0)
            {
                return null;
            }
            else
            {
                double x = (b2 * c1 - b1 * c2) / determinant;
                double y = (a1 * c2 - a2 * c1) / determinant;
                return new (x, y);
            }
        }

        public int Part1() 
        {
            int answer = 0;
            foreach(var line in Lines)
            {
                var parts = line.Split(" @ ");
                var Coordinate = parts[0].Split(", ").Select(Double.Parse).ToList();
                var delta = parts[1].Split(", ").Select(Double.Parse).ToList();
                Hailstones.Add(new(Coordinate[0], Coordinate[1], Coordinate[2], delta[0], delta[1], delta[2]));
            }

            
            for (int x = 0; x < Hailstones.Count; x++)
            {
                for (int y = 0; y < Hailstones.Count; y++)
                {


                    if (x != y)
                    {
                        (int, int) Pair = (x, y);
                        (int, int) PairReversed = (y, x);
                        var test = Pairs.TryGetValue(Pair, out _);
                        var test2 = Pairs.TryGetValue(PairReversed, out _);
                        if (!test && !test2)
                        {
                            Pairs.Add(Pair);
                        }
                    }
                }
            }
            foreach (var pair in Pairs) { 
            
                (double x, double y)?test = HailStoneIntersection(Hailstones[pair.A], Hailstones[pair.B]);
                if (test  == null) {
                    continue;
                }

                if (200000000000000 <= test.Value.x && 200000000000000 <= test.Value.y && test.Value.y <= 400000000000000 && test.Value.x <= 400000000000000) {
                    Hailstone A = Hailstones[pair.A];
                    Hailstone B = Hailstones[pair.B];

                    if ((A.dx > 0 && test.Value.x < A.x) || (B.dx > 0 && test.Value.x < B.x))
                    {
                        continue;
                    }
                    else if ((A.dx < 0 && test.Value.x > A.x) || (B.dx < 0 && test.Value.x > B.x)) 
                    {
                        continue;
                    }
                    if ((A.dy > 0 && test.Value.y < A.y) || (B.dy > 0 && test.Value.y < B.y))
                    {
                        continue;
                    }
                    if ((A.dy < 0 && test.Value.y > A.y) || (B.dy < 0 && test.Value.y > B.y))
                    {
                        continue;
                    }

                    answer++;
                }

            }
            return answer;
        }

        public int Part2()
        {
            int answer = 0;




            return answer;
        }

    }
}
