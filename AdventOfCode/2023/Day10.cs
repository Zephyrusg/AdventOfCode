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

        static bool CheckOutside(int i, int j, string[] data) {

            string Teststring = string.Empty;
            //left
            Teststring += data[i][j-1];
            //right
            Teststring += data[i ][j+1];
            //up
            Teststring += data[i-1][j];
            //down
            Teststring += data[i + 1][j];
            //leftup
            Teststring += data[i - 1][j-1];
            //rightup
            Teststring += data[i - 1][j + 1];
            //left down
            Teststring += data[i + 1][j - 1];
            //rigth down
            Teststring += data[i + 1][j + 1];

            if (Teststring.Contains('O'))
            {
                return true;
            }
            else
            {
                return false;
            }
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
            int i = 0;
            foreach(string line in lines)  //(int i = 0; i < Pipe.height; i++)
            {
                string testline = "";
                for (int j = 0; j < Pipe.width; j++)
                {
                    Pipe ?Testpipe = Pipe.LoopPipes.Find(p=> p.x== j && p.y==i);

                    if (Testpipe != null)
                    {
                        testline += '#';

                    }
                    else {
                        testline += '.';
                    }

                }
                Data[i] = testline;
                i++;
            }

            bool done = false;

            while (!done)
            {
                done = true;
                i = 0;
                string[] NewData = new string[Pipe.height];
                foreach (string line in lines)
                {
                    string NewLine = "";
                    for (int j = 0; j < Pipe.width; j++)
                    {
                        if (Data[i][j] == '.')
                        {

                            if (i == 0 || i == (Pipe.height - 1) || j == 0 || j == Pipe.width-1)  {
                               NewLine += 'O';
                               done = false;
                            }else if(CheckOutside(i, j,Data))
                            {
                                NewLine += 'O';
                                done = false;
                            }
                            
                            
                            else {
                                NewLine += "."; 
                            }

                        }
                        else
                        {
                            NewLine += Data[i][j];
                        }
                        

                    }
                    NewData[i] = NewLine;
                    i++;
                }
                Data = NewData;

                //foreach (string line in Data)
                //{
                //    Console.WriteLine(line);
                //}
            }

            for( i = 0; i < Pipe.height; i++)
            {

                for (int j = 0; j < Pipe.width; j++)
                {
                    if (Data[i][j] == '.')
                    {
                        answer++;

                    }

                }

            }

            return answer;
        }

    }
}
