using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class Y2024D12
    {
        public static string[] gardenMap = File.ReadAllLines(".\\2024\\Input\\inputDay12.txt");
        static int Height = gardenMap.Count();
        static int Width = gardenMap[0].Length;

        static (int, int) ExploreRegionV2(string[] gardenMap, bool[,] visited, int startx, int starty)
        {
            char plantType = gardenMap[startx][starty];
            int area = 0;

            Queue<(int, int)> queue = new Queue<(int, int)>();
            queue.Enqueue((startx, starty));
            visited[startx, starty] = true;

            // Store all cells in the region
            List<(int, int)> regionCells = new List<(int, int)>();

            int[] dx = { -1, 1, 0, 0 };
            int[] dy = { 0, 0, -1, 1 };

            while (queue.Count > 0)
            {
                var (x, y) = queue.Dequeue();
                area++;
                regionCells.Add((x, y));

                for (int i = 0; i < 4; i++)
                {
                    int newx = x + dx[i];
                    int newy = y + dy[i];

                    if (newx >= 0 && newx < Height && newy >= 0 && newy < Width && gardenMap[newx][newy] == plantType && !visited[newx, newy])
                    {
                        visited[newx, newy] = true;
                        queue.Enqueue((newx, newy));
                    }
                }
            }

            // Calculate sides
            int sides = CountSides(regionCells, gardenMap);
            Console.WriteLine("Planttype: " + plantType + " Area: " + area + " Sides: " + sides);
            return (area, sides);
        }

        static int CountSides(List<(int, int)> regionCells, string[] gardenMap)
        {
            int sides = 0;

            // Track which rows and columns to process
            var rows = new Dictionary<int, List<int>>();
            var cols = new Dictionary<int, List<int>>();

            foreach (var (x, y) in regionCells)
            {
                if (!rows.ContainsKey(x))
                    rows[x] = new List<int>();
                if (!cols.ContainsKey(y))
                    cols[y] = new List<int>();

                rows[x].Add(y);
                cols[y].Add(x);
            }

            // Count horizontal sides
            foreach (var row in rows)
            {
                var sortedCols = row.Value;
                sortedCols.Sort();

                // Count gaps and edges as sides
                for (int i = 0; i < sortedCols.Count; i++)
                {
                    if (i == 0 || sortedCols[i] != sortedCols[i - 1] + 1)
                        sides++; // Start of a new horizontal segment
                }
            }

            // Count vertical sides
            foreach (var col in cols)
            {
                var sortedRows = col.Value;
                sortedRows.Sort();

                // Count gaps and edges as sides
                for (int i = 0; i < sortedRows.Count; i++)
                {
                    if (i == 0 || sortedRows[i] != sortedRows[i - 1] + 1)
                        sides++; // Start of a new vertical segment
                }
            }

            return sides;
        }


        static (int, int) ExploreRegion(string[] gardenMap, bool[,] visited, int startx, int starty)
        {
            char plantType = gardenMap[startx][starty];
            int area = 0;
            int perimeter = 0;
            Queue<(int, int)> queue = new Queue<(int, int)>();
            queue.Enqueue((startx, starty));
            visited[startx, starty] = true;

            int[] dx = { -1, 1, 0, 0 };
            int[] dy = { 0, 0, -1, 1 };

            while (queue.Count > 0)
            {
                var (x, y) = queue.Dequeue();
                area++;

                for (int i = 0; i < 4; i++)
                {
                    int newx = x + dx[i];
                    int newy = y + dy[i];

                    if (newx < 0 || newx >= Width || newy < 0 || newy >= Height || gardenMap[newx][newy] != plantType)
                    {
                        // Count this side as part of the perimeter
                        perimeter++;
                    }
                    else if (!visited[newx, newy])
                    {
                        visited[newx, newy] = true;
                        queue.Enqueue((newx, newy));
                    }
                }
            }

            return (area, perimeter);
        }
        public int Part1() 
        {
           

            bool[,] visited = new bool[Width, Height];
            int totalCost = 0;

            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    if (!visited[i, j])
                    {
                        (int area, int perimeter) = ExploreRegion(gardenMap, visited, i, j);
                        totalCost += area * perimeter;
                    }
                }
            }


            return totalCost;
        }

        public int Part2()
        {
            bool[,] visited = new bool[Width, Height];
            int totalCost = 0;

            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    if (!visited[i, j])
                    {
                        (int area, int sides) = ExploreRegionV2(gardenMap, visited, i, j);
                        totalCost += area * sides;
                    }
                }
            }

            return totalCost;
        }

    }
}
