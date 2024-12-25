using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class Y2024D25
    {
        public static string[] Lines = File.ReadAllLines(".\\2024\\Input\\inputDay25.txt");

        public int Part1()
        {
            var locks = new List<List<int>>();
            var keys = new List<List<int>>();

            ParseSchematics(Lines, locks, keys);

            var uniquePairs = new HashSet<(int, int)>();

            for (int lockIndex = 0; lockIndex < locks.Count; lockIndex++)
            {
                for (int keyIndex = 0; keyIndex < keys.Count; keyIndex++)
                {
                    if (IsValidPair(locks[lockIndex], keys[keyIndex]))
                    {
                        uniquePairs.Add((lockIndex, keyIndex));
                    }
                }
            }

            return uniquePairs.Count;
        }

        
        private void ParseSchematics(string[] lines, List<List<int>> locks, List<List<int>> keys)
        {
            List<string> currentSchematic = new();

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    if (currentSchematic.Count > 0)
                    {
                        bool isLock = IsLock(currentSchematic);
                        var heights = ConvertToHeights(currentSchematic, isLock);
                        if (isLock)
                        {
                            locks.Add(heights);
                        }
                        else
                        {
                            keys.Add(heights);
                        }
                    }

                    
                    currentSchematic.Clear();
                    continue;
                }

                currentSchematic.Add(line);
            }

           
            if (currentSchematic.Count > 0)
            {
                bool isLock = IsLock(currentSchematic);
                var heights = ConvertToHeights(currentSchematic, isLock);
                if (isLock)
                {
                    locks.Add(heights);
                }
                else
                {
                    keys.Add(heights);
                }
            }
        }

        private bool IsLock(List<string> schematic)
        {
            var topRow = schematic[0];
            var bottomRow = schematic[^1]; 

            return topRow.All(c => c == '#') && bottomRow.All(c => c == '.');
        }

        private List<int> ConvertToHeights(List<string> schematic, bool isLock)
        {
            int rows = schematic.Count;
            int cols = schematic[0].Length;
            var heights = new List<int>(new int[cols]);

            for (int col = 0; col < cols; col++)
            {
                if (isLock)
                {
                    for (int row = 1; row < rows; row++)
                    {
                        if (schematic[row][col] == '#')
                        {
                            heights[col]++;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                else
                {
                    for (int row = rows - 2; row >= 0; row--)
                    {
                        if (schematic[row][col] == '#')
                        {
                            heights[col]++;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }

            return heights;
        }

        private bool IsValidPair(List<int> lockPins, List<int> keyShape)
        {
            for (int i = 0; i < lockPins.Count; i++)
            {
                if (lockPins[i] + keyShape[i] > 5) 
                {
                    return false;
                }
            }

            return true;
        }

        public int Part2()
        {
            int answer = 0;

            return answer;
        }

    }
}
