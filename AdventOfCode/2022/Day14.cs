using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022
{
    internal class Day14
    {
        static HashSet<Tuple<int, int>> SolidRocks= new HashSet<Tuple<int, int>>();
        static int UnmovableSandUnits = 0;
        static int MaxDepth = 0;
        static string Path = ".\\2022\\Input\\InputDay14.txt";
        // Makes line to the right
        static void BuildHorizontal(Tuple<int, int> PointA, Tuple<int, int> PointB) {

            for (int i = PointA.Item1; i <= PointB.Item1; i++) { 
                SolidRocks.Add(new Tuple<int,int>(i, PointA.Item2));
            }
            if(MaxDepth < PointA.Item2)
            {
                MaxDepth= PointA.Item2;
            }
        }
        //Makes Line Down the Depth
        static void BuildVertical (Tuple<int, int> PointA, Tuple<int, int> PointB)
        {
            for (int i = PointA.Item2; i <= PointB.Item2; i++)
            {
                SolidRocks.Add(new Tuple<int, int>(PointA.Item1, i));
            }
            if (MaxDepth < PointB.Item2)
            {
                MaxDepth = PointB.Item2;
            }
        }
        static bool DropSand() {
            
            bool result = true;
            Tuple<int, int> FallingSand = new Tuple<int, int>(500, 0);

            while (result)
            {
                if (FallingSand.Item2 >= MaxDepth)
                {
                    result = false;
                }
                else if (!SolidRocks.Contains(new Tuple<int, int>(FallingSand.Item1, FallingSand.Item2 + 1)))
                {
                    FallingSand = new Tuple<int, int>(FallingSand.Item1, FallingSand.Item2 + 1);
                }
                else if (!SolidRocks.Contains(new Tuple<int, int>(FallingSand.Item1 - 1, FallingSand.Item2 + 1)))
                {
                    FallingSand = new Tuple<int, int>(FallingSand.Item1 - 1, FallingSand.Item2 + 1);
                }
                else if (!SolidRocks.Contains(new Tuple<int, int>(FallingSand.Item1 + 1, FallingSand.Item2 + 1)))
                {
                    FallingSand = new Tuple<int, int>(FallingSand.Item1 + 1, FallingSand.Item2 + 1);
                } 
                else
                {
                    UnmovableSandUnits++;
                    SolidRocks.Add(FallingSand);
                    break;
                }
            }
               
            return result;
        }
        static bool DropSandPart2()
        {

            bool result = true;
            Tuple<int, int> FallingSand = new Tuple<int, int>(500, 0);
            if (SolidRocks.Contains(FallingSand)) {
                return false;
            }


            while (result)
            {
                
                if (FallingSand.Item2 == MaxDepth-1)
                {
                    UnmovableSandUnits++;
                    SolidRocks.Add(FallingSand);
                    break;
                }
                else if (!SolidRocks.Contains(new Tuple<int, int>(FallingSand.Item1, FallingSand.Item2 + 1)))
                {
                    FallingSand = new Tuple<int, int>(FallingSand.Item1, FallingSand.Item2 + 1);
                }
                else if (!SolidRocks.Contains(new Tuple<int, int>(FallingSand.Item1 - 1, FallingSand.Item2 + 1)))
                {
                    FallingSand = new Tuple<int, int>(FallingSand.Item1 - 1, FallingSand.Item2 + 1);
                }
                else if (!SolidRocks.Contains(new Tuple<int, int>(FallingSand.Item1 + 1, FallingSand.Item2 + 1)))
                {
                    FallingSand = new Tuple<int, int>(FallingSand.Item1 + 1, FallingSand.Item2 + 1);
                }
                else
                {
                    UnmovableSandUnits++;
                    SolidRocks.Add(FallingSand);
                    break;
                }
            }

            return result;
        }
        static string[] Data = File.ReadAllLines(Path);
        public static int Part1()
        {
            int answer = 0;
            foreach(string line in Data)
            {
                string[] DrawLines = line.Split(" -> ");
                for (int i = 0; i < DrawLines.Length - 1; i++)
                {
                   Tuple<int,int> Direction1 = new Tuple<int, int>(Int32.Parse(DrawLines[i].Split(",")[0]), (Int32.Parse(DrawLines[i].Split(",")[1])));
                   Tuple<int, int> Direction2 = new Tuple<int, int>(Int32.Parse(DrawLines[i+1].Split(",")[0]), (Int32.Parse(DrawLines[i+1].Split(",")[1])));
                    if (Direction1.Item2 == Direction2.Item2)
                    {
                        if (Direction1.Item1 < Direction2.Item1)
                        {
                            BuildHorizontal(Direction1, Direction2);
                        }
                        else
                        {
                            BuildHorizontal(Direction2, Direction1);
                        }

                    }
                    else {
                        if (Direction1.Item2 < Direction2.Item2)
                        {
                            BuildVertical(Direction1, Direction2);
                        }
                        else
                        {
                            BuildVertical(Direction2, Direction1);
                        }
                    }
                    
                
                }
            }
            bool result = true;

            while (result)
            {

                result = DropSand();

            }
            answer = UnmovableSandUnits;

            return answer;
        }

   
        public static int Part2()
        {
            int answer = 0;
            SolidRocks = new HashSet<Tuple<int, int>>();
            UnmovableSandUnits = 0;

            foreach (string line in Data)
            {
                string[] DrawLines = line.Split(" -> ");
                for (int i = 0; i < DrawLines.Length - 1; i++)
                {
                    Tuple<int, int> Direction1 = new Tuple<int, int>(Int32.Parse(DrawLines[i].Split(",")[0]), (Int32.Parse(DrawLines[i].Split(",")[1])));
                    Tuple<int, int> Direction2 = new Tuple<int, int>(Int32.Parse(DrawLines[i + 1].Split(",")[0]), (Int32.Parse(DrawLines[i + 1].Split(",")[1])));
                    if (Direction1.Item2 == Direction2.Item2)
                    {
                        if (Direction1.Item1 < Direction2.Item1)
                        {
                            BuildHorizontal(Direction1, Direction2);
                        }
                        else
                        {
                            BuildHorizontal(Direction2, Direction1);
                        }

                    }
                    else
                    {
                        if (Direction1.Item2 < Direction2.Item2)
                        {
                            BuildVertical(Direction1, Direction2);
                        }
                        else
                        {
                            BuildVertical(Direction2, Direction1);
                        }
                    }


                }
            }
            MaxDepth += 2;
            bool result = true;

            while (result)
            {

                result = DropSandPart2();

            }
            answer = UnmovableSandUnits;

            return answer;
        }
    }
}
