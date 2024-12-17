using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class Y2024D17
    {
        public static string[] Lines = File.ReadAllLines(".\\2024\\Input\\inputDay17.txt");
        static Dictionary<string, long> registers = new();
        static string result = "";
        static long GetComboValue(int operand)
        {
            return operand switch
            {
                <= 3 => operand,       
                4 => registers["A"],
                5 => registers["B"],
                6 => registers["C"],
            };
        }

        static string RunProgram(List<int> program, bool part2)
        {
            int ip = 0; 
            List<long> output = new List<long>();
            bool CorrectResult = true;
            while (ip < program.Count && CorrectResult)
            {
                int opcode = program[ip];
                int operand = program[ip + 1];

                switch (opcode)
                {
                    case 0: 
                        int denominator = (int)Math.Pow(2, GetComboValue(operand));
                        registers["A"] /= denominator;
                        break;

                    case 1: // bxl: B = B ^ literal_operand
                        registers["B"] ^= operand;
                        break;

                    case 2: // bst: B = combo_operand % 8
                        registers["B"] = GetComboValue(operand) % 8;
                        break;

                    case 3: // jnz: if A != 0, jump to literal_operand
                        if (registers["A"] != 0)
                        {
                            ip = operand;
                            continue;
                        }
                        break;

                    case 4: // bxc: B = B ^ C (operand ignored)
                        registers["B"] ^= registers["C"];
                        break;

                    case 5: // out: output combo_operand % 8
                        output.Add(GetComboValue(operand) % 8);

                        if (part2)
                        {
                            if (!CheckOutput(string.Join(",", output))){
                                CorrectResult = false;
                            }
                        }

                        break;

                    case 6: // bdv: B = A / (2 ^ combo_operand)
                        denominator = (int)Math.Pow(2, GetComboValue(operand));
                        registers["B"] = registers["A"] / denominator;
                        break;

                    case 7: // cdv: C = A / (2 ^ combo_operand)
                        denominator = (int)Math.Pow(2, GetComboValue(operand));
                        registers["C"] = registers["A"] / denominator;
                        break;

                    
                }

                ip += 2;
            }

            return string.Join(",", output);
        }

        static bool CheckOutput(string partOutput)
        {
            if (!result.StartsWith(partOutput))
            {
                return false;
            }
            return true;
        }

        public string Part1()
        {
          
            var program = new List<int>();

            foreach (var line in Lines)
            {
                if (line.StartsWith("Register A:"))
                {
                    registers.Add("A",int.Parse(line.Split(":")[1].Trim()));
                }
                else if (line.StartsWith("Register B:"))
                {
                    registers.Add("B", int.Parse(line.Split(":")[1].Trim()));
                }
                else if (line.StartsWith("Register C:"))
                {
                    registers.Add("C", int.Parse(line.Split(":")[1].Trim()));
                }
                else if (line.StartsWith("Program:"))
                {
                    program = line.Substring(8).Split(',').Select(int.Parse).ToList();
                }
            }

            result = RunProgram(program, false);
            return result; 
        }

        public long Part2()
        {
            long answer = 0;
            Dictionary<int, string> Results = new();
            var program = new List<int>();
            foreach (var line in Lines)
            {
                if (line.StartsWith("Program:"))
                {
                    program = line.Substring(8).Split(',').Select(int.Parse).ToList();
                }
            }
            Results.Add(63687530, result);
            long initialA = 10579880298; // Start searching from 1
            while (true)
            {
                registers = new Dictionary<string, long> { { "A", initialA }, { "B", 0 }, { "C", 0 } };
                if (initialA % 8 == 2)
                {
                    string output = RunProgram(program, true);
                    string expectedOutput = result;
                    if (output == expectedOutput)
                    {

                        answer = initialA;
                        break;
                    }
                    else
                    {
                        if(output.Length > 15)
                            
                        {
                            Console.WriteLine("initialA: " + initialA + " output: " + output);
                        }
                    }
                }
                initialA++;
            }
            return answer;
        }
    }
}
