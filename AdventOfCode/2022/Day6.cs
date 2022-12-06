using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022
{
    internal class Day6
    {

        static string Path = ".\\2022\\Input\\InputDay6.txt";
        public static int Part1()
        {
            string Data = File.ReadAllText(Path);
            int answer = 0;

            for (int i = 0; i < Data.Length; i++) {
                
                string Test = Data.Substring(i, 4);
                if (Test.Distinct().Count() == 4) {
                    answer = i + 4;
                    break;
                }    
            }
            return answer;
        }
        public static int Part2()
        {
            string Data = File.ReadAllText(Path);
            int answer = 0;

            for (int i = 0; i < Data.Length; i++)
            {
                string Test = Data.Substring(i, 14);

                if (Test.Distinct().Count() == 14)
                {
                    answer = i + 14;
                    break;
                }
            }
            return answer;
        }
    }
}
