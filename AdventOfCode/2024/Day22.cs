using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class Y2024D22
    {
        public static string[] Lines = File.ReadAllLines(".\\2024\\Input\\inputDay22.txt");

        static private List<List<(int i, int secret)>> ListofProccessedSecrets = new();
   
        private long ProccessSecret(long secret)
        {
            long nextsecret = 0;

            nextsecret = secret * 64;
            secret ^= nextsecret;
            secret = secret % 16777216;

            nextsecret = secret / 32;
            secret ^= nextsecret;
            secret = secret % 16777216;

            nextsecret = secret * 2048;
            secret ^= nextsecret;
            secret = secret % 16777216;

            return secret;
        }

        public long Part1() 
        {
            long answer = 0;

            long[] secrets = Lines.Select(line => long.Parse(line)).ToArray();
            foreach(var secret in secrets)
            {
                List<(int i, int secret)> ProccessedSecrets = new();
                ProccessedSecrets.Add((0, (int)secret % 10));
                long nextsecret = secret;
                int times = 0;
                while(times < 2000)
                {
                    nextsecret = ProccessSecret(nextsecret);
                    times++;
                    ProccessedSecrets.Add((times, (int)nextsecret % 10));
                }
                ListofProccessedSecrets.Add(ProccessedSecrets);
                answer += nextsecret;
                
               

            }


            return answer;
        }

        public int Part2()
        {
            int answer = 0;
            Dictionary<(int change1 , int change2 , int change3 , int change4), int> prices = new();
            int listindex = 0;
            foreach(var list in ListofProccessedSecrets)
            {
                
                int startsecret = list[0].secret;
           
               
                HashSet<(int change1, int change2, int change3, int change4)> kwownSequence = new();
                for (int i = 4; i < list.Count; i++)
                {
                    var change1 = list[i - 3].secret - list[i - 4].secret;
                    var change2 = list[i - 2].secret - list[i - 3].secret;
                    var change3 = list[i - 1].secret - list[i - 2].secret;
                    var change4 = list[i - 0].secret - list[i - 1].secret;
                    int tempprice = list[i].secret;
                    if(kwownSequence.Contains((change1, change2, change3, change4))){
                        continue;
                    }
                    kwownSequence.Add((change1, change2, change3, change4));
                    if (!prices.ContainsKey((change1,change2,change3,change4))) prices.Add((change1, change2, change3, change4), 0);
                    prices[(change1, change2, change3, change4)] += tempprice;
                   
                   
                }

            }

            answer = (int)prices.MaxBy(p => p.Value).Value;
            return answer;
        }

    }
}
