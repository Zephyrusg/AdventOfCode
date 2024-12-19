using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class Y2024D19
    {
        public static string[] Lines = File.ReadAllLines(".\\2024\\Input\\inputDay19.txt");
        static Dictionary<string, bool> ValidPatternCache = new Dictionary<string, bool>();
        static Dictionary<string, long> WaysValidDesignCache = new Dictionary<string, long>();
        static List<string> validDesigns = new();
        static string[] availablePatterns = [];
        public int Part1()
        {
           
            availablePatterns = Lines[0].Split(", ");
            var designs = Lines.Skip(2).ToArray();

            int possibleCount = 0;

            foreach (var design in designs)
            {
                if (CanConstructDesign(design, availablePatterns))
                {
                    possibleCount++;
                    validDesigns.Add(design);
                }
            }

            return possibleCount;
        }

        private bool CanConstructDesign(string target, string[] patterns)
        {
            if (ValidPatternCache.ContainsKey(target))
                return ValidPatternCache[target];

            if (target == string.Empty)
                return true;

            foreach (var pattern in patterns)
            {
                if (target.StartsWith(pattern))
                {
                    var remaining = target.Substring(pattern.Length);
                    if (CanConstructDesign(remaining, patterns))
                    {
                        ValidPatternCache[target] = true;
                        return true;
                    }
                }
            }

            ValidPatternCache[target] = false;
            return false;
        }

        private long CountWaysToConstruct(string target, string[] patterns)
        {
            if (WaysValidDesignCache.ContainsKey(target))
                return WaysValidDesignCache[target];

            if (target == string.Empty)
                return 1; 

            long totalWays = 0;

            foreach (var pattern in patterns)
            {
                if (target.StartsWith(pattern))
                {
                    var remaining = target.Substring(pattern.Length);
                    totalWays += CountWaysToConstruct(remaining, patterns);
                }
            }

            WaysValidDesignCache[target] = totalWays;
            return totalWays;
        }

        public long Part2()
        {

            long totalWays = 0;

            foreach (var design in validDesigns)
            {
                totalWays += CountWaysToConstruct(design, availablePatterns);
            }

            return totalWays;
        }
    }
}
