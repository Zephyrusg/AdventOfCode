using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class Y2024D7
    {
        public static string[] Lines = File.ReadAllLines(".\\2024\\Input\\inputDay7.txt");
        static public List<(long testValue, long[] numbers)> equations = new();
        static bool part2 = false;

        static bool Calculate(long[] numbers, int index, long SubResult, long testValue)
        {
            /// The End
            if (index == numbers.Length - 1)
            {
                return SubResult == testValue;
            }

            
            long nextNumber = numbers[index + 1];

            // Try addition
            if (Calculate(numbers, index + 1, SubResult + nextNumber, testValue))
            {
                return true;
            }

            // Try multiplication
            if (Calculate(numbers, index + 1, SubResult * nextNumber, testValue))
            {
                return true;
            }
            if (part2)
            {
                //Try concatenation
                long concatenatedValue = long.Parse(SubResult.ToString() + nextNumber.ToString());
                if (Calculate(numbers, index + 1, concatenatedValue, testValue))
                {
                    return true;
                }
            }

            return false;
        }

        public long Part1() 
        {
            long answer = 0;

            foreach(string line in Lines)
            {
                var parts = line.Split(':');
                long testValue = long.Parse(parts[0].Trim());
                long[] numbers = parts[1].Trim().Split(' ').Select(long.Parse).ToArray();

                equations.Add((testValue, numbers));

            }
       
            foreach(var equation in equations)
            {
                if (Calculate(equation.numbers, 0, equation.numbers[0], equation.testValue))
                {
                    answer += equation.testValue;
                }
            }

            return answer;
        }

        public long Part2()
        {
            long answer = 0;
            part2 = true;

            foreach (var equation in equations)
            {
                if (Calculate(equation.numbers, 0, equation.numbers[0], equation.testValue))
                {
                    answer += equation.testValue;
                }
            }

            return answer;
        }

    }
}
