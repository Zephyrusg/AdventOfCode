using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class Y2023D23
    {
        public static string[] Map = File.ReadAllLines(".\\2023\\Input\\inputDay23.txt");
        static int Height = Map.Count();
        static int Width = Map[0].Length;
        static bool P2 = false;
        static Dictionary<(int x, int y), int> MaxDistances = new Dictionary<(int x, int y), int>();
        static int Pathsfind = 0;

        static bool IsValidMove((int x, int y) nextlocation, (int x, int y) direction, (int x, int y) PreviousDirection, HashSet<(int x, int y)> visited) 
        {
            if(nextlocation.x == -1 || nextlocation.y == -1 || nextlocation.x == Width || nextlocation.y == Height) {  return false; }
            switch (direction)
            {
                case (0,1):

                    { 
                        if(PreviousDirection == (0, -1)){ return false; }
                        break;        
                    }
                case (-1, 0):
                    {
                        if (PreviousDirection == (1, 0)) { return false; }
                        break;
                    }
                case (1,0):
                    {
                        if (PreviousDirection == (-1, 0)) { return false; }
                        break;
                    }
                case (0,-1):
                    {
                        if (PreviousDirection == (0, 1)) { return false; }
                        break;
                    }
            }

            if (visited.Contains(nextlocation)){ return false; }

            char tile = Map[nextlocation.y][nextlocation.x];

            if (tile == '#')
            {
                return false;
            }

            if (tile == '.')
            {
                return true;
            }

            
            switch (tile)
            {
                case '^':
                    return direction == (0, -1);
                case 'v':
                    return direction == (0, 1);
                case '<':
                    return direction == (-1, 0);
                case '>':
                    return direction == (1, 0);
            }
            

            return true;
        }

        static bool IsValidMove2((int x, int y) nextlocation, (int x, int y) direction, (int x, int y) PreviousDirection)
        {
            if (nextlocation.x == -1 || nextlocation.y == -1 || nextlocation.x == Width || nextlocation.y == Height) { return false; }
            switch (direction)
            {
                case (0, 1):

                    {
                        if (PreviousDirection == (0, -1)) { return false; }
                        break;
                    }
                case (-1, 0):
                    {
                        if (PreviousDirection == (1, 0)) { return false; }
                        break;
                    }
                case (1, 0):
                    {
                        if (PreviousDirection == (-1, 0)) { return false; }
                        break;
                    }
                case (0, -1):
                    {
                        if (PreviousDirection == (0, 1)) { return false; }
                        break;
                    }
            }

            char tile = Map[nextlocation.y][nextlocation.x];

            if (tile == '#')
            {
                return false;
            }

            if (tile == '.')
            {
                return true;
            }

            return true;
        }

        static int DoStep((int x, int y) currentLocation, int distance,(int x, int y) PreviousDirection, HashSet<(int x,int y)>visited)
       {
            int MaxSteps = 0;

            if (currentLocation == (Width - 2, Height - 1))
            {
                return distance; 
            }

            if (MaxDistances.TryGetValue(currentLocation, out int maxDistance) && distance <= maxDistance)
            {
                return 0; 
            }

            MaxDistances[currentLocation] = distance;

            HashSet<(int x, int y)> tempvisted = new();
            tempvisted.UnionWith(visited);
            List<(int x, int y)> directions = new() { (0, 1), (1, 0), (-1, 0), (0, -1) };
            int ValidSteps = 0;
            foreach (var direction in directions)
            {


                (int x, int y) NextLocation = (currentLocation.x + direction.x, currentLocation.y + direction.y);


                if (IsValidMove(NextLocation, direction, PreviousDirection, tempvisted))
                {
                    ValidSteps++;
                    tempvisted.Add(NextLocation);
                    MaxSteps = Math.Max(MaxSteps, DoStep(NextLocation,  distance + 1, direction, tempvisted));
                    visited.Remove(NextLocation);

                }
                
            }

            //if (ValidSteps == 0) {
            //    return 0;
            //}



            return MaxSteps;
        }

        static bool IsSplitPoint((int x, int y) location, (int x, int y) PreviousDirection) => ((((int x, int y)[])([PreviousDirection, (((int x, int y))(PreviousDirection.y, -PreviousDirection.x)), (((int x, int y))(-PreviousDirection.y, PreviousDirection.x))])).Where(o => Map[location.y + o.y][location.x + o.x] != '#').ToList().Count >= 2);

        static int DoStep2((int x, int y) currentLocation, int distance, (int x, int y) PreviousDirection, HashSet<(int x, int y)> Splitpoints)
        {
            int MaxSteps = 0;
            List<(int x, int y)> directions = new() { (0, 1), (1, 0), (-1, 0), (0, -1) };

        

            while (!IsSplitPoint(currentLocation, PreviousDirection))
            {

                //if (MaxDistances.TryGetValue(currentLocation, out int maxDistance) && distance <= maxDistance)
                //{
                //    return 0;
                //}

                //MaxDistances[currentLocation] = distance;

                foreach (var direction in directions)
                {
                    (int x, int y) NextLocation = (currentLocation.x + direction.x, currentLocation.y + direction.y);
                    if (IsValidMove2(NextLocation, direction, PreviousDirection))
                    {
                        currentLocation = NextLocation;
                        distance = distance + 1;
                        PreviousDirection = direction;

                        break;
                    }


                }

                if (currentLocation == (Width - 2, Height - 1))
                {
                    Pathsfind++;
                    Console.WriteLine("Number Path: " + Pathsfind + " Found a Distance:" + distance );
                    return distance;
                }
            }
            HashSet<(int x, int y)> TempSplitPoints = new();
            TempSplitPoints.UnionWith(Splitpoints);
            if (TempSplitPoints.Contains(currentLocation))
            {
                return 0;
            }
            else
            {
                TempSplitPoints.Add(currentLocation);
            }

            foreach (var direction in directions)
            {
                (int x, int y) NextLocation = (currentLocation.x + direction.x, currentLocation.y + direction.y);

                if (IsValidMove2(NextLocation, direction, PreviousDirection))
                {
                    MaxSteps = Math.Max(MaxSteps, DoStep2(NextLocation, distance + 1, direction, TempSplitPoints));

                }
            }
            
            return MaxSteps;
        }
        public int Part1() 
        {
            int answer = 0;

            (int x, int y) StartLocation = (1, 0);

            HashSet<(int x, int y)> visited = new();
            visited.Add(StartLocation);

            answer = DoStep(StartLocation, 0,(0,1), visited);

            return answer;
        }

        public int Part2()
        {
            int answer = 0;
            P2 = true;

            MaxDistances = new();

            (int x, int y) StartLocation = (1, 1);

            HashSet<(int x, int y)> SplitPoints = new();

            
            answer = DoStep2(StartLocation, 1, (0, 1), SplitPoints);


            return answer;
        }

    }
}
