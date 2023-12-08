using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode
{

    internal class Y2023D8
    {
        static List<Node> AllNodes = new List<Node>();
        class Node {
            public string Name;
            public List<Node> NextNodes = new List<Node>();
            //public List<int> FoundZon = new List<int>();

            public Node(string Name) {
                this.Name = Name;
            }

            public override string ToString()
            {
                return Name;
            }

        }
        public static string[] lines = File.ReadAllLines(".\\2023\\Input\\inputDay8.txt");
        public int Part1() 
        {
            int answer = 0;

            string WalkingPattern = lines[0];
            string[] restlines = lines.Skip(2).ToArray();
            foreach (string line in restlines)
            {
                string NodeName = line.Split(" = ")[0];
                string NextNodesPart = line.Split(" = ")[1];
                string Pattern = "\\w+";
                var Matches = Regex.Matches(NextNodesPart, Pattern);
                List<Node> NextNodes = new List<Node>();
                Node? Node = AllNodes.Find(x => x.Name == NodeName);
                if (Node == null)
                {
                    Node = new(NodeName);
                }

                foreach (Match Match in Matches)
                {   
                    
                    Node? NextNode = AllNodes.Find(x => x.Name == Match.Value);
                    if (NextNode == null)
                    {
                        NextNode = new(Match.Value);
                        NextNodes.Add(NextNode);
                    }
                    else { 
                        NextNodes.Add(NextNode);
                    }
                    
                }
                Node.NextNodes = NextNodes;
                AllNodes.Add(Node);


            }

            Node StartNode = AllNodes.Find(x => x.Name == "AAA");
            Node CurrentNode = StartNode;
            int WalkingIndex = 0;
            int WalkingCounter =  0;
            while (CurrentNode.Name != "ZZZ") {

                char Direction = WalkingPattern[WalkingIndex];
                string NextNodeName = "";
                switch(Direction)
                {
                    case 'L':
                        NextNodeName = CurrentNode.NextNodes[0].Name;
                        CurrentNode = AllNodes.Find(x => x.Name == NextNodeName);
                        
                        break;
                    case 'R':
                        NextNodeName = CurrentNode.NextNodes[1].Name;
                        CurrentNode = AllNodes.Find(x => x.Name == NextNodeName);
                        break;
                }
                WalkingCounter++;
                if (WalkingIndex == WalkingPattern.Length-1)
                {
                    WalkingIndex = 0;
                }
                else { 
                    WalkingIndex++;
                }

            }

            answer = WalkingCounter;


            return answer;
        }

        public BigInteger Part2()
        {
            BigInteger answer = 0;
            string WalkingPattern = lines[0];
            List<Node> AllStartNodes = AllNodes.FindAll(x=> x.Name.EndsWith('A')).ToList();
            List<Node> CurrentNodes = AllStartNodes;
            int WalkingIndex = 0;
            BigInteger WalkingCounter = 0;
            int EndState = 0;
            while (EndState != AllStartNodes.Count())
            {

                char Direction = WalkingPattern[WalkingIndex];
                List<Node>NextNodes = new List<Node>();
                foreach (Node CurrentNode in CurrentNodes)
                {
                    WalkingCounter++;
                    string NextNodeName = "";
                    Node NextNode = null;
                    switch (Direction)
                    {
                        case 'L':
                            NextNodeName = CurrentNode.NextNodes[0].Name;
                            NextNode = AllNodes.Find(x => x.Name == NextNodeName);

                            break;
                        case 'R':
                            NextNodeName = CurrentNode.NextNodes[1].Name;
                            NextNode = AllNodes.Find(x => x.Name == NextNodeName);
                            break;
                    }
                    NextNodes.Add(NextNode);
                    //if (NextNode.Name[2] == 'Z') {
                    //    if (NextNode.FoundZon.Count == 0)
                    //    {
                    //        NextNode.FoundZon.Add(WalkingCounter);
                    //    }
                    //    else { 
                    //        int previouswalk = NextNode.FoundZon.Last() - WalkingCounter;
                    //        NextNode.FoundZon.Add(WalkingCounter);
                    //    }
                        
                    //}
                }
                CurrentNodes = NextNodes;
                
                if (WalkingIndex == WalkingPattern.Length - 1)
                {
                    WalkingIndex = 0;
                }
                else
                {
                    WalkingIndex++;
                }
                EndState = CurrentNodes.FindAll(x => x.Name.EndsWith('Z')).Count();
                if (WalkingCounter % 1000000000 == 0) {
                    Console.WriteLine("WalkingPattern = " + WalkingCounter);
                }
            }

            answer = WalkingCounter;


            return answer;
        }

    }
}
