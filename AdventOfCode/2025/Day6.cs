using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class Y2025D6
    {
        public static string[] Lines = File.ReadAllLines(".\\2025\\Input\\inputDay6.txt");
        public static List<int[]> Data = new List<int[]>();
        
        public long Part1() 
        {
            long answer = 0;
            List<int[]> Data = new List<int[]>();
            for (int i = 0; i < Lines.Count() - 1; i++)
            {
                string line = Lines[i];
                Data.Add(line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(u => int.Parse(u.Trim())).ToArray());
                
            }
            var operators = Lines[Lines.Count() - 1].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToArray();

            for (int i = 0; i  < operators.Length; i++)
            {
                int col = i;
                char op = operators[i][0];
                long colSum = 0;
                if(op == '*')
                {
                    colSum = 1;
                }
                foreach (var row in Data)
                {
                    switch (op)
                    {
                        case '+':
                            colSum += row[col];
                            break;
                        case '*':
                            colSum *= row[col];
                            break;
                    }
                }
                answer += colSum;
            }

            return answer;
        }

        public long Part2()
        {
            long answer = 0;
            int rows = Lines.Length;
            if (rows == 0) return 0;
            int operatorRow = rows - 1;
            int maxCols = Lines.Max(l => l.Length);
            var grid = new char[rows][];
            for (int r = 0; r < rows; r++)
            {
                var line = Lines[r] ?? string.Empty;
                if (line.Length < maxCols)
                    line = line.PadRight(maxCols, ' ');
                grid[r] = line.ToCharArray();
            }
         
            var groups = new List<(int start, int end)>();
            bool inGroup = false;
            int groupStart = -1;
            for (int c = 0; c < maxCols; c++)
            {
                bool anyDigitChar = false;
                for (int r = 0; r < operatorRow; r++)
                {
                    if (grid[r][c] != ' ')
                    {
                        anyDigitChar = true;
                        break;
                    }
                }

                if (anyDigitChar)
                {
                    if (!inGroup)
                    {
                        inGroup = true;
                        groupStart = c;
                    }
                }
                else
                {
                    if (inGroup)
                    {
                        inGroup = false;
                        groups.Add((groupStart, c - 1));
                        groupStart = -1;
                    }
                }
            }
            if (inGroup)
            {
                groups.Add((groupStart, maxCols - 1));
            }

            
            for (int g = groups.Count - 1; g >= 0; g--)
            {
                var (start, end) = groups[g];

           
                char op = ' ';
                for (int c = start; c <= end; c++)
                {
                    if (grid[operatorRow][c] != ' ')
                    {
                        op = grid[operatorRow][c];
                        break;
                    }
                }

                var numbers = new List<long>();
                for (int c = end; c >= start; c--)
                {
                    var sb = new StringBuilder();
                    for (int r = 0; r < operatorRow; r++)
                    {
                        sb.Append(grid[r][c]);
                    }
                    var numStr = sb.ToString().Trim();
                    numbers.Add(long.Parse(numStr));
                }

                long result;
                if (op == '+')
                {
                    result = 0;
                    foreach (var n in numbers) result += n;
                }
                else 
                {
                    result = 1;
                    foreach (var n in numbers) result *= n;
                }

                answer += result;
            }

            return answer;
        }

    }
}
