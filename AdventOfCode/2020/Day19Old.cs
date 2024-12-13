using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;
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

            static public string CheckMessage(string message, int Ruleid, string teststring)
            {
                if (Rules[Ruleid] is LiteralRule)
                {

                    string literal = (Rules[Ruleid] as LiteralRule).literal;
                    
                    return teststring + literal;
                    
                }
                else
                {

                    string[] SubPaths = (Rules[Ruleid] as PathRule).SubPaths;
                    bool match = false;
                    string result = teststring;
                    string SubResult = result;
                    foreach (string SubPath in SubPaths)
                    {
                        if (match )
                        {
                            continue;
                        }
                        else
                        {
                            result = SubResult;
                        }
                        
                        int[] Paths = SubPath.Split(" ").Select(num => Int32.Parse(num)).ToArray();
                        match = false;


                        foreach (int Path in Paths)
                        {

                            result = CheckMessage(message, Path, result);
                            if (message.StartsWith(result))
                            {
                                match = true;
                            }
                            else if (result == "X") { 
                                match = false;
                                result = teststring;
                                break;
                            }
                            else
                            {
                                match = false;
                                break;
                            }
                        }
                        if (match)
                        {

                            SubResult = result;
                        }
                        else
                        {
                            match = false;
                            continue;
                        }

                    }
                    if (match)
                    {
                        return SubResult;
                    }
                    else{
                        return "X";
                    }
                }
                
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

        //class StartRule : Rule
        //{
        //    public int[] StartPath;

        //    public StartRule(int[] StartPath) { 
            
        //        this.StartPath = StartPath;
            
        //    }
        
        //}


        public static int Part1()
        {
            int answer = 0;

            string[] Data = File.ReadAllLines(".\\2020\\Input\\InputDay19Rules.txt");
            string[] Entries = File.ReadAllLines(".\\2020\\Input\\InputDay19Entries.txt");
           
            Array.Sort(Data);
            //Array.Sort(Data);
            foreach (string line in Data)
            {
                string[] parts = line.Split(": ");
                //if (Int32.Parse(parts[0]) == 0){ 
                //    Rule.Rules.Add(0, new StartRule(parts[1].Split(" ").Select(num => Int32.Parse(num)).ToArray()));
                //} 
                //else
                if (parts[1] == "\"a\"" || parts[1] == "\"b\"")
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
                string result = Rule.CheckMessage(Entry, 0, "");
                if (result == Entry)
                {
                    answer++;
                }               
            }
            



            return answer;
        }
        public static int Part2()
        {
            int answer = 0;
            
            string[] Data = File.ReadAllLines(".\\2020\\Input\\InputDay19RulesExample.txt");
            string[] Entries = File.ReadAllLines(".\\2020\\Input\\InputDay19EntriesExample.txt");
            Rule.Rules = new Dictionary<int, Rule>();

            Array.Sort(Data);
            foreach (string line in Data)
            {
                string[] parts = line.Split(": ");
                //if (Int32.Parse(parts[0]) == 0){ 
                //    Rule.Rules.Add(0, new StartRule(parts[1].Split(" ").Select(num => Int32.Parse(num)).ToArray()));
                //} 
                //else
                if (parts[1] == "\"a\"" || parts[1] == "\"b\"")
                {
                    string part = parts[1].Replace("\"", "");
                    Rule.Rules.Add(Int32.Parse(parts[0]), new LiteralRule(part));
                }
                else
                {
                    Rule.Rules.Add(Int32.Parse(parts[0]), new PathRule(parts[1]));
                }
            }

            Rule.Rules.Remove(8);
            Rule.Rules.Remove(11);

            Rule.Rules.Add(8, new PathRule("42 | 42 8"));
            Rule.Rules.Add(11, new PathRule("42 31 | 42 11 31"));

            foreach (string Entry in Entries)
            {
                string result = Rule.CheckMessage(Entry, 0, "");
                if (result == Entry)
                {
                    answer++;
                }
            }
            return answer;
        }
    }
}
