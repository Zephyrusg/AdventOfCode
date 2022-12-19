using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Resources;
using System.Runtime.Serialization;
using System.Runtime.Versioning;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

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
            public int[] GeodeRobotCost = new int[] { 0, 0, 0 };
            public int OreMax;
          

            public BluePrint(string BlueprintText)
            {
                var Digits = Regex.Matches(BlueprintText, "\\d+");
                ID  = Int32.Parse(Digits[0].Value);
                OreRobotCosts[0] = Int32.Parse(Digits[1].Value);
                ClayRobotCosts[0] = Int32.Parse(Digits[2].Value);
                ObsidianRobotCost[0] = Int32.Parse(Digits[3].Value);
                ObsidianRobotCost[1] = Int32.Parse(Digits[4].Value);
                GeodeRobotCost[0] = Int32.Parse(Digits[5].Value);
                GeodeRobotCost[2] = Int32.Parse(Digits[6].Value);
                OreMax = Math.Max(Math.Max(OreRobotCosts[0], ClayRobotCosts[0]), Math.Max(ObsidianRobotCost[0], GeodeRobotCost[0]));

              
            }


        }

        class State {
            public static List<int> MaxResultsOverTime= new List<int>();                
            public static Dictionary<State,int> UniqueStates = new Dictionary<State, int>();
            public int[] Robots;
            public int[] Resources;
            public int Time;

            public State(int[] Robots, int[] Recources, int time) {

                this.Robots = Robots;
                this.Resources = Recources;
                this.Time = time;
            
            }

            public override bool Equals(object? obj)
            {
                return obj is State state &&
                       Robots[0] == state.Robots[0] &&
                       Robots[1] == state.Robots[1] &&
                       Robots[2] == state.Robots[0] &&
                       Robots[3] == state.Robots[0] &&
                       Resources[0] == state.Resources[0] &&
                       Resources[1] == state.Resources[1] &&
                       Resources[2] == state.Resources[2] &&
                       Resources[3] == state.Resources[3] &&
                       Time == state.Time;
                       
            }

            public override int GetHashCode()
            {
                HashCode Hash = new HashCode();
                Hash.Add(Robots[0]);
                Hash.Add(Robots[1]);
                Hash.Add(Robots[2]);
                Hash.Add(Robots[3]);
                Hash.Add(Resources[0]);
                Hash.Add(Resources[1]);
                Hash.Add(Resources[2]);
                Hash.Add(Resources[3]);
                Hash.Add(Time);
                return Hash.ToHashCode();
            }
        }
        static int DoSimulate(State CurrentState, BluePrint CurrentBuildPrint, int MaxTime)
        {
           
            //if (State.UniqueStates.Contains(CurrentState))
            //{

            //    State Compare;
            //    State.UniqueStates.TryGetValue(CurrentState, out Compare);
            //    if (Compare.Time < CurrentState.Time)
            //    {
            //        return 0;
            //    }
            //    else if (Compare.Time > CurrentState.Time)
            //    {

            //        State.UniqueStates.Remove(CurrentState);
            //        State.UniqueStates.Add(CurrentState);
            //    }


            //}
            //else {
            //    State.UniqueStates.Add(CurrentState);
            //}

            int[] Robots = CurrentState.Robots;
            int[] Resources = CurrentState.Resources;
            int Time = CurrentState.Time;
            bool BuyGeodeRobot = false;
            bool BuyObsidianRobot = false;
            bool BuyOreRobot = false;
            bool BuyClayRobot = false;
            bool SomethingtoBuy = (new bool[]{ BuyGeodeRobot, BuyObsidianRobot, BuyOreRobot, BuyClayRobot}).Any(b=> b == true);



            if (Time == MaxTime)
            {
                return Resources[3];
            }
            //if (State.UniqueStates.ContainsKey(CurrentState))
            //{
            //    return State.UniqueStates[CurrentState];
            //}
            if (State.MaxResultsOverTime[Time] > CurrentState.Resources[3] + (CurrentState.Robots[3] + (MaxTime - Time)) * (MaxTime - Time))
            {
                return 0;
            }
            int[] Addition = new int[] { Robots[0], Robots[1], Robots[2], Robots[3] };

            List<int> NumberOfMaxGeode = new List<int>();
            NumberOfMaxGeode.Add(0);
            if (CurrentBuildPrint.OreRobotCosts[0] <= Resources[0] && (Resources[0] < (MaxTime - Time) * CurrentBuildPrint.OreMax))
            {
                BuyOreRobot = true;

            }
            if (CurrentBuildPrint.ClayRobotCosts[0] <= Resources[0] && (Resources[1] < (MaxTime - Time) * CurrentBuildPrint.ObsidianRobotCost[1]))
            {
                BuyClayRobot = true;
            }
            if ((CurrentBuildPrint.ObsidianRobotCost[0] <= Resources[0]) && (CurrentBuildPrint.ObsidianRobotCost[1] <= Resources[1]) && (Resources[2] < (MaxTime - Time) * CurrentBuildPrint.GeodeRobotCost[2]))
            {
                BuyObsidianRobot = true;
            }

            if (CurrentBuildPrint.GeodeRobotCost[0] <= Resources[0] && CurrentBuildPrint.GeodeRobotCost[2] <= Resources[2])
            {
                BuyGeodeRobot = true;
            }
           
            

            if (BuyGeodeRobot)
            {

                State State = new State(

                    new int[] { Robots[0], Robots[1], Robots[2], Robots[3] + 1 },
                    new int[] { Resources[0] - CurrentBuildPrint.GeodeRobotCost[0] + Addition[0], Resources[1] + Addition[1], Resources[2] - CurrentBuildPrint.GeodeRobotCost[2] + Addition[2], Resources[3] + Addition[3] },
                    Time + 1
                    );
                NumberOfMaxGeode.Add(DoSimulate(State, CurrentBuildPrint, MaxTime));

            }
            else if (BuyObsidianRobot)
                {
                    State State = new State(
                        new int[] { Robots[0], Robots[1], Robots[2] + 1, Robots[3] },
                        new int[] { Resources[0] - CurrentBuildPrint.ObsidianRobotCost[0] + Addition[0], Resources[1] - CurrentBuildPrint.ObsidianRobotCost[1] + Addition[1], Resources[2] + Addition[2], Resources[3] + Addition[3] },
                        Time + 1
                        );
                    NumberOfMaxGeode.Add(DoSimulate(State, CurrentBuildPrint, MaxTime));
                }
            else 
            { 
                if (BuyClayRobot)
                {
                    State State = new State(
                        new int[] { Robots[0], Robots[1] + 1, Robots[2], Robots[3] },
                        new int[] { Resources[0] - CurrentBuildPrint.ClayRobotCosts[0] + Addition[0], Resources[1] + Addition[1], Resources[2] + Addition[2], Resources[3] + Addition[3] },
                        Time + 1
                        );
                    NumberOfMaxGeode.Add(DoSimulate(State, CurrentBuildPrint, MaxTime));
                }

                if (BuyOreRobot)
                {
                    State State = new(
                        new int[] { Robots[0] + 1, Robots[1], Robots[2], Robots[3] },
                        new int[] { Resources[0] - CurrentBuildPrint.OreRobotCosts[0] + Addition[0], Resources[1] + Addition[1], Resources[2] + Addition[2], Resources[3] + Addition[3] },
                        Time + 1
                        );
                    NumberOfMaxGeode.Add(DoSimulate(State, CurrentBuildPrint, MaxTime));
                }
                if (Resources[0] <= CurrentBuildPrint.OreMax)
                {
                    State State = new State(
                        (int[])Robots.Clone(),
                        new int[] { Resources[0] + Addition[0], Resources[1] + Addition[1], Resources[2] + Addition[2], Resources[3] + Addition[3] },
                        Time + 1);
                    NumberOfMaxGeode.Add(DoSimulate(State, CurrentBuildPrint, MaxTime));
                }
            }        
            int result = NumberOfMaxGeode.Max();
            if (State.MaxResultsOverTime[Time] < CurrentState.Resources[3])
                State.MaxResultsOverTime[Time] = CurrentState.Resources[3];
            //State.UniqueStates[CurrentState] = result;
            return result;
            
        }
        static string Path = ".\\2022\\Input\\InputDay19.txt";
        static string[] Data = File.ReadAllLines(Path);
        public static int Part1()
        {
            int answer = 0;

            //Bueprints in laden
         
            foreach (var line in Data)
            {
                BluePrint CurrentBuildPrint = new BluePrint(line);
                //State.UniqueStates = new Dictionary<State, int>();

                int[] Robots = new int[] { 1, 0, 0, 0 };
                int[] Resources = new int[] { 0, 0, 0, 0 };

                State StartState = new State(Robots, Resources, 0);
                State.MaxResultsOverTime = new List<int>(new int[24]);

                int Result = DoSimulate(StartState, CurrentBuildPrint, 24);
                answer += Result * CurrentBuildPrint.ID;
                Console.WriteLine("Blueprint: " + CurrentBuildPrint.ID + " With Result: " + Result + " Done.");

            }



            return answer;
        }

   
        public static int Part2()
        {
            int answer = 0;
            var lines = Data.Take(3);
            int[] Result = new int[3];
            foreach (var line in lines)
            {
                BluePrint CurrentBuildPrint = new BluePrint(line);

                int[] Robots = new int[] { 1, 0, 0, 0 };
                int[] Resources = new int[] { 0, 0, 0, 0 };

                State StartState = new State(Robots, Resources, 0);
                State.MaxResultsOverTime = new List<int>(new int[32]);
                
                Result[CurrentBuildPrint.ID - 1] = DoSimulate(StartState, CurrentBuildPrint, 32);
                Console.WriteLine("Blueprint: " + CurrentBuildPrint.ID + " With Result: " + Result[CurrentBuildPrint.ID - 1] + " Done.");
                
                
                State.UniqueStates = new Dictionary<State, int>();

            }
            answer = Result[0] * Result[1] * Result[2];


            return answer;
        }
    }
}
