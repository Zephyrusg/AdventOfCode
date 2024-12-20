using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class Y2024D20
    {
        public static string[] Lines = File.ReadAllLines(".\\2024\\Input\\inputDay20.txt");

        public record Position(int X, int Y);
        static List<Position> Positions = new();
        static Dictionary<Position, int> distances = new Dictionary<Position, int>();
        public int Part1()
        {
            var map = Lines;
            var start = FindPosition(map, 'S');
            var end = FindPosition(map, 'E');

            var distances = CalculateDistances(map, end);
            var cheats = FindCheatOptions(map);

            return cheats.Count(c => c.TimeSaved >= 100);
        }

        static bool IsInBounds(string[] map, Position pos)
        {
            return pos.Y >= 0 && pos.Y < map.Length && pos.X >= 0 && pos.X < map[0].Length;
        }
        private Position FindPosition(string[] map, char target)
        {
            for (int y = 0; y < map.Length; y++)
            {
                int x = map[y].IndexOf(target);
                if (x >= 0) return new Position(x, y);
            }
            throw new Exception("Target not found");
        }

        private Dictionary<Position, int> CalculateDistances(string[] map, Position end)
        {
            int height = map.Length;
            int width = map[0].Length;

            var queue = new Queue<Position>();
            queue.Enqueue(end);
            Positions.Add(end);
            distances.Add(end, 0);

            var directions = new[] { new Position(0, 1), new Position(1, 0), new Position(0, -1), new Position(-1, 0) };

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();

                foreach (var dir in directions)
                {
                    var next = new Position(current.X + dir.X, current.Y + dir.Y);
                    if (IsInBounds(map, next) && map[next.Y][next.X] != '#' && !distances.ContainsKey(next))
                    {
                        Positions.Add(next);
                        distances.Add(next, distances[current] + 1);
                        queue.Enqueue(next);
                    }
                }
            }

            return distances;
        }
        
        private List<(Position Start, Position End, int TimeSaved)> FindCheatOptions(string[] map)
        {
            var cheats = new List<(Position Start, Position End, int TimeSaved)>();

            foreach (var cheatStart in Positions)
            {
                foreach (var cheatEnd in Positions)
                {
                    if (cheatStart == cheatEnd) continue;

                    if (!IsValidCheat(map, cheatStart, cheatEnd)) continue;

                    int distanceAfterCheat = distances[cheatEnd] + 2;
                    int directDistance = distances[cheatStart];

                    if (distanceAfterCheat < directDistance)
                    {
                        int timeSaved = directDistance - distanceAfterCheat;
                        cheats.Add((cheatStart, cheatEnd, timeSaved));
                    }
                }
            }

            return cheats;
        }

        private Dictionary<int , int > FindCheatOptionsV2(string[] map)
        {
            var cheats = new Dictionary<int, int>();

            foreach (var cheatStart in Positions)
            {
                foreach (var cheatEnd in Positions)
                {
                    if (cheatStart == cheatEnd) continue;

                    int manhattanDistance = Math.Abs(cheatStart.X - cheatEnd.X) + Math.Abs(cheatStart.Y -  cheatEnd.Y);

                    if (manhattanDistance > 20) continue;

                    int distanceAfterCheat = distances[cheatEnd] + manhattanDistance;
                    int directDistance = distances[cheatStart];

                    if (distanceAfterCheat < directDistance)
                    {
                        int timeSaved = directDistance - distanceAfterCheat;
                        if (cheats.ContainsKey(timeSaved)) cheats[timeSaved]++;
                        else cheats.Add(timeSaved,1);
                    }
                }
            }

            return cheats;
        }


        private bool IsValidCheat(string[] map, Position cheatStart, Position cheatEnd)
        {
            // Ensure cheatStart and cheatEnd are aligned either horizontally or vertically
            if (cheatStart.X != cheatEnd.X && cheatStart.Y != cheatEnd.Y) return false;

            // Ensure the positions differ by exactly 2
            if (Math.Abs(cheatStart.X - cheatEnd.X) == 2 && cheatStart.Y == cheatEnd.Y)
            {
                // Check the wall between them horizontally
                int middleX = (cheatStart.X + cheatEnd.X) / 2;
                return map[cheatStart.Y][middleX] == '#';
            }
            else if (Math.Abs(cheatStart.Y - cheatEnd.Y) == 2 && cheatStart.X == cheatEnd.X)
            {
                // Check the wall between them vertically
                int middleY = (cheatStart.Y + cheatEnd.Y) / 2;
                return map[middleY][cheatStart.X] == '#';
            }

            return false; // Not a valid cheat
        }

        public int Part2()
        {
            
            var map = Lines;
            var cheats = FindCheatOptionsV2(map);
            return cheats.Where(c=>c.Key >= 100).Sum(c=> c.Value);
          

        }
    }
}
