using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022
{

    class Monkey
    {
        static public List<Monkey> AllMonkeys = new List<Monkey>();
        static public long commomModulo = 0;
        static public bool Part1 = true;
        public int number;
        public Queue<long> items = new Queue<long>();
        public int testvalue;
        int throwTrue;
        int throwFalse;
        public long timesInspectItem = 0;
        long operationValue;
        string operation;

        public Monkey(int number, long[] items, string operartionstring, int testvalue, int throwTrue, int throwFalse)
        {
            this.number = number;
            this.testvalue = testvalue;
            this.throwTrue = throwTrue;
            this.throwFalse = throwFalse;

            long operationValue =0;
            string[] parts = operartionstring.Split(" ");
            
            
            if (long.TryParse(parts[2], out operationValue))
            {
                this.operationValue = long.Parse(parts[2]);

            }
            else
            {
                this.operationValue = -1;
            }
            this.operation = parts[1];

            foreach (long item in items)
            {

                this.items.Enqueue(item);
            }
        }

        long DoOperation(long item)
        {
            if (this.operationValue == -1) { 
                this.operationValue = item;
            }
           
            switch (this.operation)
            {
                case "+":
                    {
                        item = (item + this.operationValue);
                        break;
                    }
                case "*":
                    {
                        item = (item * this.operationValue) % commomModulo;
                        break;
                    }

            }
            return item;
        }
        public void InspectItems()
        {
            while (this.items.Count > 0)
            {
                {
                    
                    long item = this.items.Dequeue();
                    item = DoOperation(item);
                    if (Part1)
                    {
                        item = item / 3;
                    }
                    if (item % this.testvalue == 0)

                    {
                        AllMonkeys[this.throwTrue].items.Enqueue(item);
                    }
                    else
                    {
                        
                        AllMonkeys[this.throwFalse].items.Enqueue(item);
                    }
                }
                this.timesInspectItem++;
            }

        }

    }

    internal class Day11
    {
        static string Path = ".\\2022\\Input\\InputDay11.txt";
        public static long Part1()
        {
            string[] Data = File.ReadAllLines(Path);
            long answer = 0;
            int rounds = 1;
            string operation = "";
            int test = 0;
            int iftrue = 0;
            int iffalse = 0;
            int monkeyNumber = 0;
            long[] items = new long[2];
            foreach (string line in Data)
            {
                switch (line.Trim())
                {
                    case string s when s.StartsWith("Starting items:"):
                        {
                            string unparsedItems = line.Substring(2).Split(": ")[1];
                            items = unparsedItems.Split(",").Select(num => long.Parse(num)).ToArray();
                            break;
                        }
                    case string s when s.StartsWith("Operation"):
                        {
                            operation = line.Split(" = ")[1];
                            break;
                        }
                    case string s when s.StartsWith("Test:"):
                        {
                            test = Int32.Parse(line.Split(" by ")[1]);

                            break;
                        }
                    case string s when s.StartsWith("Test:"):
                        {
                            test = Int32.Parse(line.Split(" by ")[1]);

                            break;
                        }
                    case string s when s.StartsWith("If true:"):
                        {
                            iftrue = Int32.Parse(line.Split(" monkey ")[1]);

                            break;
                        }
                    case string s when s.StartsWith("If false:"):
                        {
                            iffalse = Int32.Parse(line.Split(" monkey ")[1]);

                            break;
                        }
                    case "":
                        {
                            Monkey.AllMonkeys.Add(new Monkey(monkeyNumber, items, operation, test, iftrue, iffalse));
                            monkeyNumber++;
                            break;
                        }
                    default:
                        {
                            break;
                        }

                }
            }
            Monkey.AllMonkeys.Add(new Monkey(monkeyNumber, items, operation, test, iftrue, iffalse));
            Monkey.commomModulo = Monkey.AllMonkeys.Select(m => m.testvalue).Aggregate((m, x) => m * x);
            while (rounds <= 20) { 
                foreach(Monkey monkey in Monkey.AllMonkeys)
                {
                    monkey.InspectItems();
                }
                rounds++;
            }
           
            answer = Monkey.AllMonkeys.OrderByDescending(m => m.timesInspectItem).Take(2).Select(m => m.timesInspectItem).Aggregate((m, x) => m * x);

            return answer;
        }


        public static long Part2()
        {
            string[] Data = File.ReadAllLines(Path);
            long answer = 0;
            Monkey.AllMonkeys = new List<Monkey>();
            Monkey.Part1 = false;
            int rounds = 1;
            string operation = "";
            int test = 0;
            int iftrue = 0;
            int iffalse = 0;
            int monkeyNumber = 0;
            long[] items = new long[2];
            foreach (string line in Data)
            {
                switch (line.Trim())
                {
                    case string s when s.StartsWith("Starting items:"):
                        {
                            string unparsedItems = line.Substring(2).Split(": ")[1];
                            items = unparsedItems.Split(",").Select(num => long.Parse(num)).ToArray();
                            break;
                        }
                    case string s when s.StartsWith("Operation"):
                        {
                            operation = line.Split(" = ")[1];
                            break;
                        }
                    case string s when s.StartsWith("Test:"):
                        {
                            test = Int32.Parse(line.Split(" by ")[1]);

                            break;
                        }
                    case string s when s.StartsWith("Test:"):
                        {
                            test = Int32.Parse(line.Split(" by ")[1]);

                            break;
                        }
                    case string s when s.StartsWith("If true:"):
                        {
                            iftrue = Int32.Parse(line.Split(" monkey ")[1]);

                            break;
                        }
                    case string s when s.StartsWith("If false:"):
                        {
                            iffalse = Int32.Parse(line.Split(" monkey ")[1]);

                            break;
                        }
                    case "":
                        {
                            Monkey.AllMonkeys.Add(new Monkey(monkeyNumber, items, operation, test, iftrue, iffalse));
                            monkeyNumber++;
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
            Monkey.AllMonkeys.Add(new Monkey(monkeyNumber, items, operation, test, iftrue, iffalse));
            while (rounds <= 10000)
            {
                foreach (Monkey monkey in Monkey.AllMonkeys)
                {
                    monkey.InspectItems();
                }
                rounds++;
            }
            answer = Monkey.AllMonkeys.OrderByDescending(m => m.timesInspectItem).Take(2).Select(m => m.timesInspectItem).Aggregate((m, x) => m * x);

            return answer;
        }
    }  
}
