using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022
{
    internal class Day10
    {
        static string Path = ".\\2022\\Input\\InputDay10.txt";

        public static int Part1()
        {
            string[] Data = File.ReadAllLines(Path);
            int[] CheckMoments = new int[] { 20, 60, 100, 140, 180, 220 };
            int Cycle = 0;
            int SignalStrength = 0;
            int register = 1;
            foreach (string line in Data) {

                int ExecutionTime;
                int Increase;
                switch (line)
                {
                    case "noop":
                        {
                            ExecutionTime = 1;
                            Increase = 0;
                            break;
                        }
                    default:
                        {
                            ExecutionTime = 2;
                            Increase = Int32.Parse(line.Split(' ')[1]);
                            break;
                        }
                }

                while (ExecutionTime-- > 0) {
                    Cycle++;

                    if (CheckMoments.Contains(Cycle)) {
                        SignalStrength += register * Cycle;
                    }

                    if (ExecutionTime == 0)
                    {
                        register += Increase;
                    }

                }
                
                if (Cycle == 220) {
                    break;
                }

            }

            int answer = SignalStrength;
            return answer;
        }

        public static void Part2()
        {
            string[] Data = File.ReadAllLines(Path);
            int Cycle = 0;
            int register = 1;
            int[] SpritePosition = new int[] { 0, 1, 2 };
            string[] CRT = new string[6];
            int CRTindex = 0;
            int CRTDrawIndex = 0;
            foreach (string line in Data)
            {

                int ExecutionTime;
                int Increase;
                switch (line)
                {
                    case "noop":
                        {
                            ExecutionTime = 1;
                            Increase = 0;
                            break;
                        }
                    default:
                        {
                            ExecutionTime = 2;
                            Increase = Int32.Parse(line.Split(' ')[1]);
                            break;
                        }
                }

                while (ExecutionTime-- > 0)
                {
                    Cycle++;
                   

                    if (SpritePosition.Contains(CRTDrawIndex))
                    {
                        CRT[CRTindex] += '#';
                    }
                    else {
                        CRT[CRTindex] += '.';
                    }

                    if (ExecutionTime == 0)
                    {
                        register += Increase;
                        SpritePosition = new int[] { register - 1, register, register + 1 };

                    }
                    if (Cycle % 40 == 0)
                    {
                        CRTindex++;
                        CRTDrawIndex= 0;
                    }
                    else {
                        CRTDrawIndex++;
                    }
                }

                if (Cycle == 240)
                {
                    break;
                }

            }
            foreach(string outputline in CRT)
            {
                Console.WriteLine(outputline);
            }

        }
    }
}
