using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022
{


    internal class Day25
    {
        static long Decode(string Snafu) {
            long DecodedDigit = 0;

            int length = Snafu.Length -1;
            foreach (char c in Snafu)
            {
                long Position = (long)Math.Pow(5, length);
                switch (c)
                {
                    case '2': {
                            DecodedDigit += Position * 2;
                        break;
                    }
                    case '1':
                        {
                            DecodedDigit += Position * 1;
                            break;
                        }
                    case '0':
                        {
                            DecodedDigit += Position * 0;
                            break;
                        }
                    case '-':
                        {
                            DecodedDigit += Position * -1;
                            break;
                        }

                    case '=':
                        {
                            DecodedDigit += Position * -2;
                            break;
                        }
                    
                }
                length--;

            }

            return DecodedDigit;
        }

        static string Encode(long num) {
            string Result = "";
            string Digit;
            while(num > 0)
            {
                int carry = 0;
                Digit = (num % 5).ToString();
                
                if (Digit == "3") {
                    Digit = "=";
                    carry= 1;
                }
                if(Digit== "4")
                {
                    Digit = "-";
                    carry = 1;
                }
                Result+= Digit;
                num = num  / 5 + carry;
            }
            char[] charArray = Result.ToCharArray();
            Array.Reverse(charArray);
       

            return new string(charArray);
        
        }

        static string Path = ".\\2022\\Input\\InputDay25.txt";
        static string[] Data = File.ReadAllLines(Path);
        public static string Part1()
        {
            long answer = 0;
            long Digit;
            foreach(string line in Data)
            {
                Digit = Decode(line);
                //Console.WriteLine(line + " = " + Digit);
                answer= answer + Digit;
            }
            
            return Encode(answer); ;
        }

   
        public static int Part2()
        {
            int answer = 0;
            

            return answer;
        }
    }
}
