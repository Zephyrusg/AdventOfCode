using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022
{
    internal class Day24
    {

		static public (int x, int y, char d) MoveBlizzard ((int x, int y, char d) Location)
		{
			(int x, int y, char d) NewLocation = (0,0,'<');
			switch (Location.d)
			{

				case '<':
					{
						if (Location.x - 1 > 0)
						{
							NewLocation = (Location.x - 1, Location.y, Location.d);
						}
						else
						{
							NewLocation = (width - 1, Location.y, Location.d);
						}
						break;
					}
				case '>':
					{
						if (Location.x + 1 < width)
						{
							NewLocation = (Location.x + 1, Location.y, Location.d);
						}
						else
						{
							NewLocation = (1, Location.y, Location.d);
						}
						break;
					}
				case 'v':
					{
						if (Location.y + 1 < height)
						{
							NewLocation = (Location.x, Location.y + 1, Location.d);
						}
						else
						{
							NewLocation = (Location.x, 1, Location.d);
						}
						break;
					}
				case '^':
					{
						if (Location.y - 1 > 0)
						{
							NewLocation = (Location.x, Location.y - 1, Location.d);
						}
						else
						{
							NewLocation = (Location.x, height - 1, Location.d);
						}
						break;
					}

			}
			return NewLocation;

		}

		static void MoveTo((int x, int y) Destination){
			PriorityQueue<(int x, int y, int t), int> States = new PriorityQueue<(int x, int y, int t), int>();
			(int x, int y, int t) State = CurrentLocation;
			//int Distance = Math.Abs(Destination.x - State.x) + Math.Abs(Destination.y - State.y);
			States.Enqueue(State, State.t);
			HashSet<(int x, int y, int t)> Recorded = new HashSet<(int x, int y, int t)>();

			while(States.Count > 0)
			{
				State = States.Dequeue();

				if (State.x == Destination.x && State.y == Destination.y) {
					break;
				}
				if (Recorded.Contains(State))
				{
					continue;
				}
				else {
					Recorded.Add(State);
				}
				HashSet<(int x, int y)> BlizzardLocations = (from Blizzard in BlizzardsStates[State.t + 1] select (Blizzard.x, Blizzard.y)).ToHashSet();

				if (!BlizzardLocations.Contains((State.x, State.y)) && Map[State.y,State.x] == ".") 
				{
					//Distance = Math.Abs(Destination.x - State.x) + Math.Abs(Destination.y - State.y);
					States.Enqueue((State.x, State.y, State.t + 1), State.t + 1);
				}
				if (!BlizzardLocations.Contains((State.x + 1, State.y)) && Map[State.y, State.x + 1] == ".")
				{
					//Distance = Math.Abs(Destination.x - (State.x + 1)) + Math.Abs(Destination.y - State.y);
					States.Enqueue((State.x + 1, State.y, State.t + 1), State.t + 1);
				}
				if (!BlizzardLocations.Contains((State.x - 1, State.y)) && Map[State.y, State.x - 1] == ".")
				{
					//Distance = Math.Abs(Destination.x - (State.x - 1)) + Math.Abs(Destination.y - State.y);
					States.Enqueue((State.x - 1, State.y, State.t + 1), State.t + 1);
				}
				if (!BlizzardLocations.Contains((State.x, State.y + 1)) && (State.y < height) && Map[State.y + 1, State.x] == ".")
				{
					//Distance = Math.Abs(Destination.x - State.x) + Math.Abs(Destination.y - (State.y + 1));
					States.Enqueue((State.x, State.y + 1, State.t + 1), State.t + 1);
				}
				if (!BlizzardLocations.Contains((State.x, State.y - 1)) && (State.y >= 1) && Map[State.y - 1, State.x] == ".")
				{
					//Distance = Math.Abs(Destination.x - State.x) + Math.Abs(Destination.y  - (State.y - 1));
					States.Enqueue((State.x, State.y - 1, State.t + 1), State.t + 1);
				}

			}
			CurrentLocation = State;
		}


		static (int x, int y, int t) CurrentLocation;
		static int width;
		static int height;
		static List<List<(int x, int y, char d)>> BlizzardsStates = new List<List<(int x, int y, char d)>>();
        static string[,] Map;
		static (int x, int y) Exit;
		static (int x, int y) Start;
		static string Path = ".\\2022\\Input\\InputDay24.txt";
        static string Data = File.ReadAllText(Path);
        public static int Part1()
        {
            string[] lines = Data.Split("\r\n");
            Map = new string[lines.Length,lines[0].Length];
			width = lines[0].Length - 1;
			height = lines.Count() - 1;
			CurrentLocation = (1, 0, 0);
			Exit = (lines[0].Length - 2, lines.Count() - 1);
			Start = (1, 0);

			for (int y = 0; y < lines.Length; y++) {
                for (int x = 0; x < lines[0].Length; x++)
                {
                    if (lines[y][x] == '.' || lines[y][x] == '#')
                    {
                        Map[y, x] = lines[y][x].ToString();
                    }
                    else {
                        Map[y, x] = ".";
                    }
                }

            }

			List<(int x, int y, char d)> StartBlizzardsState = new List<(int x, int y, char d)>();

			for (int y = 0; y < lines.Length; y++)
			{
				for (int x = 0; x < lines[0].Length; x++)
				{
					if (lines[y][x] != '.' && lines[y][x] != '#')
					{
                        StartBlizzardsState.Add((x, y, lines[y][x]));
					}
				
				}

			}
            BlizzardsStates.Add(StartBlizzardsState);


            for(int t = 1; t < 1400; t++)
            {
				List<(int x, int y, char d)> BlizzardsState = new List<(int x, int y, char d)>();
				foreach ((int x, int y, char d) Blizzard in BlizzardsStates[t-1]) {
					BlizzardsState.Add(MoveBlizzard(Blizzard));	
				}
				BlizzardsStates.Add(BlizzardsState);

			}
			MoveTo(Exit);


			int answer = CurrentLocation.t;

            return answer;
        }

   
        public static int Part2()
        {
			MoveTo(Start);
			MoveTo(Exit);

            int answer = CurrentLocation.t;

            return answer;
        }
    }
}
