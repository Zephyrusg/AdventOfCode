using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class Y2023D14
    {
        

        public static string[] Beams = File.ReadAllLines(".\\2023\\Input\\inputDay14.txt");




        public void MoveRockNorth() {

            for (int y = 1; y < Beams.Count(); y++)
            {
                for (int x = 0; x < Beams[0].Length; x++)
                {

                    char AtPoint = Beams[y][x];

                    if (AtPoint != 'O')
                    {
                        continue;
                    }
                    int Next = 1;
                    while (Beams[y - Next][x] == '.')
                    {
                        Beams[y - Next] = Beams[y - Next][..x] + 'O' + Beams[y - Next][(x + 1)..];
                        Beams[y - Next + 1] = Beams[y - Next + 1][..x] + '.' + Beams[y - Next + 1][(x + 1)..];
                        Next++;

                        if (y - Next < 0)
                        {
                            break;
                        }

                    }
                }
            }
        }

        public void MoveRockSouth()
        {

            for (int y = Beams.Count() -2; y >= 0; y--)
            {
                for (int x = 0; x < Beams[0].Length; x++)
                {

                    char AtPoint = Beams[y][x];

                    if (AtPoint != 'O')
                    {
                        continue;
                    }
                    int Next = 1;
                    while (Beams[y + Next][x] == '.')
                    {
                        Beams[y + Next] = Beams[y + Next][..x] + 'O' + Beams[y + Next][(x + 1)..];
                        Beams[y + Next - 1] = Beams[y + Next - 1][..x] + '.' + Beams[y + Next - 1][(x + 1)..];
                        Next++;

                        if (y + Next == Beams.Count())
                        {
                            break;
                        }

                    }
                }
            }
        }

        public void MoveRockWest()
        {

            for (int y = 0; y < Beams.Count(); y++)
            {
                for (int x = 1; x < Beams[0].Length; x++)
                {

                    char AtPoint = Beams[y][x];

                    if (AtPoint != 'O')
                    {
                        continue;
                    }
                    int Next = 1;
                    while (Beams[y][x-Next] == '.')
                    {
                        Beams[y] = Beams[y][..(x-Next)] + 'O' + Beams[y][(x - Next +1)..];
                        Beams[y] = Beams[y][..(x - Next + 1)] + '.' + Beams[y][(x - Next + 2)..];
                        Next++;

                        if (x - Next < 0)
                        {
                            break;
                        }

                    }
                }
            }

            
        }
        public void MoveRockEast()
        {

            for (int y = 0; y < Beams.Count(); y++)
            {
                for (int x = Beams[y].Length -2 ; x >= 0; x--)
                {

                    char AtPoint = Beams[y][x];

                    if (AtPoint != 'O')
                    {
                        continue;
                    }
                    int Next = 1;
                    while (Beams[y][x + Next] == '.')
                    {
                        Beams[y] = Beams[y][..(x + Next)] + 'O' + Beams[y][(x + Next + 1)..];
                        Beams[y] = Beams[y][..(x + Next - 1)] + '.' + Beams[y][(x + Next)..];
                        Next++;

                        if (x + Next == Beams[0].Length)
                        {
                            break;
                        }

                    }
                }
            }


        
        }
        public int Part1() 
        {
            int answer = 0;

            
            MoveRockNorth();



            for (int y = 0; y < Beams.Count(); y++)
            {

                for (int x = 0; x < Beams[0].Length; x++)
                {
                
                    if (Beams[y][x] == 'O')
                    {
                        answer += Beams.Count() - y;
                    }


                }
            }







            return answer;
        }

        public int Part2()
        {
            int answer = 0;
            Beams = File.ReadAllLines(".\\2023\\Input\\inputDay14.txt");
            Dictionary<string, int> Memory = new Dictionary<string, int>();

            int Cycle = 1;
            int Goal = 1000000000;
            while (Cycle <= Goal)
            {
                MoveRockNorth();
                MoveRockWest();
                MoveRockSouth();
                MoveRockEast();

                string state = string.Join('\n', Beams);

                if (Memory.TryGetValue(state, out int value))
                {
                    int remaining = Goal - Cycle - 1;
                    int loop = Cycle - value;

                    int loopRemaining = remaining % loop;
                    Cycle = Goal - loopRemaining - 1;
                    
                }

                Memory[state] = Cycle++;
            }

            for (int y = 0; y < Beams.Count(); y++)
            {

                for (int x = 0; x < Beams[0].Length; x++)
                {

                    if (Beams[y][x] == 'O')
                    {
                        answer += Beams.Count() - y;
                    }


                }
            }

            return answer;
        }

    }
}
