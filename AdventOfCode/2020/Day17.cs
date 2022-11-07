using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2020
{
    internal class Day17
    {

        class Map{
            public static HashSet<Tuple<int, int, int>> ActiveNodes = new HashSet<Tuple<int, int, int>>();
            private static List<Tuple<int, int, int>> FindNeighbours( Tuple<int, int, int> Node) {
                List < Tuple<int, int, int>> Neighbours = new List<Tuple<int, int, int>>();
                
                
                
                
                Neighbours.Add(Tuple.Create(Node.Item1 - 1, Node.Item2,     Node.Item3    ));
                Neighbours.Add(Tuple.Create(Node.Item1 - 1, Node.Item2 - 1, Node.Item3    ));
                Neighbours.Add(Tuple.Create(Node.Item1 - 1, Node.Item2,     Node.Item3 - 1));
                Neighbours.Add(Tuple.Create(Node.Item1,     Node.Item2 -1,  Node.Item3    ));
                Neighbours.Add(Tuple.Create(Node.Item1,     Node.Item2 -1,  Node.Item3 - 1));
                Neighbours.Add(Tuple.Create(Node.Item1,     Node.Item2,     Node.Item3 - 1));
                Neighbours.Add(Tuple.Create(Node.Item1 - 1, Node.Item2 - 1, Node.Item3 - 1));
                Neighbours.Add(Tuple.Create(Node.Item1 + 1, Node.Item2,     Node.Item3    ));
                Neighbours.Add(Tuple.Create(Node.Item1 + 1, Node.Item2 + 1, Node.Item3    ));
                Neighbours.Add(Tuple.Create(Node.Item1 + 1, Node.Item2,     Node.Item3 + 1));
                Neighbours.Add(Tuple.Create(Node.Item1,     Node.Item2 + 1, Node.Item3    ));
                Neighbours.Add(Tuple.Create(Node.Item1,     Node.Item2 + 1, Node.Item3 + 1));
                Neighbours.Add(Tuple.Create(Node.Item1,     Node.Item2,     Node.Item3 + 1));
                Neighbours.Add(Tuple.Create(Node.Item1 + 1, Node.Item2 + 1, Node.Item3 + 1));
                Neighbours.Add(Tuple.Create(Node.Item1 + 1, Node.Item2 - 1, Node.Item3    ));
                Neighbours.Add(Tuple.Create(Node.Item1 + 1, Node.Item2 - 1, Node.Item3 - 1));
                Neighbours.Add(Tuple.Create(Node.Item1 + 1, Node.Item2,     Node.Item3 - 1));
                Neighbours.Add(Tuple.Create(Node.Item1 - 1, Node.Item2 + 1, Node.Item3    ));
                Neighbours.Add(Tuple.Create(Node.Item1,     Node.Item2 + 1, Node.Item3 - 1));
                Neighbours.Add(Tuple.Create(Node.Item1 - 1, Node.Item2 + 1, Node.Item3 - 1));
                Neighbours.Add(Tuple.Create(Node.Item1 - 1, Node.Item2,     Node.Item3 + 1));
                Neighbours.Add(Tuple.Create(Node.Item1,     Node.Item2 - 1, Node.Item3 + 1));
                Neighbours.Add(Tuple.Create(Node.Item1 - 1, Node.Item2 - 1, Node.Item3 + 1));
                Neighbours.Add(Tuple.Create(Node.Item1 + 1, Node.Item2 + 1, Node.Item3 - 1));
                Neighbours.Add(Tuple.Create(Node.Item1 + 1, Node.Item2 - 1, Node.Item3 + 1));
                Neighbours.Add(Tuple.Create(Node.Item1 - 1, Node.Item2 + 1, Node.Item3 + 1));


                
                return Neighbours;
            }

            public static void CheckNodes() {
                HashSet<Tuple<int, int, int>> ActiveNodes = new HashSet<Tuple<int, int, int>>();
                List<Tuple<int, int, int>> Neighbours = new List<Tuple<int, int, int>>();
                foreach (var Node in Map.ActiveNodes) {
                    Neighbours.AddRange(Map.FindNeighbours(Node));     
                }
                foreach (Tuple<int, int, int> Neighbour in Neighbours) {
                    int CountActive = 0;
                    Boolean IsActive = Map.ActiveNodes.Contains(Neighbour);
                    List<Tuple<int, int, int>> NodeNeighbours = Map.FindNeighbours(Neighbour);
                    foreach (Tuple<int, int, int> NodeNeighbour in NodeNeighbours) {
                        if (Map.ActiveNodes.Contains(NodeNeighbour)) {
                            CountActive++;
                        }
                    }
                    if (IsActive)
                    {
                        if (CountActive == 2 ^ CountActive == 3)
                        {
                            ActiveNodes.Add(Neighbour);
                        }
                    }
                    else {
                        if (CountActive == 3) {
                            ActiveNodes.Add(Neighbour);
                        }
                    }

                }
                Map.ActiveNodes = ActiveNodes;

            }

            public static int CountActivePoints() {

                int ActivePoints = Map.ActiveNodes.Count();
                return ActivePoints;
            }

        }

        class MapB
        {
            public static HashSet<Tuple<int, int, int, int>> ActiveNodes = new HashSet<Tuple<int, int, int, int>>();
            private static List<Tuple<int, int, int, int>> FindNeighbours(Tuple<int, int, int, int> Node)
            {
                List<Tuple<int, int, int, int>> Neighbours = new List<Tuple<int, int, int, int>>();
                int[] Ws = new int[] { -1, 0, 1};

                foreach (int W in Ws)
                {

                    Neighbours.Add(Tuple.Create(Node.Item1 - 1, Node.Item2,     Node.Item3,     Node.Item4 + W));
                    Neighbours.Add(Tuple.Create(Node.Item1 - 1, Node.Item2 - 1, Node.Item3,     Node.Item4 + W));
                    Neighbours.Add(Tuple.Create(Node.Item1 - 1, Node.Item2,     Node.Item3 - 1, Node.Item4 + W));
                    Neighbours.Add(Tuple.Create(Node.Item1,     Node.Item2 - 1, Node.Item3,     Node.Item4 + W));
                    Neighbours.Add(Tuple.Create(Node.Item1,     Node.Item2 - 1, Node.Item3 - 1, Node.Item4 + W));
                    Neighbours.Add(Tuple.Create(Node.Item1,     Node.Item2,     Node.Item3 - 1, Node.Item4 + W));
                    Neighbours.Add(Tuple.Create(Node.Item1 - 1, Node.Item2 - 1, Node.Item3 - 1, Node.Item4 + W));
                    Neighbours.Add(Tuple.Create(Node.Item1 + 1, Node.Item2,     Node.Item3,     Node.Item4 + W));
                    Neighbours.Add(Tuple.Create(Node.Item1 + 1, Node.Item2 + 1, Node.Item3,     Node.Item4 + W));
                    Neighbours.Add(Tuple.Create(Node.Item1 + 1, Node.Item2,     Node.Item3 + 1, Node.Item4 + W));
                    Neighbours.Add(Tuple.Create(Node.Item1,     Node.Item2 + 1, Node.Item3,     Node.Item4 + W));
                    Neighbours.Add(Tuple.Create(Node.Item1,     Node.Item2 + 1, Node.Item3 + 1, Node.Item4 + W));
                    Neighbours.Add(Tuple.Create(Node.Item1,     Node.Item2,     Node.Item3 + 1, Node.Item4 + W));
                    Neighbours.Add(Tuple.Create(Node.Item1 + 1, Node.Item2 + 1, Node.Item3 + 1, Node.Item4 + W));
                    Neighbours.Add(Tuple.Create(Node.Item1 + 1, Node.Item2 - 1, Node.Item3,     Node.Item4 + W));
                    Neighbours.Add(Tuple.Create(Node.Item1 + 1, Node.Item2 - 1, Node.Item3 - 1, Node.Item4 + W));
                    Neighbours.Add(Tuple.Create(Node.Item1 + 1, Node.Item2,     Node.Item3 - 1, Node.Item4 + W));
                    Neighbours.Add(Tuple.Create(Node.Item1 - 1, Node.Item2 + 1, Node.Item3,     Node.Item4 + W));
                    Neighbours.Add(Tuple.Create(Node.Item1,     Node.Item2 + 1, Node.Item3 - 1, Node.Item4 + W));
                    Neighbours.Add(Tuple.Create(Node.Item1 - 1, Node.Item2 + 1, Node.Item3 - 1, Node.Item4 + W));
                    Neighbours.Add(Tuple.Create(Node.Item1 - 1, Node.Item2,     Node.Item3 + 1, Node.Item4 + W));
                    Neighbours.Add(Tuple.Create(Node.Item1,     Node.Item2 - 1, Node.Item3 + 1, Node.Item4 + W));
                    Neighbours.Add(Tuple.Create(Node.Item1 - 1, Node.Item2 - 1, Node.Item3 + 1, Node.Item4 + W));
                    Neighbours.Add(Tuple.Create(Node.Item1 + 1, Node.Item2 + 1, Node.Item3 - 1, Node.Item4 + W));
                    Neighbours.Add(Tuple.Create(Node.Item1 + 1, Node.Item2 - 1, Node.Item3 + 1, Node.Item4 + W));
                    Neighbours.Add(Tuple.Create(Node.Item1 - 1, Node.Item2 + 1, Node.Item3 + 1, Node.Item4 + W));

                }

                Neighbours.Add(Tuple.Create(Node.Item1, Node.Item2, Node.Item3, Node.Item4 + 1));
                Neighbours.Add(Tuple.Create(Node.Item1, Node.Item2, Node.Item3, Node.Item4 - 1));

                return Neighbours;
            }

            public static void CheckNodes()
            {
                HashSet<Tuple<int, int, int, int>> ActiveNodes = new HashSet<Tuple<int, int, int, int>>();
                List<Tuple<int, int, int, int>> Neighbours = new List<Tuple<int, int, int, int>>();
                foreach (var Node in MapB.ActiveNodes)
                {
                    Neighbours.AddRange(MapB.FindNeighbours(Node));
                }
                foreach (Tuple<int, int, int, int> Neighbour in Neighbours)
                {
                    int CountActive = 0;
                    Boolean IsActive = MapB.ActiveNodes.Contains(Neighbour);
                    List<Tuple<int, int, int, int>> NodeNeighbours = MapB.FindNeighbours(Neighbour);
                    foreach (Tuple<int, int, int, int> NodeNeighbour in NodeNeighbours)
                    {
                        if (MapB.ActiveNodes.Contains(NodeNeighbour))
                        {
                            CountActive++;
                        }
                    }
                    if (IsActive)
                    {
                        if (CountActive == 2 ^ CountActive == 3)
                        {
                            ActiveNodes.Add(Neighbour);
                        }
                    }
                    else
                    {
                        if (CountActive == 3)
                        {
                            ActiveNodes.Add(Neighbour);
                        }
                    }

                }
                MapB.ActiveNodes = ActiveNodes;

            }

            public static int CountActivePoints()
            {

                int ActivePoints = MapB.ActiveNodes.Count();
                return ActivePoints;
            }

        }



        public static int Day17A()
        {
            string[] Lines = File.ReadAllLines(".\\2020\\Input\\InputDay17.txt");
            int y = 0;
            for (int l = Lines.Count() - 1; l >= 0; l--) {
                string line = Lines[l];
                for (int x = 0; x < line.Length; x++){
                    if (line[x] == '#'){
                        Map.ActiveNodes.Add(Tuple.Create(x,y,0));
                    }
                }
                y++;
            }
            int Cycles = 6;
            for (int c = 0; c < Cycles; c++) {
                Map.CheckNodes();
            }

            return Map.CountActivePoints();
        }

        public static int Day17B()
        {
            string[] Lines = File.ReadAllLines(".\\2020\\Input\\InputDay17.txt");
            int y = 0;
            for (int l = Lines.Count() - 1; l >= 0; l--)
            {
                string line = Lines[l];
                for (int x = 0; x < line.Length; x++)
                {
                    if (line[x] == '#')
                    {
                        MapB.ActiveNodes.Add(Tuple.Create(x, y, 0, 0));
                    }
                }
                y++;
            }
            int Cycles = 6;
            for (int c = 0; c < Cycles; c++)
            {
                MapB.CheckNodes();
            }

            return MapB.CountActivePoints();
        }



    }
}
