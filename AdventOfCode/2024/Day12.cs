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

           
            List<(int, int, int)> borderCells = new List<(int, int, int)>();

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
                        borderCells.Add((newx,newy, i));
                    }
                    else if (!visited[newx, newy])
                    {
                        visited[newx, newy] = true;
                        queue.Enqueue((newx, newy));
                    }
                
                }
            }

            int sides = CountSides(borderCells, gardenMap);
            //Console.WriteLine("Planttype: " + plantType + " Area: " + area + " Sides: " + sides);
            return (area, sides);
        }

        static int CountSides(List<(int x, int y, int d)> borderCells, string[] gardenMap)
        {
            int sides = 0;
            var rowGroups = new Dictionary<int, List<(int, int)>>();
            var colGroups = new Dictionary<int, List<(int, int)>>();

            foreach (var (x, y, d) in borderCells)
            {
                if (!rowGroups.ContainsKey(x))
                    rowGroups[x] = new List<(int, int)>();
                if (!colGroups.ContainsKey(y))
                    colGroups[y] = new List<(int, int)>();

                rowGroups[x].Add((y, d));
                colGroups[y].Add((x, d));
            }
            List<(int x, int y, int d)> Proccessed = new();
            // Count horizontal sides (row-wise)
            foreach (var row in rowGroups)
            {
                List<(int c, int d)> cols = row.Value;
                if (cols.Count < 2)
                {
                    continue;
                }
                cols = cols.OrderBy(c => c.d).ThenBy(c => c.c).ToList(); // Sort columns in the row
                for (int d = 0; d < 4; d++)
                {
                    var c = cols.Where(c => c.d == d).ToList();
                    int connectedCount = 0;
                    // Count each contiguous segment as one side
                    if (c.Count > 1)
                    {
                        for (int i = 0; i < c.Count - 1; i++)
                        {
                            if (c[i].c + 1 == c[i + 1].c)
                            {
                                if (connectedCount == 0)
                                {
                                    sides++;

                                    connectedCount++;
                                }
                                Proccessed.Add((row.Key, c[i].c, d));

                            }
                            else if (connectedCount > 0 && c[i - 1].c + 1 == c[i].c)
                            {
                                Proccessed.Add((row.Key, c[i].c, d));
                                connectedCount = 0;
                            }
                            else
                            {
                                connectedCount = 0;
                            }
                        }

                        if (connectedCount > 0 && c[c.Count - 1].c - 1 == c[c.Count - 2].c)
                        {
                            Proccessed.Add((row.Key, c[c.Count - 1].c, d));
                        }
                    }
                }

            }
                // Count vertical sides (column-wise)
            foreach (var col in colGroups)
            {
                List<(int r, int d)> rows = col.Value;
                if (rows.Count < 2)
                {
                    continue;
                }
                rows = rows.OrderBy(r => r.d).ThenBy(r => r.r).ToList(); ; // Sort rows in the column
                for (int d = 0; d < 4; d++)
                {
                    var r = rows.Where(r => r.d == d).ToList();

                    if (r.Count > 1)
                    {
                        int connectedCount = 0;
                        // Count each contiguous segment as one side
                        if (r.Count > 1)
                        {
                            for (int i = 0; i < r.Count - 1; i++)
                            {
                                if (r[i].r + 1 == r[i + 1].r)
                                {
                                    if (connectedCount == 0)
                                    {
                                        sides++;

                                        connectedCount++;
                                    }
                                    Proccessed.Add((r[i].r, col.Key, d));

                                }
                                else if(connectedCount > 0 && r[i-1].r +1 == r[i].r)
                                {
                                    Proccessed.Add((r[i].r, col.Key, d));
                                    connectedCount = 0;
                                }
                                else
                                {
                                    connectedCount = 0;
                                }
                            }

                        }
                        if (connectedCount > 0 && r[r.Count - 1].r - 1 == r[r.Count - 2].r)
                        {
                            Proccessed.Add((r[r.Count - 1].r, col.Key, d));
                        }
                    }
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
