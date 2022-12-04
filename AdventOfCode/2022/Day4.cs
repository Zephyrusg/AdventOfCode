using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022
{
    internal class Day4
    {
        static string Path = ".\\2022\\Input\\InputDay4.txt";
        public static int Part1()
        {
            string[] Data = File.ReadAllLines(Path);
            int answer = 0;

            foreach (string line in Data)
            {
                string[] Parts = line.Split(',');
                string[] Part1 = Parts[0].Split("-");
                int[] Elf1= new int[] { Int32.Parse(Part1[0]), Int32.Parse(Part1[1])};
                string[] Part2 = Parts[1].Split("-");
                int[] Elf2 = new int[] { Int32.Parse(Part2[0]), Int32.Parse(Part2[1]) };

                if ((Elf1[0] >= Elf2[0]) & (Elf1[1] <= Elf2[1])) { 
                    answer++;
                }
                else if((Elf2[0] >= Elf1[0]) & (Elf2[1] <= Elf1[1])){ 
                    answer++;
                }

            }


            return answer;
        }
        public static int Part2()
        {
            string[] Data = File.ReadAllLines(Path);
            int answer = 0;
            foreach (string line in Data)
            {
                string[] Parts = line.Split(',');
                string[] Part1 = Parts[0].Split("-");
                int[] Elf1 = new int[] { Int32.Parse(Part1[0]),Int32.Parse(Part1[1]) };
                string[] Part2 = Parts[1].Split("-");
                int[] Elf2 = new int[] { Int32.Parse(Part2[0]), Int32.Parse(Part2[1]) };

                if ((Elf1[1] >= Elf2[0]) & (Elf2[1] >= Elf1[0]))
                {
                    answer++;
                }


            }

            return answer;
        }
    }
}
