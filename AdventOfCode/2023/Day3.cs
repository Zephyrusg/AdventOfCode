using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace AdventOfCode
{
    internal class Y2023D3

    {
        static List<Digit> Digits = new List<Digit>();
        public class Digit
        {
            public int value;
            public int linenr;
            public int length;
            public int index;
            public int end;

            public Digit(int value, int linenr, int index, int length ) {
                this.value = value;
                this.linenr = linenr;
                this.length = length;
                this.index = index;

                this.end = index + length -1;
            }
        }

        public static string[] lines = File.ReadAllLines(".\\2023\\Input\\inputDay3.txt");
        public bool CheckNumber( int linenr, int index, int length) {

            string Pattern = "[^.0-9]";

            for (int x = 0; x < length; x++) {

                //Left up
                if (index+x != 0 && linenr > 0) { 
                    string testchar = lines[linenr -1][index+x-1].ToString();
                    var test = Regex.Match(testchar, Pattern);
                    if (test.Success) {
                        return true;
                    }
                }
                //Left
                if (index+x != 0)
                {
                    string testchar = lines[linenr][index + x - 1].ToString();
                    var test = Regex.Match(testchar, Pattern);
                    if (test.Success)
                    {
                        return true;
                    }
                }
                //LeftDown
                if (index+x != 0 && linenr + 1 < lines.Count())
                {
                    string testchar = lines[linenr + 1][index + x - 1].ToString();
                    var test = Regex.Match(testchar, Pattern);
                    if (test.Success)
                    {
                        return true;
                    }
                }
                //Down
                if (index + x < lines[linenr].Length && linenr + 1 < lines.Count())
                {
                    string testchar = lines[linenr + 1][index + x].ToString();
                    var test = Regex.Match(testchar, Pattern);
                    if (test.Success)
                    {
                        return true;
                    }
                }
                //RightDown
                if (index+x + 1 < lines[linenr].Length && linenr + 1 < lines.Count())
                {
                    string testchar = lines[linenr + 1][index + x + 1].ToString();
                    var test = Regex.Match(testchar, Pattern);
                    if (test.Success)
                    {
                        return true;
                    }
                }//Right
                if (index + x + 1 < lines[linenr].Length)
                {
                    string testchar = lines[linenr][index + x + 1].ToString();
                    var test = Regex.Match(testchar, Pattern);
                    if (test.Success)
                    {
                        return true;
                    }
                }//Rightup
                if (index + x + 1 < lines[linenr].Length && linenr > 0)
                {
                    string testchar = lines[linenr - 1][index + x + 1].ToString();
                    var test = Regex.Match(testchar, Pattern);
                    if (test.Success)
                    {
                        return true;
                    }
                }//up
                if (linenr > 0)
                {
                    string testchar = lines[linenr - 1][index + x].ToString();
                    var test = Regex.Match(testchar, Pattern);
                    if (test.Success)
                    {
                        return true;
                    }
                }

            }
            return false;
        }
        public int Checkgear(int linenr, int index) {

            List<Digit> PossibleMatches = new List<Digit>();
            PossibleMatches.AddRange(Digits.Where(x => x.linenr == linenr));
            PossibleMatches.AddRange(Digits.Where(x => x.linenr == linenr-1));
            PossibleMatches.AddRange(Digits.Where(x => x.linenr == linenr+1));
            int MatchedDigits = 0;
            Digit ?First = null;

            //Left up
            if (index != 0 && linenr > 0)
            {
                var Match = PossibleMatches.Find(x => x.linenr == linenr - 1 && index - 1  >= x.index && index -1 <= x.end);
                if (Match != null) {
                    if (MatchedDigits == 0)
                    {
                        First = Match;
                        MatchedDigits = 1;
                    }
                    else
                    {
                        Digit test = Match;
                        if (test != First)
                        {
                            return First.value * test.value;
                        }
                    }
                }
            }
            //Left
            if (index != 0)
            {
                var Match = PossibleMatches.Find(x => x.linenr == linenr && index - 1 >= x.index && index - 1 <= x.end);
                if (Match != null)
                {
                    if (MatchedDigits == 0)
                    {
                        First = Match;
                        MatchedDigits = 1;
                    }
                    else
                    {
                        Digit test = Match;
                        if (test != First)
                        {
                            return First.value * test.value;
                        }
                    }
                }
            }
            //LeftDown
            if (index != 0 && linenr + 1 < lines.Count())
            {
                var Match = PossibleMatches.Find(x => x.linenr == linenr + 1 && index - 1 >= x.index && index - 1 <= x.end);
                if (Match != null)
                {
                    if (MatchedDigits == 0)
                    {
                        First = Match;
                        MatchedDigits = 1;
                    }
                    else
                    {
                        Digit test = Match;
                        if (test != First)
                        {
                            return First.value * test.value;
                        }
                    }
                }
            }
            //Down
            if (linenr + 1 < lines.Count())
            {
                var Match = PossibleMatches.Find(x => x.linenr == linenr + 1 && index >= x.index && index <= x.end);
                if (Match != null)
                {
                    if (MatchedDigits == 0)
                    {
                        First = Match;
                        MatchedDigits = 1;
                    }
                    else
                    {
                        Digit test = Match;
                        if (test != First)
                        {
                            return First.value * test.value;
                        }
                    }
                }
            }
            //RightDown
            if (index + 1 < lines[linenr].Length && linenr + 1 < lines.Count())
            {
                var Match = PossibleMatches.Find(x => x.linenr == linenr + 1 && index + 1 >= x.index && index + 1 <= x.end);
                if (Match != null)
                {
                    if (MatchedDigits == 0)
                    {
                        First = Match;
                        MatchedDigits = 1;
                    }
                    else
                    {
                        Digit test = Match;
                        if (test != First)
                        {
                            return First.value * test.value;
                        }
                    }
                }
            }//Right
            if (index + 1 < lines[linenr].Length)
            {
                var Match = PossibleMatches.Find(x => x.linenr == linenr && index + 1 >= x.index && index + 1 <= x.end);
                if (Match != null)
                {
                    if (MatchedDigits == 0)
                    {
                        First = Match;
                        MatchedDigits = 1;
                    }
                    else
                    {
                        Digit test = Match;
                        if (test != First)
                        {
                            return First.value * test.value;
                        }
                    }
                }
            }//Rightup
            if (index + 1 < lines[linenr].Length && linenr > 0)
            {
                var Match = PossibleMatches.Find(x => x.linenr == linenr - 1 && index + 1 >= x.index && index + 1 <= x.end);
                
                if (Match != null)
                {
                    if (MatchedDigits == 0)
                    {
                        First = Match;
                        MatchedDigits = 1;
                    }
                    else
                    {
                        Digit test = Match;
                        if (test != First)
                        {
                            return First.value * test.value;
                        }
                    }
                }
            }//up
            if (linenr > 0)
            {
                var Match = PossibleMatches.Find(x => x.linenr == linenr - 1 && index >= x.index && index <= x.end);
                
                if (Match != null)
                {
                    if (MatchedDigits == 0)
                    {
                        First = Match;
                        MatchedDigits = 1;
                    }
                    else
                    {
                        Digit test = Match;
                        if (test != First)
                        {
                            return First.value * test.value;
                        }
                    }
                }
            }
            return 0;
        }
        public int Part1() 
        {
            string Pattern = "\\d+";
            int answer = 0;
            int x = 0;

            foreach (string line in lines) { 
                var Matches = Regex.Matches(line, Pattern);
                foreach (Match m in Matches)
                {
                    if (CheckNumber(x, m.Index, m.Length)) {
                        answer += int.Parse(m.Value);
                    }
                }
                x++;
            }

            return answer;
        }

        public int Part2()
        {
            int answer = 0;
            string Pattern = "\\d+";
            int x = 0;

            foreach (string line in lines)
            {
                var Matches = Regex.Matches(line, Pattern);
                foreach (Match m in Matches)
                {
                    Digits.Add(new(int.Parse(m.Value),x, m.Index,m.Length));
                }
                x++;
            }
            x = 0;
            Pattern = "\\*";
            foreach (string line in lines) {
                var Matches = Regex.Matches(line, Pattern);
                foreach (Match m in Matches)
                {
                    answer += Checkgear(x,m.Index);
                }
                x++;
            }



            return answer;
        }

    }
}
