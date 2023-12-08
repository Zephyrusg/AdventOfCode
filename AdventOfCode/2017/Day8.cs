using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class Y2017D8
    {
        static public Dictionary<string, int> Registers = new Dictionary<string, int>();
        public static string[] lines = File.ReadAllLines(".\\2017\\Input\\inputDay8.txt");
        static int MaxValue = 0;

        static public bool TestOperation (string Register, string test, int value){
            bool Result = false;

            switch (test) {

                case ">":
                    if (Registers[Register] > value)
                    {
                        Result = true;
                    }
                    break;
                case "<":
                    if (Registers[Register] < value)
                    {
                        Result = true;
                    }
                    break;
                case "==":
                    if (Registers[Register] == value)
                    {
                        Result = true;
                    }
                    break;
                case "!=":
                    if (Registers[Register] != value)
                    {
                        Result = true;
                    }
                    break;
                case ">=":
                    if (Registers[Register] >= value) { 
                        Result = true;
                    }
                    break;
                case "<=":
                    if (Registers[Register] <= value) { 
                        Result = true;
                    }
                    break;
            
            }
            return Result;
        }

        public int Part1() 
        {
              
            int answer = 0;
            foreach (string line in lines) {
                string register = line.Split(" ")[0];
                if (!Registers.ContainsKey(register))
                {
                    Registers.Add(register, 0);
                }
            }
            foreach(string line in lines)
            {
                string[] Parts = line.Split(" ");
                string Register = Parts[0];
                string Operation = Parts[1];
                int OperationValue = int.Parse(Parts[2]);
                string TestRegister = Parts[4];
                string Test = Parts[5];
                int TestValue = int.Parse(Parts[6]);

                if (TestOperation(TestRegister, Test, TestValue)) {
                    switch (Operation) {
                        case "inc":
                            Registers[Register] += OperationValue;
                            break;
                        case "dec":
                            Registers[Register] -= OperationValue;
                            break;
    
                    }
                }

                if(Registers.Values.Max() > MaxValue) { 
                    MaxValue = Registers.Values.Max();
                }

            }

            answer = Registers.Values.Max();

            return answer;
        }

        public int Part2()
        {

            int answer = MaxValue;



            return answer;
        }

    }
}
