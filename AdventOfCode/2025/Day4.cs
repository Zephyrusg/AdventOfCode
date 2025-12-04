using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class Y2025D4
    {
        public static string[] Lines = File.ReadAllLines(".\\2025\\Input\\inputDay4.txt");
        public static char[][] Grid = Lines.Select(item => item.ToArray()).ToArray();
        static int Height = Lines.Count();
        static int Width = Lines[0].Length;

        private static int CountNeighbours(int i, int j)
        {
            int neighbours = 0;

          
            for (int di = -1; di <= 1; di++)
            {
                for (int dj = -1; dj <= 1; dj++)
                {
                    if (di == 0 && dj == 0)
                    {
                        continue; 
                    }

                    int ni = i + di;
                    int nj = j + dj;

                    if (ni >= 0 && ni < Height && nj >= 0 && nj < Width)
                    {
                        if (Grid[ni][nj] == '@')
                        {
                            neighbours++;
                        }
                    }
                }
            }

            return neighbours;
        }

        private static void FreePaperRollSpace(int i, int j)
        {
            Grid[i][j] = '.';
        }



        public int Part1() 
        {
            int answer = 0;

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    if(Grid[i][j] == '@')
                    {
                        if(CountNeighbours(i,j) < 4)
                        {
                            answer++; 
                        }
                    }
                }
            }


            return answer;
        }

        public int Part2()
        {
            int answer = 0;
            bool StillClearing = true;
            while (StillClearing)
            {   
                StillClearing = false;
                for (int i = 0; i < Height; i++)
                {
                    for (int j = 0; j < Width; j++)
                    {
                        if (Grid[i][j] == '@')
                        {
                            if (CountNeighbours(i, j) < 4)
                            {
                                FreePaperRollSpace(i, j);
                                StillClearing = true;

                                answer++;
                            }
                        }
                    }
                }
            }


            return answer;
        }

    }
}
