using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class Y2024D3
    {
        public static string CorruptedData = File.ReadAllText(".\\2024\\Input\\inputDay3.txt");
        
        public int Part1() 
        {
            int answer = 0;
            string Pattern = "mul\\(\\d{1,3},\\d{1,3}\\)";
            string DigitPattern = "\\d{1,3}";
            var Match = Regex.Matches(CorruptedData, Pattern);

            foreach(var instruction in Match)
            {
                var DigitMatch = Regex.Matches(instruction.ToString(), DigitPattern);
                answer += int.Parse(DigitMatch[0].ToString()) * int.Parse(DigitMatch[1].ToString());


            }



            return answer;
        }

        public int Part2()
        {
            int answer = 0;
            string Pattern = "(mul\\(\\d{1,3},\\d{1,3}\\)|do\\(\\)|don't\\(\\))";
            string DigitPattern = "\\d{1,3}";
            var Match = Regex.Matches(CorruptedData, Pattern);
            bool DoMutiply = true;

            foreach (var instruction in Match)
            {
                if (instruction.ToString() == "do()")
                {
                    DoMutiply = true;
                    continue;
                } 
                else if (instruction.ToString() == "don't()") {
                    DoMutiply = false;
                    continue;
                }

                if (DoMutiply)
                {
                    var DigitMatch = Regex.Matches(instruction.ToString(), DigitPattern);
                    answer += int.Parse(DigitMatch[0].ToString()) * int.Parse(DigitMatch[1].ToString());
                }

            }



            return answer;
        }

    }
}
