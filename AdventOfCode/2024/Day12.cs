using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
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

           
            List<(int, int)> borderCells = new List<(int, int)>();

            int[] dx = { -1, 1, 0, 0 };
            int[] dy = { 0, 0, -1, 1 };

            while (queue.Count > 0)
            {
                var (x, y) = queue.Dequeue();
                area++;
                //borderCells.Add((x, y));

                for (int i = 0; i < 4; i++)
                {
                    int newx = x + dx[i];
                    int newy = y + dy[i];

                    if (newx < 0 || newx >= Width || newy < 0 || newy >= Height || gardenMap[newx][newy] != plantType)
                    {
                        // Count this side as part of the perimeter
                        borderCells.Add((newx,newy));
                    }
                    else if (!visited[newx, newy])
                    {
                        visited[newx, newy] = true;
                        queue.Enqueue((newx, newy));
                    }
                
                }
            }

            int sides = CountSides(borderCells, gardenMap);
            Console.WriteLine("Planttype: " + plantType + " Area: " + area + " Sides: " + sides);
            return (area, sides);
        }

        static int CountSides(List<(int x, int y)> borderCells, string[] gardenMap)
        {
            int sides = 0;
            var rowGroups = new Dictionary<int, List<int>>();
            var colGroups = new Dictionary<int, List<int>>();

            foreach (var (x, y) in borderCells)
            {
                if (!rowGroups.ContainsKey(x))
                    rowGroups[x] = new List<int>();
                if (!colGroups.ContainsKey(y))
                    colGroups[y] = new List<int>();

                rowGroups[x].Add(y);
                colGroups[y].Add(x);
            }
            List<(int x, int y)> Proccessed = new();
            // Count horizontal sides (row-wise)
            foreach (var row in rowGroups)
            {
                var cols = row.Value;
                if(cols.Count < 2)
                {
                    continue;
                }
                cols.Sort(); // Sort columns in the row
                int connectedCount = 0;
                // Count each contiguous segment as one side
                for (int i = 0; i < cols.Count-1; i++)
                {
                    if (cols[i] + 1 == cols[i + 1])
                    {
                        if (connectedCount == 0)
                        {
                            sides++;
                            
                            connectedCount++;
                        }
                        Proccessed.Add((row.Key, cols[i]));

                    }
                    else
                    {
                        connectedCount = 0;
                    }
                }

                if (cols[cols.Count-1] - 1 == cols[cols.Count -2])
                {
                    Proccessed.Add((row.Key, cols[cols.Count - 1]));
                }
            } 

            // Count vertical sides (column-wise)
            foreach (var col in colGroups)
            {
                var rows = col.Value;
                if(rows.Count < 2)
                {
                    continue;
                }
                rows.Sort(); // Sort rows in the column

                // Count each contiguous segment as one side
                rows.Sort(); // Sort columns in the row
                int connectedCount = 0;
                // Count each contiguous segment as one side
                for (int i = 0; i < rows.Count - 1; i++)
                {
                    if (rows[i] + 1 == rows[i + 1])
                    {
                        if (connectedCount == 0)
                        {
                            sides++;

                            connectedCount++;
                        }
                        Proccessed.Add((rows[i], col.Key));

                    }
                    else
                    {
                        connectedCount = 0;
                    }
                }
                if (rows[rows.Count - 1] - 1 == rows[rows.Count - 2])
                {
                    Proccessed.Add((rows[rows.Count - 1], col.Key));
                }
            }

            //var notproccessed = borderCells.Except(Proccessed).ToList();
            var notproccessed = borderCells.Where(item => !Proccessed.Any(item2 => item2 == item)).ToList();
            sides += notproccessed.Count;

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
