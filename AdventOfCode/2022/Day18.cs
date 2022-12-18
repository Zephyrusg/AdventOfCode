using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022
{
    internal class Day18
    {
        static HashSet<Vector3> AllLavaCubes = new HashSet<Vector3>();
        static HashSet<Vector3> NotTrappedCubes = new HashSet<Vector3>();
        static HashSet<Vector3> TrappedCubes = new HashSet<Vector3>();
        static public int CheckSides(Vector3 Location)
        {
            int EmptySides = 0;

            if (!AllLavaCubes.Contains((new Vector3(Location.X + 1, Location.Y, Location.Z)))) {
                EmptySides++;
            }
            if (!AllLavaCubes.Contains((new Vector3(Location.X + -1, Location.Y, Location.Z))))
            {
                EmptySides++;
            }
            if (!AllLavaCubes.Contains((new Vector3(Location.X, Location.Y + 1, Location.Z))))
            {
                EmptySides++;
            }
            if (!AllLavaCubes.Contains((new Vector3(Location.X, Location.Y - 1, Location.Z))))
            {
                EmptySides++;
            }
            if (!AllLavaCubes.Contains((new Vector3(Location.X, Location.Y, Location.Z + 1))))
            {
                EmptySides++;
            }
            if (!AllLavaCubes.Contains((new Vector3(Location.X, Location.Y, Location.Z - 1))))
            {
                EmptySides++;
            }


            return EmptySides;
        }

        static public List<Vector3> GetSides(Vector3 Location)
        {
            List<Vector3> List = new List<Vector3>();

            List.Add(new Vector3(Location.X + 1, Location.Y, Location.Z));
            List.Add(new Vector3(Location.X - 1, Location.Y, Location.Z));
            List.Add(new Vector3(Location.X, Location.Y + 1, Location.Z));
            List.Add(new Vector3(Location.X, Location.Y - 1, Location.Z));
            List.Add(new Vector3(Location.X, Location.Y, Location.Z + 1));
            List.Add(new Vector3(Location.X, Location.Y, Location.Z - 1));
            
            return List;
        }

        static public bool IsOutside(Vector3 point) {
            
            if (point.X < 0 || point.X >= 22 || point.Y < 0 || point.Y >= 22 || point.Z < 0 || point.Z >= 22) { 
                return true;
            }
            return false;
        }


        static public bool IsNotTrapped(Vector3 point) { 
            HashSet<Vector3> Recorded = new HashSet<Vector3>();
            Recorded.Add(point);
            Queue<Vector3> ToVisit = new Queue<Vector3>();
            ToVisit.Enqueue(point);
            while (ToVisit.Count > 0) {

                Vector3 CurrentPoint = ToVisit.Dequeue();
                if (CurrentPoint == new Vector3(0, 0, 0))
                {
                    NotTrappedCubes.UnionWith(Recorded);
                    return true;
                }
                foreach (Vector3 Side in GetSides(CurrentPoint))
                {
                    if (Recorded.Contains(Side)) 
                    {
                        continue;
                    }
                    if (IsOutside(Side) || NotTrappedCubes.Contains(Side))
                    {
                        NotTrappedCubes.UnionWith(Recorded);
                        return true;
                    }
                    if (TrappedCubes.Contains(Side)) {
                        TrappedCubes.UnionWith(Recorded);
                        return false;
                    }
                    if (!AllLavaCubes.Contains(Side)){
                        Recorded.Add(Side);
                        ToVisit.Enqueue(Side);
                    }
                }
            }
            TrappedCubes.UnionWith(Recorded);
            return false;

        }

        static string Path = ".\\2022\\Input\\InputDay18.txt";
        static string[] Data = File.ReadAllLines(Path);


        public static int Part1()
        {
            int answer = 0;

            foreach(string line in Data)
            {
                string[] Parts = line.Split(",");
                AllLavaCubes.Add((new Vector3(Int32.Parse(Parts[0]), Int32.Parse(Parts[1]), Int32.Parse(Parts[2]))));
            }

            foreach(Vector3 Cube in AllLavaCubes)
            {

                answer += CheckSides(Cube);
             
            }

            return answer;
        }

   
        public static int Part2()
        {
            int answer = 0;

            foreach (Vector3 Cube in AllLavaCubes) {
                foreach (Vector3 Side in GetSides(Cube)){
                    if ((!AllLavaCubes.Contains(Side)) && IsNotTrapped(Side)) {
                        answer++;
                    }
                       
                } 
            }
            return answer;
        }
    }
}
