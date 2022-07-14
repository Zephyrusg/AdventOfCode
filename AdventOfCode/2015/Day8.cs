using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace AdventOfCode._2015
{

    
    internal class Day8
    {
        public static void Day8A()
        {
            string[] textInput = File.ReadAllLines(".\\2015\\Input\\inputDay8.txt");
            int TotalChars = 0;
            int TotalinMem = 0;
            int encodedChars = 0;
            string proces;
            string proces2;
            foreach (string line in textInput)
            {
                TotalChars += line.Length;
                proces = line.Trim('"');
                proces = proces.Replace("\\\"", "A");
                proces = proces.Replace("\\\\", "B");
                proces = Regex.Replace(proces, "\\\\x[a-f0-9]{2}", "C");
                TotalinMem += proces.Length;

                proces2 = line.Replace("\"", "AA");
                proces2 = proces2.Replace("\\", "BB");
                encodedChars += proces2.Length + 2;

                

            }
            Console.WriteLine("8A Answer: " + (TotalChars - TotalinMem));
            Console.WriteLine("8B Answer: " + (encodedChars - TotalChars ));
        }          
     
    }

    
}