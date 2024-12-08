namespace AdventOfCode
{
    internal class Y2024D8
    {
        public static string[] Lines = File.ReadAllLines(".\\2024\\Input\\inputDay8.txt");
        static int Height = Lines.Count();
        static int Width = Lines[0].Length;
        static Dictionary<char, List<(int, int)>> antennas = new Dictionary<char, List<(int, int)>>();

        static bool IsNotOutofBound(int y, int x)
        {
            return y >= 0 && y < Height && x >= 0 && x < Width;
        }

        static int GCD(int a, int b)
        {
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }
        public int Part1() 
        {
            HashSet<(int, int)> antinodes = new HashSet<(int, int)>();
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    char cell = Lines[y][x];
                    if (char.IsLetterOrDigit(cell))
                    {
                        if (!antennas.ContainsKey(cell))
                            antennas[cell] = new List<(int, int)>();
                        antennas[cell].Add((y, x));
                    }
                }
            }

            foreach (var frequency in antennas)
            {
                List<(int, int)> positions = frequency.Value;

                for (int i = 0; i < positions.Count; i++)
                {
                    for (int j = i + 1; j < positions.Count; j++)
                    {
                        var (y1, x1) = positions[i];
                        var (y2, x2) = positions[j];

                        int dy = y2 - y1;
                        int dx = x2 - x1;


                        int y3 = y1 - dy;
                        int x3 = x1 - dx;
                        if (IsNotOutofBound(y3, x3))
                            antinodes.Add((y3, x3));

                      
                        int y4 = y2 + dy;
                        int x4 = x2 + dx;
                        if (IsNotOutofBound(y4, x4))
                            antinodes.Add((y4, x4));
                    }
                }
            }


            return antinodes.Count;
        }

        public int Part2()
        {
    
            HashSet<(int, int)> antinodes = new HashSet<(int, int)>();

            foreach (var frequency in antennas)
            {
                List<(int, int)> positions = frequency.Value;

                if (positions.Count > 1)
                {
                    foreach (var pos in positions)
                    {
                        antinodes.Add(pos);
                    }
                }

                for (int i = 0; i < positions.Count; i++)
                {
                    for (int j = i + 1; j < positions.Count; j++)
                    {
                        var (y1, x1) = positions[i];
                        var (y2, x2) = positions[j];

                      
                        int dy = y2 - y1;
                        int dx = x2 - x1;

                   
                        int gcd = GCD(Math.Abs(dy), Math.Abs(dx));
                        if (gcd == 0) continue;
                        dy /= gcd;
                        dx /= gcd;


                        // Walk backwards
                        int y = y1 - dy;
                        int x = x1 - dx;
                        while (IsNotOutofBound(y, x))
                        {
                            antinodes.Add((y, x));
                            y -= dy;
                            x -= dx;
                        }

                        // Walk forward
                        y = y2 + dy;
                        x = x2 + dx;
                        while (IsNotOutofBound(y, x))
                        {
                            antinodes.Add((y, x));
                            y += dy;
                            x += dx;
                        }

                        // Between
                        y = y1;
                        x = x1;
                        while (true)
                        {
                            antinodes.Add((y, x));
                            if ((y, x) == (y2, x2)) break; 
                            y += dy;
                            x += dx;
                        }
                    }
                }
            }



            return antinodes.Count;
        }

    }
}
