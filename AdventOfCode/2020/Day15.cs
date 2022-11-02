using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace AdventOfCode._2020
{
    internal class Day15
    {
        public static int Day15A()
        {
            int answer = 0;

            int[] Input = File.ReadAllText(".\\2020\\Input\\inputDay15.txt").Split(",").Select(num => Int32.Parse(num)).ToArray();

            Dictionary<int, int> Memory = new Dictionary<int, int>();
            int Turn = 1;

            foreach (int number in Input)
            {
                Memory.Add(number, Turn);
                Turn++;

            }
            // first starting round fix
            int Lastnumber = 0;
            Turn++;

            while (Turn <= 30000000) {

                if (Memory.ContainsKey(Lastnumber))
                {
                    int turninmemory = Memory[Lastnumber];
                    Memory[Lastnumber] = Turn - 1;
                    int lastturn = Turn - 1;
                    Lastnumber = lastturn - turninmemory;
                }
                else { 
                    Memory.Add(Lastnumber, Turn-1);
                    Lastnumber = 0;
                }
                Turn++;
            
            }

            answer = Lastnumber;
            return answer;
        }
    }
}
