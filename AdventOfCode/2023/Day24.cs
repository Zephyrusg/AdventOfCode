using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class Y2023D24
    {
        public static string[] Lines = File.ReadAllLines(".\\2023\\Input\\inputDay24.txt");
        static int Height = Lines.Count();
        static int Width = Lines[0].Length;

        static List<Hailstone> Hailstones = new List<Hailstone>();
        static HashSet<(int A, int B)> Pairs = new HashSet<(int, int)>();
        public class Hailstone
        {
            public decimal x, y, z, dx, dy, dz;

            public Hailstone(decimal x, decimal y, decimal z, decimal dx, decimal dy, decimal dz)
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

        public static (decimal x, decimal y)? HailStoneIntersection(Hailstone A, Hailstone B)
        {
            decimal a1 = A.dy;
            decimal b1 = (-1) * A.dx;
            decimal c1 = a1 * (A.x) + b1 * (A.y);

            decimal a2 = B.dy;
            decimal b2 = (-1) * B.dx;
            decimal c2 = a2 * (B.x) + b2 * (B.y);

            decimal determinant = a1 * b2 - a2 * b1;

            if (determinant == 0)
            {
                return null;
            }
            else
            {
                decimal x = (b2 * c1 - b1 * c2) / determinant;
                decimal y = (a1 * c2 - a2 * c1) / determinant;
                return new ( x, y);
            }
        }

        List<Hailstone> ConvertSpeed(decimal dx, decimal dy, decimal dz) { 
        
            List<Hailstone> ConvertStoneList = new List<Hailstone>();

            foreach (Hailstone A in Hailstones) {
                Hailstone ConvertStone = new(A.x, A.y,A.z,A.dx - dx, A.dy - dy, A.dz - dz);
                ConvertStoneList.Add(ConvertStone);
            }
            return ConvertStoneList;
        }


        bool TestXY((decimal x, decimal y) A, (decimal x, decimal y) B) {
            if (Math.Abs(A.x - B.x) < (decimal)0.001 && Math.Abs(A.y - B.y) < (decimal)0.001)
            {
                return true;
            }
            else {
                return false;
            }
        }

        bool TestZ(decimal A , decimal B)
        {
            if (Math.Abs(A - B) < (decimal)0.001)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private ((decimal x, decimal y),(decimal dx, decimal dy)) FindDX()
        {
            decimal dx = 500;
            decimal dy = 500;

            for (dx = 500; dx > -500; dx--)
            {
                for (dy = 500; dy > -500; dy--)
                {

                    bool Correctpoint = true;
                    List<Hailstone> list = ConvertSpeed(dx, dy, 0);
                    (decimal x, decimal y)? intersect = HailStoneIntersection(list[0], list[1]);
                    if (intersect == null) { continue; }
                    foreach (var pair in Pairs)
                    {
                        if (pair == (0, 1)) { continue; }
                        (decimal x, decimal y)? testinteresect = HailStoneIntersection(list[pair.A], list[pair.B]);
                        if (testinteresect == null) { continue; }
                        if (!TestXY((testinteresect.Value.x, testinteresect.Value.y), (intersect.Value.x, intersect.Value.y)))
                        {
                            Correctpoint = false;
                            break;
                        }
                        //Console.WriteLine($"One correct Intersection: dx: {dx} dy: {dy} ");
                    }

                    if (Correctpoint)
                    {
                        return ((intersect.Value.x, intersect.Value.y),(dx, dy));
                    }
                }

            }
            return ((0,0),(0,0));
        }

        public int Part1() 
        {
            int answer = 0;
            foreach(var line in Lines)
            {
                var parts = line.Split(" @ ");
                var Coordinate = parts[0].Split(", ").Select(decimal.Parse).ToList();
                var delta = parts[1].Split(", ").Select(decimal.Parse).ToList();
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
            
                (decimal x, decimal y)?InterSect = HailStoneIntersection(Hailstones[pair.A], Hailstones[pair.B]);
                if (InterSect  == null) {
                    continue;
                }

                if (200000000000000 <= InterSect.Value.x && 200000000000000 <= InterSect.Value.y && InterSect.Value.y <= 400000000000000 && InterSect.Value.x <= 400000000000000) {
                    Hailstone A = Hailstones[pair.A];
                    Hailstone B = Hailstones[pair.B];

                    if ((A.dx > 0 && InterSect.Value.x < A.x) || (B.dx > 0 && InterSect.Value.x < B.x))
                    {
                        continue;
                    }
                    else if ((A.dx < 0 && InterSect.Value.x > A.x) || (B.dx < 0 && InterSect.Value.x > B.x)) 
                    {
                        continue;
                    }
                    if ((A.dy > 0 && InterSect.Value.y < A.y) || (B.dy > 0 && InterSect.Value.y < B.y))
                    {
                        continue;
                    }
                    if ((A.dy < 0 && InterSect.Value.y > A.y) || (B.dy < 0 && InterSect.Value.y > B.y))
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
            ((decimal x, decimal y) coordinate,(decimal dx, decimal dy) speed) Speed =  FindDX();

            
            

            for (decimal dz = 10; dz > -10; dz--) {
                List<Hailstone> List = ConvertSpeed(Speed.coordinate.x, Speed.coordinate.y, dz);
                Hailstone FirstStone = List[0];
                decimal Z = FirstStone.z - ((( FirstStone.x + Speed.coordinate.x )/ FirstStone.dx) * FirstStone.dz);
                bool Correctpoint = true;
                foreach (Hailstone TestStone in List)
                {
                    decimal ZTest = TestStone.z - ((( TestStone.x + Speed.coordinate.x) / TestStone.dx) * TestStone.dz);

                    if (!TestZ(Z, ZTest))
                    {
                        Correctpoint = false;
                        break;
                    }

                }
                if (Correctpoint) {
                    break;
                }
            }


            return answer;
        }

        
    }
}
