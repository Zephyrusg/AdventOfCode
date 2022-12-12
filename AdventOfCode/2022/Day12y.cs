using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdventOfCode._2022
{
    class point
    {
        public static point Endpoint;
        public static point Startpoint;
        public static int width = 0;
        public static int height = 0;
        public static HashSet<point> openList = new HashSet<point>();
        public static HashSet<point> closedList = new HashSet<point>();
        public static PriorityQueue<point, int> priorityList = new PriorityQueue<point, int>();
        public static point[,] map = new point[0, 0];
        public  int x, y;
        public int value;
        public int distance;
        public int estimatedDistance;


        public point(int y, int x, int value){
            this.x = x;
            this.y = y;
            this.value = value;
            this.distance = Int32.MaxValue; ;
        }
        public bool Equals(point otherPoint)
        {

            return ((this.x == otherPoint.x) && (this.y == otherPoint.y));

            {

            }
        }

        public List<point> GetNeighbours()
        {
            List<point> neighbourPoints = new List<point>();
            if (this.x > 0)
            {
                neighbourPoints.Add(point.map[this.y, (this.x - 1)]);
            }
            if (this.x < point.width - 1)
            {
                neighbourPoints.Add(point.map[this.y, (this.x + 1)]);
            }
            if (this.y > 0)
            {
                neighbourPoints.Add(point.map[(this.y - 1), this.x]);
            }
            if (this.y < point.height - 1)
            {
                neighbourPoints.Add(point.map[(this.y + 1), this.x]);
            }
            return neighbourPoints;
        }
        public override int GetHashCode()
        {
            return (this.y * point.width + this.x);
        }

    }





    internal class Day12
    {
        static string Path = ".\\2022\\Input\\InputDay12.txt";
        public static int Part1()
        {
            int answer = 0;
            string[] Data = File.ReadAllLines(Path);
            point.height = Data.Count();
            point.width = Data[0].Length;
            point.map = new point[point.height, point.width];
            int hNumber = 20;
            int Exiti = 0;
            int Exitj = 0;
            int Starti = 0;
            int Startj = 0;
            for (int i = 0; i < point.height ;  i++)
            {

                for (int j = 0; j < point.width; j++)
                {
                    switch (Data[i][j])
                    {

                        case 'S':
                            {
                                int value = 0;
                                Starti = i;
                                Startj = j;
                                point.map[i, j] = new point(i, j, value);
                                break;
                            }
                        case 'E':
                            {

                                int value = 27;
                                Exiti = i;
                                Exitj = j;
                                point.map[i, j] = new point(i, j, value);

                                break;
                            }
                        default:
                            {
                                int value = (Data[i][j] - 'a') + 1;
                                point.map[i, j] = new point(i, j, value);
                                break;
                            }
                    }


                }                
            }

            

            point.Endpoint = point.map[Exiti, Exitj];
            point.Startpoint = point.map[Starti, Startj];
            point.map[Starti, Startj].distance = 0;
            point.map[Startj, Startj].estimatedDistance = 0;

            HashSet<point> openList = new HashSet<point>();
            HashSet<point> closedList = new HashSet<point>();
            PriorityQueue<point, int> priorityList = new PriorityQueue<point, int>();
 
            openList.Add(point.Startpoint);
 
            priorityList.Enqueue(point.Startpoint, point.Startpoint.distance);
            bool endReached = false;
            int neighbourDistance = Int32.MaxValue;

            while (openList.Count > 0 && !endReached)
            {


                point current = priorityList.Dequeue();
                //Write-host "$($current.x), $($current.y): $($openList.Count) $($closedList.Count)"
                foreach (point neighbourPoint in current.GetNeighbours().Where(p => p.value <= current.value || p.value - 1 == current.value))
                {

                  
                    neighbourDistance = current.distance + 1;

                    if (neighbourPoint == point.Endpoint)
                    {
                        answer = neighbourDistance;
                        goto loop;
                    } 
                    int estimatedNeighbourTotalDistance = neighbourDistance + (((point.width - neighbourPoint.x) + (point.height - neighbourPoint.y)) * hNumber);
                    if (openList.Contains(neighbourPoint))
                    {
                        //estimatedNeighbourTotalDistance < neighbourPoint.estimatedDistance
                        if (estimatedNeighbourTotalDistance < neighbourPoint.estimatedDistance)
                        {
                            neighbourPoint.distance = neighbourDistance;
                            neighbourPoint.estimatedDistance = estimatedNeighbourTotalDistance;
                            continue;
                        }
                        else
                        {
                            continue;
                        }
                    }

                    if (closedList.Contains(neighbourPoint))
                    {
                        if (estimatedNeighbourTotalDistance < neighbourPoint.estimatedDistance)
                        {

                            closedList.Remove(neighbourPoint);
                        }
                        else
                        {
                            continue;
                        }
                    }

                    neighbourPoint.distance = neighbourDistance;
                    neighbourPoint.estimatedDistance = estimatedNeighbourTotalDistance;
                    openList.Add(neighbourPoint);
                    priorityList.Enqueue(neighbourPoint, neighbourPoint.distance);
                    
                    
                }
                closedList.Add(current);
                openList.Remove(current);


            }

        loop:

            var test = closedList.OrderBy(p => p.distance);

            return answer;
        }


        public static int Part2()
        {
        //    string[] Data = File.ReadAllLines(Path);
            int answer = 0;

            List<int> possibleAnswer = new List<int>();
            int hNumber = 20;
            int[] StartLocations = Enumerable.Range(0, point.height).ToArray();
            foreach (int starty in StartLocations) {

                for (int i = 0; i < point.height; i++)
                {

                    for (int j = 0; j < point.width; j++)
                    {
                        point.map[i, j].distance = Int32.MaxValue;
                    }
                }

                point.Startpoint = point.map[starty, 0];
                point.Startpoint.distance = 0;
                point.Startpoint.estimatedDistance = 0;
                HashSet<point> openList = new HashSet<point>();
                HashSet<point> closedList = new HashSet<point>();
                PriorityQueue<point, int> priorityList = new PriorityQueue<point, int>();
                
                openList.Add(point.Startpoint);

                priorityList.Enqueue(point.Startpoint, point.Startpoint.distance);
                bool endReached = false;
                int neighbourDistance = Int32.MaxValue;

                while (openList.Count > 0 && !endReached)
                {


                    point current = priorityList.Dequeue();
                    //Write-host "$($current.x), $($current.y): $($openList.Count) $($closedList.Count)"
                    foreach (point neighbourPoint in current.GetNeighbours().Where(p => p.value <= current.value || p.value - 1 == current.value))
                    {


                        neighbourDistance = current.distance + 1;

                        if (neighbourPoint == point.Endpoint)
                        {
                            possibleAnswer.Add(neighbourDistance);
                            goto loop;
                        }
                        int estimatedNeighbourTotalDistance = neighbourDistance + (((point.width - neighbourPoint.x) + (point.height - neighbourPoint.y)) * hNumber);
                        if (openList.Contains(neighbourPoint))
                        {
                            //estimatedNeighbourTotalDistance < neighbourPoint.estimatedDistance
                            if (estimatedNeighbourTotalDistance < neighbourPoint.estimatedDistance)
                            {
                                neighbourPoint.distance = neighbourDistance;
                                neighbourPoint.estimatedDistance = estimatedNeighbourTotalDistance;
                                continue;
                            }
                            else
                            {
                                continue;
                            }
                        }

                        if (closedList.Contains(neighbourPoint))
                        {
                            if (estimatedNeighbourTotalDistance < neighbourPoint.estimatedDistance)
                            {

                                closedList.Remove(neighbourPoint);
                            }
                            else
                            {
                                continue;
                            }
                        }

                        neighbourPoint.distance = neighbourDistance;
                        neighbourPoint.estimatedDistance = estimatedNeighbourTotalDistance;
                        openList.Add(neighbourPoint);
                        priorityList.Enqueue(neighbourPoint, neighbourPoint.distance);


                    }
                    closedList.Add(current);
                    openList.Remove(current);


                }
            loop:;

            }
       
            answer = possibleAnswer.Min();
            return answer;

        }


    }
    
}
