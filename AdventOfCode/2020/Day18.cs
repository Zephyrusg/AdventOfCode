using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode._2020
{
    internal class Day18
    {
        public static BigInteger CalculateSum(string sum) {
            BigInteger result = 0;
            char SumOperator;
            if (sum.Contains("+")){
                SumOperator = '+';
            }
            else {
                SumOperator = '*';
            }
            switch (SumOperator)
            {
                case '+':
                    BigInteger[] AdditionNumbers = sum.Split('+').Select(num => BigInteger.Parse(num)).ToArray();
                    result = AdditionNumbers[0] + AdditionNumbers[1];
                    
                    break;
                    
                case '*':

                    BigInteger[] MutiplyNumbers = sum.Split('*').Select(num => BigInteger.Parse(num)).ToArray();
                    result = MutiplyNumbers[0] * MutiplyNumbers[1];

                    break;
            }

            return result;
        }
        public static BigInteger Day18A() {
            BigInteger Total = 0;

            string[] lines = File.ReadAllLines(".\\2020\\Input\\InputDay18.txt");
            string SubSumPattern = @"[(]((\d+[+*])|\d+)+[)]";  
            Regex SubSumRg = new Regex(SubSumPattern);

            string SumPattern = @"\d+[*+]\d+";
            Regex SumRg = new Regex(SumPattern);


            int Linecounter = 1;
            foreach (string line in lines) { 
                string SumLine = line.Replace(" ",string.Empty);
                
                while (SubSumRg.Matches(SumLine).Count() > 0) {
                    MatchCollection SubSums = SubSumRg.Matches(SumLine);
                    foreach (Match SubSum in SubSums) {
                        string SubSumLine = SubSum.Value;
                        SubSumLine = SubSumLine.Remove(0, 1);
                        SubSumLine = SubSumLine.Remove(SubSumLine.Length - 1);
                        while (SumRg.Matches(SubSumLine).Count() > 0)
                        {
                            MatchCollection SubSumparts = SumRg.Matches(SubSumLine);
                            string SubSumPart = SubSumparts[0].Value;
                            BigInteger Result = CalculateSum(SubSumPart);
                            int length = SubSumPart.Length;
                            SubSumLine = SubSumLine.Remove(0, length);
                            SubSumLine = Result.ToString() + SubSumLine;
                        }

                        
                        SumLine = SumLine.Replace(SubSum.Value, SubSumLine);

                    }
                }
                while (SumRg.Matches(SumLine).Count() > 0) { 
                    MatchCollection SumParts = SumRg.Matches(SumLine);
                    string SumPart = SumParts[0].Value;
                    BigInteger Result = CalculateSum(SumPart);
                    int length = SumPart.Length;
                    SumLine = SumLine.Remove(0, length);
                    SumLine = Result.ToString() + SumLine;
                    //SumLine = SumLine.Replace(SumPart, Result.ToString());
                }
                BigInteger Resultsum = BigInteger.Parse(SumLine);
                Total += Resultsum;
                //Console.WriteLine("Line: " + Linecounter + " Result: " + Resultsum);
                Linecounter++;



            }
        
        
            
            return Total;
        }
    }
}
