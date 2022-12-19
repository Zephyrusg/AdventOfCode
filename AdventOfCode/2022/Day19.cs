using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Runtime.Versioning;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode._2022
{
    internal class Day19
    {

        class BluePrint
        {
            public int ID = 0;
            public int[] OreRobotCosts = new int[] { 0, 0, 0 };
            public int[] ClayRobotCosts = new int[] { 0, 0, 0 };
            public int[] ObsidianRobotCost = new int[] { 0, 0, 0 };
            public int[] GeodeRpbotCost = new int[] { 0, 0, 0 };

            public BluePrint(string BlueprintText)
            {
                var Lines = BlueprintText.Split("\r\n");
                ID  = Int32.Parse(Regex.Match(Lines[0], "\\d+").Value);
                OreRobotCosts[0] = Int32.Parse(Regex.Match(Lines[1], "\\d+").Value);
                ClayRobotCosts[0] = Int32.Parse(Regex.Match(Lines[2], "\\d+").Value);
                ObsidianRobotCost[0] = Int32.Parse((Regex.Matches(Lines[3], "\\d+")[0]).Value);
                ObsidianRobotCost[1] = Int32.Parse((Regex.Matches(Lines[3], "\\d+")[1]).Value);
                GeodeRpbotCost[0] = Int32.Parse((Regex.Matches(Lines[4], "\\d+")[0]).Value);
                GeodeRpbotCost[2] = Int32.Parse((Regex.Matches(Lines[4], "\\d+")[1]).Value);

            }


        }

        class State {
            public int[] Robots = new int[4];
            public int[] Resources= new int[4];
            public int Time;

            public State(int[] Robots, int[] Recources, int time) {

                this.Robots = Robots;
                this.Resources = Recources;
                this.Time = time;
            
            }
        }
        static int DoSimulate(State CurrentState, BluePrint CurrentBuildPrint) {

            int result = 0;
            int[] Robots = CurrentState.Robots;
            int[] Resources = CurrentState.Resources;
            int Time = CurrentState.Time;
            Queue<State> ToCheck = new Queue<State>();

            while (Time < 25)
            {
                int[] ResourcesAtStart = (int[])Resources.Clone();
                int[] TempResources = new int[] { 0, 0, 0, 0 };
                int[] TempRobots = (int[])Robots.Clone();


                TempResources[0] = Robots[0] * 1;
                TempResources[1] = Robots[1] * 1;
                TempResources[2] = Robots[2] * 1;
                TempResources[3] = Robots[3] * 1;


                if (CurrentBuildPrint.GeodeRpbotCost[0] <= Resources[0] && CurrentBuildPrint.GeodeRpbotCost[2] <= Resources[2])
                {
                    Robots[3]++;
                    Resources[2] -= CurrentBuildPrint.GeodeRpbotCost[2];
                    Resources[0] -= CurrentBuildPrint.GeodeRpbotCost[0];

                }
                else if ((CurrentBuildPrint.ObsidianRobotCost[0] <= Resources[0]) && (CurrentBuildPrint.ObsidianRobotCost[1] <= Resources[1]))
                {
                    Robots[2]++;
                    Resources[1] -= CurrentBuildPrint.ObsidianRobotCost[1];
                    Resources[0] -= CurrentBuildPrint.ObsidianRobotCost[0];
                }
                else if (CurrentBuildPrint.ClayRobotCosts[0] <= Resources[0])
                {
                    Robots[1]++;
                    Resources[0] -= CurrentBuildPrint.ClayRobotCosts[0];
                }

                else if (CurrentBuildPrint.OreRobotCosts[0] <= Resources[0])
                {

                    Robots[0]++;
                    Resources[0] -= CurrentBuildPrint.OreRobotCosts[0];
                }

                Resources[0] += TempResources[0];
                Resources[1] += TempResources[1];
                Resources[2] += TempResources[2];
                Resources[3] += TempResources[3];

                if (Robots.Sum() > TempRobots.Sum())
                {
                    ResourcesAtStart[0] += TempResources[0];
                    ResourcesAtStart[1] += TempResources[1];
                    ResourcesAtStart[2] += TempResources[2];
                    ResourcesAtStart[3] += TempResources[3];
                    Time++;
                    ToCheck.Enqueue(new State(TempRobots, ResourcesAtStart, Time));

                }
                else {
                    Time++;
                }



            }
            if (ToCheck.Count > 0){ 
                while (ToCheck.Count > 0) {

                    State State = ToCheck.Dequeue();
                    result = Math.Max(Resources[3], DoSimulate(State, CurrentBuildPrint));

                }
            }
            return Math.Max(Resources[3],result);
        }

        static string Path = ".\\2022\\Input\\InputDay19.txt";
        static string Data = File.ReadAllText(Path);
        public static int Part1()
        {
            int answer = 0;

            //Bueprints in laden
            var BluePrints = Data.Split("\r\n\r\n");
            foreach (var BluePrintText in BluePrints)
            {
                BluePrint CurrentBuildPrint = new BluePrint(BluePrintText);

                int[] Robots = new int[] { 1, 0, 0, 0 };
                int[] Resources = new int[] { 0, 0, 0, 0 };

                State StartState = new State(Robots, Resources, 1);


                int Result = DoSimulate(StartState, CurrentBuildPrint);
                answer += Result * CurrentBuildPrint.ID;


            }



            return answer;
        }

   
        public static int Part2()
        {
            int answer = 0;

            return answer;
        }
    }
}
