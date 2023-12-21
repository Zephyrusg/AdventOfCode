using System;
using System.Collections.Generic;
using System.Linq;
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

        HashSet<(int x, int y)> DostepP2((int x, int y) currentpoint)
        {

            HashSet<(int x, int y)> Newpoints = new();

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
                    NextStep.UnionWith(DostepP2(point));
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

            //HashSet<(int x, int y)> Points = new();
            //Points.Add(Start);
            //int times = 26501365;
            //int counter = 0;

            //while (counter < times)
            //{

            //    HashSet<(int x, int y)> NextStep = new();
            //    foreach (var point in Points)
            //    {
            //        NextStep.UnionWith(DostepP2(point));
            //    }

            //    Points = NextStep;
            //    counter++;
            //}

            //answer = Points.Count;




            return answer;
        }

    }
}
