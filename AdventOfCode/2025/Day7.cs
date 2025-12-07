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
            List<(int r, int c)> Beams = new();
            
            int startCol = -1;
            for (int c = 0; c < cols; c++) if (grid[0][c] == 'S') { startCol = c; break; }
            Beams.Add((0, startCol));

            for (int r = 0; r < rows - 1; r++)
            {
                var NextBeams = new List<(int r, int c)>();
                var added = new bool[cols];

                foreach (var beam in Beams)
                {
                    char onedown = grid[beam.r + 1][beam.c];
                    if (onedown == '^')
                    {
                        answer++;
                        int rc = beam.c + 1;
                        if (rc < cols && !added[rc]) { added[rc] = true; NextBeams.Add((beam.r + 1, rc)); }
                        rc = beam.c - 1;
                        if (rc >= 0 && !added[rc]) { added[rc] = true; NextBeams.Add((beam.r + 1, rc)); }
                    }
                    else if (onedown == '.')
                    {
                        int rc = beam.c;
                        if (!added[rc]) { added[rc] = true; NextBeams.Add((beam.r + 1, rc)); }
                    }
                }

                Beams = NextBeams;
            }

            return answer;
        }

        public long Part2()
        {
            long answer = 0;
            int startCol = -1;
            for (int c = 0; c < cols; c++) if (grid[0][c] == 'S') { startCol = c; break; }
            if (startCol == -1) return 0;

            var Gridrow = new long[cols];
            Gridrow[startCol] = 1;
            var Beams = new List<int> { startCol };

            for (int r = 0; r < rows - 1; r++)
            {
                var NextGridRow = new long[cols];
                var NextRowBeams = new List<int>();
                var added = new bool[cols];

                foreach (var c in Beams)
                {
                    long Timelines = Gridrow[c];
                    char onedown = grid[r + 1][c];
                    if (onedown == '^')
                    {
                        int rc = c + 1;
                        if (rc < cols)
                        {
                            NextGridRow[rc] += Timelines;
                            if (!added[rc]) { added[rc] = true; NextRowBeams.Add(rc); }
                        }
                        rc = c - 1;
                        if (rc >= 0)
                        {
                            NextGridRow[rc] += Timelines;
                            if (!added[rc]) { added[rc] = true; NextRowBeams.Add(rc); }
                        }
                    }
                    else if (onedown == '.')
                    {
                        NextGridRow[c] += Timelines;
                        if (!added[c]) { added[c] = true; NextRowBeams.Add(c); }
                    }
                }

                Gridrow = NextGridRow;
                Beams = NextRowBeams;
                if (Beams.Count == 0) break;
            }
  
            foreach (var Beam in Beams) answer += Gridrow[Beam];
            return answer;
        }

      

    }
}
