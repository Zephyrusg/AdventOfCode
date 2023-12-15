using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class Y2023D15
    {
        static List<Box> Boxes = new List<Box>();
        class Box{
            public int number;
            public List<(string label, int focus)> Lenses = new List<(string label, int focus)> ();

            public Box(int number)
            {
                this.number = number;
            }
        }
        public static string line = File.ReadAllText(".\\2023\\Input\\inputDay15.txt");
        public int Part1() 
        {
            int answer = 0;
            int CurrentValue = 0;
            string[] steps = line.Split(',');
            foreach(string step in steps)
            {
                for(int i = 0; i < step.Length; i++)
                {
                    CurrentValue += Convert.ToInt32(step[i]);
                    CurrentValue *= 17;
                    CurrentValue %= 256;
                }
                answer += CurrentValue;
                CurrentValue = 0;
            }


            
            return answer;
        }

        public int Part2()
        {
            int answer = 0;
            string[] steps = line.Split(',');
            string label = "";
            int focus = 0;
            char Operator = '=';
            foreach (string step in steps)
            {

                if (step.Contains('='))
                {   
                    label = step.Split("=")[0];
                    focus = int.Parse(step.Split("=")[1]);
                    Operator = '=';
                }
                else
                {
                    Operator = '-';
                    label = step[..(step.Length - 1)];
                }
                int boxnumber = 0;
                for (int i = 0; i < label.Length; i++)
                {
                    boxnumber += Convert.ToInt32(label[i]);
                    boxnumber *= 17;
                    boxnumber %= 256;
                }
                
                switch(Operator)
                {
                    case '-':
                        {
                            Box ?Box = Boxes.Find(Box => Box.number == boxnumber);
                            if(Box != null)
                            {
                                int LensFound = Box.Lenses.FindIndex(l => l.label == label);
                                if (LensFound != -1)
                                {
                                    Box.Lenses.RemoveAt(LensFound);
                                }
                            }
                            break;
                        }
                        
                    case '=':
                        {
                            Box? Box = Boxes.Find(Box => Box.number == boxnumber);
                            if(Box == null)
                            {
                                Box = new(boxnumber);
                                Box.Lenses.Add((label, focus));
                                Boxes.Add(Box);
                            }
                            else
                            {   int LensFound = Box.Lenses.FindIndex(l => l.label == label);
                                if (LensFound == -1) { 
                                    Box.Lenses.Add((label,focus));
                                }
                                else
                                {
                                    
                                    Box.Lenses[LensFound] = (label, focus);
                                }
                                        
                            }


                            break;
                        }
                }
            }

            foreach(Box box in Boxes)
            {
                int boxvalue = box.number + 1;
                int lensvalue = 0;
                for(int i = 0; i < box.Lenses.Count; i++) { 
                    lensvalue = boxvalue * (i+ 1) * box.Lenses[i].focus;
                    answer += lensvalue;
                }
            }



            return answer;
        }

    }
}
