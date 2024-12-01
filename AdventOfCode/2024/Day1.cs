using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace AdventOfCode
{
    internal class Y2024D1
    {
        public static string[] Lines = File.ReadAllLines(".\\2024\\Input\\inputDay1.txt");
        public static List<int> ListA = new();
        public static List<int> ListB = new();
        public int Part1() 
        {
            int answer = 0;
           
            
            foreach(string line in Lines)
            {
                int[]parts = line.Split("   ").Select(u => int.Parse(u)).ToArray();
                ListA.Add(parts[0]);
                ListB.Add(parts[1]);

            }
            ListA.Sort();
            ListB.Sort();
            for(int x =0; x < ListA.Count(); x++)
            {
                answer += Math.Abs(ListA[x] - ListB[x]);
            }

            return answer;
        }

        public int Part2()
        {
            int answer = 0;
            for (int x = 0; x < ListA.Count(); x++)
            {
                answer += ListB.Where(n => n == ListA[x]).Count() * ListA[x];
            }

            return answer;
        }

    }
}
