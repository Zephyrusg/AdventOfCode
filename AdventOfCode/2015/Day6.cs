using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace AdventOfCode._2015
{
    internal class Day6
    {

        public static int Day6A()
        {

            int lightsOn = 0;
            int[,] Lightgrid = new int[1000, 1000];
            string[] textInput = File.ReadAllLines(".\\2015\\Input\\inputDay6.txt");
            foreach (string line in textInput)
            {
                string? action = null;
                string instruction = line;
                if (line.StartsWith("turn on"))
                {
                    action = "on";
                    instruction = line.Replace("turn on ", "");

                }
                else if (line.StartsWith("turn off"))
                {
                    action = "off";
                    instruction = line.Replace("turn off ", "");
                }
                else
                {
                    action = "toggle";
                    instruction = line.Replace("toggle ", "");
                }
                instruction = instruction.Replace("through ", "");
                string[] parts = instruction.Split(' ');
                int[] left = parts[0].Split(',').Select(num => Int32.Parse(num)).ToArray();
                int[] right = parts[1].Split(',').Select(num => Int32.Parse(num)).ToArray();
                for (int x = left[0]; x <= right[0]; x++)
                {
                    for (int y = left[1]; y <= right[1]; y++)
                    {
                        switch (action)
                        {
                            case "on":
                                Lightgrid[x, y] = 1;
                                break;
                            case "off":
                                Lightgrid[x, y] = 0;
                                break;
                            case "toggle":
                                if (Lightgrid[x, y] == 1)
                                {
                                    Lightgrid[x, y] = 0;
                                }
                                else
                                {
                                    Lightgrid[x, y] = 1;
                                }
                                break;
                        }
                    }

                }
            }
            lightsOn = Lightgrid.Cast<int>().Sum();
            return lightsOn;
        }
        public static int Day6B()
        {

            int lightsOn = 0;
            int[,] Lightgrid = new int[1000, 1000];
            string[] textInput = File.ReadAllLines(".\\2015\\Input\\inputDay6.txt");
            foreach (string line in textInput)
            {
                string? action = null;
                string instruction = line;
                if (line.StartsWith("turn on"))
                {
                    action = "on";
                    instruction = line.Replace("turn on ", "");

                }
                else if (line.StartsWith("turn off"))
                {
                    action = "off";
                    instruction = line.Replace("turn off ", "");
                }
                else
                {
                    action = "toggle";
                    instruction = line.Replace("toggle ", "");
                }
                instruction = instruction.Replace("through ", "");
                string[] parts = instruction.Split(' ');
                int[] left = parts[0].Split(',').Select(num => Int32.Parse(num)).ToArray();
                int[] right = parts[1].Split(',').Select(num => Int32.Parse(num)).ToArray();
                for (int x = left[0]; x <= right[0]; x++)
                {
                    for (int y = left[1]; y <= right[1]; y++)
                    {
                        switch (action)
                        {
                            case "on":
                                Lightgrid[x, y] += 1;
                                break;
                            case "off":
                                if (Lightgrid[x, y] > 0)
                                    Lightgrid[x, y] -= 1;
                                break;
                            case "toggle":
                                Lightgrid[x, y] += 2;
                                break;
                        }
                    }

                }
            }
            lightsOn = Lightgrid.Cast<int>().Sum();
            return lightsOn;

        }
    }
}