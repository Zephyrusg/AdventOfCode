using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class Y2024D18
    {
        public static string[] Lines = File.ReadAllLines(".\\2024\\Input\\inputDay18.txt");
        static int gridSize = 71;
        static bool[,] grid = new bool[gridSize, gridSize];

        static int WalkPathToExit()
        {
            (int dx, int dy)[] directions = new (int dx, int dy)[]
            {
                (1, 0), (0, 1), (-1, 0), (0, -1)
            };

            var queue = new Queue<(int x, int y, int step)>();
            queue.Enqueue((0, 0, 0));

            var visited = new HashSet<(int, int)>();
            visited.Add((0, 0));

            while (queue.Count > 0)
            {
                var (x, y, steps) = queue.Dequeue();


                if (x == gridSize - 1 && y == gridSize - 1)
                {
                    return steps;
                }

                foreach (var (dx, dy) in directions)
                {
                    int nx = x + dx, ny = y + dy;


                    if (nx >= 0 && nx < gridSize && ny >= 0 && ny < gridSize &&
                        !grid[nx, ny] && !visited.Contains((nx, ny)))
                    {
                        queue.Enqueue((nx, ny, steps + 1));
                        visited.Add((nx, ny));
                    }
                }
            }

            return -1;
        }

        public int Part1()
        {
          
            var corruptedCells = Lines.Take(1024)
                .Select(line => line.Split(',').Select(int.Parse).ToArray())
                .ToList();
          
            foreach (var cell in corruptedCells)
            {
                grid[cell[0], cell[1]] = true;
            }

            return WalkPathToExit();
        }

        public string Part2()
        {
            
            var RestcorruptedCells = Lines.Skip(1024)
                .Select(line => line.Split(',').Select(int.Parse).ToArray())
                .ToList();

            for (int i = 0; i < RestcorruptedCells.Count; i++)
            {
                var cell = RestcorruptedCells[i];
                grid[cell[0], cell[1]] = true;

                
                if (WalkPathToExit() == -1)
                {
                    
                    return $"{cell[0]},{cell[1]}";
                }
            }
            
            return "-1,-1";
        }
    }
}
