using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022
{
    internal class Day15
    {
        static string Path = ".\\2022\\Input\\InputDay15.txt";
        static string[] Data = File.ReadAllLines(Path);
        static HashSet<(int x, int y)> BlockedLocations = new HashSet<(int x, int y)>();
        
        class Sensor
        {
            public static bool Part1 = true;
            static public int YtoCheck = 2000000;
            public (int x, int y) SensorLocation;
            public (int x, int y) BeaconLocation;
            public HashSet<int> BlockedXCoordinate = new HashSet<int>();
            public int Scope = 0;
            public Sensor((int x, int y) Sensor, (int x, int y) Beacon) {
                SensorLocation = Sensor;
                BeaconLocation = Beacon;
                if (Part1)
                {
                    ListBlockedLocations();
                }
                else {
                    Scope = Math.Abs(SensorLocation.x - BeaconLocation.x) + Math.Abs(SensorLocation.y - BeaconLocation.y);
                }
            }
        
            void ListBlockedLocations() {
                int StepsAway = Math.Abs(SensorLocation.x - BeaconLocation.x) + Math.Abs(SensorLocation.y - BeaconLocation.y);


                if ((SensorLocation.y + StepsAway > YtoCheck) || (SensorLocation.y - StepsAway < YtoCheck)) {
                    int NeededXSteps = StepsAway - Math.Abs(SensorLocation.y - YtoCheck);

                    BlockedXCoordinate.Add(SensorLocation.x);
                    for (int x = 1; x <= NeededXSteps; x++)
                    {
                        BlockedXCoordinate.Add(SensorLocation.x - x);
                        BlockedXCoordinate.Add(SensorLocation.x + x);
                    }
                }
            }

            public bool IsLocationinScope(int x, int y) {

                int AbsScannertoPointx = Math.Abs(SensorLocation.x - x);
                int AbsscannertoPointy = Math.Abs(SensorLocation.y - y);
                if (AbsScannertoPointx + AbsscannertoPointy <= Scope) {
                    return true;
                }
                else {
                    return false;
                }    
            }


        }
        public static int Part1()
        {
           

            List<Sensor> SensorList = new List<Sensor>();
            foreach (string line in Data)
            {
                string LineParse = line.Replace("Sensor at ", "");
                string[] LineParseParts = LineParse.Split(": closest beacon is at ");
                string[] Sensorparts = LineParseParts[0].Split(",");
                string[] Beaconparts = LineParseParts[1].Split(",");
                int Sensorx = Int32.Parse(Sensorparts[0].Replace("x=", ""));
                int Sensory = Int32.Parse(Sensorparts[1].Replace("y=", ""));
                int Beaconx = Int32.Parse(Beaconparts[0].Replace("x=", ""));
                int Beacony = Int32.Parse(Beaconparts[1].Replace("y=", ""));
                SensorList.Add(new Sensor((Sensorx, Sensory), (Beaconx, Beacony)));
            }
            HashSet<int> BlockedLocations = new HashSet<int>();
            List<(int x, int y)> TestSet = new List<(int x, int y)>();
            foreach (Sensor sensor in SensorList)
            {
                BlockedLocations.UnionWith(sensor.BlockedXCoordinate);

            }

            foreach (Sensor sensor in SensorList)
            {
                if (sensor.BeaconLocation.y == Sensor.YtoCheck)
                {
                    if (BlockedLocations.Contains(sensor.BeaconLocation.x))
                    {
                        BlockedLocations.Remove(sensor.BeaconLocation.x);
                    }

                }
                if (sensor.SensorLocation.y == Sensor.YtoCheck)
                {
                    if (BlockedLocations.Contains(sensor.SensorLocation.x))
                    {
                        BlockedLocations.Remove(sensor.SensorLocation.x);
                    }

                }
            }

            return BlockedLocations.Count;
        }


        public static long Part2()
        {
            string[] Data = File.ReadAllLines(Path);
            long answer = 0;
            Sensor.Part1 = false;
            List<Sensor> SensorList = new List<Sensor>();
            foreach (string line in Data)
            {
                string LineParse = line.Replace("Sensor at ", "");
                string[] LineParseParts = LineParse.Split(": closest beacon is at ");
                string[] Sensorparts = LineParseParts[0].Split(",");
                string[] Beaconparts = LineParseParts[1].Split(",");
                int Sensorx = Int32.Parse(Sensorparts[0].Replace("x=", ""));
                int Sensory = Int32.Parse(Sensorparts[1].Replace("y=", ""));
                int Beaconx = Int32.Parse(Beaconparts[0].Replace("x=", ""));
                int Beacony = Int32.Parse(Beaconparts[1].Replace("y=", ""));
                SensorList.Add(new Sensor((Sensorx, Sensory), (Beaconx, Beacony)));
            }

            bool FreeLocationFound = false;
            int x = 0;
            int y = 0;
          
            for (y = 0; y <= 4000000;y++)
            {
                for (x = 0; x <= 4000000; x++)
                {
                    bool isnotfree = false;
                    foreach (Sensor sensor in SensorList)
                    {
                        isnotfree = sensor.IsLocationinScope(x, y);
                        if (isnotfree) {
                            int AbsscannertoPointy = Math.Abs(sensor.SensorLocation.y - y);
                            int stepx = sensor.Scope - AbsscannertoPointy;
                            x = stepx + sensor.SensorLocation.x ;
                            break;
                        }
                    }
                    if (!isnotfree) { 
                        FreeLocationFound= true;
                        break;
                    } 
                }
                if (FreeLocationFound == true) break;
            }
            answer = (long)x * 4000000 + (long)y; 


            return answer;

        }
         
            

    }
}
            

           
        
    

