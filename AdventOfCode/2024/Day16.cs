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
        static (Point Point, int dir) endstate = new();
        static (Point Point, int dir) startstate = new();
        static Dictionary<(Point Point, int dir), List<(Point Point, int dir)>> PreviousDir = new();
        class Point
        {
            public int X { get; }
            public int Y { get; }

            public Point(int x, int y)
            {
                X = x;
                Y = y;

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
           
            var DistanceDir = new Dictionary<(Point Point, int dir),int>();
            var priorityList = new PriorityQueue<(Point Point, int dir), int>();
            var closedList = new HashSet<(Point Point, int dir)>();
            DistanceDir.Add((start, 0), 0);
            priorityList.Enqueue((start,0), 0);
            startstate = (start, 0);
            
            int[][] directions = { new[] { 0, 1 }, new[] { 1, 0 }, new[] { 0, -1 }, new[] { -1, 0 } }; // East, South, West, North


            while (priorityList.Count > 0)
            {
                var currentState = priorityList.Dequeue();
                Point current = currentState.Point;
                int Direction = currentState.dir;
                int currentDistance = DistanceDir[currentState];
                if (current.X == end.X && current.Y == end.Y)
                {
                    endstate = currentState;
                    answer = DistanceDir[currentState];
                    break;
                }

                for (int dir = 0; dir < directions.Length; dir++)
                {
                    var neighbourPoint = map[(current.Y + directions[dir][0]), (current.X + directions[dir][1])];
                    int newX = neighbourPoint.X;
                    int newY = neighbourPoint.Y;
                    int turnCost = Direction == dir ? 0 : 1000;

                    if (Lines[newY][newX] == '#')
                        continue;

                    int neighbourDistance = currentDistance + 1 + turnCost;

                    if (!DistanceDir.TryGetValue((neighbourPoint, dir), out int estimatedNeighbourTotalDistance) || neighbourDistance <= estimatedNeighbourTotalDistance)
                    {
                        if(!DistanceDir.ContainsKey((neighbourPoint, dir)) || (neighbourDistance < estimatedNeighbourTotalDistance)){
                            PreviousDir[(neighbourPoint, dir)] = [currentState];
                            DistanceDir[(neighbourPoint, dir)] = neighbourDistance;
                            priorityList.Enqueue((neighbourPoint, dir), neighbourDistance);
                        }
                        else
                        {
                            PreviousDir[(neighbourPoint, dir)].Add(currentState);
                        }

                    }
                }
                closedList.Add((current, Direction));
            }
            return answer;
        }

        public int Part2()
        {
            int answer = 0;
            var endPrevious = PreviousDir[endstate];
            HashSet<(int x, int y)> PointVisited = new();
            Queue<(Point Point , int dir)> Openlist = new();
            Openlist.Enqueue(endstate);
            while(Openlist.Count > 0)
            {
                var state = Openlist.Dequeue();
                PointVisited.Add((state.Point.X, state.Point.Y));
                if (state != startstate)
                {
                    foreach (var previousState in PreviousDir[state])
                    {

                        Openlist.Enqueue(previousState);
                    }
                }
            }

            answer = PointVisited.Count();
            return answer;
        
        }

    }
}
