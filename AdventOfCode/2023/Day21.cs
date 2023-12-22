using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Drawing;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class Y2023D21
    {
        public static string[] Lines = File.ReadAllLines(".\\2023\\Input\\inputDay21.txt");
        static int Height = Lines.Count();
        static int Width = Lines[0].Length;
        static bool[,] map = new bool[Width, Height];
        static (int x, int y) Start;

        HashSet<(int x, int y)> Dostep((int x,int y) currentpoint)
        {

            HashSet<(int x, int y)> Newpoints = new();

            if (currentpoint.x > 0 && map[currentpoint.x - 1, currentpoint.y] == true) {
                Newpoints.Add((currentpoint.x - 1, currentpoint.y));
            }
            if (currentpoint.x < Width - 1 && map[currentpoint.x + 1, currentpoint.y] == true)
            {
                Newpoints.Add((currentpoint.x + 1, currentpoint.y));
            }
            if (currentpoint.y > 0 && map[currentpoint.x, currentpoint.y - 1] == true)
            {
                Newpoints.Add((currentpoint.x, currentpoint.y - 1));
            }
            if (currentpoint.y < Height - 1 && map[currentpoint.x, currentpoint.y + 1] == true)
            {
                Newpoints.Add((currentpoint.x, currentpoint.y + 1));
            }

            return Newpoints;
        }

        List<(int x, int y)> DostepP2((int x, int y) currentpoint)
        {

            List<(int x, int y)> Newpoints = new();

            (int x, int y) Testpoint = currentpoint; ;
            int x = currentpoint.x;
            int y = currentpoint.y;
            if (currentpoint.x < 0)
            {
                x = Width - ((currentpoint.x * -1) % Width);
                if(x == Width) { x = 0; }
            }
            if(currentpoint.x >= Width)
            {
                x = currentpoint.x % Width;
            }

            if(currentpoint.y < 0)
            {
                y = Height - ((currentpoint.y * -1) % Height);
                if (y == Height) { y = 0; }
            }

            if (currentpoint.y >= Height)
            {
                y = currentpoint.y % Height;
            }

            Testpoint = (x,y);


            if (Testpoint.x == 0 && map[Width - 1, Testpoint.y] == true)
            {
                Newpoints.Add((currentpoint.x - 1, currentpoint.y));
            }
            else if (Testpoint.x > 0 && map[Testpoint.x - 1, Testpoint.y] == true)
            {
                Newpoints.Add((currentpoint.x - 1, currentpoint.y));
            }
            if (Testpoint.x == Width - 1 && map[0, Testpoint.y] == true)
            {
                Newpoints.Add((currentpoint.x + 1, currentpoint.y));
            }
            else if (Testpoint.x < Width - 1 && map[Testpoint.x + 1, Testpoint.y] == true)
            {
                Newpoints.Add((currentpoint.x + 1, currentpoint.y));
            }
            if (Testpoint.y == 0 && map[Testpoint.x, Height - 1] == true)
            {
                Newpoints.Add((currentpoint.x, currentpoint.y - 1));
            }
            else if (Testpoint.y > 0 && map[Testpoint.x, Testpoint.y - 1] == true)
            {
                Newpoints.Add((currentpoint.x, currentpoint.y - 1));
            }

            if (Testpoint.y == Height - 1 && map[Width - 1, 0] == true)
            {
                Newpoints.Add((currentpoint.x, currentpoint.y + 1));
            }
            else if (Testpoint.y < Height - 1 && map[Testpoint.x, Testpoint.y + 1] == true)
            {
                Newpoints.Add((currentpoint.x, currentpoint.y + 1));
            }
            
            
           

            return Newpoints;
        }

        static long Formule(int a, int b, int c, int n) {

            return a * (long)(Math.Pow(n, 2)) + b * n + c;
        }

        public int Part1() 
        {
            int answer = 0;

            HashSet<(int x, int y)> Points = new();


            for (int y = 0; y < Height; y++)
            {

                for (int x = 0; x < Width; x++)
                {
                    switch (Lines[y][x])
                    {
                        case '.':
                            map[x, y] = true;
                            break;
                        case '#':
                            break;
                        case 'S':
                            map[x, y] = true;
                            Start = (x, y);
                            Points.Add(Start);

                            break;
                    }
             
                }
            }

            int times = 64;
            int counter = 0;

            while (counter < times) { 
            
                HashSet<(int x, int y)> NextStep = new();
                foreach(var point in Points)
                {
                    NextStep.UnionWith(Dostep(point));
                }

                Points = NextStep;
                counter++;
            }

            answer = Points.Count;




            return answer;
        }

        public long Part2()
        {
            long answer = 0;
            int times = 26501365;

            int Maps = times / Width;
            int remaining = times % Width;
            // List<int> sequence = new List<int>();
            // Sequence : Remaining , Width + Remaining, Width * 2 + remaining, ...)
            ConcurrentBag<int> sequence = new();
            Parallel.For(0, 3, (n) =>
            {
                HashSet<(int x, int y)> Points = new();
                Points.Add(Start);
                for (int s = 0; s < (n * Width + remaining); s++)
                {

                    HashSet<(int x, int y)> NextStep = new();
                    foreach (var point in Points)
                    {
                        foreach(var  tempPoint in DostepP2(point))
                        {
                            if (!NextStep.Contains(tempPoint))
                            {
                                NextStep.Add(tempPoint);
                            }
                        }
      
                    }

                    Points = NextStep;

                }
                sequence.Add(Points.Count);
            });
            List<int> sequencelist = sequence.ToList();
            sequencelist.Sort();
            // Sequence[0] = Remaining
            int c = sequencelist[0];
            //Sequence[1] (A* (1*1) + 1 * B) - c
            int A1B1 = sequencelist [1] - c;
            //Sequence[1] (A* (2*2) + 2 * B) - c
            int A4B2 = sequencelist[2] - c;
            int A2 = A4B2 - (2 * A1B1);
            int a = A2 / 2;
            int b = A1B1 - a;

            answer = Formule(a, b, c, Maps);

            return answer;
        }

    }
}
