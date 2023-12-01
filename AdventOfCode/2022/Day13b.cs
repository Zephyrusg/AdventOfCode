using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022
{

    class Packet {

        string PacketLine;
        
        List<SubLists> SubList = new List<SubLists>();
        public Packet(string Line) { 
            this.PacketLine = Line;
         
        }
        void CreateSubList() { 
            
        }

    }

    class SubLists {
        List<SubLists> SubList = new List<SubLists>();
        string Subline;
        public SubLists(string Line)
        {
            this.Subline = Line;
        }

        public void CreateSubList()
        {

        }
    }

    internal class Day13b
    {
        static string Path = ".\\2022\\Input\\InputDay13.txt";
        static string[] Data = File.ReadAllLines(Path);
        public static int Part1()
        {
            int answer = 0;

            return answer;
        }

   
        public static int Part2()
        {
            string[] Data = File.ReadAllLines(Path);
            int answer = 0;

            return answer;
        }
    }
}
