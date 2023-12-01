using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AdventOfCode
{
    internal class Y2018D1
    {

        public static int Part1()
        {

            string[] lines = File.ReadAllLines(".\\2018\\Input\\InputDay1.txt");
            int som = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                som += Int32.Parse(lines[i]);
            }
            return som;

        }
        public static int Part2()
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
