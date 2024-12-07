using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static AdventOfCode.Y2023D3;

namespace AdventOfCode
{
    internal class Y2024D3
    {
        public static string CorruptedData = File.ReadAllText(".\\2024\\Input\\inputDay3.txt");
        
        public int Part1() 
        {
            int answer = 0;
            string Pattern = "mul\\(\\d{1,3},\\d{1,3}\\)";
            Regex Mul = new Regex(Pattern);
            string DigitPattern = "\\d{1,3}";
            Regex Digit = new Regex(DigitPattern);
            MatchCollection Match = Mul.Matches(CorruptedData);

            foreach(Match instruction in Match)
            {
                MatchCollection DigitMatch = Digit.Matches(instruction.ToString());
                answer += int.Parse(DigitMatch[0].ToString()) * int.Parse(DigitMatch[1].ToString());
            }

            return answer;
        }

        public int Part2()
        {
            int answer = 0;
            string Pattern = "(mul\\(\\d{1,3},\\d{1,3}\\)|do\\(\\)|don't\\(\\))";
            Regex MulDoDont = new Regex(Pattern);
            string DigitPattern = "\\d{1,3}";
            Regex Digit = new Regex(DigitPattern);
            MatchCollection Match = MulDoDont.Matches(CorruptedData);
            bool DoMutiply = true;

            foreach (Match instruction in Match)
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
                    MatchCollection DigitMatch = Digit.Matches(instruction.ToString());
                    answer += int.Parse(DigitMatch[0].ToString()) * int.Parse(DigitMatch[1].ToString());
                }

            }

            return answer;
        }

    }
}
