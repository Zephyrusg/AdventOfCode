using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class Y2025D12
    {
        public static string[] Lines = File.ReadAllLines(".\\2025\\Input\\inputDay12.txt");

        private record Orientation((int x, int y)[] Cells, int Width, int Height);
        private record Shape(List<Orientation> Orients, int Area);

        private static List<Shape> Shapes = new();
        private static List<(int W, int H, int[] Counts)> Regions = new();

        private static Shape BuildShape(List<string> rows)
        {
            // build base grid as char[,] then generate orientations
            int h = rows.Count;
            int w = rows[0].Length;
            char[,] grid = new char[h, w];
            for (int y = 0; y < h; y++)
                for (int x = 0; x < w; x++)
                    grid[y, x] = rows[y][x];

            var orients = GenerateOrientations(grid, out int area);
            return new Shape(orients, area);
        }

        private static List<Orientation> GenerateOrientations(char[,] grid, out int area)
        {
            var list = new List<Orientation>();
            var seen = new HashSet<string>();

            // Count '#' once from the original grid
            int h0 = grid.GetLength(0);
            int w0 = grid.GetLength(1);
            int a = 0;
            for (int y = 0; y < h0; y++)
                for (int x = 0; x < w0; x++)
                    if (grid[y, x] == '#') a++;

            area = a;

            void AddFromGrid(char[,] g)
            {
                var trimmed = TrimGrid(g);
                string key = GridToKey(trimmed);
                if (!seen.Add(key)) return;

                var cells = new List<(int x, int y)>();
                int h = trimmed.GetLength(0);
                int w = trimmed.GetLength(1);
                for (int y = 0; y < h; y++)
                    for (int x = 0; x < w; x++)
                        if (trimmed[y, x] == '#')
                            cells.Add((x, y));

                list.Add(new Orientation(cells.ToArray(), w, h));
            }

            // same rotations & flips as before
            char[,] g0 = grid;
            char[,] gFlip = FlipH(g0);

            char[,] g = g0;
            for (int r = 0; r < 4; r++)
            {
                AddFromGrid(g);
                g = Rotate90(g);
            }

            g = gFlip;
            for (int r = 0; r < 4; r++)
            {
                AddFromGrid(g);
                g = Rotate90(g);
            }

            return list;
        }

        private static char[,] Rotate90(char[,] g)
        {
            int h = g.GetLength(0);
            int w = g.GetLength(1);
            char[,] r = new char[w, h];
            for (int y = 0; y < h; y++)
                for (int x = 0; x < w; x++)
                    r[x, h - 1 - y] = g[y, x];
            return r;
        }

        private static char[,] FlipH(char[,] g)
        {
            int h = g.GetLength(0);
            int w = g.GetLength(1);
            char[,] r = new char[h, w];
            for (int y = 0; y < h; y++)
                for (int x = 0; x < w; x++)
                    r[y, w - 1 - x] = g[y, x];
            return r;
        }

        private static char[,] TrimGrid(char[,] g)
        {
            int h = g.GetLength(0);
            int w = g.GetLength(1);

            int minY = h, maxY = -1, minX = w, maxX = -1;
            for (int y = 0; y < h; y++)
                for (int x = 0; x < w; x++)
                    if (g[y, x] == '#')
                    {
                        if (y < minY) minY = y;
                        if (y > maxY) maxY = y;
                        if (x < minX) minX = x;
                        if (x > maxX) maxX = x;
                    }

            int hh = maxY - minY + 1;
            int ww = maxX - minX + 1;
            char[,] r = new char[hh, ww];
            for (int y = 0; y < hh; y++)
                for (int x = 0; x < ww; x++)
                    r[y, x] = g[minY + y, minX + x];

            return r;
        }

        private static string GridToKey(char[,] g)
        {
            int h = g.GetLength(0);
            int w = g.GetLength(1);
            var chars = new char[h * (w + 1)];
            int idx = 0;
            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                    chars[idx++] = g[y, x];
                chars[idx++] = '/';
            }
            return new string(chars);
        }

        private static void ParseInput()
        {
            if (Shapes.Count > 0) return; // already parsed

            int i = 0;
            // 1) parse shapes until first "WxH:" line
            while (i < Lines.Length)
            {
                var line = Lines[i].Trim();
                if (string.IsNullOrEmpty(line)) { i++; continue; }

                if (line.Contains('x') && line.Contains(':'))
                    break; // first region line

                // shape header like "0:"
                if (line.EndsWith(":"))
                {
                    // read rows
                    var rows = new List<string>();
                    i++;
                    while (i < Lines.Length)
                    {
                        var row = Lines[i].TrimEnd();
                        if (string.IsNullOrEmpty(row) || row.EndsWith(":"))
                            break;
                        rows.Add(row);
                        i++;
                    }

                    Shapes.Add(BuildShape(rows));
                    // don't increment i here if we stopped on header; loop will handle it
                    continue;
                }

                i++;
            }

            // 2) parse regions from i onward
            for (; i < Lines.Length; i++)
            {
                var line = Lines[i].Trim();
                if (string.IsNullOrEmpty(line)) continue;

                var parts = line.Split(':', 2);
                var wh = parts[0].Split('x');
                int w = int.Parse(wh[0]);
                int h = int.Parse(wh[1]);

                var countParts = parts[1]
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries);

                int[] counts = Array.ConvertAll(countParts, int.Parse);
                Regions.Add((w, h, counts));
            }
        }

        public int Part1()
        {
            ParseInput();

            int fitCount = 0;

            foreach (var region in Regions)
            {
                if (CanFitRegion(region.W, region.H, region.Counts))
                    fitCount++;
            }

            return fitCount;
        }
        private static bool CanFitRegion(int width, int height, int[] counts)
        {
            // quick area check
            int neededArea = 0;
            for (int i = 0; i < counts.Length; i++)
                neededArea += counts[i] * Shapes[i].Area;
            if (neededArea > width * height)
                return false;

            // build list of pieces (shape indices), largest area first
            var pieces = new List<int>();
            for (int i = 0; i < counts.Length; i++)
            {
                for (int k = 0; k < counts[i]; k++)
                    pieces.Add(i);
            }

            pieces.Sort((a, b) => Shapes[b].Area.CompareTo(Shapes[a].Area));

            var occ = new bool[height, width];

            bool Search(int idxPiece)
            {
                if (idxPiece == pieces.Count)
                    return true;

                int si = pieces[idxPiece];
                var shape = Shapes[si];

                foreach (var ori in shape.Orients)
                {
                    for (int y = 0; y <= height - ori.Height; y++)
                    {
                        for (int x = 0; x <= width - ori.Width; x++)
                        {
                            if (!CanPlace(ori, x, y, occ)) continue;
                            Place(ori, x, y, occ);
                            if (Search(idxPiece + 1))
                                return true;
                            Unplace(ori, x, y, occ);
                        }
                    }
                }

                return false;
            }

            return Search(0);
        }

        private static bool CanPlace(Orientation ori, int ox, int oy, bool[,] occ)
        {
            foreach (var (dx, dy) in ori.Cells)
            {
                int x = ox + dx;
                int y = oy + dy;
                if (occ[y, x]) return false;
            }
            return true;
        }

        private static void Place(Orientation ori, int ox, int oy, bool[,] occ)
        {
            foreach (var (dx, dy) in ori.Cells)
                occ[oy + dy, ox + dx] = true;
        }

        private static void Unplace(Orientation ori, int ox, int oy, bool[,] occ)
        {
            foreach (var (dx, dy) in ori.Cells)
                occ[oy + dy, ox + dx] = false;
        }

        public int Part2()
        {
            int answer = 0;




            return answer;
        }

    }
}
