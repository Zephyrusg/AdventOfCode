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
        string PatternIsList = "^[[].*[]]";
        string PatternNextList = "[[](\\d+)((\\d*,\\d*))*[]]"; // [[](\d+)((\d*,\d*))*[]] [[]((\\d*)|(\\d*,\\d*))*[]]
        string PatternIsDigit = "\\d{1,2}";
        string PatternBrokenList = "^[[](\\d)(,\\d+)*";
        string PatternTest = "^([[]+((\\d+,)*))|(([[])(\\d+,\\d+)[]]),*|[]]";

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
            
            if (FirstOneString && SecondOneString) {
                string1 = string1.Substring(1, string1.Length - 2);
                string2 = string2.Substring(1, string2.Length - 2);
                return CompareSimpleLists(string1, string2);
            }

            string1 = string1.Substring(1, string1.Length - 2);
            string2 = string2.Substring(1, string2.Length - 2);

            while (result && string1.Length > 0 && string2.Length > 0)
            {
                Match NextListFirstString = Regex.Match(string1, "^" + this.PatternNextList);
                Match NextListSecondString = Regex.Match(string2, "^" + this.PatternNextList);
                Match FirstisDigit = Regex.Match(string1, "^" + this.PatternIsDigit);
                Match SecondisDigit = Regex.Match(string2, "^" + this.PatternIsDigit);
                Match FirstBrokenList = Regex.Match(string1, "^" + this.PatternBrokenList);
                Match SecondBrokenList = Regex.Match(string2, "^" + this.PatternBrokenList);

                // Removes next comma
                if (string1[0] == ',') { 
                    string1 =  string1.Substring(1, string1.Length-1);
                }
                if (string2[0] == ',')
                {
                    string2 = string2.Substring(1, string2.Length-1);
                }
                if (string1.StartsWith("[]"))
                {
                    return true;
                }
                else if(string2.StartsWith("[]")) {
                    return false; 
                }

                else if (NextListFirstString.Success && NextListSecondString.Success)
                {
                    string1 = string1.Remove(NextListFirstString.Index, NextListFirstString.Length);
                    string2 = string2.Remove(NextListSecondString.Index, NextListSecondString.Length);
                    result = CompareSimpleLists(NextListFirstString.Value.Substring(1, NextListFirstString.Length - 2), NextListSecondString.Value.Substring(1, NextListSecondString.Length - 2));
                }
                else if (NextListFirstString.Success && SecondisDigit.Success)
                {
                    string1 = string1.Remove(NextListFirstString.Index, NextListFirstString.Length);
                    string2 = string2.Remove(SecondisDigit.Index, SecondisDigit.Length);
                    result = CompareSimpleLists(NextListFirstString.Value.Substring(1, NextListFirstString.Length - 2),SecondisDigit.Value);

                }
                else if(NextListFirstString.Success && SecondBrokenList.Success)
                    
                {
                    int SecondEndingBracket = FindClosingBracket(string2);
                    string2 = string2.Remove(SecondEndingBracket, 1);
                    string2 = string2.Remove(0, SecondBrokenList.Length);
                    string1 = string1.Remove(NextListFirstString.Index, NextListFirstString.Length);
                    result = CompareSimpleLists(NextListFirstString.Value.Substring(1,NextListFirstString.Length - 2), SecondBrokenList.Value.Substring(1, SecondBrokenList.Length - 1));
                }
                else if (FirstisDigit.Success && NextListSecondString.Success)
                {
                    string1 = string1.Remove(FirstisDigit.Index, FirstisDigit.Length);
                    string2 = string2.Remove(NextListSecondString.Index, NextListSecondString.Length);
                    result = CompareSimpleLists(FirstisDigit.Value, NextListSecondString.Value.Substring(1, NextListSecondString.Length - 2));

                }
                else if (FirstBrokenList.Success && NextListSecondString.Success)
                {
                    int FirstEndingBracket = FindClosingBracket(string1);
                    string1 = string1.Remove(FirstEndingBracket, 1);
                    string1 = string1.Remove(0,FirstBrokenList.Length);
                    string2 = string2.Remove(NextListSecondString.Index, NextListSecondString.Length);
                    result = CompareSimpleLists(FirstBrokenList.Value.Substring(1, FirstBrokenList.Length - 1), NextListSecondString.Value.Substring(1, NextListSecondString.Length - 2));

                }
                else if (string1[0] == '[' && string2[0] == '[')
                {
                    
                    int FirstEndingBracket = FindClosingBracket(string1);
                    int SecondEndingBracket = FindClosingBracket(string2);
                    string1 = string1.Remove(FirstEndingBracket, 1);
                    string1 = string1.Remove(0, 1);
                    string2 = string2.Remove(SecondEndingBracket, 1);
                    string2 = string2.Remove(0, 1);
                    
                  

                }
                else if (string1[0] == '[' && SecondisDigit.Success)
                {
                    int FirstEndingBracket = FindClosingBracket(string1);
                    string1 = string1.Remove(FirstEndingBracket, 1);
                    string1 = string1.Remove(0, 1);
                    
                }
                
                else if (FirstisDigit.Success && string2[0] == '[')
                {
                    int SecondEndingBracket = FindClosingBracket(string2);
                    string2 = string2.Remove(SecondEndingBracket, 1);
                    string2 = string2.Remove(0, 1);
                    
                }
                else if (FirstisDigit.Success && SecondisDigit.Success)
                {
                    string1 = string1.Remove(FirstisDigit.Index, FirstisDigit.Length);
                    string2 = string2.Remove(SecondisDigit.Index, SecondisDigit.Length);
                    result = Int32.Parse(FirstisDigit.Value) <= Int32.Parse(SecondisDigit.Value);

                }
            }

            if (string1.Length == 0 || string2.Length == 0) {
                if (string1.Length == string2.Length)
                {
                    return result;
                }
                else {
                    return string1.Length < string2.Length;
                }
                
            }

            return result;
        }

        int FindClosingBracket(string checkstring) { 
            int index;
            int CountOpen = 1;

            for (index = 1; index < checkstring.Length; index++)
            {
                if (checkstring[index] == '[')
                {
                    CountOpen++;
                }
                else if (checkstring[index] == ']') { 
                    CountOpen--;
                    if (CountOpen == 0) {
                        return index;
                    }
                }
            }
            return index;
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
                }else if((FirstList[i] < SecondList[i])) {
                    return true;     
                }
            }

            return FirstList.Count <= SecondList.Count;
        }
    }
    internal class Day13
    {
        
        static string Path = ".\\2022\\Input\\InputDay13.txt";
        static string Data = File.ReadAllText(Path);
        public static int Part1()
        {
            
            int answer = 0;

            string[] Pairs = Data.Split("\r\n\r\n");
            int index = 1;
            foreach (string Pair in Pairs) { 
                
                CheckPairs Checkpair = new CheckPairs(Pair.Split("\r\n")[0], Pair.Split("\r\n")[1]);
                if (Checkpair.WorkPairs()) { 
                    answer= answer + index; 
                }
                index++;
            }

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
