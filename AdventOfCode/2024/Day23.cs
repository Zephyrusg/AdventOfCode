using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class Y2024D23
    {
        public static string[] Lines = File.ReadAllLines(".\\2024\\Input\\inputDay23.txt");
        static Dictionary<string, HashSet<string>> graph = new Dictionary<string, HashSet<string>>();
        public int Part1()
        {
           
            foreach (string line in Lines)
            {
                var parts = line.Split('-');
                string node1 = parts[0];
                string node2 = parts[1];

                if (!graph.ContainsKey(node1)) graph[node1] = new HashSet<string>();
                if (!graph.ContainsKey(node2)) graph[node2] = new HashSet<string>();

                graph[node1].Add(node2);
                graph[node2].Add(node1);
            }

            HashSet<(string, string, string)> triangles = new HashSet<(string, string, string)>();

            foreach (var node in graph.Keys)
            {
                foreach (var neighbor1 in graph[node])
                {
                    foreach (var neighbor2 in graph[node])
                    {
                        if (neighbor1 != neighbor2 && graph[neighbor1].Contains(neighbor2))
                        {
                            var triangle = new[] { node, neighbor1, neighbor2 }.OrderBy(x => x).ToArray();
                            triangles.Add((triangle[0], triangle[1], triangle[2]));

                        }
                    }
                }
            }

            int count = triangles.Count(t =>
                t.Item1.StartsWith("t") || t.Item2.StartsWith("t") || t.Item3.StartsWith("t"));

            return count;
        }

        public string Part2()
        {
                     
            var allCliques = new List<HashSet<string>>();
            BronKerbosch(new HashSet<string>(), new HashSet<string>(graph.Keys), new HashSet<string>(), graph, allCliques);

            var largestClique = allCliques.OrderByDescending(clique => clique.Count).FirstOrDefault();
           
            var sortedClique = largestClique.OrderBy(x => x).ToList();
            return string.Join(",", sortedClique);
        }

        private void BronKerbosch(
            HashSet<string> R,
            HashSet<string> P,
            HashSet<string> X,
            Dictionary<string, HashSet<string>> graph,
            List<HashSet<string>> cliques)
        {
            if (P.Count == 0 && X.Count == 0)
            {
               
                cliques.Add(new HashSet<string>(R));
                return;
            }

            foreach (var node in new HashSet<string>(P))
            {
                var neighbors = graph[node];
                BronKerbosch(
                    new HashSet<string>(R) { node },
                    new HashSet<string>(P.Intersect(neighbors)),
                    new HashSet<string>(X.Intersect(neighbors)),
                    graph,
                    cliques
                );
                P.Remove(node);
                X.Add(node);
            }
        }

    }
}
