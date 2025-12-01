using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdventOfCode
{
    internal class Y2025D1
    {
        public static string[] Lines = File.ReadAllLines(".\\2025\\Input\\inputDay1.txt");
        
        public int Part1() 
        {
            int dialNumber = 50;
            int counter = 0;
            foreach(string line in Lines) { 
            
                char turn = line[0];
                int distance = int.Parse(line.Substring(1));

                switch (turn)
                {
                    case 'L':

                        if(dialNumber - distance < 0)
                        {
                            dialNumber = ((dialNumber - distance) % 100 + 100) % 100;
                        }
                        else
                        {
                            dialNumber -= distance;
                        }
                            break;

                    case 'R':
                        if(dialNumber + distance > 99)
                        {
                            dialNumber = (dialNumber + distance) % 100;
                        }
                        else
                        {
                            dialNumber += distance;
                        }
                            break;

                }

                if (dialNumber == 0)
                {
                    counter++;
                }

            }
            int answer = counter;

            return answer;
        }

        public int Part2()
        {

            int dialNumber = 50;
            int counter = 0;
            foreach (string line in Lines)
            {

                char turn = line[0];
                int distance = int.Parse(line.Substring(1));

                switch (turn)
                {
                    case 'L':

                        int startL = dialNumber;
                 
                        int firstHit = startL == 0 ? 100 : startL;

                        if (distance >= firstHit)
                        {
                            counter += (distance - firstHit) / 100 + 1;
                        }

                        dialNumber = (int)(((startL - (long)distance) % 100 + 100) % 100); 
                        
                        break;

                    case 'R':
                        int startR = dialNumber;
                        long total = startR + (long)distance;
        
                        counter += (int)(total / 100);

                        dialNumber = (int)(total % 100);
                        break;

                }

            }
            int answer = counter;

            return answer;
        }

    }
}
