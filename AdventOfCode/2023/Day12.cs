using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class Y2023D12
    {
        static Dictionary<string,long> Memory = new Dictionary<string, long>();
        public static string[] lines = File.ReadAllLines(".\\2023\\Input\\inputDay12.txt");


        long Calculate(string UnknownPart, List<int> groups)
        {
            string key = $"{UnknownPart},{string.Join(',', groups)}";  

            if (Memory.TryGetValue(key, out var value))
            {
                return value;
            }

            value = GetCount(UnknownPart, groups);
            Memory[key] = value;

            return value;
        }

        long GetCount(string UnknownPart, List<int> groups)
        {
            while (true)
            {
                if (groups.Count == 0)
                {
                    if (UnknownPart.Contains('#')) 
                    {
                        return 0;
                    }
                    else 
                    { 
                        return 1;
                    }
                    
                }

                if (string.IsNullOrEmpty(UnknownPart))
                {
                    return 0;
                }

                if (UnknownPart.StartsWith('.'))
                {
                    UnknownPart = UnknownPart.Trim('.'); 
                    continue;
                }

                if (UnknownPart.StartsWith('?'))
                {
                    return Calculate("." + UnknownPart[1..], groups) + Calculate("#" + UnknownPart[1..], groups);
                }

                if (UnknownPart.StartsWith('#'))                 {
                    if (groups.Count == 0)
                    {
                        return 0;
                    }

                    if (UnknownPart.Length < groups[0])
                    {
                        return 0;
                    }

                    if (UnknownPart[..groups[0]].Contains('.'))
                    {
                        return 0; 
                    }

                    if (groups.Count > 1)
                    {
                        if (UnknownPart.Length < groups[0] + 1 || UnknownPart[groups[0]] == '#')
                        {
                            return 0;
                        }

                        UnknownPart = UnknownPart[(groups[0] + 1)..]; 
                        groups = groups[1..];
                        continue;
                    }

                    UnknownPart = UnknownPart[groups[0]..];
                    groups = groups[1..];
                    continue;
                }

            }
        }
        
        public long Part1() 
        {
            long answer = 0;
            long totalArrangements = 0;

            foreach (string line in lines)
            {

                string[] parts = line.Split(' ');

                string unknownPart = parts[0];
                string[] groups = parts[1].Split(',').ToArray();

                List<int> damagedGroups = groups.Select(int.Parse).ToList();

                totalArrangements += Calculate(unknownPart, damagedGroups);
           

                //Console.WriteLine($"{line} - {arrangements} arrangements");
            }

            answer = totalArrangements;

            return answer;
        }

        public long Part2()
        {
            long answer = 0;
            long totalArrangements = 0;

            foreach (string line in lines)
            {
                string[] parts = line.Split(' ');

                string unknownPart = parts[0];
                string unknownPart5x =String.Concat(Enumerable.Repeat(unknownPart+'?', 4)) + unknownPart;
                
                
                string[] groups = parts[1].Split(',').ToArray();

                List<int> damagedGroups = groups.Select(int.Parse).ToList();
                List<int> DamagedGroups5x = new List<int>();
                for (int i = 0; i < 5; i++)
                {
                    DamagedGroups5x = DamagedGroups5x.Concat(damagedGroups).ToList();
                }

                totalArrangements += Calculate(unknownPart5x, DamagedGroups5x);

            }

            answer = totalArrangements;

            return answer;
        }

    }
}
