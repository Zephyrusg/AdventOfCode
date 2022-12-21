using AdventOfCode._2022;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;

namespace AdventOfCode._2022
{  
    internal class Day21
    {
        static bool Foundhumn = false;
        static bool MuduloError = false;
        static long DoOperation(long FirstNumber, string Operator, long SecondNumber)
        {
 
            switch (Operator)
            {

                case "+":
                    {
                        return FirstNumber + SecondNumber;
                        
                    }
                case "-":
                    {
                        return FirstNumber - SecondNumber;
                   
                    }
                case "*":
                    {
                        return FirstNumber * SecondNumber;
                        
                    }
                case "/":
                    {
                        if (FirstNumber % SecondNumber != 0)
                        {
                            MuduloError = true;
                        }
                        return FirstNumber / SecondNumber;
                  
                    }
            }
            throw new InvalidOperationException();
        }

        static long EvaluteMonkey(string monkey) {

            if (monkey == "humn") {
                Foundhumn= true;
            }
            if (IntMonkeys.ContainsKey(monkey)){
                return IntMonkeys[monkey];
            }else
            {
                string operation = OperatorMonkeys[monkey];
                string[] Parts = operation.Split(" ");
                return DoOperation(EvaluteMonkey(Parts[0]), Parts[1], EvaluteMonkey(Parts[2]));
            }
        }

        static Dictionary<string, string> OperatorMonkeys = new Dictionary<string, string>();
        static Dictionary<string, long> IntMonkeys= new Dictionary<string, long>();
        static string Path = ".\\2022\\Input\\InputDay21.txt";
        static string[] Data = File.ReadAllLines(Path);
        public static long Part1()
        {
            foreach(string monkey in Data)
            {
              
                if (long.TryParse(monkey.Split(": ")[1], out long result))
                {
                    IntMonkeys[monkey.Split(": ")[0]] = result;
                }
                else {
                    OperatorMonkeys.Add(monkey.Split(": ")[0], monkey.Split(": ")[1]);
                }
                
            }

            return EvaluteMonkey("root");
        }

   
        public static long Part2()
        {
            Foundhumn = false;
            long answer = 0;
            long result = 0;
            string RootMonkey = OperatorMonkeys["root"];
            string[] parts = RootMonkey.Split(" ");
            result = EvaluteMonkey(parts[0]);
            string unkown = parts[2];
            if(Foundhumn)
            {
                unkown = parts[0];
                result = EvaluteMonkey(parts[2]);
            }
            var Branches = OperatorMonkeys.Where(M => M.Value.Contains("humn")).Select(v => v.Value).First();
            parts = Branches.Split(" ");

            if (parts[0] == "humn")
            {
                OperatorMonkeys[parts[0]] = EvaluteMonkey(parts[0]).ToString();
            }
            else
            {
                OperatorMonkeys[parts[2]] = EvaluteMonkey(parts[2]).ToString();
            }

            long Steps = 1;
            
            int count = 0;
            bool NotFound = true;
            long imax = 10000000000000;
            long imin = 0;
            long i =(imax+ imin) /2;

            while (NotFound) {
               
              
                MuduloError = false;
                IntMonkeys["humn"] = i;
                answer = EvaluteMonkey(unkown);

                if (result - answer == 0 && MuduloError == false)
                {
                    NotFound = false;
                    answer = i;
                    
                }
                else if (result < answer)
                {
                    //Console.WriteLine("Need Higher human: " + i + " Answer: " + answer + " == Result: " + result);
                
                    imin = i;
                    i = (imax + imin) / 2;
                }
                else if (result > answer || (result > answer && MuduloError == true))
                {
                    //Console.WriteLine("Need Lower human: " + i + " Answer: " + answer + " == Result: " + result);
                    imax = i;
                    i = (imax + imin) / 2;
                }
                count++;

            }
            return answer;
        }
    }
}
