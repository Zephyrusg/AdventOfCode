using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class Y2024D21
    {
        private static readonly char[,] NumericKeypad = {
        { '7', '8', '9' },
        { '4', '5', '6' },
        { '1', '2', '3' },
        { ' ', '0', 'A' }
    };

        private static readonly char[,] DirectionalKeypad = {
        { ' ', '^', 'A' },
        { '<', 'v', '>' }
    };

        private static readonly (int Row, int Col) NumericStart = (3, 2); // Starting at 'A'
        private static readonly (int Row, int Col) DirectionalStart = (0, 2); // Starting at 'A'

        public int Part1()
        {
            var codes = new List<string> { "029A", "980A", "179A", "456A", "379A" };
            int totalComplexity = 0;

            foreach (var code in codes)
            {
               
                string robot2ToRobot1 = CalculateRobot2ToRobot1(code);

                
                string robot3ToRobot2 = CalculateRobot3ToRobot2(robot2ToRobot1);

                
                int mySequenceLength = CalculateSequenceLength(robot3ToRobot2).Length;

                
                int numericValue = int.Parse(code.TrimStart('0').TrimEnd('A'));
                totalComplexity += mySequenceLength * numericValue;
            }

            return totalComplexity;
        }

        private string CalculateRobot2ToRobot1(string code)
        {
            
            var sequence = new List<string>();
            var currentPos = NumericStart;

            foreach (char target in code)
            {
                string path = GenerateShortestSequence(currentPos, target, NumericKeypad);
                sequence.Add(path);
                currentPos = FindPosition(NumericKeypad, target);
            }

            return string.Join("", sequence); // Combine all sequences
        }

        private string CalculateRobot3ToRobot2(string robot2Commands)
        {
            
            var sequence = new List<string>();
            var currentPos = DirectionalStart;

            foreach (char target in robot2Commands)
            {
                string path = GenerateShortestSequence(currentPos, target, DirectionalKeypad);
                sequence.Add(path);
                currentPos = FindPosition(DirectionalKeypad, target);
            }

            return string.Join("", sequence); 
        }

        private string CalculateSequenceLength(string robot3commands)
        {
           
            var sequence = new List<string>();
            var currentPos = DirectionalStart;

            foreach (char target in robot3commands)
            {
                string path = GenerateShortestSequence(currentPos, target, DirectionalKeypad);
                sequence.Add(path);
                currentPos = FindPosition(DirectionalKeypad, target);
            }

            return string.Join("", sequence);
        }

        static string GetPathLeastVariation(List<string> ShortestPaths)
        {
            string path = "";

            Dictionary<string, int> Variants = new();
            foreach(var ShortestPath in ShortestPaths)
            {
                int Tempvariant = 0;
                for (int i = 0; i < ShortestPath.Length - 1; i++)
                {
                    if (ShortestPath[i] != ShortestPath[i + 1])
                    {
                        Tempvariant++;
                    }
                }
                Variants.Add(ShortestPath, Tempvariant);
            }

            path = Variants.MinBy(p => p.Value).Key;

            return path;
        }

        private string GenerateShortestSequence((int Row, int Col) start, char target, char[,] keypad)
        {
            var queue = new Queue<((int Row, int Col) Position, string Path, HashSet<(int, int)> VisitedPath)>();
            queue.Enqueue((start, "", new HashSet<(int, int)> { start }));
            List<string> ShortestPaths = new();
            int pathlength = int.MaxValue;
            while (queue.Count > 0)
            {
                
                var (position, path, visitedPath) = queue.Dequeue();

                if (keypad[position.Row, position.Col] == target)
                {
                    ShortestPaths.Add(path + 'A');
                    pathlength = path.Length;
                    //return path + "A";
                }
                foreach (var (dRow, dCol, direction) in GetDirections())
                {
                    var neighbor = (position.Row + dRow, position.Col + dCol);
                    if (IsValid(neighbor, keypad) && !visitedPath.Contains(neighbor) && path.Length <= pathlength)
                    {
                        var newVisitedPath = new HashSet<(int, int)>(visitedPath) { neighbor };
                        queue.Enqueue((neighbor, path + direction, newVisitedPath));
                    }
                }
            }

            if (ShortestPaths.Count == 1) return ShortestPaths[0];
            else
            {
                return GetPathLeastVariation(ShortestPaths);
            }
        }

        private IEnumerable<(int Row, int Col, char Direction)> GetDirections()
        {
            yield return (-1, 0, '^'); 
            yield return (1, 0, 'v'); 
            yield return (0, -1, '<'); 
            yield return (0, 1, '>'); 
        }

        private bool IsValid((int Row, int Col) position, char[,] keypad)
        {
            return position.Row >= 0 && position.Row < keypad.GetLength(0)
                && position.Col >= 0 && position.Col < keypad.GetLength(1)
                && keypad[position.Row, position.Col] != ' ';
        }

        private (int Row, int Col) FindPosition(char[,] keypad, char target)
        {
            for (int row = 0; row < keypad.GetLength(0); row++)
            {
                for (int col = 0; col < keypad.GetLength(1); col++)
                {
                    if (keypad[row, col] == target) return (row, col);
                }
            }

            throw new ArgumentException($"Target {target} not found on keypad");
        }

        public int Part2()
        {
            int answer = 0;




            return answer;
        }

    }
}
