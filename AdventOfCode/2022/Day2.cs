using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022
{

    internal class Day2
    {

        public static void FillCheatSheet() {
            CheatSheet.Add("A X", 4);
            CheatSheet.Add("A Y", 8);
            CheatSheet.Add("A Z", 3);
            CheatSheet.Add("B Y", 5);
            CheatSheet.Add("B Z", 9);
            CheatSheet.Add("B X", 1);
            CheatSheet.Add("C Z", 6);
            CheatSheet.Add("C X", 7);
            CheatSheet.Add("C Y", 2);
        }
        public static Char GetNeededResult(Tuple<Char, Char> Play)
        {
            Char NeededResult = 'X';


            switch (Play.Item2)
            {
                case 'X':

                    if (Play.Item1 == 'A')
                    {
                        NeededResult = 'Z';
                    }
                    else if (Play.Item1 == 'B')
                    {
                        NeededResult = 'X';
                    }
                    else {
                        NeededResult = 'Y';
                    }
                        
                    break;

                case 'Y':

                    if (Play.Item1 == 'A')
                    {
                        NeededResult = 'X';
                    }
                    else if (Play.Item1 == 'B')
                    {
                        NeededResult = 'Y';
                    }
                    else
                    {
                        NeededResult = 'Z';
                    }

                    break;

                case 'Z':
                    if (Play.Item1 == 'A')
                    {
                        NeededResult = 'Y';
                    }
                    else if (Play.Item1 == 'B')
                    {
                        NeededResult = 'Z';
                    }
                    else
                    {
                        NeededResult = 'X';
                    }
                    break;

            }

            return NeededResult;
        }
        static Dictionary<string, int> CheatSheet = new Dictionary<string, int>();


        static string Path = ".\\2022\\Input\\InputDay2.txt";

        public static int Part1()
        {
            int answer = 0;

            FillCheatSheet();


            string[] Data = File.ReadAllLines(Path);

            foreach (string Line in Data)
            {
            
                answer += CheatSheet[Line];
            }

            return answer;
        }


        public static int Part2()
        {
            
            int answer = 0;
            string[] Data = File.ReadAllLines(Path);


            foreach (string Line in Data)
            {
                Char[] Temp = new Char[] { };
                Tuple<Char, Char> TestHand = Tuple.Create(Line[0], Line[2]);
                Temp = Line.ToCharArray();
                Temp[2] = GetNeededResult(TestHand);
                string Result = new string(Temp);
                answer += CheatSheet[Result];
            }

            return answer;
        }

    }

}
