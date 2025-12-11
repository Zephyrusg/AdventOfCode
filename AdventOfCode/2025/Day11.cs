using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class Y2025D11
    {
        public static string[] Lines = File.ReadAllLines(".\\2025\\Input\\inputDay11.txt");

        public static Dictionary<string, List<string>> Graph = new(StringComparer.Ordinal);

        public static  Dictionary<string, int> Memo = new(StringComparer.Ordinal);
        public  record struct State(string Node, bool HasDac, bool HasFft);

        private static Dictionary<State, long> Memo2 = new();

        public static int CountPathsRecursive(string node, string target)
        {
            if (Memo.TryGetValue(node, out int cached))
                return cached;

            if (node == target)
            {
                Memo[node] = 1;
                return 1;
            }

            if (!Graph.TryGetValue(node, out var children) || children.Count == 0)
            {
                Memo[node] = 0;
                return 0;
            }

            int sum = 0;
            foreach (var child in children)
            {
                sum += CountPathsRecursive(child, target);
            }

            Memo[node] = sum;
            return sum;
        }

        private static long CountPathsRecursive2(
            string node,
            string target,
            string dac,
            string fft,
            bool hasDac,
            bool hasFft)
        {
            bool newHasDac = hasDac || node == dac;
            bool newHasFft = hasFft || node == fft;

            var state = new State(node, newHasDac, newHasFft);

            if (Memo2.TryGetValue(state, out long cached))
                return cached;

            if (node == target)
            {
                long result = (newHasDac && newHasFft) ? 1L : 0L;
                Memo2[state] = result;
                return result;
            }

            if (!Graph.TryGetValue(node, out var children) || children.Count == 0)
            {
                Memo2[state] = 0;
                return 0;
            }

            long sum = 0;
            foreach (var child in children)
            {
                sum += CountPathsRecursive2(child, target, dac, fft, newHasDac, newHasFft);
            }

            Memo2[state] = sum;
            return sum;
        }


        public int Part1() 
        {
            int answer = 0;


            foreach (var line in Lines)
            {
                var parts = line.Split(':', 2);
                string from = parts[0].Trim();
                string to = parts[1].Trim();

                var outputs = new List<string>();

                var tokens = to.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                foreach (var t in tokens)
                {
                    outputs.Add(t.Trim());
                }
               

                Graph[from] = outputs;
            }
            
            answer = CountPathsRecursive("you", "out");


            return answer;
        }

        public long Part2()
        {
            long answer = 0;

            answer = CountPathsRecursive2("svr", "out", "dac", "fft", false, false);

            return answer;
        }

    }
}
