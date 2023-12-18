using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace AdventOfCode
{
    internal class Y2023D18
    {
        public static string[] Lines = File.ReadAllLines(".\\2023\\Input\\inputDay18.txt");
        static int Height = Lines.Count();
        static int Width = Lines[0].Length;
        static char[,] map = new char[Width, Height];
        public int Part1() 
        {
            int answer = 0;
            HashSet<(int x, int y)> EdgePoints = new();
            (int x, int y) current = (0, 0);
            foreach (var line in Lines)
            {
                string direction = line.Split(' ')[0];
                int meters = int.Parse(line.Split(' ')[1]);
                switch (direction)
                {
                    case "U":
                        for (int m = 0; m < meters; m++)
                        {
                            current = (current.x, current.y - 1);
                            EdgePoints.Add(current);
                        }
                        break;
                    case "D":
                        for (int m = 0; m < meters; m++)
                        {
                            current = (current.x, current.y + 1);
                            EdgePoints.Add(current);
                        }
                        break;
                    case "L":
                        for (int m = 0; m < meters; m++)
                        {
                            current = (current.x - 1, current.y);
                            EdgePoints.Add(current);
                        }
                        break;
                    case "R":
                        for (int m = 0; m < meters; m++)
                        {
                            current = (current.x + 1, current.y);
                            EdgePoints.Add(current);
                        }
                        break;
                }
            }

            Width = EdgePoints.Max(x=> x.x) - EdgePoints.Min(x => x.x);
            Height = EdgePoints.Max(y => y.y) - EdgePoints.Min(y => y.y);
            int MinWidth = Math.Abs(EdgePoints.Min(x => x.x));
            int MinHeight = Math.Abs(EdgePoints.Min(y => y.y));

            map = new char[Width+1, Height+1];
            for (int y = 0; y <= Height; y++)
            {
                for (int x = 0; x <= Width; x++)
                {
                    map[x, y] = '.';
                }
                
            }

            foreach ((int x, int y)  in EdgePoints)
            {
                map[x+MinWidth, y+ MinHeight] = '#';
            }
            (int x, int y) insideStartPoint = (0,0);
            for(int x=0; x <= Width; x++)
            {
                if (map[x,0] == '#')
                {
                    insideStartPoint = (x+1, 1);
                    break;
                }

            }
            HashSet<(int x, int y)> Insides = new();
            Insides.Add(insideStartPoint);
            while (Insides.Count > 0)
            {
                HashSet<(int x, int y)> NewInsides = new();
                foreach ((int x , int y) in Insides)
                {
                   
                    map[x, y] = '#';
                    if (x-1 >= 0 &&      map[x - 1, y] == '.') { NewInsides.Add((x - 1, y)); }
                    if (x+1 <= Width &&  map[x + 1, y] == '.') { NewInsides.Add((x + 1, y)); }
                    if (y+1 <= Height && map[x, y + 1] == '.') { NewInsides.Add((x, y + 1)); }
                    if (y-1 >= 0 &&      map[x, y - 1] == '.') { NewInsides.Add((x, y - 1)); }

                }
                Insides = NewInsides;
            }


            //Console.WriteLine();
            //for (int y = 0; y <= Height; y++)
            //{
            //    for (int x = 0; x <= Width; x++)
            //    {
            //        Console.Write(map[x, y]);
            //    }
            //    Console.WriteLine();
            //}

            for (int y = 0; y <= Height; y++)
            {
                for (int x = 0; x <= Width; x++)
                {
                    if (map[x, y] == '#')
                    {
                        answer++;
                    }
                }
                
            }





            return answer;
        }

        public int Part2()
        {
            int answer = 0;
            HashSet<(int x, int y)> EdgePoints = new();
            (int x, int y) current = (0, 0);
            foreach (var line in Lines)
            {
                int meters = int.Parse(((line.Split(' ')[2])[2..7]), NumberStyles.HexNumber);
                char direction = (line.Split(' ')[2])[7];
                switch (direction)
                {
                    case '3':
                        for (int m = 0; m < meters; m++)
                        {
                            current = (current.x, current.y - 1);
                            EdgePoints.Add(current);
                        }
                        break;
                    case '1':
                        for (int m = 0; m < meters; m++)
                        {
                            current = (current.x, current.y + 1);
                            EdgePoints.Add(current);
                        }
                        break;
                    case '2':
                        for (int m = 0; m < meters; m++)
                        {
                            current = (current.x - 1, current.y);
                            EdgePoints.Add(current);
                        }
                        break;
                    case '0':
                        for (int m = 0; m < meters; m++)
                        {
                            current = (current.x + 1, current.y);
                            EdgePoints.Add(current);
                        }
                        break;
                }
            }


            Width = EdgePoints.Max(x => x.x) - EdgePoints.Min(x => x.x);
            Height = EdgePoints.Max(y => y.y) - EdgePoints.Min(y => y.y);
            //int MinWidth = Math.Abs(EdgePoints.Min(x => x.x));
            //int MinHeight = Math.Abs(EdgePoints.Min(y => y.y));

            bool[,]map = new bool[Width + 1, Height + 1];
            //for (int y = 0; y <= Height; y++)
            //{s
            //    for (int x = 0; x <= Width; x++)
            //    {
            //        map[x, y] = '.';
            //    }

            //}

            foreach ((int x, int y) in EdgePoints)
            {
                map[x , y] = true;
            }
            (int x, int y) insideStartPoint = (0, 0);
            for (int x = 0; x <= Width; x++)
            {
                if (map[x, 0] == true)
                {
                    insideStartPoint = (x + 1, 1);
                    break;
                }

            }
            HashSet<(int x, int y)> Insides = new();
            Insides.Add(insideStartPoint);
            while (Insides.Count > 0)
            {
                HashSet<(int x, int y)> NewInsides = new();
                foreach ((int x, int y) in Insides)
                {

                    map[x, y] = true;
                    if (x - 1 >= 0 && map[x - 1, y] == false) { NewInsides.Add((x - 1, y)); }
                    if (x + 1 <= Width && map[x + 1, y] == false) { NewInsides.Add((x + 1, y)); }
                    if (y + 1 <= Height && map[x, y + 1] == false) { NewInsides.Add((x, y + 1)); }
                    if (y - 1 >= 0 && map[x, y - 1] == false) { NewInsides.Add((x, y - 1)); }

                }
                Insides = NewInsides;
            }


            for (int y = 0; y <= Height; y++)
            {
                for (int x = 0; x <= Width; x++)
                {
                    if (map[x, y] == true)
                    {
                        answer++;
                    }
                }

            }





            return answer;




        }

    }
}
