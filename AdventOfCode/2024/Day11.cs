using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class Y2024D11
    {
        public static string Line = File.ReadAllText(".\\2024\\Input\\inputDay11.txt");   
        static Dictionary<long, long> stones = new();

        void DoBlink(int TimestoBlink)
        {

            for (int blink = 0; blink < TimestoBlink; blink++)
            {
                Dictionary<long, long> nextStones = new Dictionary<long, long>();

                foreach (var stoneGroup in stones)
                {
                    long stone = stoneGroup.Key;
                    long count = stoneGroup.Value;
                    if (stone == 0)
                    {
                        // Rule 1: 
                        if(!nextStones.ContainsKey(1))
                            nextStones[1] = 0;
                        nextStones[1] += count;
                    }
                    else if (stone.ToString().Length % 2 == 0)
                    {
                        
                        string StringStone = stone.ToString();
                        // Rule 2: 
                        int mid = StringStone.Length / 2;
                        int leftHalf = int.Parse(StringStone.Substring(0, mid));
                        string rightHalf = StringStone.Substring(mid).TrimStart('0');


                        if (!nextStones.ContainsKey(leftHalf))
                            nextStones[leftHalf] = 0;
                        nextStones[leftHalf] += count;

                        var rightPart = rightHalf == "" ? 0 : long.Parse(rightHalf);

                        if (!nextStones.ContainsKey(rightPart))
                            nextStones[rightPart] = 0;
                        nextStones[rightPart] += count;

                    }
                    else
                    {
                        // Rule 3:
                      
                        var nextStone = stone * 2024;
                        if (!nextStones.ContainsKey(nextStone))
                            nextStones[nextStone] = 0;
                        nextStones[nextStone] += count;

                    }
                }

                stones = nextStones;
            }
        }
        public long Part1() 
        {
            var list = Line.Split(' ').Select(l => long.Parse(l));
            foreach(long number in list) {
                stones.Add(number, 1);
            }

            DoBlink(25);

            long totalStones = 0;
            foreach (var count in stones.Values)
            {
                totalStones += count;
            }

            return totalStones;
        }

        public long Part2()
        {
            stones = new();

            var list = Line.Split(' ').Select(l => long.Parse(l));
            foreach (long number in list)
            {
                stones.Add(number, 1);
            }

            DoBlink(75);

            long totalStones = 0;
            foreach (var count in stones.Values)
            {
                totalStones += count;
            }

            return totalStones;



        }

    }
}
