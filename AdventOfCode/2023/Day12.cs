using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class Y2023D12
    {
        public static string[] lines = File.ReadAllLines(".\\2023\\Input\\inputDay12.txt");
        static int CountArrangements(string unknownPart, int[] damagedGroups)
        {
            List<char[]> arrangements = new List<char[]>();
            char[] currentArrangement = new char[unknownPart.Length];
            int LengthReport = damagedGroups.Sum() + damagedGroups.Count() - 1;

            if(unknownPart.Length == LengthReport) {
                return 1;
            }

            void GenerateArrangements(int index)
            {
                if (index == unknownPart.Length)
                {
                    arrangements.Add(currentArrangement.ToArray());
                    return;
                }

                if (unknownPart[index] == '?')
                {
                    currentArrangement[index] = '.';
                    GenerateArrangements(index + 1);

                    currentArrangement[index] = '#';
                    GenerateArrangements(index + 1);
                }
                else
                {
                    currentArrangement[index] = unknownPart[index];
                    GenerateArrangements(index + 1);
                }
            }

            GenerateArrangements(0);

            int count = arrangements.Count(arrangement =>
            {

                

                //int consecutiveOperational = 0;
                //int groupIndex = 0;

                //for (int i = 0; i < arrangement.Length; i++)
                //{
                //    char c = arrangement[i];

                //    if (c == '.')
                //    {
                //        consecutiveOperational++;
                //    }
                //    else if (c == '#' && consecutiveOperational > 0)
                //    {
                //        if (groupIndex >= damagedGroups.Length)
                //        {
                //            return false;  // More damaged groups in the arrangement than specified
                //        }

                //        int damagedGroupSize = damagedGroups[groupIndex];

                //        if (consecutiveOperational > damagedGroupSize)
                //        {
                //            // Adjust for unknown symbols between operational springs
                //            int unknownSymbols = consecutiveOperational - damagedGroupSize;
                //            groupIndex += unknownSymbols;
                //            consecutiveOperational = damagedGroupSize;
                //        }

                //        if (consecutiveOperational != damagedGroupSize)
                //        {
                //            return false;  // Mismatched damaged group size
                //        }

                //        groupIndex++;
                //        consecutiveOperational = 0;
                //    }
                //}

                //// Check if there are remaining damaged groups
                //return groupIndex == damagedGroups.Length && consecutiveOperational == 0;
                return false;
            });

            return count;
            
        } 
    
        public int Part1() 
        {
            int answer = 0;
            int totalArrangements = 0;

            foreach (string line in lines)
            {
                string[] parts = line.Split(' ');

                string unknownPart = parts[0];
                string[] groups = parts[1].Split(',').ToArray();

                int[] damagedGroups = groups.Select(int.Parse).ToArray();

                int arrangements = CountArrangements(unknownPart, damagedGroups);
                totalArrangements += arrangements;

                Console.WriteLine($"{line} - {arrangements} arrangements");
            }

            answer = totalArrangements;

            return answer;
        }

        public int Part2()
        {
            int answer = 0;




            return answer;
        }

    }
}
