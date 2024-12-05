using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class Y2015D12
    {
        public static string Jsondata = File.ReadAllText(".\\2015\\Input\\inputDay12.txt");
        
        public int Part1() 
        {
            int answer = 0;

            string pattern = "-?\\d+";
            var Match = Regex.Matches(Jsondata, pattern);

            foreach (Match m in Match)
            {
                answer += int.Parse(m.ToString());
            }


            return answer;
        }

        public int Part2()
        {
            int answer = 0;

            var json = JsonObject.Parse(Jsondata);
            //int red = 0;

            //string pattern = "\\{[^{}]*\\}";

            //var Match = Regex.Matches(Json, pattern);

            //foreach (Match m in Match)
            //{
            //    if (m.Value.Contains("red")){
            //        string Digitpattern = "-?\\d+";
            //        var DigitMatch = Regex.Matches(m.Value, Digitpattern);
            //        foreach (Match d in DigitMatch)
            //        {
            //            red += int.Parse(d.ToString());
            //        }
            //    }
            //}

            //pattern = "-?\\d+";
            //Match = Regex.Matches(Json, pattern);

            //foreach (Match m in Match)
            //{
            //    answer += int.Parse(m.ToString());
            //}
            //if (Json is JsonObject) { }

            return answer; 
        }

    }
}
