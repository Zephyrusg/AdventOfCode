using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
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
            Dictionary<(int change4 , int chang3 , int change2 , int change1, int changeindex), int> prices = new();
            int listindex = 0;
            foreach(var list in ListofProccessedSecrets)
            {
                
                int startsecret = list[0].secret;
           
                int changeindex = 0;
                for (int i = 4; i < list.Count; i++)
                {
                    var change1 = list[i - 4].secret - list[i - 3].secret;
                    var change2 = list[i - 3].secret - list[i - 2].secret;
                    var change3 = list[i - 2].secret - list[i - 1].secret;
                    var change4 = list[i - 1].secret - list[i - 0].secret;
                    int tempprice = list[i].secret;
                    //int temprice = startsecret + change4 + change3 + change2 + change1;
                    if (!prices.ContainsKey((change4,change3,change2,change1, changeindex))) prices.Add((change4, change3, change2, change1, changeindex), 0);
                    prices[(change4, change3, change2, change1, changeindex)] += tempprice;
                    changeindex++;
                   
                }
                
                listindex++;

            }

            answer = (int)prices.MaxBy(p => p.Value).Value;
            return answer;
        }

    }
}
