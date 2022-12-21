using AdventOfCode._2022;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AdventOfCode._2022
{  
    internal class Day21
    {
        static bool Foundhumn = false;
        static bool MuduloError = false;
        static BigInteger DoOperation(BigInteger FirstNumber, string Operator, BigInteger SecondNumber)
        {
           
            BigInteger Result = 0;


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
                        if (FirstNumber % SecondNumber != 0) { 
                            MuduloError= true;
                        }

                        break;
                    }
            }
            return Result;
        }

        static BigInteger EvaluteMonkey(string monkey) {

            string operation = AllMonkeys[monkey];

            if (monkey == "humn") {
                Foundhumn= true;
            }
                

            if (BigInteger.TryParse(operation, out BigInteger result)){
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
        public static BigInteger Part1()
        {
            foreach(string monkey in Data)
            {

                AllMonkeys.Add(monkey.Split(": ")[0], monkey.Split(": ")[1]);
            }

            return EvaluteMonkey("root");
        }

   
        public static BigInteger Part2()
        {
            Foundhumn = false;
            BigInteger answer = 0;
            BigInteger result = 0;
            string RootMonkey = AllMonkeys["root"];
            string[] parts = RootMonkey.Split(" ");
            result = EvaluteMonkey(parts[0]);
            string unkown = parts[2];
            if(Foundhumn)
            {
                unkown = parts[0];
                result = EvaluteMonkey(parts[2]);
            }
            var Branches = AllMonkeys.Where(M => M.Value.Contains("humn")).Select(v => v.Value).First();
            parts = Branches.Split(" ");

            if (parts[0] == "humn")
            {
                AllMonkeys[parts[0]] = EvaluteMonkey(parts[0]).ToString();
            }
            else {
                AllMonkeys[parts[2]] = EvaluteMonkey(parts[2]).ToString();
            }

            BigInteger Steps = 1;
            BigInteger i = 1;
            bool NotFound = true;
            while(NotFound) {
                i += Steps;
                MuduloError = false;
                AllMonkeys["humn"] = i.ToString();
                answer = EvaluteMonkey(unkown);
                
                if (result - answer == 0 && MuduloError == false) { 
                    NotFound= false;
                    answer = i;
                    return answer;
                }else if (result < answer)
                {
                    //Console.WriteLine("Need Lower human: " + i + " Answer: " + answer + " == Result: " + result);
                    Steps *= 2;

                }else if(result > answer || (result > answer && MuduloError == true))
                {
                    //Console.WriteLine("Need Lower human: " + i + " Answer: " + answer + " == Result: " + result);
                    i -= Steps;
                    Steps = 1;
                }                       
            }
            return answer;
        }
    }
}
