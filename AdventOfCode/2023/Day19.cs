using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class Y2023D19
    {
        static List<Gear> Gears = new List<Gear>();
        static List<WorkFlow> Workflows = new List<WorkFlow>();

        static Dictionary<string, int> conversion = new Dictionary<string, int> {
            {"x", 0 },
            {"m", 1 },
            {"a", 2 },
            {"s", 3 },

        };
        class Gear
        {
            public int[] Values = new int[4];
         
            public string Status;

            public Gear(int x, int m, int a, int s)
            {
                Values[0] = x;
                Values[1] = m;
                Values[2] = a;
                Values[3] = s;
                Status = "in";
            }

            public void WalkWorkflow()
            {

                while (Status != "A" && Status != "R")
                {
                    WorkFlow Workflow = Workflows.Find(w => w.name == Status);

                    foreach (Rule Rule in Workflow.Rules)
                    {
                        if (Rule.partvalue == null)
                        {
                            Status = Rule.Path;
                        }
                        else
                        {
                            switch (Rule.symbol)
                            {

                                case "<":
                                    {
                                        if (this.Values[conversion[Rule.partvalue]] < Rule.testvalue) { 
                                            this.Status = Rule.Path;
                                            goto NextWorkFlow;

                                        }
                                        break;
                                    }
                                case ">":
                                    {
                                        if (this.Values[conversion[Rule.partvalue]] > Rule.testvalue)
                                        {
                                            this.Status = Rule.Path;
                                            goto NextWorkFlow;
                                        }
                                        break;

                                    }
                            }
                        }

                    }
                NextWorkFlow:;

                }

            }
        }

        

        class Rule {
            public string? partvalue;
            public string? symbol;
            public int? testvalue;
            public string Path;

            public Rule(string partvalue, string symbol, int testvalue, string Path)
            {
                this.partvalue = partvalue;
                this.symbol = symbol;
                this.testvalue = testvalue;
                this.Path = Path;
            }

            public Rule(string defaultpath)
            {
                Path = defaultpath;
            }

            public override string ToString()
            {
                if (partvalue == null) {
                    return "Default: " + Path;
                }
                else
                {
                    return (partvalue + " " + symbol + " " + testvalue + " " + Path);
                }

            }

            
        }

        public static long[][] CopyArray(long[][] source)
        {
            long[][] destination = new long[source.Length][];
            // For each Row
            for (int y = 0; y < source.Length; y++)
            {
                destination[y] = (long[])source[y].Clone();
            }
            return destination;
        }
        public long FindCombinations(long[][] GearRange, string WorkflowName)
        {
            long result = 0;
            if (WorkflowName == "A")
            {
                result = (GearRange[0][1] - GearRange[0][0] + 1) * (GearRange[1][1] - GearRange[1][0] + 1) * (GearRange[2][1] - GearRange[2][0] + 1) * (GearRange[3][1] - GearRange[3][0] + 1);
                return result;  
            }
            else if (WorkflowName == "R")
            {
                return 0;
            }

            GearRange = CopyArray(GearRange);

            WorkFlow Workflow = Workflows.Find(w => w.name == WorkflowName);

            foreach (Rule Rule in Workflow.Rules)
            {               
                switch (Rule.symbol)
                {
                    

                    case "<":
                        {
                            int conversionnumber = conversion[Rule.partvalue];
                            if (GearRange[conversionnumber][1] < Rule.testvalue) {
                                result += FindCombinations(GearRange, Rule.Path);
                            }else if (GearRange[conversionnumber][0] < Rule.testvalue && GearRange[conversionnumber][1] > Rule.testvalue)
                            {
                                long[][] GearTestRange = CopyArray(GearRange);
                                GearTestRange[conversionnumber][1] = (long)Rule.testvalue - 1;
                                result += FindCombinations(GearTestRange, Rule.Path);
                                GearRange[conversionnumber][0] = (long)Rule.testvalue;

                            }

                            break;
                        }
                    case ">":
                        {

                            int conversionnumber = conversion[Rule.partvalue];
                            if (GearRange[conversionnumber][0] > Rule.testvalue)
                            {
                                Console.WriteLine("Geater " + Rule.testvalue + " " + "[" + GearRange[conversionnumber][0] + "-" + GearRange[conversionnumber][1] + "]");
                                result += FindCombinations(GearRange, Rule.Path);
                            }
                            else if (GearRange[conversionnumber][0] <= Rule.testvalue && GearRange[conversionnumber][1] > Rule.testvalue)
                            {
                                // 6..15
                                //  7 >
                                // 8..15
                                long[][] GearTestRange = CopyArray(GearRange);
                                GearTestRange[conversionnumber][0] = (long)Rule.testvalue + 1;
                                result += FindCombinations(GearTestRange, Rule.Path);
                                GearRange[conversionnumber][1] = (long)Rule.testvalue;
                            }
                            break;

                        }
                    case null:
                        {
                            result += FindCombinations(GearRange, Rule.Path);
                            break;
                        }
                }
                

            }

            return result;

        }



        class WorkFlow {

            public string name;
            public List<Rule> Rules = new();

            public WorkFlow(string name, List<Rule> rules)
            {
                this.name = name;
                Rules = rules;
            }

            public override string ToString()
            {
                return this.name;
            }
        }

        public static string[] Lines = File.ReadAllLines(".\\2023\\Input\\inputDay19.txt");
        static int Height = Lines.Count();
        static int Width = Lines[0].Length;
        public int Part1() 
        {
            int answer = 0;

            foreach(var line in Lines)
            {
                if (line == "") {
                    continue;
                }

                if (line[0] == '{')
                {
                    string Gearline = line[1..(line.Length - 1)];
                    string[] parts = Gearline.Split(',');
                    int x; int m; int a; int s;
                    x = int.Parse(parts[0].Split("=")[1]);
                    m = int.Parse(parts[1].Split("=")[1]);
                    a = int.Parse(parts[2].Split("=")[1]);
                    s = int.Parse(parts[3].Split("=")[1]);
                    Gears.Add(new(x, m, a, s));
                }
                else { 

                    string WorkFlowLine = line[0..(line.Length - 1)];
                    string[] parts = WorkFlowLine.Split("{");
                    string WorkflowName = parts[0];
                    List<Rule> rules = new List<Rule>();
                    string[] Ruleparts = parts[1].Split(",");
                    for(int x = 0; x < Ruleparts.Count() -1; x ++) {
                        string RulePart = Ruleparts[x];
                        string partvalue = RulePart[0].ToString();
                        string symbol = RulePart[1].ToString();
                        string Restpart = RulePart[2..(RulePart.Length)];
                        int value = int.Parse(Restpart.Split(":")[0]);
                        string location = Restpart.Split(":")[1];

                        rules.Add(new(partvalue, symbol, value, location));

                    }
   
                    rules.Add(new(Ruleparts[Ruleparts.Length-1]));
                    Workflows.Add(new(WorkflowName, rules));


                }

                foreach(Gear Gear in Gears)
                {
                    Gear.WalkWorkflow();
                }
            }

            var AllowedGears =  Gears.Where(s => s.Status == "A").ToList();
            foreach (Gear Gear in AllowedGears) {
                answer += Gear.Values.Sum();
            }


            return answer;
        }

        public long Part2()
        {
            long answer = 0;
            long[][] GearRange = [
                [1, 4000],
                [1, 4000],
                [1, 4000],
                [1, 4000]
        ];

            answer += FindCombinations(GearRange, "in");


            return answer;
        }

    }
}
