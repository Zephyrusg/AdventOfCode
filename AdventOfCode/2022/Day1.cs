using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022
{
    internal class Day1
    { 
        static string Path = ".\\2022\\Input\\InputDay1.txt";

        public static int Part1() {
            
            int answer = 0;
            string Data = File.ReadAllText(Path);
            string[]DataList = Data.Split(new string[] { Environment.NewLine + Environment.NewLine },
                               StringSplitOptions.RemoveEmptyEntries);
            List<int> ElfsCalories = new List<int>();
            foreach (string Elf in DataList)
            {
                string[] ListofCalories = Elf.Split(Environment.NewLine);
                int Total = ListofCalories.Sum(a => Int32.Parse(a));
                ElfsCalories.Add(Total);            
            }

            ElfsCalories.Sort();
            answer = ElfsCalories.Last();

            return answer;
        }
        public static int Part2()
        {
            int answer = 0;
            string Data = File.ReadAllText(Path);
            string[] DataList = Data.Split(new string[] { Environment.NewLine + Environment.NewLine },
                               StringSplitOptions.RemoveEmptyEntries);
            List<int> ElfsCalories = new List<int>();
            foreach (string Elf in DataList)
            {
                string[] ListofCalories = Elf.Split(Environment.NewLine);
                int Total = ListofCalories.Sum(a => Int32.Parse(a));
                ElfsCalories.Add(Total);
            }

            ElfsCalories.Sort();
            answer = ElfsCalories.TakeLast(3).Sum();

            return answer;
        }

    }

}

