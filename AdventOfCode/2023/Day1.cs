using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Collections;
using static System.Net.Mime.MediaTypeNames;

namespace AdventOfCode
{
    internal class Y2023D1
    {
        public static int Part1() 
        {
            string[] lines = File.ReadAllLines(".\\2023\\Input\\inputDay1.txt");
            int answer = 0;

            foreach (string line in lines)
            {
                string Pattern = "\\d";
        
                var Match = Regex.Matches(line, Pattern);
                
                int value = 0;
                if (Match.Count == 1)
                {
                    value = int.Parse(Match[0].Value + Match[0].Value) ;
                }
                else {
                    string first = Match[0].Value;
                    string last = Match[Match.Count - 1].Value;
                    value = int.Parse(first+last);
                }

                answer += value;

            }

            return answer;
        }

        public static int Part2()
        {

            string[] lines = File.ReadAllLines(".\\2023\\Input\\inputDay1b.txt");
            int answer = 0;
            List<string> parsedlines = new List<string>();
            string Pattern = "(?=(one|two|three|four|five|six|seven|eight|nine|\\d))";
            Dictionary<string, string> Numbers = new Dictionary<string, string>() {
                {"one",   "1"},
                {"two",   "2"},
                {"three", "3"},
                {"four",  "4"},
                {"five",  "5"},
                {"six",   "6"},
                {"seven", "7"},
                {"eight", "8"},
                {"nine",  "9"}
            };

            foreach (string line in lines) {

                var Match = Regex.Matches(line, Pattern);


                int value = 0;
                if (Match.Count == 1)
                {
                    string result;
                    bool test = int.TryParse(Match[0].Groups[1].Value, out _);
                    if (!test)
                    {
                        result = Numbers[Match[0].Groups[1].Value];
                    }
                    else
                    {
                        result = Match[0].Groups[1].Value;
                    }

                    value = int.Parse(result + result);
                }
                else
                {
                    string first;
                    string last;
                    bool firsttest = int.TryParse(Match[0].Groups[1].Value, out _);
                    bool Secondtest = int.TryParse(Match[Match.Count - 1].Groups[1].Value, out _);
                    if (!firsttest)
                    {
                        first = Numbers[Match[0].Groups[1].Value];
                    }
                    else
                    {
                        first = Match[0].Groups[1].Value;
                    }
                    if (!Secondtest)
                    {
                        last = Numbers[Match[Match.Count - 1].Groups[1].Value];
                    }
                    else
                    {
                        last = Match[Match.Count - 1].Groups[1].Value;
                    }
                    value = int.Parse(first + last);
                }

                answer += value;

            }

            return answer;
        }

    }
}
