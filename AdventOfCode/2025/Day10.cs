using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private static int GreedyUpperBound(int[] target, int[][] buttons)
        {
            int numCounters = target.Length;
            var residual = (int[])target.Clone();
            int residualSum = 0;
            for (int i = 0; i < numCounters; i++)
                residualSum += residual[i];

            int presses = 0;

            while (true)
            {
                if (residualSum == 0)
                    return presses; 
                int bestButton = -1;
                int bestGain = -1;

              
                for (int b = 0; b < buttons.Length; b++)
                {
                    var affected = buttons[b];
                    int gain = 0;
                    bool ok = true;

                    foreach (int c in affected)
                    {
                        if (residual[c] <= 0)
                        {
                            ok = false; 
                            break;
                        }
                        gain++;
                    }

                    if (!ok || gain == 0)
                        continue;

                    if (gain > bestGain)
                    {
                        bestGain = gain;
                        bestButton = b;
                    }
                }

              
                if (bestButton == -1)
                    return int.MaxValue / 4; 

               
                foreach (int c in buttons[bestButton])
                {
                    residual[c]--;
                    residualSum--;
                }

                presses++;
            }
        }

        private static int SolveJoltageMachine(int[] target, int[][] buttons)
        {
            int numCounters = target.Length;
            int numButtons = buttons.Length;

            
            int greedy = GreedyUpperBound(target, buttons);
            int best = (greedy >= int.MaxValue / 4) ? int.MaxValue : greedy;

            var residual = (int[])target.Clone();
            int residualSum = 0;
            for (int i = 0; i < numCounters; i++)
                residualSum += residual[i];

            if (residualSum == 0)
                return 0;

            int maxEffect = 0;
            foreach (var b in buttons)
                if (b.Length > maxEffect)
                    maxEffect = b.Length;

            
            var order = new int[numButtons];
            for (int i = 0; i < numButtons; i++) order[i] = i;

            Array.Sort(order, (i, j) => buttons[j].Length.CompareTo(buttons[i].Length));
            var sortedButtons = new int[numButtons][];
            for (int i = 0; i < numButtons; i++)
                sortedButtons[i] = buttons[order[i]];

            
            void Search(int idx, int pressesSoFar, int currentResidualSum)
            {
                if (pressesSoFar >= best)
                    return;

                if (currentResidualSum == 0)
                {
                    
                    if (pressesSoFar < best)
                        best = pressesSoFar;
                    return;
                }

                if (idx == sortedButtons.Length)
                    return; 

                int minExtra = (currentResidualSum + maxEffect - 1) / maxEffect;
                if (pressesSoFar + minExtra >= best)
                    return;

                var affected = sortedButtons[idx];
                int perPressEffect = affected.Length;

                
                int maxTimes = int.MaxValue;
                foreach (int c in affected)
                {
                    if (residual[c] < maxTimes)
                        maxTimes = residual[c];
                }

                if (maxTimes <= 0)
                {
                    
                    Search(idx + 1, pressesSoFar, currentResidualSum);
                    return;
                }

               
                for (int x = 0; x <= maxTimes; x++)
                {
                    if (x > 0)
                    {
                        foreach (int c in affected)
                            residual[c]--;

                        currentResidualSum -= perPressEffect;
                    }

                    
                    int localMinExtra = (currentResidualSum + maxEffect - 1) / maxEffect;
                    if (pressesSoFar + x + localMinExtra < best)
                    {
                        Search(idx + 1, pressesSoFar + x, currentResidualSum);
                    }

                    
                    if (pressesSoFar + x >= best)
                        break;
                }

                foreach (int c in affected)
                    residual[c] += maxTimes;
               
            }

            Search(0, 0, residualSum);

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

            foreach (var m in Machines)
            {
                int presses = SolveJoltageMachine(m.JoltageTargets, m.ButtonCounters);
                if (presses < 0)
                    throw new InvalidOperationException("Machine has no solution (part 2).");

                answer += presses;
            }


            return answer;
        }

    }
}
