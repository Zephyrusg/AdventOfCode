using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdventOfCode
{
    internal class Y2023D22
    {
        public static string[] Lines = File.ReadAllLines(".\\2023\\Input\\inputDay22.txt");
        static int Height = Lines.Count();
        static int Width = Lines[0].Length;
        static List<Brick> Bricks = new();
        static Dictionary<int, List<int>> SupportList = new Dictionary<int, List<int>>();
        class Brick : IComparable<Brick>
        {
            public (int x, int y, int z) coordinate1;
            public (int x, int y, int z) coordinate2;
            int Supports = 0;
            public int id;

            public Brick((int x, int y, int z) coordinate1, (int x, int y, int z) coordinate2, int id)
            {
                this.coordinate1 = coordinate1;
                this.coordinate2 = coordinate2;
                this.id = id;
            }
            public int CompareTo(Brick other)
            {
                return coordinate1.z.CompareTo(other.coordinate1.z);
            }

            public void CanMove()
            {
                bool Canfall = true;

                while (Canfall)
                {
                    int i = coordinate1.z;
                    if (i == 1)
                    {
                        break;
                    }
                    List<Brick> Bricksonebelow = Bricks.Where(b =>b.coordinate1.z <= i - 1 && b.coordinate2.z >= i - 1).ToList();
                    foreach (Brick b in Bricksonebelow)
                    {
                        for(int x = coordinate1.x; x <= coordinate2.x; x++)
                        {
                            for (int y = coordinate1.y; y <= coordinate2.y; y++)
                            {
                                if (b.coordinate1.x <= x && b.coordinate2.x >= x  && b.coordinate1.y <= y && b.coordinate2.y >= y) { 
                                    Canfall = false;
                                    b.Supports++;
                                    SupportList[b.id].Add(this.id);
                                    goto nextBlock;
                                }
                            }
                        }
                    nextBlock:;
                    }
                    if (Canfall) {
                        coordinate1.z--;
                        coordinate2.z--;
                    }
                }

            }




        }
        
        public int Part1() 
        {
            int answer = 0;
            int id = 0;
            foreach(var line in Lines)
            {
                var parts = line.Split('~');
                List<int> temp = parts[0].Split(',').Select(int.Parse).ToList();
                (int x, int y, int z) Coo1 = (temp[0], temp[1], temp[2]);
                temp = parts[1].Split(',').Select(int.Parse).ToList();
                (int x, int y, int z) Coo2 = (temp[0], temp[1], temp[2]);
                Bricks.Add(new(Coo1, Coo2, id));
                SupportList.Add(id, []);
                id++;
                

            }
            Bricks.Sort();

            int max = Bricks.Max(b => b.coordinate2.z);
            bool CanFall = true;
            
            foreach(var b in Bricks)
            {
                b.CanMove();
            }


            foreach (var b in Bricks) {
                if (SupportList[b.id].Count == 0)
                {
                    answer++;
                    continue;
                }
               
                foreach (var brick in SupportList[b.id])
                {
                    int count = 0;
                    foreach (var list in SupportList.Values)
                    {
                        if (list.Contains(brick))
                        {
                            count++;
                        }
                    }
                    if(count == 1)
                    {
                        goto NextBrick;
                    }
                }
                answer++;

            NextBrick:;
            }

            return answer;
        }

        public int Part2()
        {
            int answer = 0;




            return answer;
        }

    }
}
