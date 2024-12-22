using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AdventOfCode.Y2024D20;

namespace AdventOfCode
{
    internal class Y2024D21
    {
        //private readonly string AOnlySequence = new string[] { "A" };

        private char[,] NumericKeypad = {
        { '7', '8', '9' },
        { '4', '5', '6' },
        { '1', '2', '3' },
        { '#', '0', 'A' }
    };

        private char[,] DirectionalKeypad = {
        { '#', '^', 'A' },
        { '<', 'v', '>' }
    };
        private Dictionary<(char from, char to), List<string>> shortestSequences = new();

        private Dictionary<(string code, int level), long> shortestsSubSequencesLengths = new();

        private void CacheAllShortestSequences() {
            CacheAllShortestSequences(NumericKeypad);
            CacheAllShortestSequences(DirectionalKeypad);
        }

        private void CacheAllShortestSequences(char[,] keypad)
        {
            foreach(var character in keypad)
            {
                foreach( var otherCharacter in keypad)
                {
                    shortestSequences[(character, otherCharacter)] = new List<string>();
                    if (character == otherCharacter)
                    {
                        shortestSequences[(character, otherCharacter)].Add("A");
                        continue;
                    }

                    CacheAllShortestSequences(character, otherCharacter, keypad);
                }
            }
        }

        private void CacheAllShortestSequences(char from, char to, char[,] keypad)
        {
            var fromPoint = FindPosition(keypad, from);
            var toPoint = FindPosition(keypad, to);

            var shortestPathLength = Math.Abs(fromPoint.Row - toPoint.Row) + Math.Abs(fromPoint.Col - toPoint.Col);

            Queue<((int Row, int Col) Point, string sequence)> queue = new();
            queue.Enqueue((fromPoint, ""));
            while (queue.Count > 0)
            {
                var (Point, sequence) = queue.Dequeue();

                if (Point == toPoint)
                {
                    shortestSequences[(from, to)].Add(sequence + "A");
                    continue;
                }

                if (sequence.Length >= shortestPathLength)
                {
                    continue;
                }

                foreach (var (dRow, dCol, direction) in GetDirections())
                {
                    (int Row, int Col) neighbor = (Point.Row + dRow, Point.Col + dCol);
                    if (neighbor.Col >= 0 && neighbor.Col < keypad.GetLength(1) &&
                        neighbor.Row >= 0 && neighbor.Row < keypad.GetLength(0) &&
                        keypad[neighbor.Row, neighbor.Col] != '#')
                    {
                        queue.Enqueue((neighbor, sequence + direction));
                    }
                }
            }
        }

       

        private string GetShortestSequence(string code, int layer)
        {
            if(layer == 3)
            {
                return code;
            }
            var keypad = layer == 0 ? NumericKeypad : DirectionalKeypad;
            var sequence = new StringBuilder();
            var previousCharacter = 'A';
            foreach (var character in code)
            {
                var shortestSubSequences = shortestSequences[(previousCharacter, character)];
                string currentBestSolution = null;
                foreach(var subsequence in shortestSubSequences)
                {
                    var solution = GetShortestSequence(subsequence, layer + 1);
                    if (currentBestSolution is null || solution.Length < currentBestSolution.Length) 
                    {
                        currentBestSolution = solution;
                    }
                }

                sequence.Append(currentBestSolution);

                previousCharacter = character;
            }

            return sequence.ToString();
        }

        private long GetShortestSequenceLength(string code, int layer)
        {
            if(shortestsSubSequencesLengths.TryGetValue((code, layer), out var cached))
            {
                return cached;
            }
            if (layer == 26)
            {
                shortestsSubSequencesLengths[(code, layer)] = (long)code.Length;
                return (long)code.Length;
            }
            long best = 0;
            char previous = 'A';
            for (int codeIndex = 0; codeIndex < code.Length; codeIndex++)
            {
                var current = code[codeIndex];

                var keypad = layer == 0 ? NumericKeypad : DirectionalKeypad;
                var paths = shortestSequences;

                var shortestpaths = paths[(previous, current)];

                long currentBest = long.MaxValue;
                foreach (var path in shortestpaths)
                {
                    
                    var length  = GetShortestSequenceLength(path, layer + 1);
                    if(currentBest > length)
                    {
                        currentBest = length;
                    }
                   
                }
                best += currentBest;

                previous = current;
            }
            shortestsSubSequencesLengths[(code, layer)] = best;
            return best;
        }


        private IEnumerable<(int Row, int Col, char Direction)> GetDirections()
        {
            yield return (-1, 0, '^');
            yield return (1, 0, 'v');
            yield return (0, -1, '<');
            yield return (0, 1, '>');
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

        public int Part1()
        {
           var codes = new List<string> { "029A", "980A", "179A", "456A", "379A" };
            codes = new List<string> { "319A", "985A", "340A", "489A", "964A" };
            int totalComplexity = 0;
            CacheAllShortestSequences();

            foreach (var code in codes)
            {
                var numericCode = int.Parse(string.Join("", code.Where(char.IsDigit)));
                int mySequenceLength = GetShortestSequence(code, 0).Length;
                totalComplexity += mySequenceLength * numericCode;

            }

            return totalComplexity;
        }

        public long Part2()
        {
            var codes = new List<string> { "029A", "980A", "179A", "456A", "379A" };
            codes = new List<string> { "319A", "985A", "340A", "489A", "964A" };
            long totalComplexity = 0;
            CacheAllShortestSequences();

            foreach (var code in codes)
            {
                var numericCode = int.Parse(string.Join("", code.Where(char.IsDigit)));
                long mySequenceLength = GetShortestSequenceLength(code, 0);
                totalComplexity += mySequenceLength * (long)numericCode;

            }

            return totalComplexity;




        }

    }
}
