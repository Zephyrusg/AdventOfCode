using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class Y2024D16
    {
        public static string[] Lines = File.ReadAllLines(".\\2024\\Input\\inputDay16.txt");
        static int Height = Lines.Count();
        static int Width = Lines[0].Length;
        static Point[,] map = new Point[Height, Width];
        class Point
        {
            public int X { get; }
            public int Y { get; }
            public int Distance { get; set; }
            public int EstimatedDistance { get; set; }
            public int Direction { get; set; } // 0 = East, 1 = South, 2 = West, 3 = North

            public Point(int x, int y)
            {
                X = x;
                Y = y;
                Distance = int.MaxValue;
                EstimatedDistance = int.MaxValue;
                Direction = -1; // Unknown initial direction
            }

            public override bool Equals(object obj)
            {
                return obj is Point other && X == other.X && Y == other.Y;
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(X, Y);
            }

        }
        public int Part1()
        {
            int answer = 0;


            int rows = Lines.Length;
            int cols = Lines[0].Length;
            int hNumber = 2000;
            Point start = null, end = null;

            // Parse the input
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < cols; x++)
                {
                    map[y, x] = new Point(x, y);
                    if (Lines[y][x] == 'S') start = map[y, x];
                    if (Lines[y][x] == 'E') end = map[y, x];
                }
            }

            // Initialize start point
            start.Distance = 0;
            start.EstimatedDistance = 0;
            start.Direction = 0; // Initially facing East

            // A* search
            var openList = new HashSet<Point>();
            var priorityList = new PriorityQueue<Point, int>();
            var closedList = new HashSet<Point>();
            openList.Add(start);
            priorityList.Enqueue(start, start.EstimatedDistance);

            int[][] directions = { new[] { 0, 1 }, new[] { 1, 0 }, new[] { 0, -1 }, new[] { -1, 0 } }; // East, South, West, North


            while (priorityList.Count > 0)
            {
                Point current = priorityList.Dequeue();
                
                if (current == end)
                {
                    answer = current.Distance;
                    Console.WriteLine($"Lowest score: {current.Distance}");
                    break;
                }

                for (int dir = 0; dir < directions.Length; dir++)
                {

                    int newX = current.X + directions[dir][0];
                    int newY = current.Y + directions[dir][1];

                    // Check boundaries and walls
                    if (newX < 0 || newX >= cols || newY < 0 || newY >= rows || Lines[newY][newX] == '#')
                        continue;

                    Point neighbourPoint = map[newY, newX];
                    int turnCost = current.Direction == -1 || current.Direction == dir ? 0 : 1000;
                    int neighbourDistance = current.Distance + 1 + turnCost; // Moving forward costs 1, turning costs 1000 if needed
                    int estimatedNeighbourTotalDistance = neighbourDistance + Math.Abs(end.X - neighbourPoint.X) + Math.Abs(end.Y - neighbourPoint.Y) * hNumber;


                    if (openList.Contains(neighbourPoint))
                    {
                        if (estimatedNeighbourTotalDistance < neighbourPoint.EstimatedDistance)
                        {
                            neighbourPoint.Distance = neighbourDistance;
                            neighbourPoint.EstimatedDistance = estimatedNeighbourTotalDistance;
                            continue;
                        }
                        else
                        {
                            continue;
                        }
                    }

                    if (closedList.Contains(neighbourPoint))
                    {
                        if (estimatedNeighbourTotalDistance < neighbourPoint.EstimatedDistance)
                        {
                            closedList.Remove(neighbourPoint);
                        }
                        else
                        {
                            continue;
                        }
                    }


                    neighbourPoint.Distance = neighbourDistance;
                    neighbourPoint.EstimatedDistance = estimatedNeighbourTotalDistance;
                    neighbourPoint.Direction = dir;
                    priorityList.Enqueue(neighbourPoint, neighbourPoint.Distance);


                }
                closedList.Add(current);
                openList.Remove(current);
                
            }
            return answer;
        }

        public int Part2()
        {
            int answer = 0;




            return answer;
        }

    }
}
