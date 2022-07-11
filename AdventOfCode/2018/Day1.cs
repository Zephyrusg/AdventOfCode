using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AdventOfCode._2018
{
    internal class Day1
    {

        public static int Day1A()
        {

            string[] lines = File.ReadAllLines(".\\2018\\Input\\InputDay1.txt");
            int som = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                som += Int32.Parse(lines[i]);
            }
            return som;

        }
        public static int Day1B()
        {
            string[] lines = File.ReadAllLines(".\\2018\\Input\\InputDay1.txt");

            HashSet<int> results = new HashSet<int>();

            int som = 0;
            while (true)
            {
                for (int i = 0; i < lines.Length; i++)
                {
                    som += Int32.Parse(lines[i]);
                    if (results.Contains(som))
                    {
                        return som;
                    }
                    else
                    {
                        results.Add(som);
                    }

                }
            }
        }
    }
}
