using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022
{
    internal class Day9
    {
        static string Path = ".\\2022\\Input\\InputDay9.txt";

        static HashSet<Tuple<int,int>> VisitedPoints= new HashSet<Tuple<int,int>>();
        static public Tuple<int, int> Head = new Tuple<int, int>(0, 0);
        static public Tuple<int, int> Tail = new Tuple<int, int>(0, 0);

        class Rope
        {
            public int id = 0;
            public Tuple<int, int> Position = new Tuple<int, int>(0, 0);

            public Rope(int id)
            {
                this.id = id;
            }
        }
        static void MoveHead(string Direction , int number) {


            while (number--> 0)
            {
              
                switch (Direction)
                {
                    case "L":

                        Head = new Tuple<int, int>(Head.Item1 - 1, Head.Item2);

                        // Same Row Follow
                        if (Tail.Item1 - Head.Item1 == 2 && Tail.Item2 == Head.Item2)
                        {
                            Tail = new Tuple<int, int>(Tail.Item1 - 1, Tail.Item2);
                        }
                        else if (Math.Abs(Head.Item1 - Tail.Item1) > 1)
                        {
                            if (Head.Item2 < Tail.Item2)
                            {
                                Tail = new Tuple<int, int>(Tail.Item1 - 1, Tail.Item2 - 1);
                            }
                            else
                            {
                                Tail = new Tuple<int, int>(Tail.Item1 - 1, Tail.Item2 + 1);
                            }
                        }

                        break;

                    case "R":

                        Head = new Tuple<int, int>(Head.Item1 + 1, Head.Item2);
                        if (Head.Item1 - Tail.Item1 == 2 && Tail.Item2 == Head.Item2)
                        {
                            Tail = new Tuple<int, int>(Tail.Item1 + 1, Tail.Item2);
                        }
                        else if (Math.Abs(Head.Item1 - Tail.Item1) > 1)
                        {
                            if (Head.Item2 < Tail.Item2)
                            {
                                Tail = new Tuple<int, int>(Tail.Item1 + 1, Tail.Item2 - 1);
                            }
                            else
                            {
                                Tail = new Tuple<int, int>(Tail.Item1 + 1, Tail.Item2 + 1);
                            }
                        }

                        break;
                    case "U":

                        Head = new Tuple<int, int>(Head.Item1, Head.Item2 + 1);

                        if (Head.Item2 - Tail.Item2 == 2 && Tail.Item1 == Head.Item1)
                        {
                            Tail = new Tuple<int, int>(Tail.Item1, Tail.Item2 + 1);
                        }
                        else if (Math.Abs(Head.Item2 - Tail.Item2) > 1)
                        {
                            if (Head.Item1 < Tail.Item1)
                            {
                                Tail = new Tuple<int, int>(Tail.Item1 - 1, Tail.Item2 + 1);
                            }
                            else
                            {
                                Tail = new Tuple<int, int>(Tail.Item1 + 1, Tail.Item2 + 1);
                            }
                        }

                        break;
                    case "D":

                        Head = new Tuple<int, int>(Head.Item1, Head.Item2 - 1);

                        if (Tail.Item2 - Head.Item2 == 2 && Tail.Item1 == Head.Item1)
                        {
                            Tail = new Tuple<int, int>(Tail.Item1, Tail.Item2 - 1);
                        }
                        else if (Math.Abs(Head.Item2 - Tail.Item2) > 1)
                        {
                            if (Head.Item1 < Tail.Item1)
                            {
                                Tail = new Tuple<int, int>(Tail.Item1 - 1, Tail.Item2 - 1);
                            }
                            else
                            {
                                Tail = new Tuple<int, int>(Tail.Item1 + 1, Tail.Item2 - 1);
                            }
                        }



                        break;
                }
       
                    if (!VisitedPoints.Contains(Tail))
                    {
                        VisitedPoints.Add(Tail);
                    }
       
            }

        }
        static void MoveRopes(string Direction, int number, List<Rope> Ropes, int CheckId)
        {


            while (number-- > 0)
            {
                foreach (Rope CurrentRope in Ropes)
                {
                    if (CurrentRope.id == 0)
                    {
                        switch (Direction)
                        {
                            case "L":
                                CurrentRope.Position = new Tuple<int, int>(CurrentRope.Position.Item1 - 1, CurrentRope.Position.Item2);
                                break;
                            case "R":
                                CurrentRope.Position = new Tuple<int, int>(CurrentRope.Position.Item1 + 1, CurrentRope.Position.Item2);
                                break;
                            case "U":
                                CurrentRope.Position = new Tuple<int, int>(CurrentRope.Position.Item1, CurrentRope.Position.Item2 + 1);
                                break;
                                
                            case "D":
                                CurrentRope.Position = new Tuple<int, int>(CurrentRope.Position.Item1, CurrentRope.Position.Item2 - 1);
                                break;

                        }

                        Head = CurrentRope.Position;
                    }
                    else
                    {

                        if (CurrentRope.Position.Item1 - Head.Item1 == 2 && CurrentRope.Position.Item2 == Head.Item2)
                        {
                            //Left Follow
                            CurrentRope.Position = new Tuple<int, int>(CurrentRope.Position.Item1 - 1, CurrentRope.Position.Item2);
                        }

                        else if ((Head.Item1 - CurrentRope.Position.Item1 == 2 && CurrentRope.Position.Item2 == Head.Item2))
                        {
                            // Right Folow
                            CurrentRope.Position = new Tuple<int, int>(CurrentRope.Position.Item1 + 1, CurrentRope.Position.Item2);
                        }
                        else if (Head.Item2 - CurrentRope.Position.Item2 == 2 && CurrentRope.Position.Item1 == Head.Item1)
                        {
                            // Up Follow 
                            CurrentRope.Position = new Tuple<int, int>(CurrentRope.Position.Item1, CurrentRope.Position.Item2 + 1);
                        }
                        else if (CurrentRope.Position.Item2 - Head.Item2 == 2 && CurrentRope.Position.Item1 == Head.Item1) {
                            // Down Follow
                            CurrentRope.Position = new Tuple<int, int>(CurrentRope.Position.Item1, CurrentRope.Position.Item2 - 1);
                        }
                        else if (
                            Head.Item1 - CurrentRope.Position.Item1 == -1 && Head.Item2 - CurrentRope.Position.Item2 == 2 ||
                            Head.Item1 - CurrentRope.Position.Item1 == -2 && Head.Item2 - CurrentRope.Position.Item2 == 1 ||
                            Head.Item1 - CurrentRope.Position.Item1 == -2 && Head.Item2 - CurrentRope.Position.Item2 == 2
                            )
                        {
                            // Left Up
                            CurrentRope.Position = new Tuple<int, int>(CurrentRope.Position.Item1 - 1, CurrentRope.Position.Item2 + 1);
                        }
                        else if (
                            Head.Item1 - CurrentRope.Position.Item1 == 2 && Head.Item2 - CurrentRope.Position.Item2 == 1 ||
                            Head.Item1 - CurrentRope.Position.Item1 == 1 && Head.Item2 - CurrentRope.Position.Item2 == 2 ||
                            Head.Item1 - CurrentRope.Position.Item1 == 2 && Head.Item2 - CurrentRope.Position.Item2 == 2
                            )
                        {   // Right Up
                            CurrentRope.Position = new Tuple<int, int>(CurrentRope.Position.Item1 + 1, CurrentRope.Position.Item2 + 1);
                        }
                        else if (
                            Head.Item1 - CurrentRope.Position.Item1 == -2 && Head.Item2 - CurrentRope.Position.Item2 == -1 ||
                            Head.Item1 - CurrentRope.Position.Item1 == -1 && Head.Item2 - CurrentRope.Position.Item2 == -2 ||
                            Head.Item1 - CurrentRope.Position.Item1 == -2 && Head.Item2 - CurrentRope.Position.Item2 == -2
                            )
                        {   // Left Down
                            CurrentRope.Position = new Tuple<int, int>(CurrentRope.Position.Item1 - 1, CurrentRope.Position.Item2 - 1);
                        }
                        else if (
                            Head.Item1 - CurrentRope.Position.Item1 == + 2 && Head.Item2 - CurrentRope.Position.Item2 == - 1 ||
                            Head.Item1 - CurrentRope.Position.Item1 == + 1 && Head.Item2 - CurrentRope.Position.Item2 == - 2 ||
                            Head.Item1 - CurrentRope.Position.Item1 == +2 && Head.Item2 - CurrentRope.Position.Item2 == -2
                            )
                        {   // Right Down
                            CurrentRope.Position = new Tuple<int, int>(CurrentRope.Position.Item1 + 1, CurrentRope.Position.Item2 - 1);
                        }


                        //switch (Direction)
                        //{
                        //    case "L":

                        //        // Same Row Follow
                        //        if (CurrentRope.Position.Item1 - Head.Item1 == 2 && CurrentRope.Position.Item2 == Head.Item2)
                        //        {
                        //            CurrentRope.Position = new Tuple<int, int>(CurrentRope.Position.Item1 - 1, CurrentRope.Position.Item2);
                        //        }
                        //        else if (Math.Abs(Head.Item1 - CurrentRope.Position.Item1) > 1)
                        //        {
                        //            if (Head.Item2 < CurrentRope.Position.Item2)
                        //            {
                        //                CurrentRope.Position = new Tuple<int, int>(CurrentRope.Position.Item1 - 1, CurrentRope.Position.Item2 - 1);
                        //            }
                        //            else
                        //            {
                        //                CurrentRope.Position = new Tuple<int, int>(CurrentRope.Position.Item1 - 1, CurrentRope.Position.Item2 + 1);
                        //            }
                        //        }
                        //        else if (Math.Abs(Head.Item2 - CurrentRope.Position.Item2) > 1)
                        //        {
                        //            if (Head.Item2 < CurrentRope.Position.Item2)
                        //            {
                        //                CurrentRope.Position = new Tuple<int, int>(CurrentRope.Position.Item1 - 1, CurrentRope.Position.Item2 - 1);
                        //            }
                        //            else
                        //            {
                        //                CurrentRope.Position = new Tuple<int, int>(CurrentRope.Position.Item1 - 1, CurrentRope.Position.Item2 + 1);
                        //            }
                        //        }

                        //        break;

                        //    case "R":

                        //        if (Head.Item1 - CurrentRope.Position.Item1 == 2 && CurrentRope.Position.Item2 == Head.Item2)
                        //        {
                        //            CurrentRope.Position = new Tuple<int, int>(CurrentRope.Position.Item1 + 1, CurrentRope.Position.Item2);
                        //        }
                        //        else if (Math.Abs(Head.Item1 - CurrentRope.Position.Item1) > 1)
                        //        {
                        //            if (Head.Item2 < CurrentRope.Position.Item2)
                        //            {
                        //                CurrentRope.Position = new Tuple<int, int>(CurrentRope.Position.Item1 + 1, CurrentRope.Position.Item2 - 1);
                        //            }
                        //            else
                        //            {
                        //                CurrentRope.Position = new Tuple<int, int>(CurrentRope.Position.Item1 + 1, CurrentRope.Position.Item2 + 1);
                        //            }
                        //        }
                        //        else if (Math.Abs(Head.Item2 - CurrentRope.Position.Item2) > 1)
                        //        {
                        //            if (Head.Item2 < CurrentRope.Position.Item2)
                        //            {
                        //                CurrentRope.Position = new Tuple<int, int>(CurrentRope.Position.Item1 + 1, CurrentRope.Position.Item2 - 1);
                        //            }
                        //            else
                        //            {
                        //                CurrentRope.Position = new Tuple<int, int>(CurrentRope.Position.Item1 + 1, CurrentRope.Position.Item2 + 1);
                        //            }
                        //        }

                        //        break;
                        //    case "U":

                        //        if (Head.Item2 - CurrentRope.Position.Item2 == 2 && CurrentRope.Position.Item1 == Head.Item1)
                        //        {
                        //            CurrentRope.Position = new Tuple<int, int>(CurrentRope.Position.Item1, CurrentRope.Position.Item2 + 1);
                        //        }
                        //        else if (Math.Abs(Head.Item2 - CurrentRope.Position.Item2) > 1)
                        //        {
                        //            if (Head.Item1 < CurrentRope.Position.Item1)
                        //            {
                        //                CurrentRope.Position = new Tuple<int, int>(CurrentRope.Position.Item1 - 1, CurrentRope.Position.Item2 + 1);
                        //            }
                        //            else
                        //            {
                        //                CurrentRope.Position = new Tuple<int, int>(CurrentRope.Position.Item1 + 1, CurrentRope.Position.Item2 + 1);
                        //            }
                        //        }
                        //        else if (Math.Abs(Head.Item1 - CurrentRope.Position.Item1) > 1)
                        //        {
                        //            if (Head.Item1 < CurrentRope.Position.Item1)
                        //            {
                        //                CurrentRope.Position = new Tuple<int, int>(CurrentRope.Position.Item1 - 1, CurrentRope.Position.Item2 + 1);
                        //            }
                        //            else
                        //            {
                        //                CurrentRope.Position = new Tuple<int, int>(CurrentRope.Position.Item1 + 1, CurrentRope.Position.Item2 + 1);
                        //            }
                        //        }

                        //        break;
                        //    case "D":

                        //        if (CurrentRope.Position.Item2 - Head.Item2 == 2 && CurrentRope.Position.Item1 == Head.Item1)
                        //        {
                        //            CurrentRope.Position = new Tuple<int, int>(CurrentRope.Position.Item1, CurrentRope.Position.Item2 - 1);
                        //        }
                        //        else if (Math.Abs(Head.Item2 - CurrentRope.Position.Item2) > 1)
                        //        {
                        //            if (Head.Item1 < CurrentRope.Position.Item1)
                        //            {
                        //                CurrentRope.Position = new Tuple<int, int>(CurrentRope.Position.Item1 - 1, CurrentRope.Position.Item2 - 1);
                        //            }
                        //            else
                        //            {
                        //                CurrentRope.Position = new Tuple<int, int>(CurrentRope.Position.Item1 + 1, CurrentRope.Position.Item2 - 1);
                        //            }
                        //        }
                        //        else if (Math.Abs(Head.Item1 - CurrentRope.Position.Item1) > 1)
                        //        {
                        //            if (Head.Item1 < CurrentRope.Position.Item1)
                        //            {
                        //                CurrentRope.Position = new Tuple<int, int>(CurrentRope.Position.Item1 - 1, CurrentRope.Position.Item2 - 1);
                        //            }
                        //            else
                        //            {
                        //                CurrentRope.Position = new Tuple<int, int>(CurrentRope.Position.Item1 + 1, CurrentRope.Position.Item2 - 1);
                        //            }
                        //        }
                        //        break;
                        //}

                        if (CurrentRope.id == CheckId)
                        {
                            if (!VisitedPoints.Contains(CurrentRope.Position))
                            {
                                VisitedPoints.Add(CurrentRope.Position);
                            }
                        }
                        Head = CurrentRope.Position;
                    }
                }
            }

        }
        public static int Part1()
        {
            string[] Data = File.ReadAllLines(Path);
            int answer = 0;
           
            foreach(string Line in Data) { 
                MoveHead(Line.Split(" ")[0], Int32.Parse(Line.Split(" ")[1]));
            }

            answer = VisitedPoints.Count();

            return answer;
        }

        public static int Part2()
        {
            string[] Data = File.ReadAllLines(Path);
            int answer = 0;
           
            List<Rope> Ropes = new List<Rope>();
            Ropes.Add(new Rope(0));
            Ropes.Add(new Rope(1));
            Ropes.Add(new Rope(2));
            Ropes.Add(new Rope(3));
            Ropes.Add(new Rope(4));
            Ropes.Add(new Rope(5));
            Ropes.Add(new Rope(6));
            Ropes.Add(new Rope(7));
            Ropes.Add(new Rope(8));
            Ropes.Add(new Rope(9));

            VisitedPoints = new HashSet<Tuple<int, int>>();

            foreach (string Line in Data)
            {
                MoveRopes(Line.Split(" ")[0], Int32.Parse(Line.Split(" ")[1]), Ropes, 9);
            }

            answer = VisitedPoints.Count();

            return answer;
        }
    }
}
