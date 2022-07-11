﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2015
{
    internal class Day2
    {

        public static int Day2A() 
        {
            int totalWrappingPaper = 0;
            string[] textInput = File.ReadAllLines(".\\2015\\Input\\inputDay2.txt");
            foreach (string line in textInput) 
            {
                int neededWrappingPaper = 0;
                int[] measurements = line.Split('x').Select(num => Int32.Parse(num)).ToArray();
                Array.Sort(measurements);
                int l = measurements[0];
                int w = measurements[1];
                int h = measurements[2];
                neededWrappingPaper = ((2 * l * w) + (2 * w * h) + (2 * h * l) + (l * w));
                totalWrappingPaper += neededWrappingPaper;

            }

            return totalWrappingPaper;
        }

        public static int Day2B()
        {
            int totalWrappingPaper = 0;
            string[] textInput = File.ReadAllLines(".\\2015\\Input\\inputDay2.txt");
            foreach (string line in textInput)
            {
                int neededWrappingPaper = 0;
                int[] measurements = line.Split('x').Select(num => Int32.Parse(num)).ToArray();
                Array.Sort(measurements);
                int l = measurements[0];
                int w = measurements[1];
                int h = measurements[2];
                neededWrappingPaper = ((2 * l + 2 * w) + (l * w * h));
                totalWrappingPaper += neededWrappingPaper;

            }

            return totalWrappingPaper;
        }
    }
}
