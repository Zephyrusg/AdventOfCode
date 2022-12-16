using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode._2022
{
    internal class Day16
    {
        static string Path = ".\\2022\\Input\\InputDay16.txt";
        static string[] Data = File.ReadAllLines(Path);
        class Valve
        {
            public string ValveName;
            public int ValvePressure;
            public string[] PossiblePaths;
            public Valve(string Name, int Pressure, string[] Paths)
            {
                ValveName = Name;
                ValvePressure = Pressure;
                PossiblePaths = Paths;

            }
        }

        class State {
            public int Minutes;
            public string ValveName;
            public List<string> OpenValves;
            public int Flow;
            public int Total;

            public State(int minutes, string valvename, List<string> openvalves, int flow, int total) {
                Minutes = minutes;
                ValveName = valvename;
                OpenValves = openvalves;
                Flow = flow;
                Total = total;
            }
            public int GetFlow() {
                return (Total + (30 - Minutes + 1) * Flow);
            }
        }

        class PathWay {
            public string PathTo;
            public int Step;

            public PathWay(string pathto, int step) {
                PathTo = pathto;
                Step = step;
            }
        }
        static Dictionary<string, Valve> AllValves = new Dictionary<string, Valve>();

        static int GetDistancetoValve(string Start, string End) {
            if (Start == End) {
                return 0;
            }
            List<string> Recorded = new List<string>();
            Recorded.Add(Start);
            Queue<PathWay> ToVisit = new Queue<PathWay>();
            foreach (string Pathto in AllValves[Start].PossiblePaths) {

                Recorded.Add(Pathto);
                ToVisit.Enqueue(new PathWay(Pathto, 1));
            }
            while (ToVisit.Count > 0) {
                PathWay CurrentPath = ToVisit.Dequeue();
                if (CurrentPath.PathTo == End) {
                    return CurrentPath.Step;
                }
                foreach (string PathTo in AllValves[CurrentPath.PathTo].PossiblePaths) {

                    if (!Recorded.Contains(PathTo)) {
                        Recorded.Add(PathTo);
                        ToVisit.Enqueue(new PathWay(PathTo, CurrentPath.Step + 1));
                    }
                }
            }
            Console.WriteLine("Useless Return :P");
            return 9999; 

        }

        public static int Part1()
        {
            
            foreach (string line in Data)
            {
                string ValveName = line.Substring(6, 2);
                Match number = Regex.Match(line, "\\d+");
                string[] parts = line.Split("valves ");
                if (parts.Length == 1)
                {
                    parts = line.Split("valve ");
                }
                int valvepressure = Int32.Parse(number.Value);
                if (parts[1].Length > 2)
                {
                    string[] Paths = parts[1].Split(", ");
                    AllValves.Add(ValveName, new Valve(ValveName, valvepressure, Paths));
                }
                else
                {
                    string[] Paths = new string[1];
                    Paths[0] = (parts[1]);
                    AllValves.Add(ValveName, new Valve(ValveName, valvepressure, Paths));
                }

            }
            List<string> GoodValves = AllValves.Where(V => V.Value.ValvePressure > 0).Select(P => P.Value.ValveName).ToList();

            Dictionary<string, Dictionary<string, int>> Distances = new Dictionary<string, Dictionary<string, int>>();
            foreach (string Valve in AllValves.Keys) {
                Distances[Valve] = new Dictionary<string, int>();
                foreach (string target in GoodValves)
                {

                    Distances[Valve][target] = GetDistancetoValve(Valve, target);

                }

            }
            State StartState = new State(1, "AA", new List<string>(), 0, 0);
            int max_flow = 0;
            Stack<State> States = new Stack<State>();
            States.Push(StartState);
            while (States.Count > 0)
            {
                State state = States.Pop();
                max_flow = Math.Max((int)max_flow, state.GetFlow());
                Valve valve = AllValves[state.ValveName];
                if (state.Minutes == 30) {
                    continue;
                }
                if ((!state.OpenValves.Contains(state.ValveName) && valve.ValvePressure > 0) ){
                    
                    List<string> templist = new List<string>();
                    templist.Add(state.ValveName);
                    templist = templist.Concat(state.OpenValves).ToList();
                    States.Push(
                    new State(state.Minutes + 1, state.ValveName, templist, state.Flow + valve.ValvePressure,
                                    state.Total + state.Flow));
                    continue;
                }
                foreach (string DestinationValve in GoodValves) {
                    if (state.OpenValves.Contains(DestinationValve)){
                        continue;
                    }
                    int distance = Distances[state.ValveName][DestinationValve];
                    if ((state.Minutes + distance) > 29) {
                        continue;
                    }
                    States.Push(new State(state.Minutes + distance, DestinationValve, state.OpenValves, state.Flow,
                                        state.Total + (state.Flow * distance)));
                }
                
            }

            return max_flow;
        }



        public static int Part2()
        {
            string[] Data = File.ReadAllLines(Path);
            int answer = 0;

            List<string> GoodValves = AllValves.Where(V => V.Value.ValvePressure > 0).Select(P => P.Value.ValveName).ToList();

            Dictionary<string, Dictionary<string, int>> Distances = new Dictionary<string, Dictionary<string, int>>();
            foreach (string Valve in AllValves.Keys)
            {
                Distances[Valve] = new Dictionary<string, int>();
                foreach (string target in GoodValves)
                {

                    Distances[Valve][target] = GetDistancetoValve(Valve, target);

                }

            }




            return answer;
        }
    }
   
}