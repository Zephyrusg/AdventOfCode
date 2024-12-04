using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class Y2024D4
    {
        public static string[] Lines = File.ReadAllLines(".\\2024\\Input\\inputDay4.txt");
        public static int answer;
        void CheckXMas(int x, int y)
        {
            int maxheight = Lines.Count() - 1;
            int maxlength = Lines[0].Length - 1;
            //N

            if (y >= 3)
            {
                if (Lines[y][x] == 'X' && Lines[y - 1][x] == 'M' && Lines[y - 2][x] == 'A' && Lines[y - 3][x] == 'S')
                {
                    answer++;
                }
            }

            //NE

            if (y >= 3 && x <= maxlength - 3)
            {
                
                if (Lines[y][x] == 'X' && Lines[y - 1][x + 1] == 'M' && Lines[y - 2][x + 2] == 'A' && Lines[y - 3][x + 3] == 'S')
                {
                    answer++;
                }
                
            }
            //E

            if (x <= maxlength - 3)
            {
                if (Lines[y][x] == 'X' && Lines[y][x + 1] == 'M' && Lines[y][x + 2] == 'A' && Lines[y][x + 3] == 'S')
                {
                    answer++;
                }
            }

            //SE
            if (y <= maxheight - 3 && x <= maxlength - 3)
                
            {
                if (Lines[y][x] == 'X' && Lines[y + 1][x + 1] == 'M' && Lines[y + 2][x + 2] == 'A' && Lines[y + 3][x + 3] == 'S')
                {
                    answer++;
                }
            }
            //S
            if (y <= maxheight - 3)
            {
                if (Lines[y][x] == 'X' && Lines[y + 1][x] == 'M' && Lines[y + 2][x] == 'A' && Lines[y + 3][x] == 'S')
                {
                    answer++;
                }
            }
            //SW
            if (y <= maxheight - 3 && x >= 3)

            {
                if (Lines[y][x] == 'X' && Lines[y + 1][x - 1] == 'M' && Lines[y + 2][x - 2] == 'A' && Lines[y + 3][x - 3] == 'S')
                {
                    answer++;
                }
            }
            //W
            if (x >= 3)
            {
                if (Lines[y][x] == 'X' && Lines[y][x - 1] == 'M' && Lines[y][x - 2] == 'A' && Lines[y][x - 3] == 'S')
                {
                    answer++;
                }
            }

            //NW
            if (y >= 3 && x >= 3)

            {
                if (Lines[y][x] == 'X' && Lines[y - 1][x - 1] == 'M' && Lines[y - 2][x - 2] == 'A' && Lines[y - 3][x - 3] == 'S')
                {
                    answer++;
                }
            }



        }

        void CheckXMasv2(int x, int y)
        {
            int maxheight = Lines.Count() - 1;
            int maxlength = Lines[0].Length - 1;

            if (y > 0 && y < maxheight && x > 0 && x < maxlength) {
                //M M
                // A
                //S S

                if (Lines[y-1][x-1] == 'M' && Lines[y - 1][x + 1] == 'M' && Lines[y + 1][x - 1] == 'S' && Lines[y + 1][x + 1] == 'S')
                {
                    answer++;
                }

                //S S
                // A
                //M M
                if (Lines[y - 1][x - 1] == 'S' && Lines[y - 1][x + 1] == 'S' && Lines[y + 1][x - 1] == 'M' && Lines[y + 1][x + 1] == 'M')
                {
                    answer++;
                }
                //S M
                // A
                //S M
                if (Lines[y - 1][x - 1] == 'S' && Lines[y - 1][x + 1] == 'M' && Lines[y + 1][x - 1] == 'S' && Lines[y + 1][x + 1] == 'M')
                {
                    answer++;
                }
                //M S
                // A
                //M S
                if (Lines[y - 1][x - 1] == 'M' && Lines[y - 1][x + 1] == 'S' && Lines[y + 1][x - 1] == 'M' && Lines[y + 1][x + 1] == 'S')
                {
                    answer++;
                }
            }

        }
        public int Part1() 
        {
            for (int y = 0; y < Lines.Count(); y++)
            {
                for(int x = 0; x  < Lines[0].Length; x++)
                {
                    if (Lines[y][x] == 'X')
                    {
                        CheckXMas(x, y);
                     
                    }
                }
            }

       


            return answer;
        }

        public int Part2()
        {
            answer = 0;

            for (int y = 0; y < Lines.Count(); y++)
            {
                for (int x = 0; x < Lines[0].Length; x++)
                {
                    if (Lines[y][x] == 'A')
                    {
                        CheckXMasv2(x, y);
                  
                    }
                }
            }


            return answer;
        }

    }
}
