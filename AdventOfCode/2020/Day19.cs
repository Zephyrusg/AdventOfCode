using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2020
{
    internal class Day19
    {

        public static int Day19A() {
            int test = 0;

            string[] Data = File.ReadAllLines(".\\2020\\Input\\InputDay19Rules.txt");

            Array.Sort(Data);
            foreach (string Item in Data) {
                Console.WriteLine(Item);
            }
            

            return test;
        }
    }
}
