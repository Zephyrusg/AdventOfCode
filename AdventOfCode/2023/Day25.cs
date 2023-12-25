using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class Y2023D25
    {
        public static List<string> Lines = File.ReadAllLines(".\\2023\\Input\\inputDay25.txt").ToList();
        static int Height = Lines.Count();
        static int Width = Lines[0].Length;
        static Dictionary<string, List<string>> Graph = new Dictionary<string, List<string>>();
        static void BuildGraph(List<string> components)
        {
            Graph = new Dictionary<string, List<string>>();
            var dotFile = new StringBuilder();
            dotFile.AppendLine("graph {");



            foreach (var line in components)
            {
                var parts = line.Split(": ");
                var node = parts[0];
                var neighbors = parts[1].Split();

                if (!Graph.ContainsKey(node))
                {
                    Graph[node] = new List<string>();
                }

                foreach (var neighbor in neighbors)
                {
                    dotFile.AppendLine($"    {node} -- {neighbor}");
                    // Adding bidirectional connection
                    Graph[node].Add(neighbor);
                    if (!Graph.ContainsKey(neighbor))
                    {
                        Graph[neighbor] = new List<string>();
                    }
                    Graph[neighbor].Add(node);
                }
            }

            dotFile.AppendLine("}");
            File.WriteAllText("graph.dot", dotFile.ToString());


        }

        static HashSet<string> GetConnectedComponents(string start)
        {
            HashSet<string> visited = new HashSet<string>();
            var component = new HashSet<string>();
            var stack = new Stack<string>();
            stack.Push(start);

            while (stack.Count > 0)
            {
                var node = stack.Pop();
                if (!visited.Contains(node))
                {
                    visited.Add(node);
                    foreach (var neighbor in Graph[node])
                    {
                        stack.Push(neighbor);
                    }
                    component.Add(node);
                }
            }

            return component;
        }

        public HashSet<(string,string)> GetAllWires()
        {
            HashSet<(string, string)> AllWires = new HashSet<(string, string)>();
            foreach(var wire in Graph)
            {
                foreach( var connectedWire in wire.Value)
                {
                    if (!AllWires.Contains((connectedWire, wire.Key))) {
                        AllWires.Add((wire.Key,connectedWire));
                    }
                }
            }

            return AllWires;
        }

        static bool ArePairsOverlap((string, string) pair1, (string, string) pair2)
        {
            return pair1.Item1 == pair2.Item1 || pair1.Item1 == pair2.Item2 ||
                   pair1.Item2 == pair2.Item1 || pair1.Item2 == pair2.Item2;
        }

        public int Part1() 
        {
            int answer = 0;

            BuildGraph(Lines);
            HashSet<(string, string)> AllWires = new HashSet<(string, string)>();
            AllWires = GetAllWires();
            (string A, string B) Wire1 = ("xvh", "dhn");
            (string A, string B) Wire2 = ("lsv", "lxt");
            (string A, string B) Wire3 = ("ptj", "qmr");

            Graph[Wire1.A].Remove(Wire1.B);
            Graph[Wire1.B].Remove(Wire1.A);
            Graph[Wire2.A].Remove(Wire2.B);
            Graph[Wire2.B].Remove(Wire2.A);
            Graph[Wire3.A].Remove(Wire3.B);
            Graph[Wire3.B].Remove(Wire3.A);

            var result = GetConnectedComponents(Graph.Keys.First());

            answer = result.Count * (Graph.Count - result.Count);
            return answer;
        }

        public int Part2()
        {
            int answer = 0;




            return answer;
        }

    }
}
