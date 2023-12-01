using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Collections;

namespace AdventOfCode
{
    internal class Y2023D1
    {
        public static string ReplaceFirst(string str, string term, string replace)
        {
            int position = str.IndexOf(term);
            if (position < 0)
            {
                return str;
            }
            str = str.Substring(0, position) + replace + str.Substring(position + 1);
            return str;
        }
        public static Int64 Part1() 
        {
            string[] lines = File.ReadAllLines(".\\2023\\Input\\inputDay1.txt");
            Int64 answer = 0;

            foreach (string line in lines)
            {
                string digit = "";
                List<string> Digits = new List<string>();
                foreach(char ch in line)
                {
                    
                    bool result = int.TryParse(ch.ToString(), out _);
                    if (result)
                    {
                        Digits.Add(ch.ToString());
                        digit = "";
                    }
                }
                if (digit != "")
                {
                    Digits.Add(digit);
                }




                    Int64 value = 0;
                if (Digits.Count == 1)
                {
                    value = Int64.Parse(Digits[0] + Digits[0]) ;
                }
                else { 
                    string first = Digits[0];
                    string last = Digits[Digits.Count-1];
                    value = Int64.Parse(first+last);
                }

                answer += value;

            }

       


            return answer;
        }

        public static Int64 Part2()
        {

            string[] lines = File.ReadAllLines(".\\2023\\Input\\inputDay1b.txt");
            Int64 answer = 0;
            List<string> parsedlines = new List<string>();
            foreach (string line in lines) {
                string parsedline = line;

                for (int x = 0; x < line.Length; x++) {

                    switch (line[x]) {



                        case 'o':
                            if (x + 2 <= line.Length - 1)
                            {
                                if (line[x + 1] == 'n' && line[x + 2] == 'e')
                                {
                                    parsedline = ReplaceFirst(parsedline, "one", "1");
                                    //x = x + 2;
                                }
                            }

                            break;
                        case 't':
                            if (x + 2 <= line.Length - 1)
                            {
                                if (line[x + 1] == 'w' && line[x + 2] == 'o')
                                {
                                    parsedline = ReplaceFirst(parsedline, "two", "2");
                                    //parsedline = parsedline.Replace("two", "2");
                                    //x = x + 2;
                                    break;
                                }
                            }
                            if (x + 4 <= line.Length - 1)
                            {
                                if (line[x + 1] == 'h' && line[x + 2] == 'r' && line[x + 3] == 'e' && line[x + 4] == 'e')
                                {
                                    parsedline = ReplaceFirst(parsedline, "three", "3");
                                    //x = x + 4;
                                    break;
                                }
                            }
                            break;


                        case 'f':
                            if (x + 3 <= line.Length - 1)
                            {
                                if (line[x + 1] == 'o' && line[x + 2] == 'u' && line[x + 3] == 'r')
                                {
                                    parsedline = ReplaceFirst(parsedline, "four", "4");
                                    //x = x + 3;
                                    break;
                                }
                            }
                            if (x + 3 <= line.Length - 1)
                            {
                                if (line[x + 1] == 'i' && line[x + 2] == 'v' && line[x + 3] == 'e')
                                {
                                    parsedline = ReplaceFirst(parsedline, "five", "5");
                                    //x = x + 3;
                                    break;
                                }
                            }
                            break;
                        case 's':
                            if (x + 2 <= line.Length - 1)
                            {
                                if (line[x + 1] == 'i' && line[x + 2] == 'x')
                                {
                                    parsedline = ReplaceFirst(parsedline, "six", "6");
                                    //x = x + 2;
                                    break;
                                }
                            }
                            if (x + 4 <= line.Length - 1)
                            {
                                if (line[x + 1] == 'e' && line[x + 2] == 'v' && line[x + 3] == 'e' && line[x + 4] == 'n')
                                {
                                    parsedline = ReplaceFirst(parsedline, "seven", "7");
                                    //x = x + 4;
                                    break;
                                }
                            }
                            break;
                        case 'e':
                            if (x + 4 <= line.Length - 1)
                            {
                                if (line[x + 1] == 'i' && line[x + 2] == 'g' && line[x + 3] == 'h' && line[x + 4] == 't')
                                {
                                    parsedline = ReplaceFirst(parsedline, "eight", "8");
                                    //x += 4;
                                }
                            }
                            break;
                        case 'n':
                            if (x + 3 <= line.Length - 1)
                            {
                                if (line[x + 1] == 'i' && line[x + 2] == 'n' && line[x + 3] == 'e')
                                {
                                    parsedline = ReplaceFirst(parsedline, "nine", "9");
                                    //x = x + 3;
                                }
                            }
                            break;



                    }

                }

   

                parsedlines.Add(parsedline);
            }

            using (StreamWriter outputfile = new StreamWriter(".\\2023\\Input\\23output1.txt"))
            {
                foreach (string line in parsedlines)
                {
                   
                    outputfile.WriteLine(line);
                   
                }
            }


            foreach (string line in parsedlines)
            {
                string digit = "";
                List<string> Digits = new List<string>();
                foreach (char ch in line)
                {

                    bool result = int.TryParse(ch.ToString(), out _);
                    if (result)
                    {
                        Digits.Add(ch.ToString());
                        digit = "";
                    }
                }
                if (digit != "")
                {
                    Digits.Add(digit);
                }




                Int64 value = 0;
                if (Digits.Count == 1)
                {
                    value = Int64.Parse(Digits[0] + Digits[0]);
                }
                else
                {
                    string first = Digits[0];
                    string last = Digits[Digits.Count - 1];
                    value = Int64.Parse(first + last);
                }

                answer += value;

            }






            return answer;
        }

    }
}
