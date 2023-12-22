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
        static List<Ghost> Ghosts = new List<Ghost>();
        class Node {
            public string Name;
            public List<Node> NextNodes = new List<Node>();

            public Node(string Name) {
                this.Name = Name;
            }

            public override string ToString()
            {
                return Name;
            }

        }

        class Ghost {
            public Node Node;
            public List<int> FoundZon = new List<int>();
            public int Walked = 0;
            public List<string> FoundZs = new List<string>();
            public bool FoundZ = false;

            public Ghost(Node Node)
            {
                this.Node = Node;
            }
        
        }

        static public long gcf(long a, long b)
        {
            while (b != 0)
            {
                long temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        static public long lcm(long a, long b)
        {
            return (a / gcf(a, b)) * b;
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

        public long Part2()
        {
            long answer = 0;
            string WalkingPattern = lines[0];
            List<Node> AllStartNodes = AllNodes.FindAll(x=> x.Name.EndsWith('A')).ToList();
            
            foreach(Node Node in AllStartNodes)
            {
                Ghost Ghost = new(Node);
                Ghosts.Add(Ghost);
            }
            
            List<Ghost> CurrentGhosts = Ghosts;


            int WalkingIndex = 0;
            while (!(CurrentGhosts.All(x=>x.FoundZ == true)))
            {

                char Direction = WalkingPattern[WalkingIndex];
               
                foreach (Ghost Ghost in Ghosts)
                {
                    if (Ghost.FoundZ == true)
                    {
                        continue;
                    }
                    Ghost.Walked++;
                   
                    string NextNodeName = "";
                    Node NextNode = null;
                    switch (Direction)
                    {
                        case 'L':
                            NextNodeName = Ghost.Node.NextNodes[0].Name;
                            NextNode = AllNodes.Find(x => x.Name == NextNodeName);

                            break;
                        case 'R':
                            NextNodeName = Ghost.Node.NextNodes[1].Name;
                            NextNode = AllNodes.Find(x => x.Name == NextNodeName);
                            break;
                    }
                    Ghost.Node = NextNode;
                    
                    
                    if (NextNode.Name[2] == 'Z')
                    {
                        Ghost.FoundZon.Add(Ghost.Walked);
                        Ghost.FoundZs.Add(NextNode.Name);
                        Ghost.FoundZ = true;

                    }
                   
                }
               
                
                if (WalkingIndex == WalkingPattern.Length - 1)
                {
                    WalkingIndex = 0;
                }
                else
                {
                    WalkingIndex++;
                }
            }

            long lcmd = Ghosts[0].FoundZon[0];

            for(int x=1; x<Ghosts.Count(); x++)
            {
               lcmd = lcm(lcmd, Ghosts[x].FoundZon[0]);
            }

            answer = lcmd;

            return answer;
        }

    }
}
