using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class Y2017D9
    {
        static public int GarbageCounter = 0;
        public static string Text = File.ReadAllText(".\\2017\\Input\\inputDay9.txt");
        public int Part1() 
        {
            int answer = 0;
            int Depth = 0;
            bool Garbage = false;
            for (int x = 0; x < Text.Length; x++) { 
                char c = Text[x];
                if (c == '!') {
                    x++;
                    continue;
                }

                switch (c)
                {
                    case '{':
                        if (Garbage == false)
                        {
                            Depth++;
                            continue;
                        }
                        break;
                    case '}':
                        if (Garbage == false)
                        {
                            answer += Depth;

                            Depth--;

                            continue;
                        }
                        break;

                    case '<':
                        if (Garbage == false)
                        {
                            Garbage = true;
                            continue;
                        }
                        break;
                    case '>':
                       
                        Garbage = false;
                        continue;
                }
                if (Garbage == true)
                {
                    GarbageCounter++;
                }
            }

            return answer;
        }

        public int Part2()
        {
            int answer = 0;

            answer = GarbageCounter;


            return answer;
        }

    }
}
