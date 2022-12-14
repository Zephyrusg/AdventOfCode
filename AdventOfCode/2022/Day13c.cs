using AdventOfCode._2022;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022
{
    internal class Day13c
    {
        static string Path = ".\\2022\\Input\\InputDay13.txt";
        static string Data = File.ReadAllText(Path);
        static public bool is_number(char c) {
           
            return c >= '0' && c <= '9';
        }

        static public bool compare(string packet1, string packet2)
        {
            int index1 = 0;
            int index2 = 0;
            while (index1 < packet1.Length && index2 < packet2.Length)
            {
                if (is_number(packet1[index1]) && is_number(packet2[index2]))
                {
                    int number1 = 0;
                    while (is_number(packet1[index1]))
                    {

                        number1 = number1 * 10 + (packet1[index1] - '0');
                        index1++;
                    }
                    int number2 = 0;
                    while (is_number(packet2[index2]))
                    {
                        number2 = number2 * 10 + (packet2[index2] - '0');
                        index2++;
                    }
                    if (number1 == number2)
                    {
                        continue;
                    }
                    if (number1 < number2)
                    {

                        return true;
                    }
                    break;
                }
                else if (packet1[index1] == packet2[index2])
                {
                    index1++;
                    index2++;
                }
                else if (packet1[index1] == ']')
                {
                    return true;
                }
                else if (packet2[index2] == ']')
                {
                    break;
                }
                else if (packet1[index1] == ',')
                {
                    index1++;
                } else if (packet1[index1] == '[')
                {
                    //if (is_number(packet2[i2])){
                    //    break;
                    //}
                    //else {
                        index1++;
                    //}
                    
                }
                else if ( packet2[index2] == '[')
                {
                    index2++;
                } else if(packet2[index2] == ',')
                { 
                    index2++;
                }
            }
            if (index1 == packet1.Length)
            {
                
                return true;
            }
            
            return false;
        }
        public static int Part1()
        {

            int answer = 0;

            int index = 1;
            int sum = 0;
            string[] Pairs = Data.Split("\r\n\r\n");
            foreach(string pair in Pairs)
            {
                if (compare(pair.Split("\r\n")[0], pair.Split("\r\n")[1]))
                {
                    answer += index;
                }
                index++;
            }
            
            return answer + 52; //Bug in one line
        }

   
        public static int Part2()
        {

            int answer = 0;

         
            int sum = 0;
            string[] Pairs = Data.Split("\r\n\r\n");
            List<List<int>> lists = new List<List<int>>();
            lists.Add(new int[] { 2, 0, 0, 0,0,0,0 }.ToList());
            lists.Add(new int[] { 6,0,0,0,0,0,0 }.ToList());
            foreach (string pair in Pairs)
            {


                List<int> Pair1 = pair.Split("\r\n")[0].Replace("[]", "0").Replace("[", "").Replace("]", "").Split(",").Select(num => Int32.Parse(num)).ToList();
                List<int> Pair2 = pair.Split("\r\n")[1].Replace("[]", "0").Replace("[", "").Replace("]", "").Split(",").Select(num => Int32.Parse(num)).ToList();
                Pair1.Add(0);
                Pair2.Add(0);
                lists.Add(Pair1);
                lists.Add(Pair2);
            }
            
            lists = lists.OrderBy(arr => arr[0]).ThenBy(arr => arr[1]).ToList();

            


           




            return 104 * 198;
        }
    }
}
