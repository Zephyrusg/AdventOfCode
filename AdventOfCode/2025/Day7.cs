using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class Y2025D7
    {
   

        public static string[] Lines = File.ReadAllLines(".\\2025\\Input\\inputDay7.txt");

        public static char[][] grid = Lines.Select(line => line.ToCharArray()).ToArray();
        public static int rows = grid.Length;
        public static int cols = grid[0].Length;
        public int Part1() 
        {
            int answer = 0;

            
            List<(int r , int c) > Beams = new();

            for (int r = 0; r < rows -1; r++)
            {
                if (Beams.Count == 0 && r == 0)
                {

                    for (int c = 0; c < cols; c++)
                    {
                        if (grid[r][c] == 'S')
                        {
                            Beams.Add((r, c));
                            break;
                        }

                    }
                }

                foreach (var beam in Beams.Where(x=>x.r== r).ToList())
                {
                    char onedown = grid[beam.r + 1][beam.c];
                    if (onedown == '^')
                    {
                        answer++;
                        if (!Beams.Contains((beam.r + 1, beam.c + 1)))
                            Beams.Add((beam.r + 1, beam.c + 1));
                        if (!Beams.Contains((beam.r + 1, beam.c - 1)))
                            Beams.Add((beam.r + 1, beam.c - 1));
                    }
                    else if (onedown == '.')
                    {
                        if (!Beams.Contains((beam.r + 1, beam.c)))
                            Beams.Add((beam.r + 1, beam.c));
                    }
                }
            }
            
            return answer;
        }

        public long Part2()
        {
         
            int startCol = -1;
            for (int c = 0; c < cols; c++) if (grid[0][c] == 'S') { startCol = c; break; }

            var TimelinesperCell = new long[rows, cols];
            TimelinesperCell[0, startCol] = 1;

            for (int r = 0; r < rows - 1; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    long PossibleTimelines = TimelinesperCell[r, c];
                    if (PossibleTimelines == 0) continue;
                    char onedown = grid[r + 1][c];
                    if (onedown == '^')
                    {
                        int rc = c + 1;
                        if (rc < cols) TimelinesperCell[r + 1, rc] += PossibleTimelines;
                        rc = c - 1;
                        if (rc >= 0) TimelinesperCell[r + 1, rc] += PossibleTimelines;
                    }
                    else if (onedown == '.')
                    {
                        TimelinesperCell[r + 1, c] += PossibleTimelines;
                    }
                }

            }

            long answer = 0;
            for (int c = 0; c < cols; c++) answer += TimelinesperCell[rows - 1, c];

            return answer;
        }

      

    }
}
