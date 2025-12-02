using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class Y2025D2
    {
        public static string Data = File.ReadAllText(".\\2025\\Input\\inputDay2.txt");
        
        public static int CountNumbers(long number)
        {
            int count = 0;

            while (number > 0)
            {

                count++;
                number /= 10;

                
            }
            return count;
        }

        public static bool HasRepeatSequence(long number)
        {
            string s = number.ToString();
            int n = s.Length;
            
            for (int p = 1; p <= n / 2; p++)
            {
                if (n % p != 0)
                    continue;

                int repeats = n / p;
                string pattern = s.Substring(0, p);


                var sb = new StringBuilder(n);
                for (int r = 0; r < repeats; r++)
                    sb.Append(pattern);

                if (sb.ToString() == s)
                    return true;
            }

            return false;
        }

        public long Part1() 
        {
            long answer = 0;
            List<long> InvalidIDs = new List <long>();
            string[] IdPairs = Data.Split(',');
            foreach(string line in IdPairs) {
                
                string[]parts = line.Split('-');
                long start = long.Parse(parts[0]);
                long end = long.Parse(parts[1]);
                for (long i = start; i <= end; i++)
                {
                    if (CountNumbers(i) % 2 == 0)
                    {
                        string number = i.ToString();
                        string sub1 = number.Substring(0, number.Length / 2);
                        string sub2 = number.Substring(number.Length / 2);
                        if(sub1 == sub2)
                        {
                            InvalidIDs.Add(i);
                        }
                    }
                }

            }

            answer = InvalidIDs.Sum();

            return answer;
        }

        public long Part2()
        {
            List<long> InvalidIDs = new List<long>();
            long answer = 0;

            string[] IdPairs = Data.Split(',');
            foreach (string line in IdPairs)
            {
                string[] parts = line.Split('-');
                long start = long.Parse(parts[0]);
                long end = long.Parse(parts[1]);
                for (long i = start; i <= end; i++)
                {
                    if (HasRepeatSequence(i))
                    {
                        InvalidIDs.Add(i);
                    }
                }
            }

            answer = InvalidIDs.Sum();

            return answer;
        }

    }
}

      