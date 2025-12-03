using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class Y2025D3
    {
        public static string[] Lines = File.ReadAllLines(".\\2025\\Input\\inputDay3.txt");

        private static int MaxTwoDigitFromLine(string line)
        {
            int n = line.Length;
            char[] suffixMax = new char[n];
            suffixMax[n - 1] = line[n - 1];
            for (int i = n - 2; i >= 0; i--)
            {
                suffixMax[i] = (char)('0' + Math.Max(line[i + 1] - '0', suffixMax[i + 1] - '0'));
            }

            int maxValue = -1;
            for (int i = 0; i < n - 1; i++)
            {

                int value = (line[i] - '0') * 10 + (suffixMax[i] - '0');
                if (value > maxValue) maxValue = value;
            }

            return maxValue;
        }

        private static string MaxKDigitsFromLine(string line, int k)
        {
          
            int n = line.Length;
            var sb = new StringBuilder(k);
            int start = 0;
            for (int remaining = k; remaining > 0; remaining--)
            {
             
                int lastIndex = n - remaining;
       
                char best = '\0';
                int bestIdx = start;
                for (int i = start; i <= lastIndex; i++)
                {
                    char c = line[i];
                    if (c > best)
                    {
                        best = c;
                        bestIdx = i;
                        if (best == '9') break; 
                    }
                }

                sb.Append(best);
                start = bestIdx + 1;
            }

            return sb.ToString();
        }

        public int Part1() 
        {
            int answer = 0;
            foreach (string line in Lines)
            {
                int number = MaxTwoDigitFromLine(line);
                answer += number;
            }

            return answer;
        }

        public long Part2()
        {
            long answer = 0;
            const int k = 12;

            foreach (string line in Lines)
            {

                string maxK = MaxKDigitsFromLine(line, k);
                // parse as long (12 digits fits in Int64)
                if (long.TryParse(maxK, out long val))
                {
                    answer += val;
                }
            }

            return answer;
        }

    }
}
