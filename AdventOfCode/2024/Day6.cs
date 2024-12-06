using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class Y2024D6
    {
        public static string[] Lines = File.ReadAllLines(".\\2024\\Input\\inputDay6.txt");
        static int Height = Lines.Count();
        static int Width = Lines[0].Length;
        static char[,] map = new char[Width, Height];
        static HashSet<(int x, int y)> positions = new();
        static (int x, int y) startposition = new();
        
        public bool CheckOutofBound((int x, int y) newlocation)
        {
            if(newlocation.x < 0 || newlocation.x == Width || newlocation.y < 0 || newlocation.y == Height) return true;

            return false;
        }

        public (int x, int y) Moveright((int x ,int y) direction)
        {
            switch (direction)
            {
                case (0, -1):
                    direction = (1, 0);
                        break;
                case (1, 0):
                    direction = (0, 1);
                    break;
                case (0, 1):
                    direction = (-1, 0);
                    break;
                case (-1, 0):
                    direction = (0, -1);
                    break;
            }


            return direction;
        }

        public int Part1() 
        {
            int answer = 0;
            
            (int x, int y) direction = (0, -1);
            bool OnMap = true;
            for (int y = 0; y < Height; y++)
            {

                for (int x = 0; x < Width; x++)
                {
                    if(Lines[y][x] == '^') {
                        startposition = (x,y);
                        positions.Add(startposition);
                    }
                    map[y, x] = Lines[y][x];
                }
            }
            (int x, int y) currentposition = startposition;
            while (OnMap)
            {
                
                if (!positions.Contains(currentposition))
                {
                    positions.Add(currentposition);
                }
                (int x, int y) Nextlocation = (currentposition.x + direction.x, currentposition.y + direction.y);
                if (CheckOutofBound(Nextlocation))
                {
                    OnMap = false;
                } else {

                    while(map[Nextlocation.y, Nextlocation.x] == '#')
                    {
                        direction = Moveright(direction);
                        Nextlocation = (currentposition.x + direction.x, currentposition.y + direction.y);
                    }
                }
                currentposition = (currentposition.x + direction.x, currentposition.y + direction.y);
            }

            answer = positions.Count;

            return answer;
        }

        public int Part2()
        {
            int answer = 0;

            HashSet<(int x, int y)> possibleBlocks = positions; 
            possibleBlocks.Remove(startposition);
            Parallel.ForEach(possibleBlocks, possibleBlock =>
            {
            
                HashSet<((int x, int y), (int dx, int dy))> Looplist = new();

                bool OnMap = true;

                (int x, int y) currentposition = startposition;
                (int x, int y) direction = (0, -1);
                while (OnMap)
                {

                    if (Looplist.Contains((currentposition, direction)))
                    {
                        answer++;
                        break;
                    }
                    else
                    {
                        Looplist.Add((currentposition, direction));
                    }
                    (int x, int y) Nextlocation = (currentposition.x + direction.x, currentposition.y + direction.y);
                    if (CheckOutofBound(Nextlocation))
                    {
                        OnMap = false;
                    }
                    else
                    {
                        
                        while (map[Nextlocation.y, Nextlocation.x] == '#' || Nextlocation == possibleBlock)
                        {
                            direction = Moveright(direction);
                            Nextlocation = (currentposition.x + direction.x, currentposition.y + direction.y);
                        }
                    }
                    currentposition = (currentposition.x + direction.x, currentposition.y + direction.y);
                }
            });

            return answer;
        }

    }
}
