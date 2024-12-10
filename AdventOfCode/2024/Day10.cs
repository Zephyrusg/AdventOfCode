using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class Y2024D10
    {
        public static string[] Lines = File.ReadAllLines(".\\2024\\Input\\inputDay10.txt");
        static int Height = Lines.Count();
        static int Width = Lines[0].Length;
        static int[,] map = new int[Width, Height];
        static List<(int x, int y)> trailheads = new List<(int, int)>();

        static int GetTrailheadScore(int startX, int startY)
        {
           
            HashSet<(int, int)> reachableNines = new HashSet<(int, int)>();
            bool[,] visited = new bool[Width, Height];

           
            void Walk(int x, int y)
            {
                if (x < 0 || x >= Width || y < 0 || y >= Height || visited[x, y])
                    return;

                visited[x, y] = true;

                if (map[x, y] == 9)
                {
                    reachableNines.Add((x, y));
                    return;
                }

                int currentHeight = map[x, y];

                int[] dx = { -1, 1, 0, 0 };
                int[] dy = { 0, 0, -1, 1 };

                for (int i = 0; i < 4; i++)
                {
                    int nx = x + dx[i];
                    int ny = y + dy[i];
                    if (nx >= 0 && nx < Width && ny >= 0 && ny < Height && map[nx, ny] == currentHeight + 1)
                    {
                        Walk(nx, ny);
                    }
                }
            }

            Walk(startX, startY);

            return reachableNines.Count;
        }

        static int GetTrailheadRating( int startX, int startY)
        {
            
            var cache = new Dictionary<(int, int), int>();

          
            int CountPaths(int x, int y)
            {
                if (x < 0 || x >= Width || y < 0 || y >= Height) return 0; 
                if (map[x, y] == 9) return 1; 

                if (cache.ContainsKey((x, y))) return cache[(x, y)]; 

                int currentHeight = map[x, y];
                int[] dx = { -1, 1, 0, 0 };
                int[] dy = { 0, 0, -1, 1 };

                int paths = 0;

                
                for (int i = 0; i < 4; i++)
                {
                    int nx = x + dx[i];
                    int ny = y + dy[i];

                    if (nx >= 0 && nx < Width && ny >= 0 && ny < Height && map[nx, ny] == currentHeight + 1)
                    {
                        paths += CountPaths(nx, ny);
                    }
                }

                cache[(x, y)] = paths;
                return paths;
            }

            return CountPaths(startX, startY);
        }

        public int Part1() 
        {
            int answer = 0;

           
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    map[i, j] = int.Parse(Lines[i][j].ToString());
                }
            }

            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    if (map[i, j] == 0)
                    {
                        trailheads.Add((i, j));
                    }
                }
            }

            foreach (var trailhead in trailheads)
            {
                answer += GetTrailheadScore(trailhead.x, trailhead.y);
            }

            return answer;
        }

        public int Part2()
        {
            int answer = 0;

            foreach (var trailhead in trailheads)
            {
                answer += GetTrailheadRating(trailhead.x, trailhead.y);
            }

            return answer;
        }

    }
}
