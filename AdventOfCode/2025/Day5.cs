using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class Y2025D5
    {
        public static string[] Lines = File.ReadAllLines(".\\2025\\Input\\inputDay5.txt");
        public static List<Tuple<long, long>> Ranges = new List<Tuple<long, long>>();
        public int Part1() 
        {
            int answer = 0;
            int i = 0;
            while (Lines[i] != "")
            {
                string[] parts = Lines[i].Trim().Split('-');
                long Start = long.Parse(parts[0].Trim());
                long End = long.Parse(parts[1].Trim());
                Ranges.Add(new Tuple<long, long>(Start, End));
                i++;
            }
                
            i++;
            while (i < Lines.Length)
            {
                long ingredient = long.Parse(Lines[i].Trim());
                for (int j = 0; j < Ranges.Count; j++)
                {
                    if (ingredient >= Ranges[j].Item1 && ingredient <= Ranges[j].Item2)
                    {
                        answer++;
                        break;
                    }
                }
                
                i++;
            }

                return answer;
        }

        public long Part2()
        {  

            var sorted = Ranges.OrderBy(r => r.Item1).ToList();
            long curStart = sorted[0].Item1;
            long curEnd = sorted[0].Item2;
            long total = 0;
            for (int idx = 1; idx < sorted.Count; idx++)
            {
                var r = sorted[idx];
                if (r.Item1 <= curEnd) 
                {
                    curEnd = Math.Max(curEnd, r.Item2);
                }
                else
                {
                    total += (curEnd - curStart + 1);
                    curStart = r.Item1;
                    curEnd = r.Item2;
                }
            }
       
            total += (curEnd - curStart + 1);
            return total;
        }

    }
}
