using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdventOfCode
{
    internal class Y2017D6
    {
        public static int loopstart = 0;
        public static bool loopstop = false;
        public static int cycle = 0;
        public static bool part2 = false;
        public static int[] foundstate = new int[0];
        public static bool isInArray(List<int[]> list1, int[] array)
        {
            int times = 0;
            foreach (var subArray in list1)
            {
                if (Enumerable.SequenceEqual(subArray, array))
                {    
                    part2 = true;
                    loopstart = cycle;
                    cycle = 0;
                    foundstate = array;
                    return true;
                }
            }

            return false;
        }
        public static int Part1() 
        {
            string text = File.ReadAllText(".\\2017\\Input\\inputDay6.txt");
            List<int> MemoryBanks = text.Split(" ").Select(x => int.Parse(x)).ToList();
            List<int[]> States = new List<int[]>();
            int[] state = MemoryBanks.ToArray();
            
            do {
                cycle++;
                int HigestNumber = MemoryBanks.Max();
                int MemoryBank = MemoryBanks.IndexOf(HigestNumber);

                MemoryBanks[MemoryBank] = 0;

                for (int i = 0; i < HigestNumber; i++)
                {
                    if (MemoryBank + 1 == MemoryBanks.Count)
                    {
                        MemoryBank = 0;
                    }
                    else
                    {
                        MemoryBank += 1;
                    }

                    MemoryBanks[MemoryBank]++;
                }
                state = MemoryBanks.ToArray();

                if (isInArray(States,state) ){

                    break;


                }
                States.Add(state);
                
                
            }               
            while (true);       

            return loopstart;
        }

        public static int Part2()
        {

            string text = File.ReadAllText(".\\2017\\Input\\inputDay6.txt");
            cycle = 0;
            part2 = false;
            List<int> MemoryBanks = text.Split(" ").Select(x => int.Parse(x)).ToList();
            List<int[]> States = new List<int[]>();
            int[] state = MemoryBanks.ToArray();

            do
            {
                cycle++;
                int HigestNumber = MemoryBanks.Max();
                int MemoryBank = MemoryBanks.IndexOf(HigestNumber);

                MemoryBanks[MemoryBank] = 0;

                for (int i = 0; i < HigestNumber; i++)
                {
                    if (MemoryBank + 1 == MemoryBanks.Count)
                    {
                        MemoryBank = 0;
                    }
                    else
                    {
                        MemoryBank += 1;
                    }

                    MemoryBanks[MemoryBank]++;
                }
                state = MemoryBanks.ToArray();
                if (!part2)
                {
                    isInArray(States, state);
                }
                else if (Enumerable.SequenceEqual(state, foundstate)) { 
                    loopstop = true;
                }
               
                   
                States.Add(state);


            }
            while (!loopstop);

            return cycle;

        }

    }
}
