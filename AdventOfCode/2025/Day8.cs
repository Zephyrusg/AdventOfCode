using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class Y2025D8
    {
        public static string[] Lines = File.ReadAllLines(".\\2025\\Input\\inputDay8.txt");
        public static List<Connection> pairs = new();
        public static List<(int x, int y, int z)> JunctionBoxes = new();
        public record Connection(
            (int x, int y, int z) A,
            (int x, int y, int z) B,
            long DistanceSquared
        );

        
        public static void ProcessPair(
            (int x, int y, int z) a,
            (int x, int y, int z) b,
            List<HashSet<(int, int, int)>> allCircuits,
            Dictionary<(int, int, int), HashSet<(int, int, int)>> nodeToCircuit)
                {
            nodeToCircuit.TryGetValue(a, out var circuitA);
            nodeToCircuit.TryGetValue(b, out var circuitB);

            //Same
            if (circuitA != null && ReferenceEquals(circuitA, circuitB))
            {
                return;
            }

            // Differnt -> Merge
            if (circuitA != null && circuitB != null)
            {
                
                var target = circuitA;
                var source = circuitB;

                if (source.Count > target.Count)
                {
                    var tmp = target;
                    target = source;
                    source = tmp;
                }

                foreach (var node in source)
                {
                    target.Add(node);
                    nodeToCircuit[node] = target;
                }

                allCircuits.Remove(source);
                return;
            }

            //  Only A
            if (circuitA != null && circuitB == null)
            {
                circuitA.Add(b);
                nodeToCircuit[b] = circuitA;
                return;
            }

            // Only B
            if (circuitA == null && circuitB != null)
            {
                circuitB.Add(a);
                nodeToCircuit[a] = circuitB;
                return;
            }

            // Both new
            if (circuitA == null && circuitB == null)
            {
                var newCircuit = new HashSet<(int, int, int)> { a, b };

                allCircuits.Add(newCircuit);
                nodeToCircuit[a] = newCircuit;
                nodeToCircuit[b] = newCircuit;
            }
        }

        public int Part1() 
        {
            int answer = 1;

            var allCircuits = new List<HashSet<(int, int, int)>>();
            var nodeToCircuit = new Dictionary<(int, int, int), HashSet<(int, int, int)>>();
            foreach (var line in Lines)
            {
                var parts = line.Split(',');


                int x = int.Parse(parts[0]);
                int y = int.Parse(parts[1]);
                int z = int.Parse(parts[2]);
                
                var node = (x, y, z);
                JunctionBoxes.Add(node);
            }

            

            for (int i = 0; i < JunctionBoxes.Count; i++)
            {
                for (int j = i + 1; j < JunctionBoxes.Count; j++)
                {
                    var a = JunctionBoxes[i];
                    var b = JunctionBoxes[j];

                    long dx = (long)a.x - b.x;
                    long dy = (long)a.y - b.y;
                    long dz = (long)a.z - b.z;

                    long d2 = dx * dx + dy * dy + dz * dz; 

                    pairs.Add(new Connection(a, b, d2));
                }
            }

            pairs.Sort((p1, p2) => p1.DistanceSquared.CompareTo(p2.DistanceSquared));
            
            int toProcess = 0;
            if (JunctionBoxes.Count == 20) {
                toProcess = 10;
            }else {
                toProcess = 1000;
            }

           

            for (int k = 0; k < toProcess; k++)
            {
                var (a, b, _) = pairs[k];
                ProcessPair(a, b, allCircuits, nodeToCircuit);
            }

            var Top3Sizes = allCircuits.OrderByDescending(x => x.Count).Take(3).Select(x => x.Count).ToList();
            foreach (var size in Top3Sizes)
            {
                answer *= size;
            }
            return answer;
        }

        public long Part2()
        {
            long answer = 0;
            var allCircuits = new List<HashSet<(int x, int y, int z)>>();
            var nodeToCircuit = new Dictionary<(int x, int y, int z), HashSet<(int x, int y, int z)>>();

            foreach (var box in JunctionBoxes)
            {
                var circuit = new HashSet<(int, int, int)> { box };
                allCircuits.Add(circuit);
                nodeToCircuit[box] = circuit;
            }

            (long x1, long x2) lastXs = (0, 0);

            foreach (var (a, b, _) in pairs)
            {
                var circuitA = nodeToCircuit[a];
                var circuitB = nodeToCircuit[b];

               
                if (ReferenceEquals(circuitA, circuitB))
                    continue;

              
                var target = circuitA;
                var source = circuitB;

                if (source.Count > target.Count)
                {
                    var tmp = target;
                    target = source;
                    source = tmp;
                }

                foreach (var node in source)
                {
                    target.Add(node);
                    nodeToCircuit[node] = target;
                }

                allCircuits.Remove(source);

                
                if (allCircuits.Count == 1)
                {
                   
                    lastXs = (a.x, b.x);
                    break;
                }
            }
            
            answer = lastXs.x1 * lastXs.x2;

            return answer;
        }

    }
}
