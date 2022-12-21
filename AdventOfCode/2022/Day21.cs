using AdventOfCode._2022;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AdventOfCode._2022
{  
    internal class Day21
    {
        static bool Foundhumn = false;
        static long DoOperation(long FirstNumber, string Operator, long SecondNumber)
        {
           
            long Result = 0;


            switch (Operator)
            {

                case "+":
                    {
                        Result = FirstNumber + SecondNumber;
                        break;
                    }
                case "-":
                    {
                        Result = FirstNumber - SecondNumber;
                        break;
                    }
                case "*":
                    {
                        Result = FirstNumber * SecondNumber;
                        break;
                    }
                case "/":
                    {
                        Result = FirstNumber / SecondNumber;
                        break;
                    }
            }
            return Result;
        }

        static long EvaluteMonkey(string monkey) {

            string operation = AllMonkeys[monkey];

            if (monkey == "humn") {
                Foundhumn= true;
            }
                

            if (long.TryParse(operation, out long result)){
                return result;
            }
            else
            {
                string[] Parts = operation.Split(" ");


                return DoOperation(EvaluteMonkey(Parts[0]), Parts[1], EvaluteMonkey(Parts[2]));
            }
        }




        static Dictionary<string, string> AllMonkeys = new Dictionary<string, string>();
        static string Path = ".\\2022\\Input\\InputDay21.txt";
        static string[] Data = File.ReadAllLines(Path);
        public static long Part1()
        {
            foreach(string monkey in Data)
            {

                AllMonkeys.Add(monkey.Split(": ")[0], monkey.Split(": ")[1]);
            }

            return EvaluteMonkey("root");
        }

   
        public static long Part2()
        {
            Foundhumn = false;
            long answer = 0;
            long result = 0;
            string RootMonkey = AllMonkeys["root"];
            string[] parts = RootMonkey.Split(" ");
            result = EvaluteMonkey(parts[0]);
            string unkown = parts[2];
            if(Foundhumn)
            {
                unkown = parts[0];
                result = EvaluteMonkey(parts[2]);
            }

            long Steps = 1;
            long i = 1;
            bool NotFound = true;
            while(NotFound) {
                i += Steps;
                AllMonkeys["humn"] = i.ToString();
                answer = EvaluteMonkey(unkown);
                
                if (result - answer == 0) { 
                    NotFound= false;
                    answer = i;
                    return answer;
                }
                
                if (result > answer)
                {
                    Steps *= 2;

                }

                if (result < answer)
                {

                    i -= Steps;
                    i--;
                }

                    
                    
            }



            return answer;
        }
    }
}
