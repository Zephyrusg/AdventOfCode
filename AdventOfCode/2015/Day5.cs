using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace AdventOfCode._2015
{
    internal class Day5
    {
        public static int Day5A()
        {
            int niceKids = 0;
            string[] textInput = File.ReadAllLines(".\\2015\\Input\\inputDay5.txt");

            foreach (string line in textInput)
            {

                int countVowels = 0;
                foreach (char c in line)
                {
                    if (c == 'a' || c == 'o' || c == 'e' || c == 'i' || c == 'u')
                    {
                        countVowels++;
                    }
                }
                if (countVowels < 3)
                {
                    continue;
                }
                Regex pattern = new Regex(@"([a-z])\1");
                Match match = pattern.Match(line);
                if (!match.Success)
                {
                    continue;
                }
                if (line.Contains("ab") || line.Contains("cd") || line.Contains("pq") || line.Contains("xy"))
                {
                    continue;
                }
                niceKids++;
            }

            return niceKids;
        }
        public static int Day5B()
        {
            int nicekids = 0;
            string[] textInput = File.ReadAllLines(".\\2015\\Input\\inputDay5.txt");

            foreach (string line in textInput)
            {
                bool found = false;
                string? lastset = null;
                HashSet<string> sets = new HashSet<string>();
                for (int i = 0; i < line.Length - 1; i++) 
                {
                    string set = line.Substring(i, 2);
                    if (sets.Contains(set)) {
                        if (lastset == set) 
                        {
                            continue;
                        }
                        else
                        {
                            found = true;
                            break;
                        }
                    }
                    else { 
                        sets.Add(set);
                    }
                    lastset = set;
                
                }
                if (!found) {
                    continue;
                }
                for (int i = 1; i < line.Length - 1; i++) {

                    found = false;
                    char previous = line[i-1];
                    char next = line[i+1];
                    if (previous == next) 
                    {
                        found = true;
                        break;

                    }
                }
                if (found)
                {
                    nicekids++;
                }
            }
            return nicekids;
        }
    }
}
