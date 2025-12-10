using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace AdventOfCode
{
    internal class Y2025D10
    {
        public static string[] Lines = File.ReadAllLines(".\\2025\\Input\\inputDay10.txt");

      
        public record struct Machine(
            int NumLights,
            int TargetState,
            int[] ButtonMasks,   
            int NumCounters,
            int[] JoltageTargets,
            int[][] ButtonCounters 
        );

        public static List<Machine> Machines = new();

        public readonly struct Fraction : IEquatable<Fraction>
        {
            public long Num { get; }
            public long Den { get; } // always > 0

            public static Fraction Zero => new Fraction(0, 1);
            public static Fraction One => new Fraction(1, 1);

            public Fraction(long num, long den)
            {
                if (den == 0) throw new DivideByZeroException();
                if (den < 0)
                {
                    num = -num;
                    den = -den;
                }
                if (num == 0)
                {
                    Num = 0;
                    Den = 1;
                }
                else
                {
                    long g = Gcd(Math.Abs(num), den);
                    Num = num / g;
                    Den = den / g;
                }
            }

            public static Fraction FromInt(int v) => new Fraction(v, 1);

            private static long Gcd(long a, long b)
            {
                while (b != 0)
                {
                    long t = a % b;
                    a = b;
                    b = t;
                }
                return a < 0 ? -a : a;
            }

            public bool IsZero => Num == 0;
            public bool IsInteger => Den == 1;

            public int ToInt32()
            {
                if (!IsInteger) throw new InvalidOperationException("Fraction is not an integer.");
                checked
                {
                    return (int)Num;
                }
            }

            // Operators
            public static Fraction operator +(Fraction a, Fraction b) =>
                new Fraction(a.Num * b.Den + b.Num * a.Den, a.Den * b.Den);

            public static Fraction operator -(Fraction a, Fraction b) =>
                new Fraction(a.Num * b.Den - b.Num * a.Den, a.Den * b.Den);

            public static Fraction operator *(Fraction a, Fraction b) =>
                new Fraction(a.Num * b.Num, a.Den * b.Den);

            public static Fraction operator /(Fraction a, Fraction b)
            {
                if (b.Num == 0) throw new DivideByZeroException();
                return new Fraction(a.Num * b.Den, a.Den * b.Num);
            }

            public bool Equals(Fraction other) => Num == other.Num && Den == other.Den;
            public override bool Equals(object? obj) => obj is Fraction f && Equals(f);
            public override int GetHashCode() => HashCode.Combine(Num, Den);

            public static bool operator ==(Fraction a, Fraction b) => a.Equals(b);
            public static bool operator !=(Fraction a, Fraction b) => !a.Equals(b);

            public override string ToString() => Den == 1 ? Num.ToString() : $"{Num}/{Den}";
        }

        private static bool BuildParametricSolution(
    int[,] AInt,
    int[] t,
    out Fraction[] consts,
    out Fraction[,] coeffs,
    out int[] freeCols)
        {
            int rows = AInt.GetLength(0);
            int cols = AInt.GetLength(1);

            // Augmented matrix [A | t] with Fractions
            Fraction[,] mat = new Fraction[rows, cols + 1];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                    mat[i, j] = Fraction.FromInt(AInt[i, j]);
                mat[i, cols] = Fraction.FromInt(t[i]);
            }

            int[] pivotColForRow = new int[rows];
            for (int i = 0; i < rows; i++) pivotColForRow[i] = -1;

            int r = 0;
            for (int c = 0; c < cols && r < rows; c++)
            {
                // Find pivot row
                int pivot = -1;
                for (int i = r; i < rows; i++)
                {
                    if (!mat[i, c].IsZero)
                    {
                        pivot = i;
                        break;
                    }
                }

                if (pivot == -1)
                    continue; // no pivot in this column (free column)

                // Swap pivot row into position r
                if (pivot != r)
                {
                    for (int j = c; j <= cols; j++)
                    {
                        var tmp = mat[r, j];
                        mat[r, j] = mat[pivot, j];
                        mat[pivot, j] = tmp;
                    }
                }

                // Normalize pivot row so that pivot element becomes 1
                Fraction pivotVal = mat[r, c];
                for (int j = c; j <= cols; j++)
                    mat[r, j] /= pivotVal;

                // Eliminate this column in all other rows
                for (int i = 0; i < rows; i++)
                {
                    if (i == r) continue;
                    Fraction factor = mat[i, c];
                    if (factor.IsZero) continue;
                    for (int j = c; j <= cols; j++)
                        mat[i, j] -= factor * mat[r, j];
                }

                pivotColForRow[r] = c;
                r++;
            }

            // Check for inconsistency: [0 0 ... 0 | b] with b != 0
            for (int i = 0; i < rows; i++)
            {
                bool allZero = true;
                for (int j = 0; j < cols; j++)
                {
                    if (!mat[i, j].IsZero)
                    {
                        allZero = false;
                        break;
                    }
                }
                if (allZero && !mat[i, cols].IsZero)
                {
                    consts = null!;
                    coeffs = null!;
                    freeCols = null!;
                    return false; // no solution
                }
            }

            // Determine pivot columns and free columns
            bool[] isPivot = new bool[cols];
            for (int i = 0; i < rows; i++)
            {
                int pc = pivotColForRow[i];
                if (pc >= 0) isPivot[pc] = true;
            }

            var freeList = new List<int>();
            for (int j = 0; j < cols; j++)
            {
                if (!isPivot[j]) freeList.Add(j);
            }

            freeCols = freeList.ToArray();
            int d = freeCols.Length;   // # free parameters
            int B = cols;              // # variables

            consts = new Fraction[B];
            coeffs = new Fraction[B, d]; // coeffs[varIndex, paramIndex]

            // Initialize free variables: x_free = alpha_p (1 for its own parameter, 0 otherwise)
            var paramIndexForCol = new int[cols];
            for (int j = 0; j < cols; j++) paramIndexForCol[j] = -1;
            for (int p = 0; p < d; p++)
            {
                int col = freeCols[p];
                paramIndexForCol[col] = p;
                consts[col] = Fraction.Zero;
                coeffs[col, p] = Fraction.One;
            }

            // Pivot variables: each pivot row expresses x_pivot in terms of free vars
            for (int i = 0; i < rows; i++)
            {
                int pc = pivotColForRow[i];
                if (pc < 0) continue; // no pivot in this row (pure-zero row)

                // Equation: x_pc + sum_{f in free} mat[i,f]*x_f = mat[i,cols]
                // So: x_pc = mat[i,cols] - sum_{f} mat[i,f] * x_f
                Fraction constant = mat[i, cols];
                var paramCoefs = new Fraction[d];
                for (int p = 0; p < d; p++)
                    paramCoefs[p] = Fraction.Zero;

                // x_f already = alpha_p for its param p
                for (int p = 0; p < d; p++)
                {
                    int fcol = freeCols[p];
                    Fraction a = mat[i, fcol];
                    if (!a.IsZero)
                    {
                        paramCoefs[p] -= a; // because we move it to RHS with minus sign
                    }
                }

                consts[pc] = constant;
                for (int p = 0; p < d; p++)
                    coeffs[pc, p] = paramCoefs[p];
            }

            return true;
        }

        private static int SolveJoltageMachine(int[] target, int[][] buttons)
        {
            int C = target.Length;       // counters
            int B = buttons.Length;      // buttons

            if (C == 0) return 0;
            if (B == 0) return -1;

            // Build A as int[C,B]: A[i,j] = 1 if button j affects counter i
            int[,] A = new int[C, B];
            for (int j = 0; j < B; j++)
            {
                foreach (int c in buttons[j])
                {
                    if (c < 0 || c >= C)
                        throw new InvalidOperationException("Button index outside counter range.");
                    A[c, j] = 1;
                }
            }

            // Build parametric solution x = consts + sum_p coeffs[*,p] * alpha_p
            if (!BuildParametricSolution(A, target, out Fraction[] consts, out Fraction[,] coeffs, out int[] freeCols))
                return -1; // no solution at all

            int d = freeCols.Length;       // number of free parameters
            int[] upper = new int[B];      // per-button maximum presses
            int totalTarget = 0;

            for (int i = 0; i < C; i++)
                totalTarget += target[i];

            // Upper bound per button: can't press it more than the smallest target among counters it touches
            for (int j = 0; j < B; j++)
            {
                int minT = int.MaxValue;
                foreach (int c in buttons[j])
                    minT = Math.Min(minT, target[c]);

                upper[j] = (minT == int.MaxValue ? 0 : minT);
            }

            // If there are no free variables: unique solution.
            if (d == 0)
            {
                int sum = 0;
                for (int j = 0; j < B; j++)
                {
                    var v = consts[j];
                    if (!v.IsInteger) return -1;
                    int xj = v.ToInt32();
                    if (xj < 0 || xj > upper[j]) return -1;
                    sum += xj;
                }
                if (sum > totalTarget) return -1; // sanity
                return sum;
            }

            // Free variables: alpha_p = x[freeCols[p]] and must satisfy 0 <= alpha_p <= upper[freeCols[p]]
            int[] maxAlpha = new int[d];
            for (int p = 0; p < d; p++)
            {
                int j = freeCols[p];
                maxAlpha[p] = upper[j];
            }

            int[] alpha = new int[d];
            int best = int.MaxValue;

            void SearchAlpha(int p)
            {
                if (p == d)
                {
                    // Evaluate x for this alpha vector
                    int sum = 0;
                    for (int j = 0; j < B; j++)
                    {
                        Fraction v = consts[j];
                        for (int q = 0; q < d; q++)
                        {
                            if (!coeffs[j, q].IsZero && alpha[q] != 0)
                            {
                                v += coeffs[j, q] * Fraction.FromInt(alpha[q]);
                            }
                        }

                        if (!v.IsInteger) return;

                        int xj = v.ToInt32();
                        if (xj < 0 || xj > upper[j]) return;

                        sum += xj;
                        if (sum >= best) return;          // pruning
                        if (sum > totalTarget) return;    // can't exceed total target sum
                    }

                    best = Math.Min(best, sum);
                    return;
                }

                int limit = maxAlpha[p];
                for (int v = 0; v <= limit; v++)
                {
                    alpha[p] = v;
                    SearchAlpha(p + 1);
                }
            }

            SearchAlpha(0);

            return best == int.MaxValue ? -1 : best;
        }

        public static int BfsMinPresses(int numLights, int targetState, int[] buttonMasks)
        {
            int nStates = 1 << numLights;
            var dist = new int[nStates];
            for (int i = 0; i < nStates; i++)
            {
                dist[i] = -1;
            }

            var q = new Queue<int>();
            int start = 0;
            dist[start] = 0;
            q.Enqueue(start);

            while (q.Count > 0)
            {
                int state = q.Dequeue();
                int d = dist[state];

                if (state == targetState)
                {
                    return d; 
                }

                foreach (int mask in buttonMasks)
                {
                    int next = state ^ mask; // toggle lights
                    if (dist[next] != -1)
                        continue; // already visited

                    dist[next] = d + 1;
                    q.Enqueue(next);
                }
            }

            
            return -1;
        }
        public int Part1()
        {


            int answer = 0;

            foreach (var line in Lines)
            {
                // ----- 1. Pattern in [ ... ] -----
                int lbr = line.IndexOf('[');
                int rbr = line.IndexOf(']');
                if (lbr < 0 || rbr < 0 || rbr <= lbr)
                    throw new InvalidOperationException("Invalid line, missing []: " + line);

                string pattern = line.Substring(lbr + 1, rbr - lbr - 1);
                int numLights = pattern.Length;

                int targetState = 0;
                for (int i = 0; i < numLights; i++)
                {
                    if (pattern[i] == '#')
                    {
                        targetState |= (1 << i);
                    }
                }

                
                int braceIndex = line.IndexOf('{');
                if (braceIndex < 0)
                    throw new InvalidOperationException("Invalid line, missing {}: " + line);

                string mainPart = line.Substring(0, braceIndex);
                string bracePart = line.Substring(braceIndex);

                
                var buttonMasks = new List<int>();
                var buttonCounters = new List<int[]>();

                int pos = 0;
                while (true)
                {
                    int open = mainPart.IndexOf('(', pos);
                    if (open < 0) break;

                    int close = mainPart.IndexOf(')', open + 1);
                    if (close < 0) break;

                    string inner = mainPart.Substring(open + 1, close - open - 1).Trim();
                    if (!string.IsNullOrEmpty(inner))
                    {
                        int mask = 0;
                        var indices = new List<int>();

                        string[] parts = inner.Split(',', StringSplitOptions.RemoveEmptyEntries);
                        foreach (string part in parts)
                        {
                            int idx = int.Parse(part.Trim());
                            mask |= (1 << idx);
                            indices.Add(idx);
                        }

                        buttonMasks.Add(mask);
                        buttonCounters.Add(indices.ToArray());
                    }

                    pos = close + 1;
                }

              
                int lcur = bracePart.IndexOf('{');
                int rcur = bracePart.IndexOf('}');
                if (lcur < 0 || rcur < 0 || rcur <= lcur)
                    throw new InvalidOperationException("Invalid line, malformed {}: " + line);

                string insideCurly = bracePart.Substring(lcur + 1, rcur - lcur - 1);
                string[] joltageParts = insideCurly
                    .Split(',', StringSplitOptions.RemoveEmptyEntries);

                int[] joltageTargets = joltageParts
                    .Select(p => int.Parse(p.Trim()))
                    .ToArray();

                int numCounters = joltageTargets.Length;

                Machines.Add(new Machine(
                    NumLights: numLights,
                    TargetState: targetState,
                    ButtonMasks: buttonMasks.ToArray(),
                    NumCounters: numCounters,
                    JoltageTargets: joltageTargets,
                    ButtonCounters: buttonCounters.ToArray()
                ));

            }

            foreach (var machine in Machines)
            {
                int presses = BfsMinPresses(machine.NumLights, machine.TargetState, machine.ButtonMasks);

                answer += presses;
            }



            return answer;
        }


        public int Part2()
        {
            int answer = 0;
            int i = 0;
            foreach (var m in Machines)
            {
                string line = Lines[i];
                Console.WriteLine("Starting with machine: " + line);
                int presses = SolveJoltageMachine(m.JoltageTargets, m.ButtonCounters);
                if (presses < 0)
                    throw new InvalidOperationException("Machine has no solution (part 2).");
                i++;
                answer += presses;
            }


            return answer;
        }

    }
}
