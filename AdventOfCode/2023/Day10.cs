using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdventOfCode
{
    internal class Y2023D10
    {
        public static string[] lines = File.ReadAllLines(".\\2023\\Input\\inputDay10.txt");

        class Pipe {
            public char KindofPipe;
            public int x, y;
            public List<Pipe> ConnectedPipes = new List<Pipe>();
            public int distance;
            public bool Connected;


            public static Pipe Startpoint;

            public static int width = 0;
            public static int height = 0;
            public static List<Pipe> LoopPipes = new List<Pipe>();
            public static List<Pipe> AllPipes = new();
            //public static HashSet<Pipe> openList = new HashSet<Pipe>();
            //public static HashSet<Pipe> closedList = new HashSet<Pipe>();
            //public static PriorityQueue<Pipe, int> priorityList = new PriorityQueue<Pipe, int>();
            public Pipe(int x, int y, char KindofPipe) {
                this.x = x;
                this.y = y;
                this.KindofPipe = KindofPipe;
                int distance = int.MaxValue;
            }

            public void FindNeighbourPipes() {

                int nextx = x;
                int nexty = y;
                Pipe? nextPipe;
                switch (KindofPipe)
                {
                    case 'F':
                        nextx = x + 1;
                        nexty = y;
                        nextPipe = Pipe.AllPipes.Find(x => x.x == nextx && x.y == nexty && (x.KindofPipe == '-' || x.KindofPipe == '7' || x.KindofPipe == 'J' || x.KindofPipe == 'S'));
                        if (nextPipe != null)
                        {
                            this.ConnectedPipes.Add(nextPipe);
                        }
                        nextx = x;
                        nexty = y + 1;

                        nextPipe = Pipe.AllPipes.Find(x => x.x == nextx && x.y == nexty && (x.KindofPipe == '|' || x.KindofPipe == 'L' || x.KindofPipe == 'J' || x.KindofPipe == 'S'));
                        if (nextPipe != null)
                        {
                            this.ConnectedPipes.Add(nextPipe);
                        }
                        break;
                    case 'J':
                        nextx = x;
                        nexty = y - 1;
                        nextPipe = Pipe.AllPipes.Find(x => x.x == nextx && x.y == nexty && (x.KindofPipe == '|' || x.KindofPipe == '7' || x.KindofPipe == 'F' || x.KindofPipe == 'S'));
                        if (nextPipe != null)
                        {
                            this.ConnectedPipes.Add(nextPipe);
                        }
                        nextx = x - 1;
                        nexty = y;

                        nextPipe = Pipe.AllPipes.Find(x => x.x == nextx && x.y == nexty && (x.KindofPipe == '-' || x.KindofPipe == 'L' || x.KindofPipe == 'F' || x.KindofPipe == 'S'));
                        if (nextPipe != null)
                        {
                            this.ConnectedPipes.Add(nextPipe);
                        }
                        break;
                    case 'L':
                        nextx = x;
                        nexty = y - 1;
                        nextPipe = Pipe.AllPipes.Find(x => x.x == nextx && x.y == nexty && (x.KindofPipe == '|' || x.KindofPipe == '7' || x.KindofPipe == 'F' || x.KindofPipe == 'S'));
                        if (nextPipe != null)
                        {
                            this.ConnectedPipes.Add(nextPipe);
                        }
                        nextx = x + 1;
                        nexty = y;

                        nextPipe = Pipe.AllPipes.Find(x => x.x == nextx && x.y == nexty && (x.KindofPipe == '-' || x.KindofPipe == 'J' || x.KindofPipe == '7' || x.KindofPipe == 'S'));
                        if (nextPipe != null)
                        {
                            this.ConnectedPipes.Add(nextPipe);
                        }
                        break;
                    case '7':
                        nextx = x;
                        nexty = y + 1;
                        nextPipe = Pipe.AllPipes.Find(x => x.x == nextx && x.y == nexty && (x.KindofPipe == '|' || x.KindofPipe == 'J' || x.KindofPipe == 'L' || x.KindofPipe == 'S'));
                        if (nextPipe != null)
                        {
                            this.ConnectedPipes.Add(nextPipe);
                        }
                        nextx = x - 1;
                        nexty = y;

                        nextPipe = Pipe.AllPipes.Find(x => x.x == nextx && x.y == nexty && (x.KindofPipe == 'F' || x.KindofPipe == '-' || x.KindofPipe == 'L' || x.KindofPipe == 'S'));
                        if (nextPipe != null)
                        {
                            this.ConnectedPipes.Add(nextPipe);
                        }
                        break;
                    case '|':
                        nextx = x;
                        nexty = y + 1;
                        nextPipe = Pipe.AllPipes.Find(x => x.x == nextx && x.y == nexty && (x.KindofPipe == '|' || x.KindofPipe == 'L' || x.KindofPipe == 'J' || x.KindofPipe == 'S'));
                        if (nextPipe != null)
                        {
                            this.ConnectedPipes.Add(nextPipe);
                        }
                        nextx = x;
                        nexty = y - 1;

                        nextPipe = Pipe.AllPipes.Find(x => x.x == nextx && x.y == nexty && (x.KindofPipe == '|' || x.KindofPipe == '7' || x.KindofPipe == 'F' || x.KindofPipe == 'S'));
                        if (nextPipe != null)
                        {
                            this.ConnectedPipes.Add(nextPipe);
                        }
                        break;
                    case '-':
                        nextx = x + 1;
                        nexty = y;
                        nextPipe = Pipe.AllPipes.Find(x => x.x == nextx && x.y == nexty && (x.KindofPipe == '7' || x.KindofPipe == 'J' || x.KindofPipe == '-' || x.KindofPipe == 'S'));
                        if (nextPipe != null)
                        {
                            this.ConnectedPipes.Add(nextPipe);
                        }
                        nextx = x - 1;
                        nexty = y;

                        nextPipe = Pipe.AllPipes.Find(x => x.x == nextx && x.y == nexty && (x.KindofPipe == 'L' || x.KindofPipe == 'F' || x.KindofPipe == '-' || x.KindofPipe == 'S'));
                        if (nextPipe != null)
                        {
                            this.ConnectedPipes.Add(nextPipe);
                        }
                        break;
                    case 'S':
                        nextx = x + 1;
                        nexty = y;
                        nextPipe = Pipe.AllPipes.Find(x => x.x == nextx && x.y == nexty && (x.KindofPipe == '7' || x.KindofPipe == 'J' || x.KindofPipe == '-'));
                        if (nextPipe != null)
                        {
                            this.ConnectedPipes.Add(nextPipe);
                        }
                        nextx = x - 1;
                        nexty = y;

                        nextPipe = Pipe.AllPipes.Find(x => x.x == nextx && x.y == nexty && (x.KindofPipe == 'L' || x.KindofPipe == 'F' || x.KindofPipe == '-'));
                        if (nextPipe != null)
                        {
                            this.ConnectedPipes.Add(nextPipe);
                        }

                        nextx = x;
                        nexty = y + 1;
                        nextPipe = Pipe.AllPipes.Find(x => x.x == nextx && x.y == nexty && (x.KindofPipe == 'L' || x.KindofPipe == 'J' || x.KindofPipe == '|'));
                        if (nextPipe != null)
                        {
                            this.ConnectedPipes.Add(nextPipe);
                        }
                        nextx = x;
                        nexty = y - 1;

                        nextPipe = Pipe.AllPipes.Find(x => x.x == nextx && x.y == nexty && (x.KindofPipe == 'F' || x.KindofPipe == '7' || x.KindofPipe == '|'));
                        if (nextPipe != null)
                        {
                            this.ConnectedPipes.Add(nextPipe);
                        }
                        break;

                }

                if (ConnectedPipes.Count < 2) {
                    Connected = false;
                }
                else
                {
                    Connected = true;
                }
            }
            public override int GetHashCode()
            {
                return (this.y * Pipe.width + this.x);
            }
        }

        static bool CheckOutside(int x, int y, bool[,] data) {

            //left
            if (data[x - 1, y] == true) { return true; }
            //right
            if (data[x + 1, y]== true) { return true; }
            //up
            if (data[x, y-1] ==true) { return true; }
       
            //down
            if(data[x,y+1] == true) { return true; }
            ////leftup
            //Teststring += data[i - 1][j-1];
            ////rightup
            //Teststring += data[i - 1][j + 1];
            ////left down
            //Teststring += data[i + 1][j - 1];
            ////rigth down
            //Teststring += data[i + 1][j + 1];

            return false;
        }
        public int Part1() 
        {
            int answer = 0;

            Pipe.height = lines.Count();
            Pipe.width = lines[0].Length;
            
   
            for(int i = 0; i < Pipe.height; i++)
            {

                for (int j = 0; j < Pipe.width; j++)
                {
                    if (lines[i][j] != '.')
                    {
                        Pipe.AllPipes.Add(new(j, i, lines[i][j]));

                    }
                    
                }

            }

            foreach(Pipe Pipe in Pipe.AllPipes)
            {
                Pipe.FindNeighbourPipes();
            }

            int distance = 1;
            Pipe StartPipe = Pipe.AllPipes.Find(x => x.KindofPipe == 'S');
            Pipe.LoopPipes.Add(StartPipe);
            Pipe Previous = StartPipe;
            Pipe NextPipe = Previous.ConnectedPipes[0];

            while (NextPipe != StartPipe)
           {

                Pipe CurrentPipe = NextPipe;
                Pipe.LoopPipes.Add(CurrentPipe);
                CurrentPipe.distance = distance;
                NextPipe = CurrentPipe.ConnectedPipes.Find(x => x != Previous);
                Previous = CurrentPipe;
                distance++;
                
            }


            answer = distance / 2;

            return answer;
        }

        public int Part2()
        {
            int answer = 0;
            string[] Data = new string[Pipe.height];
            bool[,] fillmap = new bool[Pipe.width * 2 + 1, Pipe.height * 2 + 1];
            int distance = 1;
            Pipe StartPipe = Pipe.AllPipes.Find(x => x.KindofPipe == 'S');
            fillmap[StartPipe.x * 2 + 1,StartPipe.y * 2 + 1] = true;

            Pipe.LoopPipes.Add(StartPipe);
            Pipe Previous = StartPipe;
            Pipe NextPipe = Previous.ConnectedPipes[0];
            fillmap[NextPipe.x * 2 + 1, NextPipe.y * 2 + 1] = true;
            //(A.x * 2 + B.x *2 + 2 / 2)
            fillmap[((NextPipe.x * 2) + (StartPipe.x * 2) + 2) / 2, ((NextPipe.y * 2) + (StartPipe.y * 2) + 2) / 2] = true;

            while (NextPipe != StartPipe)
            {

                Pipe CurrentPipe = NextPipe;
                Pipe.LoopPipes.Add(CurrentPipe);
                CurrentPipe.distance = distance;
                NextPipe = CurrentPipe.ConnectedPipes.Find(x => x != Previous);
                fillmap[NextPipe.x * 2 + 1, NextPipe.y * 2 + 1] = true;
                fillmap[((NextPipe.x * 2) + (CurrentPipe.x * 2) + 2) / 2, ((NextPipe.y * 2) + (CurrentPipe.y * 2) + 2) / 2] = true;

                Previous = CurrentPipe;
                distance++;

            }
            fillmap[((NextPipe.x * 2) + (Previous.x * 2) + 2) / 2, ((NextPipe.y * 2) + (Previous.y * 2) + 2) / 2] = true;

            //for(int x = 0; x < Pipe.width * 2 + 1; x++)
            //{
            //    fillmap[x,0] = true;
            //    fillmap[x, fillmap.GetLength(1) -1] = true;
            //}
            //for (int y = 0; y < Pipe.height * 2 + 1; y++) {
            //    fillmap[0,y] = true;
            //    fillmap[fillmap.GetLength(0)-1,y] = true;
            //}



            //bool done = false;

            //while (!done)
            //{
            //    done = true;
            //    for(int x= 0; x< fillmap.GetLength(0); x++)
            //    {
            //        for (int y = 0; y < fillmap.GetLength(1); y++)
            //        {
            //            if (fillmap[x, y] == false)
            //            {
            //                if (CheckOutside(x, y, fillmap))
            //                {

            //                    done = false;
            //                }


            //            }
            //        }



            //    }



            //}

            Queue<(int x, int y)> fillqueue = new();
            fillqueue.Enqueue((0, 0));
            while (fillqueue.Count > 0)
            {
                (int x, int y) fillLocation = fillqueue.Dequeue();
                if (fillLocation.x > 0 && !fillmap[fillLocation.x - 1, fillLocation.y])
                {
                    fillmap[fillLocation.x - 1, fillLocation.y] = true;
                    fillqueue.Enqueue((fillLocation.x - 1, fillLocation.y));
                }
                if (fillLocation.x < fillmap.GetLength(0) - 1 && !fillmap[fillLocation.x + 1, fillLocation.y])
                {
                    fillmap[fillLocation.x + 1, fillLocation.y] = true;
                    fillqueue.Enqueue((fillLocation.x + 1, fillLocation.y));
                }
                if (fillLocation.y > 0 && !fillmap[fillLocation.x, fillLocation.y - 1])
                {
                    fillmap[fillLocation.x, fillLocation.y - 1] = true;
                    fillqueue.Enqueue((fillLocation.x, fillLocation.y - 1));
                }
                if (fillLocation.y < fillmap.GetLength(1) - 1 && !fillmap[fillLocation.x, fillLocation.y + 1])
                {
                    fillmap[fillLocation.x, fillLocation.y + 1] = true;
                    fillqueue.Enqueue((fillLocation.x, fillLocation.y + 1));
                }
            }

            int totalInclusions = 0;
            for (int y = 1; y < fillmap.GetLength(1); y += 2)
            {
                for (int x = 1; x < fillmap.GetLength(0); x += 2)
                {
                    if (!fillmap[x, y])
                        totalInclusions++;
                }
            }

            answer = totalInclusions;

            //for( i = 0; i < Pipe.height; i++)
            //{

            //    for (int j = 0; j < Pipe.width; j++)
            //    {
            //        if (Data[i][j] == '.')
            //        {
            //            answer++;

            //        }

            //    }



            return answer;
        }

    }
}
