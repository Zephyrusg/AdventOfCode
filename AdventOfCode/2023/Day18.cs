using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Security;
using System.Diagnostics.Metrics;

namespace AdventOfCode
{
    internal class Y2023D18
    {
        Dictionary<int, string> Direction = new Dictionary<int, string>
        {
            { 3, "U" },
            { 1, "D" },
            { 2, "L" },
            { 0, "R" }
        };

        public static string[] Lines = File.ReadAllLines(".\\2023\\Input\\inputDay18.txt");
        static int Height = Lines.Count();
        static int Width = Lines[0].Length;
        static char[,] map = new char[Width, Height];
        public int Part1() 
        {
            int answer = 0;
            HashSet<(int x, int y)> EdgePoints = new();
            (int x, int y) current = (0, 0);
            List<(int distance, string direction)> Steps = new();

            foreach(var line in Lines)
            {
                string direction = line.Split(' ')[0];
                int meters = int.Parse(line.Split(' ')[1]);
                Steps.Add((meters, direction));
            }
            (int x, int y) lastcorner = current;
            (int x, int y) currentcorner = current;
            string lastdirect = Steps[Steps.Count - 1].direction;
            string ?nextdirection = null;
            for(int s = 0; s < Steps.Count; s++) 
            {
                var Step = Steps[s];

                if (s != 0) { lastdirect = Steps[s - 1].direction; }
                if(s == Steps.Count - 1) { nextdirection = Steps[0].direction; }
                else { nextdirection = Steps[s + 1].direction; }

                switch (Steps[s].direction) {


                    case "U":
                        current = (current.x, current.y - Step.distance);
                        break;
                    case "D":
                        current = (current.x, current.y + Step.distance);
                        break;
                    case "L":
                        if (nextdirection == "D")
                        {
                            if (lastdirect == "D") { currentcorner = (current.x - Step.distance + 1, current.y + 1); }
                            else { currentcorner = (current.x - Step.distance + 1, current.y); }
                        }
                        else {
                            if (lastdirect == "U") { currentcorner = (current.x - Step.distance, current.y + 1); }
                            else { currentcorner = (current.x - Step.distance, current.y+1); } 
                        }
                        
                        
                        current= (current.x - Step.distance, current.y);
                        answer += ((lastcorner.x - currentcorner.x) * currentcorner.y);
                        lastcorner = currentcorner;
                        break;
                    case "R":

                        if (nextdirection == "D")
                        {
                            if (lastdirect == "U")
                            {
                                currentcorner = (current.x + Step.distance + 1, current.y);
                            }
                            else { currentcorner = (current.x + Step.distance + 1, current.y); }
                        }
                        else { 
                            currentcorner = (current.x + Step.distance, current.y); 
                        }

                        current= (current.x + Step.distance, current.y);
                        answer += ((lastcorner.x - currentcorner.x) * current.y);
                        lastcorner = currentcorner;
                        break;
                }
            }

            

            //foreach (var line in Lines)
            //{
            //    string direction = line.Split(' ')[0];
            //    int meters = int.Parse(line.Split(' ')[1]);
            //    switch (direction)
            //    {
            //        case "U":
            //            for (int m = 0; m < meters; m++)
            //            {
            //                current = (current.x, current.y - 1);
            //                EdgePoints.Add(current);
            //            }
            //            break;
            //        case "D":
            //            for (int m = 0; m < meters; m++)
            //            {
            //                current = (current.x, current.y + 1);
            //                EdgePoints.Add(current);
            //            }
            //            break;
            //        case "L":
            //            for (int m = 0; m < meters; m++)
            //            {
            //                current = (current.x - 1, current.y);
            //                EdgePoints.Add(current);
            //            }
            //            break;
            //        case "R":
            //            for (int m = 0; m < meters; m++)
            //            {
            //                current = (current.x + 1, current.y);
            //                EdgePoints.Add(current);
            //            }
            //            break;
            //    }
            //}

            //Width = EdgePoints.Max(x=> x.x) - EdgePoints.Min(x => x.x);
            //Height = EdgePoints.Max(y => y.y) - EdgePoints.Min(y => y.y);
            //int MinWidth = Math.Abs(EdgePoints.Min(x => x.x));
            //int MinHeight = Math.Abs(EdgePoints.Min(y => y.y));

            //map = new char[Width+1, Height+1];
            //for (int y = 0; y <= Height; y++)
            //{
            //    for (int x = 0; x <= Width; x++)
            //    {
            //        map[x, y] = '.';
            //    }
                
            //}

            //foreach ((int x, int y)  in EdgePoints)
            //{
            //    map[x+MinWidth, y+ MinHeight] = '#';
            //}
            //(int x, int y) insideStartPoint = (0,0);
            //for(int x=0; x <= Width; x++)
            //{
            //    if (map[x,0] == '#')
            //    {
            //        insideStartPoint = (x+1, 1);
            //        break;
            //    }

            //}
            //HashSet<(int x, int y)> Insides = new();
            //Insides.Add(insideStartPoint);
            //while (Insides.Count > 0)
            //{
            //    HashSet<(int x, int y)> NewInsides = new();
            //    foreach ((int x , int y) in Insides)
            //    {
                   
            //        map[x, y] = '#';
            //        if (x-1 >= 0 &&      map[x - 1, y] == '.') { NewInsides.Add((x - 1, y)); }
            //        if (x+1 <= Width &&  map[x + 1, y] == '.') { NewInsides.Add((x + 1, y)); }
            //        if (y+1 <= Height && map[x, y + 1] == '.') { NewInsides.Add((x, y + 1)); }
            //        if (y-1 >= 0 &&      map[x, y - 1] == '.') { NewInsides.Add((x, y - 1)); }

            //    }
            //    Insides = NewInsides;
            //}


            //Console.WriteLine();
            //for (int y = 0; y <= Height; y++)
            //{
            //    for (int x = 0; x <= Width; x++)
            //    {
            //        Console.Write(map[x, y]);
            //    }
            //    Console.WriteLine();
            //}

            //for (int y = 0; y <= Height; y++)
            //{
            //    for (int x = 0; x <= Width; x++)
            //    {
            //        if (map[x, y] == '#')
            //        {
            //            answer++;
            //        }
            //    }
                
            //}





            return answer;
        }

        public int Part2()
        {
            int answer = 0;
            //HashSet<((int xmin, int xmax) x,( int miny, int maxy) y)> EdgePoints = new();
            //(int x, int y) current = (0, 0);

            //List<(int meters, string direction )> Steps = new();
            //foreach(var line in Lines)
            //{
            //    int meters = int.Parse(((line.Split(' ')[2])[2..7]), NumberStyles.HexNumber);
            //    string direction = Direction[int.Parse(((line.Split(' ')[2])[7].ToString()))];
            //    Steps.Add((meters, direction));
            //}


            //foreach (var step in Steps)
            //{
                
            //    switch (step.direction)
            //    {
            //        //Up
            //        case "U":
                      
                        

            //            ((int xmin, int xmax), (int miny, int maxy)) tempup = ((current.x, current.x ), (current.y - meters, current.y - 1));
            //            EdgePoints.Add(tempup);
            //            current = (current.x, current.y - meters);

            //                //current = (current.x, current.y - 1,1);
            //                //EdgePoints.Add(current);
                      
            //            break;
            //        //Down
            //        case "D":

            //            ((int xmin, int xmax), (int miny, int maxy)) tempdown = ((current.x, current.x), (current.y + 1, current.y + meters));
            //            EdgePoints.Add(tempdown);
            //            current = (current.x, current.y + meters);

            //            //for (int m = 0; m < meters; m++)
            //            //{
            //            //    current = (current.x, current.y + 1,1);
            //            //    EdgePoints.Add(current);
            //            //}
            //            break;
            //        //Left
            //        case "L":
            //            ((int xmin, int xmax), (int miny, int maxy)) templeft = ((current.x - meters, current.x - 1 ), (current.y, current.y));
            //            EdgePoints.Add(templeft);
            //            current = (current.x - meters, current.y);

            //            //for (int m = 0; m < meters; m++)
            //            //{
            //            //    current = (current.x - meters, current.y,meters-m);
            //            //    EdgePoints.Add(current);
            //            //}
            //            break;
            //        //Right
            //        case "R":
            //            ((int xmin, int xmax), (int miny, int maxy)) tempright = ((current.x + 1, current.x + meters), (current.y, current.y));
            //            EdgePoints.Add(tempright);
            //            current = (current.x + meters, current.y);
            //            //for (int m = 0; m < meters; m++)
            //            //{
            //            //    current = (current.x + 1, current.y,meters-m);
            //            //    EdgePoints.Add(current);
            //            //}
            //            break;
            //    }
            //}



            //Height = EdgePoints.Max(y => y.y.maxy);
            //int minx = EdgePoints.Min(x => x.x.xmin);
            //int miny = EdgePoints.Min(y => y.y.miny);

            //for (int x = 0; x < EdgePoints.Count(); x++)
            //{
            //    EdgePoints[x] = ((EdgePoints[x].x.xmin + minx, edgepoint.x.xmax + minx), (edgepoint.y.miny + miny, edgepoint.y.maxy + miny));

            //}
            ////int MinWidth = Math.Abs(EdgePoints.Min(x => x.x));
            ////int MinHeight = Math.Abs(EdgePoints.Min(y => y.y));
            //var Wall = EdgePoints.Where(point => point.y.miny <= 0 && point.y.maxy >= 0 ).ToHashSet();
            //var wall2 =  EdgePoints.Where(point => point.y.miny <= 1 && point.y.maxy >= 1).ToHashSet();
           
            ////for (int y = 0; y <= Height; y++)
            ////{s
            ////    for (int x = 0; x <= Width; x++)
            ////    {
            ////        map[x, y] = '.';
            ////    }

            ////}

            ////foreach ((int x, int , int l) in EdgePoints)
            ////{
            ////    map[x , y] = true;
            ////}
            ////(int x, int y) insideStartPoint = (0, 0);
            ////for (int x = 0; x <= Width; x++)
            ////{
            ////    if (map[x, 0] == true)
            ////    {
            ////        insideStartPoint = (x + 1, 1);
            ////        break;
            ////    }

            ////}
            ////HashSet<(int x, int y)> Insides = new();
            ////Insides.Add(insideStartPoint);
            ////while (Insides.Count > 0)
            ////{
            ////    HashSet<(int x, int y)> NewInsides = new();
            ////    foreach ((int x, int y) in Insides)
            ////    {

            ////        map[x, y] = true;
            ////        if (x - 1 >= 0 && map[x - 1, y] == false) { NewInsides.Add((x - 1, y)); }
            ////        if (x + 1 <= Width && map[x + 1, y] == false) { NewInsides.Add((x + 1, y)); }
            ////        if (y + 1 <= Height && map[x, y + 1] == false) { NewInsides.Add((x, y + 1)); }
            ////        if (y - 1 >= 0 && map[x, y - 1] == false) { NewInsides.Add((x, y - 1)); }

            ////    }
            ////    Insides = NewInsides;
            ////}


            ////for (int y = 0; y <= Height; y++)
            ////{
            ////    for (int x = 0; x <= Width; x++)
            ////    {
            ////        if (map[x, y] == true)
            ////        {
            ////            answer++;
            ////        }
            ////    }

            ////}





            return answer;




        }

    }
}
