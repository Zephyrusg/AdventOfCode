using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode._2022
{

    class CheckPairs {
        string PatternIsList = "[[].*[]]";
        string PatternNextList = "[[]((\\d*)|(\\d*,\\d*))[]]";
        string PatternIsDigit = "\\d{1,2}";

        readonly string FirstPair;
        readonly string SecondPair;

        public CheckPairs( string first, string second) { 
            this.FirstPair = first;
            this.SecondPair = second;
        }

        public bool WorkPairs() {
            bool result = true;
            
            bool FirstOneString = Regex.Match(this.FirstPair, this.PatternNextList).Length == this.FirstPair.Length;
            bool SecondOneString = Regex.Match(this.SecondPair, this.PatternNextList).Length == this.SecondPair.Length;

            string string1 = this.FirstPair;
            string string2 = this.SecondPair;
            
            if (FirstOneString || SecondOneString) {
                string1 = this.FirstPair.Substring(1, this.FirstPair.Length - 2);
                string2 = this.SecondPair.Substring(1, this.SecondPair.Length - 2);
                return CompareSimpleLists(string1, string2);
            }

            bool busy = true;

            while (busy)
            {
                Match NextListFirstString = Regex.Match(string1, "^" + this.PatternNextList);
                Match NextListSecondString = Regex.Match(string2, "^" + this.PatternNextList);
                Match FirstisDigit = Regex.Match(string1, "^" + this.PatternIsDigit);
                Match SecondisDigit = Regex.Match(string2, "^" + this.PatternIsDigit);

                if (isDigit.Success)
                {

                }
            }

            






            return result;
        }

        bool CompareSimpleLists(string first, string second) {
            List<int> FirstList = new List<int>();
            if (first.Length > 0)
            {
                FirstList = first.Split(",").Select(num => Int32.Parse(num)).ToList();
            }
            List<int> SecondList = new List<int>();

            if (second.Length > 0)
            {
                SecondList = second.Split(",").Select(num => Int32.Parse(num)).ToList();

            }
       
            for(int i= 0; i < FirstList.Count && i < SecondList.Count; i++ )
            {
                if (FirstList[i] > SecondList[i]) { 
                    return false;
                }
            }

            return SecondList.Count < FirstList.Count;
        }
    }
    internal class Day13
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
