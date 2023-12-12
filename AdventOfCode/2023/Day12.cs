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
        static int CountArrangements(string unknownPart, List<int>damagedGroups)
        {
            List<char[]> arrangements = new List<char[]>();
            char[] currentArrangement = new char[unknownPart.Length];
            int LengthReport = damagedGroups.Sum() + damagedGroups.Count() - 1;
            if(unknownPart.Length == LengthReport) {
                return 1;
            }

            void GenerateArrangements(int index)
            
            {

                if(currentArrangement.Count(x=> x == '#') > damagedGroups.Sum())
                {
                    return;
                }

                if (index == unknownPart.Length)
                {
                    if (currentArrangement.Where(x => x == '#').Count() == damagedGroups.Sum()) {
                        arrangements.Add(currentArrangement.ToArray());
                    }
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
                if (arrangement.Where(x => x == '#').Count() != damagedGroups.Sum()) {
                    return false;
                }
                
                int ReportStep = 0;
                int groupIndex = 0;
                int DamagedReportLength = arrangement.Count();
                int NeededLength = LengthReport;
                int DamagedGroupCounter = 0;
                //int consecutiveOperational = 0;
                //int groupIndex = 0;

                for (int i = 0; i < arrangement.Length; i++) {

                    char c = arrangement[i];
                    if (c == '.')
                    {
                        

                        if (DamagedGroupCounter > 0)
                        {
                            if (DamagedGroupCounter != damagedGroups[groupIndex])
                            {
                                return false;
                            }
                            else
                            {
                                ReportStep++;
                                groupIndex++;
                                DamagedGroupCounter = 0;
                                NeededLength = damagedGroups.Skip(groupIndex).Select(i => i).Sum() + damagedGroups.Skip(groupIndex).Count()-1;
                            }

                        }
                        else {
                            ReportStep++;
                        }

                        if (DamagedReportLength - ReportStep < NeededLength) {
                            return false;
                        }
                        

                    }
                    else { 
                        DamagedGroupCounter++;
                        ReportStep++;
                    }
                }
            
                return true;
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

                List<int> damagedGroups = groups.Select(int.Parse).ToList();
                var key = $"{unknownPart},{string.Join(',', groups)}";

                int arrangements = CountArrangements(unknownPart, damagedGroups);
                totalArrangements += arrangements;

                //Console.WriteLine($"{line} - {arrangements} arrangements");
            }

            answer = totalArrangements;

            return answer;
        }

        public int Part2()
        {
            int answer = 0;
            int totalArrangements = 0;

            foreach (string line in lines)
            {
                string[] parts = line.Split(' ');

                string unknownPart = parts[0];
                string unknownPart5x =String.Concat(Enumerable.Repeat(unknownPart+'?', 4)) + unknownPart;
                
                
                string[] groups = parts[1].Split(',').ToArray();

                List<int> damagedGroups = groups.Select(int.Parse).ToList();
                List<int> DamagedGroupx5 = new List<int>();
                for (int i = 0; i < 5; i++)
                {
                    DamagedGroupx5 = DamagedGroupx5.Concat(damagedGroups).ToList();
                }

                int arrangements = CountArrangements(unknownPart5x, DamagedGroupx5);
                totalArrangements += arrangements;

                Console.WriteLine($"{line} - {arrangements} arrangements");
            }

            answer = totalArrangements;

            return answer;
        }

    }
}
