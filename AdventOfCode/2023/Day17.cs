using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class Y2023D17
    {
        public static string[] Lines = File.ReadAllLines(".\\2023\\Input\\inputDay17.txt");
        static int Height = Lines.Count();
        static int Width = Lines[0].Length;
        static int[,] map = new int[Width, Height];

        //class Point 
        //{
        //    public int x;
        //    public int y;
        //    public int value;
        //    public int distance;
        //    public int estimatedDistance;
        //    bool horizointal = false;
        //    public Point(int x, int y, int value)
        //    {
        //        this.x = x;
        //        this.y = y;
        //        this.value = value;
        //        distance = int.MaxValue;
        //        estimatedDistance = int.MaxValue;
        //    }
            
        //}

        class State : IEquatable<State>
        {

            public int x;
            public int y;
            public (int x, int y) direction;
            public int SameDirection;
            public int distance;
            public int estimatedDistance;

            public State(int x, int y, (int x, int y) direction, int Samedirection) { 
                this.x = x;
                this.y = y;
                this.direction = direction;
                this.SameDirection = Samedirection;
                distance = int.MaxValue; 
                estimatedDistance = int.MaxValue;

            }

            public bool Equals(State otherState)
            {
                return ((this.x == otherState.x) && (this.y == otherState.y) && (this.direction == otherState.direction) && this.SameDirection == otherState.SameDirection);
            }
            public List<State> GetNextSteps()
            {
                List<State> NextSteps = new List<State>();

                switch (direction) {
                    //Right
                    case (1, 0):
                        {
                            NextSteps.Add(new(x, y + 1, (0,  1), 1));
                            NextSteps.Add(new(x, y - 1, (0, -1), 1));
                            if (SameDirection < 3)
                            {
                                NextSteps.Add(new(x+1, y, (1, 0), SameDirection + 1));
                            }
                            break;
                        }
                    //Left
                    case (-1, 0):
                        {
                            NextSteps.Add(new(x, y + 1, (0,  1), 1));
                            NextSteps.Add(new(x, y - 1, (0, -1), 1));
                            if (SameDirection < 3)
                            {
                                NextSteps.Add(new(x - 1, y, (-1, 0), SameDirection +1));
                            }
                            break;
                        }
                    //Down
                    case (0, 1):
                        {
                            NextSteps.Add(new(x - 1, y, (-1, 0), 1));
                            NextSteps.Add(new(x + 1, y, ( 1, 0), 1));
                            if (SameDirection < 3)
                            {
                                NextSteps.Add(new(x, y + 1, (0, 1), SameDirection + 1));
                            }
                            break;
                        }
                    //UP
                    case (0, -1):
                        {
                            NextSteps.Add(new(x - 1, y, (-1, 0), 1));
                            NextSteps.Add(new(x + 1, y, (1,  0), 1));
                            if (SameDirection < 3)
                            {
                                NextSteps.Add(new(x, y - 1, (0, -1), SameDirection + 1));
                            }
                            break;
                        }


                }

                return NextSteps;
            }

            public List<State> GetNextStepsP2()
            {
                List<State> NextSteps = new List<State>();
                if (SameDirection < 10)
                {
                    NextSteps.Add(new(x + direction.x, y + direction.y, direction, SameDirection + 1));
                }
                if (SameDirection < 4)
                {
                    
                    return NextSteps;
                }
              

                switch (direction)
                {
                    //Right
                    case (1, 0):
                        {
                            if (y+4 < Height) { NextSteps.Add(new(x, y + 1, (0, 1), 1)); }
                            if (y - 4 >= 0) { NextSteps.Add(new(x, y - 1, (0, -1), 1)); }
                            
                            break;
                        }
                    //Left
                    case (-1, 0):
                        {
                            
                            
                            if (y + 4 < Height) { NextSteps.Add(new(x, y + 1, (0, 1), 1)); }
                            if (y - 4 >= 0) { NextSteps.Add(new(x, y - 1, (0, -1), 1)); }

                            break;
                        }
                    //Down
                    case (0, 1):
                        {
                            
                          
                            
                            if (x - 4 >= 0) { NextSteps.Add(new(x - 1, y, (-1, 0), 1)); }
                            if (x + 4 < Width) { NextSteps.Add(new(x + 1, y, (1, 0), 1)); }
                            break;
                        }
                    //UP
                    case (0, -1):
                        {
                            if (x - 4 >= 0) { NextSteps.Add(new(x - 1, y, (-1, 0), 1)); }
                            if (x + 4 < Width) { NextSteps.Add(new(x + 1, y, (1, 0), 1)); }
                            
                            break;
                        }


                }

                return NextSteps;
            }

            public override int GetHashCode()
            {
                return (this.y * Width + this.x + this.SameDirection);
            }

            public override string ToString()
            {
                string something= "";
                something = "x: " + x.ToString() + " y: " + y.ToString() + " Distance: " + distance.ToString();
                return something;
            }

        }

        public int Part1() 
        {
            int answer = 0;
           
            int hNumber = 30;
            for (int y = 0;y < Height;y ++){

                for (int x = 0;x < Width; x++){
                    map[x, y] = Int32.Parse(Lines[y][x].ToString());
                }
            }

            HashSet<State>openList = new HashSet<State>();
            HashSet<State>closedList = new HashSet<State>();
            PriorityQueue<State, int> priorityList = new PriorityQueue<State, int>();

            State Start = new(0, 0, (1, 0), 0);
            Start.distance = 0;
            Start.estimatedDistance = 0;

            openList.Add(Start);


            priorityList.Enqueue(Start, Start.distance);
            bool endReached = false;

            while (openList.Count > 0 && !endReached)
            {
                State current = priorityList.Dequeue();
                //Console.WriteLine(current.x + "," + current.y + ": " + openList.Count + "," + closedList.Count);
                foreach (State neighbourState in current.GetNextSteps())
                {
                    if(neighbourState.x <0  || neighbourState.x >= Width || neighbourState.y < 0 || neighbourState.y >= Height) continue;
                    int neighbourDistance = current.distance + map[neighbourState.x, neighbourState.y];
                    if (neighbourState.x == Width - 1 && neighbourState.y == Height -1 )
                    {
                       // Console.WriteLine("Found output" + neighbourDistance);
                        endReached = true;
                        answer = neighbourDistance;
                        break;
                    }
                    int estimatedNeighbourTotalDistance = neighbourDistance + (((Width - neighbourState.x) +(Height - neighbourState.y)) * hNumber);
                    if (openList.Contains(neighbourState))
                    {
                        openList.TryGetValue(neighbourState, out State PreviousVisit);
                        if (estimatedNeighbourTotalDistance < PreviousVisit.estimatedDistance) 
                        {
                            openList.Remove(PreviousVisit);
                             
                        }
                        else
                        {
                            continue;
                        }
                    }
                    if (closedList.Contains(neighbourState))
                    {
                        closedList.TryGetValue(neighbourState, out State PreviousVisit);
                        if (estimatedNeighbourTotalDistance < PreviousVisit.estimatedDistance)
                        
                        {
                            closedList.Remove(PreviousVisit);
                        }
                        else
                        {
                            continue;
                        }
                    }
                    neighbourState.distance = neighbourDistance;
                    neighbourState.estimatedDistance = estimatedNeighbourTotalDistance;
                    openList.Add(neighbourState);
                    priorityList.Enqueue(neighbourState, neighbourState.distance);
                    }
                closedList.Add(current);
                openList.Remove(current);
            }

            return answer;
        }

        public int Part2()
        {


            int answer = 0;

            int hNumber = 2;

            HashSet<State> openList = new HashSet<State>();
            HashSet<State> closedList = new HashSet<State>();
            PriorityQueue<State, int> priorityList = new PriorityQueue<State, int>();

            State Start1 = new(0, 0, (1, 0), 0);
            State Start2 = new(0, 0, (0, 1), 0);
            Start1.distance = 0;
            Start1.estimatedDistance = 0;
            Start2.distance = 0;
            Start2.estimatedDistance = 0;

            openList.Add(Start1);
            openList.Add(Start2);


            priorityList.Enqueue(Start1, Start1.distance);
            priorityList.Enqueue(Start2, Start2.distance);


            bool endReached = false;

            while (openList.Count > 0 && !endReached)
            {
                State current = priorityList.Dequeue();
                //Console.WriteLine(current.x + "," + current.y + ": " + openList.Count + "," + closedList.Count);
                foreach (State neighbourState in current.GetNextStepsP2())
                {
                    if (neighbourState.x < 0 || neighbourState.x >= Width || neighbourState.y < 0 || neighbourState.y >= Height) continue;
                    int neighbourDistance = current.distance + map[neighbourState.x, neighbourState.y];
                    if (neighbourState.x == Width - 1 && neighbourState.y == Height - 1 && neighbourState.SameDirection >= 4)
                    {
                        // Console.WriteLine("Found output" + neighbourDistance);
                        endReached = true;
                        answer = neighbourDistance;
                        break;
                    }
                    int estimatedNeighbourTotalDistance = neighbourDistance + (((Width - neighbourState.x) + (Height - neighbourState.y)) * hNumber);
                    if (openList.Contains(neighbourState))
                    {
                        openList.TryGetValue(neighbourState, out State PreviousVisit);
                        if (estimatedNeighbourTotalDistance < PreviousVisit.estimatedDistance)
                        {
                            openList.Remove(PreviousVisit);

                        }
                        else
                        {
                            continue;
                        }
                    }
                    if (closedList.Contains(neighbourState))
                    {
                        closedList.TryGetValue(neighbourState, out State PreviousVisit);
                        if (estimatedNeighbourTotalDistance < PreviousVisit.estimatedDistance)

                        {
                            closedList.Remove(PreviousVisit);
                        }
                        else
                        {
                            continue;
                        }
                    }
                    neighbourState.distance = neighbourDistance;
                    neighbourState.estimatedDistance = estimatedNeighbourTotalDistance;
                    openList.Add(neighbourState);
                    priorityList.Enqueue(neighbourState, neighbourState.estimatedDistance);
                }
                closedList.Add(current);
                openList.Remove(current);
            }

            return answer;
        }
       

    }
}
