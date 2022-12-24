using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022
{

    internal class Day24
    {
        class Blizzard
        {
            public char Direction;
            public (int x, int y) Location;

            public Blizzard(char direction, (int x, int y) location)
            {
                Direction = direction;
                Location = location;
            }
            public void MoveBlizzard()
            {

                switch (Direction)
                {

                    case '<':
                        {
                            if (Location.x - 1 > 0)
                            {
                                Location = (Location.x - 1, Location.y);
                            }
                            else
                            {
                                Location = (width - 1, Location.y);
                            }
                            break;
                        }
                    case '>':
                        {
                            if (Location.x + 1 < width)
                            {
                                Location = (Location.x + 1, Location.y);
                            }
                            else
                            {
                                Location = (1, Location.y);
                            }
                            break;
                        }
                    case 'v':
                        {
                            if (Location.y + 1 < height)
                            {
                                Location = (Location.x, Location.y + 1);
                            }
                            else
                            {
                                Location = (Location.x, 1);
                            }
                            break;
                        }
                    case '^':
                        {
                            if (Location.y - 1 > 0)
                            {
                                Location = (Location.x, Location.y - 1);
                            }
                            else
                            {
                                Location = (Location.x, height - 1);
                            }
                            break;
                        }

                }

            }
        }

        class State{
            public static int BestResult = int.MaxValue;
            public int Minutes;
            public HashSet<Blizzard> AllBlizzards;
            public(int x, int y) CurrentLocation;
            public State(HashSet<Blizzard> Blizzards, int minutes, (int x, int y)CurrentLocation) {
                AllBlizzards = Blizzards; 
                Minutes= minutes;
                this.CurrentLocation = CurrentLocation;
            }

        }

        
        static int MakeMove(State CurrentState) {

            int Minutes = CurrentState.Minutes;
            (int x, int y) CurrentLocation = CurrentState.CurrentLocation;

            if ((CurrentLocation.x, CurrentLocation.y + 1) == Exit) {
                return CurrentState.Minutes + 1;
            }

            if (CurrentState.Minutes > State.BestResult) {
                return int.MaxValue;
            }
            HashSet<Blizzard> NewBlizzardsSate = new HashSet<Blizzard>();
            NewBlizzardsSate.UnionWith(CurrentState.AllBlizzards);
            
            foreach (Blizzard Blizzard in NewBlizzardsSate) { 
                Blizzard.MoveBlizzard();
            }

            if (!NewBlizzardsSate.Any(Bliz => Bliz.Location == (CurrentLocation.x + 1, CurrentLocation.y)) && CurrentLocation.x + 1 < width && CurrentLocation.y > 0)
            {
                State.BestResult = Math.Min(State.BestResult, MakeMove(new State(NewBlizzardsSate, Minutes + 1, (CurrentLocation.x + 1, CurrentLocation.y))));
            }// Right

            if (!NewBlizzardsSate.Any(Bliz => Bliz.Location == (CurrentLocation.x, CurrentLocation.y + 1)) && CurrentLocation.y + 1 < height)
            {
                State.BestResult = Math.Min(State.BestResult, MakeMove(new State(NewBlizzardsSate, Minutes + 1, (CurrentLocation.x, CurrentLocation.y + 1))));
            }// Down

            if (!NewBlizzardsSate.Any(Bliz => Bliz.Location == CurrentLocation) && CurrentLocation.x > 0 && CurrentLocation.y > 0) {
                State.BestResult = Math.Min(State.BestResult,MakeMove(new State(NewBlizzardsSate, Minutes + 1, CurrentLocation)));
            }// Not Moving

            if (!NewBlizzardsSate.Any(Bliz => Bliz.Location == (CurrentLocation.x - 1, CurrentLocation.y)) && CurrentLocation.x - 1 > 0 && CurrentLocation.y > 0) {
                State.BestResult = Math.Min(State.BestResult, MakeMove(new State(NewBlizzardsSate, Minutes + 1, (CurrentLocation.x - 1, CurrentLocation.y))));
            } // Left

            if (!NewBlizzardsSate.Any(Bliz => Bliz.Location == (CurrentLocation.x, CurrentLocation.y - 1)) && CurrentLocation.y - 1 > 0) 
            {
                State.BestResult = Math.Min(State.BestResult, MakeMove(new State(NewBlizzardsSate, Minutes + 1, (CurrentLocation.x , CurrentLocation.y - 1))));
            } // UP


            return State.BestResult;
        }

        static int width;
        static int height;
        static (int x, int y) Exit;
        static string Path = ".\\2022\\Input\\InputDay24.txt";
        static string Data = File.ReadAllText(Path);
        public static int Part1()
        {
            int answer = 0;
            
            Char[] BlizardTypes = new char[] { '<', '>', '^', 'v' };
            HashSet<Blizzard> StartBlizzardState= new HashSet<Blizzard>();
            
            string[] lines = Data.Split("\r\n");
            Exit = (lines[0].Length - 2, lines.Count() - 1);
            width= lines[0].Length - 1;
            height= lines.Count() - 1;
            for (int y = 0; y < lines.Length; y++) { 
                for (int x = 0; x < lines[0].Length; x++) {
                    if (BlizardTypes.Contains(lines[y][x])) {
                        StartBlizzardState.Add(new Blizzard(lines[y][x], (x, y)));
                    }
                }
            }
            State StartState = new State(StartBlizzardState, 0, (1,0));
            answer = MakeMove(StartState);
            

            return answer;
        }

   
        public static int Part2()
        {
            int answer = 0;

            return answer;
        }
    }
}
