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

            return cheats;
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
        
        private int FindCheatOptions(string[] map)
        {
            var timeSaved = 0;
            var directions = new[] { new Position(0, 2), new Position(2, 0), new Position(0, -2), new Position(-2, 0) };
            foreach (var cheatStart in Positions)
            {
                for (int i = 0; i < directions.Count(); i++)
                {

                    var cheatEnd = new Position(cheatStart.X + directions[i].X, cheatStart.Y + directions[i].Y);
                    if (Positions.Contains(cheatEnd)){ 

                        int distanceAfterCheat = distances[cheatEnd] + 2;
                        int directDistance = distances[cheatStart];

                        if(directDistance - distanceAfterCheat >= 100)
                        {
                            timeSaved ++;
                            
                        }
                    }
                }
                
            }

            return timeSaved;
        }

        private int FindCheatOptionsV2(string[] map)
        {
            var timeSaved = 0;

            foreach (var cheatStart in Positions)
            {
                List<Position> CheatsToTest = Positions.Where(cheatEnd => Math.Abs(cheatStart.X - cheatEnd.X) + Math.Abs(cheatStart.Y - cheatEnd.Y) <= 20).ToList();
                foreach (var cheatEnd in CheatsToTest)
                {

                    if (cheatStart == cheatEnd) continue;

                    int manhattanDistance = Math.Abs(cheatStart.X - cheatEnd.X) + Math.Abs(cheatStart.Y -  cheatEnd.Y);

                    if (manhattanDistance > 20) continue;

                    int distanceAfterCheat = distances[cheatEnd] + manhattanDistance;
                    int directDistance = distances[cheatStart];

                    if (directDistance - distanceAfterCheat >= 100)
                    {
                        timeSaved ++;
                    }
                }
            }

            return timeSaved;
        }

        public int Part2()
        {
            
            var map = Lines;
            var cheats = FindCheatOptionsV2(map);
            return cheats;
          

        }
    }
}
