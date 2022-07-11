﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2015
{
    internal class Day1
    {
        public static int Day1A() 
        {
            string text = File.ReadAllText(".\\2015\\Input\\inputDay1.txt");
            int level = 0;

            for (int i = 0; i < text.Length; i++)
            {
                char Char = text[i];
                if (Char == '(') { level++; }
                else if (Char == ')') { level--; }
            }
            return level;
        }

        public static int Day1B()
        {

            string text = File.ReadAllText(".\\2015\\Input\\inputDay1.txt");
            int level = 0;

            for (int i = 0; i < text.Length; i++)
            {
                char Char = text[i];
                if (Char == '(') { level++; }
                else if (Char == ')') { level--; }

                if (level == -1) { 
                    return i + 1;
                }
            }
            return level;
        }

    }
}
