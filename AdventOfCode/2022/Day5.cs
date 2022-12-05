using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022
{
    internal class Day5
    {
        static string Path = ".\\2022\\Input\\InputDay5.txt";
        static string CratePath = ".\\2022\\Input\\InputDay5Crates.txt";

        class Crate
        {
            public Char value;
            static public List<Stack<Crate>> CrateList = new List<Stack<Crate>>();
            public Crate(Char value)
            {
                this.value = value;
            }
            public static void MoveCrates(int count, int from, int to)
            {

                for (int i = 0; i < count; i++)
                {

                    CrateList[to].Push(CrateList[from].Pop());
                }

            }
            public static void MoveContainer(int count, int from, int to)
            {
                Stack<Crate> temp = new Stack<Crate>();
                for (int i = 0; i < count; i++)
                {

                    temp.Push(CrateList[from].Pop());


                }
                for (int i = 0; i < count; i++)
                {
                    CrateList[to].Push(temp.Pop());
                }
            }
        }
        public static string Part1()
        {
            string answer = "";
            
            string[] Data = File.ReadAllLines(CratePath);
            string[] Instructions = File.ReadAllLines(Path);

            foreach (string line in Data)
            {
                IEnumerable<char> LineData = line.Split(",").SelectMany(item=>item.ToCharArray());
                Stack<Crate> CrateQueue= new Stack<Crate>();
                foreach (char c in LineData)
                {
                    CrateQueue.Push(new Crate(c));
                }
                Crate.CrateList.Add(CrateQueue);
            }

            foreach(string line in Instructions)
            {
                string[] instruction = line.Replace(" from ", ",").Replace("move ", "").Replace(" to ", ",").Split(",");
                int count = Int32.Parse(instruction[0]);
                int from = Int32.Parse(instruction[1]) -1;
                int to = Int32.Parse(instruction[2]) -1;
                Crate.MoveCrates(count, from, to);
                 

            }
            
            foreach (Stack<Crate> Crateline in Crate.CrateList)
            {
                answer += Crateline.Peek().value;
                
            }



            return answer;
        }

        public static string Part2()
        {
            string answer = "";
            Crate.CrateList = new List<Stack<Crate>>();
            string[] Data = File.ReadAllLines(CratePath);
            string[] Instructions = File.ReadAllLines(Path);

            foreach (string line in Data)
            {
                IEnumerable<char> LineData = line.Split(",").SelectMany(item => item.ToCharArray());
                Stack<Crate> CrateQueue = new Stack<Crate>();
                foreach (char c in LineData)
                {
                    CrateQueue.Push(new Crate(c));
                }
                Crate.CrateList.Add(CrateQueue);
            }

            foreach (string line in Instructions)
            {
                string[] instruction = line.Replace(" from ", ",").Replace("move ", "").Replace(" to ", ",").Split(",");
                int count = Int32.Parse(instruction[0]);
                int from = Int32.Parse(instruction[1]) - 1;
                int to = Int32.Parse(instruction[2]) - 1;
                Crate.MoveContainer(count, from, to);


            }

            foreach (Stack<Crate> Crateline in Crate.CrateList)
            {
                answer += Crateline.Peek().value;

            }



            return answer;
        }
    }
}
