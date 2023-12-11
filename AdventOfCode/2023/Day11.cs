using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class Y2023D11
    {
        public static string[] lines = File.ReadAllLines(".\\2023\\Input\\inputDay11.txt");
        public int Part1()
        {
            int answer = 0;
            List<int> EmptyRow = new List<int>();
            List<int> EmptyColumn = new List<int>();
            List<(int, int)> Galaxyies = new List<(int, int)>();
            for (int y = 0; y < lines.Length; y++) {
                if (!lines[y].Contains("#")) {
                    EmptyRow.Add(y);
                }
            }
            for (int x = 0; x < lines[0].Length; x++)
            {
                bool GalaxyFound = false;
                for (int y = 0; y < lines.Length; y++) {

                    if (lines[y][x] == '#') {
                        GalaxyFound = true;
                        Galaxyies.Add((x, y));
                    }

                }
                if (!GalaxyFound) {
                    EmptyColumn.Add(x);
                }
            }


            for (int i = 0; i < Galaxyies.Count; i++) {

                int Rows = EmptyRow.FindAll(x => x < Galaxyies[i].Item2).Count();
                int Columns = EmptyColumn.FindAll(x => x < Galaxyies[i].Item1).Count();

                Galaxyies[i] = (Galaxyies[i].Item1 + Columns, Galaxyies[i].Item2 + Rows);


            }


            HashSet<(int, int)> Pairs = new HashSet<(int, int)>();
            for (int x = 0; x < Galaxyies.Count; x++)
            {
                for (int y = 0; y < Galaxyies.Count; y++) {


                    if (x != y)
                    {
                        (int, int) Pair = (x, y);
                        (int, int) PairReversed = (y, x);
                        var test = Pairs.TryGetValue(Pair, out _);
                        var test2 = Pairs.TryGetValue(PairReversed, out _);
                        if (!test && !test2) {
                            Pairs.Add(Pair);
                        }
                    }
                }
            }

            foreach ((int, int) Pair in Pairs) { 
                (int,int) Galaxy1 = Galaxyies[Pair.Item1];
                (int, int) Galaxy2 = Galaxyies[Pair.Item2];

                int Distance = Math.Abs(Galaxy1.Item1 - Galaxy2.Item1) + Math.Abs(Galaxy1.Item2 - Galaxy2.Item2);
                answer += Distance;
            }

            return answer;
        }

        public long Part2()
        {
            long answer = 0;

            List<int> EmptyRow = new List<int>();
            List<int> EmptyColumn = new List<int>();
            List<(long, long)> Galaxyies = new List<(long, long)>();
            for (int y = 0; y < lines.Length; y++)
            {
                if (!lines[y].Contains("#"))
                {
                    EmptyRow.Add(y);
                }
            }
            for (int x = 0; x < lines[0].Length; x++)
            {
                bool GalaxyFound = false;
                for (int y = 0; y < lines.Length; y++)
                {

                    if (lines[y][x] == '#')
                    {
                        GalaxyFound = true;
                        Galaxyies.Add((x, y));
                    }

                }
                if (!GalaxyFound)
                {
                    EmptyColumn.Add(x);
                }
            }

            long Multiplier = 10;

            for (int i = 0; i < Galaxyies.Count; i++)
            {

                long Rows = (EmptyRow.FindAll(x => x < Galaxyies[i].Item2).Count()) * Multiplier;
                long Columns = (EmptyColumn.FindAll(x => x < Galaxyies[i].Item1).Count()) * Multiplier;

                Galaxyies[i] = (Galaxyies[i].Item1 + Columns, Galaxyies[i].Item2 + Rows);


            }


            HashSet<(int, int)> Pairs = new HashSet<(int, int)>();
            for (int x = 0; x < Galaxyies.Count; x++)
            {
                for (int y = 0; y < Galaxyies.Count; y++)
                {


                    if (x != y)
                    {
                        (int, int) Pair = (x, y);
                        (int, int) PairReversed = (y, x);
                        var test = Pairs.TryGetValue(Pair, out _);
                        var test2 = Pairs.TryGetValue(PairReversed, out _);
                        if (!test && !test2)
                        {
                            Pairs.Add(Pair);
                        }
                    }
                }
            }

            foreach ((int, int) Pair in Pairs)
            {
                (long, long) Galaxy1 = Galaxyies[Pair.Item1];
                (long, long) Galaxy2 = Galaxyies[Pair.Item2];

                long Distance = Math.Abs(Galaxy1.Item1 - Galaxy2.Item1) + Math.Abs(Galaxy1.Item2 - Galaxy2.Item2);
                answer += Distance;
            }


            return answer;
        }

    }
}
