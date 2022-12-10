using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2020
{
    internal class Day19
    {
        class RuleProcessing{

        }
        class Rule {
            static int index = 0;
            static public Dictionary<int,Rule> Rules = new Dictionary<int,Rule>();

            static public bool checkpart(string message, int Ruleid)
            {
                if (Rules[Ruleid] is LiteralRule)
                {

                    string literal = (Rules[Ruleid] as LiteralRule).literal;

                    return (message[index].ToString() == literal);
                }


                else {

                    string[] Subpaths = (Rules[Ruleid] as PathRule).SubPaths;

                    foreach (int[] subpath in Subpaths.Select(s => s.Split(" ").Select(num => Int32.Parse(num))).ToArray())  {
                        int steps = subpath.Count();
                        var match = true;
                        foreach (int path in subpath)
                        {
                            
                            
                            if (checkpart(message, path))
                            {



                            }
                            else { 
                                match= false;
                                break;
                            }
                        }
                        if (match)
                        {
                            index += steps;
                        }
                    }
                }


                return false;
            }

            static public int GetIndexRule (string message, int Ruleid) {
                int index = 0;




                return index;
            }
            

        }

        class LiteralRule : Rule
        {
            public string literal;
            public LiteralRule(string literal)
            {
                this.literal = literal;
            }
        }

        class PathRule : Rule {

            public string[] SubPaths = new string[2];

            public PathRule(string Data)
            {
                SubPaths = Data.Split(" | ");
                
            }
        }

        class StartRule : Rule
        {
            public int[] StartPath;

            public StartRule(int[] StartPath) { 
            
                this.StartPath = StartPath;
            
            }
        
        }


        public static int Part1()
        {
            int answer = 0;

            string[] Data = File.ReadAllLines(".\\2020\\Input\\InputDay19RulesExample.txt");
            string[] Entries = File.ReadAllLines(".\\2020\\Input\\InputDay19EntriesExample.txt");


            //Array.Sort(Data);
            foreach (string line in Data)
            {
                string[] parts = line.Split(": ");
                if (Int32.Parse(parts[0]) == 0){ 
                    Rule.Rules.Add(0, new StartRule(parts[1].Split(" ").Select(num => Int32.Parse(num)).ToArray()));
                } 
                else if (parts[1].Length == 3)
                {
                    string part = parts[1].Replace("\"", "");
                    Rule.Rules.Add(Int32.Parse(parts[0]), new LiteralRule(part));
                }
                else {
                    Rule.Rules.Add(Int32.Parse(parts[0]), new PathRule(parts[1]));
                }
            }

            foreach(string Entry in Entries)
            {
                int[] Subpaths = (Rule.Rules[0] as StartRule).StartPath;
                foreach (int Subpath in Subpaths) { 
                

                
                }
                
                answer++;
                
            }
            



            return answer;
        }
        public static int Part2()
        {
            int test = 0;

            return test;
        }
    }
}
