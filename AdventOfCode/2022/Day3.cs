using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022
{
    internal class Day3
    {
        static string Path = ".\\2022\\Input\\InputDay3.txt";

        public static char FindDouble(string s1, string s2)
        {
            
            var Map = new HashSet<char>();


            for (int i = 0; i < s1.Length; i++)
            {
                if (!Map.Contains(s1[i]))
                {
                    Map.Add(s1[i]);
                }
                
            }

            for (int i = 0; i < s2.Length; i++)
            {
                if (Map.Contains(s2[i]))
                {
                    return s2[i];
                }
                // else return 0
            }
            return '0'; 
        }

        public static char FindBadge(string s1, string s2, string s3) {


            var Map = new HashSet<char>();


            for (int i = 0; i < s1.Length; i++)
            {
                if (!Map.Contains(s1[i]))
                {
                    Map.Add(s1[i]);
                }

            }

            foreach (char item in Map) { 
                
                if(s2.Contains(item) && s3.Contains(item))
                {
                    return item;
                }
            
            } 

            return '0';
        }

        public static int Part1()
        {
            string[] Data = File.ReadAllLines(Path);
            int answer = 0;

            foreach (string line in Data) {

                int PriorityValue = 0;
                string s1 = line.Substring(0, (line.Length / 2));
                string s2 = line.Substring((line.Length / 2), (line.Length / 2));
                char package = FindDouble(s1, s2);

                if (char.IsLower(package))
                {
                    PriorityValue = (int)package % 32;
                }
                else {
                    PriorityValue = (int)package % 32 + 26;
                }
                answer += PriorityValue;
                
            }

            return answer;
        }

        public static int Part2()
        {
            string[] Data = File.ReadAllLines(Path);
            int answer = 0;

            List<List<string>> AllGroups = new List<List<string>>();
            int index = 0;
            List<string> Group = new List<string>();
            foreach (string line in Data) {
                if (index < 3)
                {
                    Group.Add(line);
                    index++;
                }
                else {
                    index = 1;
                    AllGroups.Add(Group);
                    Group = new List<string>();
                    Group.Add(line);
                }

            }
            AllGroups.Add(Group);

            foreach (List<string> GroupofBags in AllGroups) {
                int PriorityValue = 0;
                char Badge = FindBadge(GroupofBags[0], GroupofBags[1], GroupofBags[2]);
                if (char.IsLower(Badge))
                {
                    PriorityValue = (int)Badge % 32;
                }
                else
                {
                    PriorityValue = (int)Badge % 32 + 26;
                }
                answer += PriorityValue;
            }

            


            return answer;
        }
    }
}
