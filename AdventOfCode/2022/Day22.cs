using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode._2022
{
    
    internal class Day22
    {
        enum Field
        {
            Path, Wall
        
        };
        
        static HashSet<(int x,int y)> AllLoctions = new HashSet<(int x,int y)> ();
        static Dictionary<(int x, int y), Field> AllLocationFields = new Dictionary<(int x, int y), Field>();
        static string Path = ".\\2022\\Input\\InputDay22.txt";
        static string Data = File.ReadAllText(Path);

        static (int x, int y) MoveRight((int x, int y) CurrentPosition, int Steps){

            while(Steps != 0)
            {
                (int x, int y) Next = (CurrentPosition.x + 1, CurrentPosition.y);
                if (AllLoctions.Contains(Next)) 
                {
                    if (AllLocationFields[Next] != Field.Wall)
                    {

                        CurrentPosition = Next;
                    }
                    else {
                        break;
                    }
                }
                else {
                    Next = (AllLoctions.Where(loc => loc.y == CurrentPosition.y).MinBy(loc => loc.x));
                    if (AllLocationFields[Next] != Field.Wall)
                    {

                        CurrentPosition = Next;
                    }
                    else
                    {
                        break;
                    }
                }

                Steps--;
            }
            return CurrentPosition;
        }
        static (int x, int y) MoveLeft((int x, int y) CurrentPosition, int Steps)
        {

            while (Steps != 0)
            {
                (int x, int y) Next = (CurrentPosition.x - 1, CurrentPosition.y);
                if (AllLoctions.Contains(Next))
                {
                    if (AllLocationFields[Next] != Field.Wall)
                    {

                        CurrentPosition = Next;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    Next = (AllLoctions.Where(loc => loc.y == CurrentPosition.y).MaxBy(loc => loc.x));
                    if (AllLocationFields[Next] != Field.Wall)
                    {

                        CurrentPosition = Next;
                    }
                    else
                    {
                        break;
                    }
                }

                Steps--;
            }
            return CurrentPosition;
        }
        static (int x, int y) MoveUp((int x, int y) CurrentPosition, int Steps)
        {

            while (Steps != 0)
            {
                (int x, int y) Next = (CurrentPosition.x, CurrentPosition.y - 1);
                if (AllLoctions.Contains(Next))
                {
                    if (AllLocationFields[Next] != Field.Wall)
                    {

                        CurrentPosition = Next;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    Next = (AllLoctions.Where(loc => loc.x == CurrentPosition.x).MaxBy(loc => loc.y));
                    if (AllLocationFields[Next] != Field.Wall)
                    {

                        CurrentPosition = Next;
                    }
                    else
                    {
                        break;
                    }
                }

                Steps--;
            }
            return CurrentPosition;
        }
        static (int x, int y) MoveDown((int x, int y) CurrentPosition, int Steps)
        {

            while (Steps != 0)
            {
                (int x, int y) Next = (CurrentPosition.x, CurrentPosition.y + 1);
                if (AllLoctions.Contains(Next))
                {
                    if (AllLocationFields[Next] != Field.Wall)
                    {

                        CurrentPosition = Next;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    Next = (AllLoctions.Where(loc => loc.x == CurrentPosition.x).MinBy(loc => loc.y));
                    if (AllLocationFields[Next] != Field.Wall)
                    {

                        CurrentPosition = Next;
                    }
                    else
                    {
                        break;
                    }
                }

                Steps--;
            }
            return CurrentPosition;
        }


        public static int Part1()
        {
            int answer = 0;

            string MapText = Data.Split("\r\n\r\n")[0];
            string Directions = Data.Split("\r\n\r\n")[1];

            string[] Map = MapText.Split("\r\n");

            for (int y = 0; y < Map.Length; y++)
            {
                for (int x = 0; x < Map[y].Length; x++) {
                

                    switch (Map[y][x])
                    {
                        case '.':
                            {
                                AllLocationFields.Add((x, y), Field.Path);
                                AllLoctions.Add((x, y));
                                break;
                            }
                        case '#': {
                                AllLocationFields.Add((x, y), Field.Wall);
                                AllLoctions.Add((x, y));
                                break;
                            }
                        case ' ':
                            {
                                break;
                            }
                    }
                }
            }
            int Direction = 0;

            Directions = 'R' + Directions;
            List<string> DirectionMatches = Regex.Matches(Directions, "(R|L)\\d{1,2}").Select(M=>M.Value).ToList();
            int Steps = 0;
            (int x, int y) CurrentLocation = AllLoctions.Where(loc => loc.y == 0).MinBy(loc => loc.x);
            foreach(string Move in DirectionMatches) {

                if (Move[0] == 'R')
                {
                    Direction += 90;
                    if (Direction == 360)
                    {
                        Direction = 0;
                    }
                }
                else {
                    Direction -= 90;
                    if(Direction < 0)
                    {
                        Direction = 270;
                    }
                    
                }
                Steps = int.Parse(Regex.Match(Move, "\\d{1,2}").Value);
                switch(Direction)
                {
                    case 0:
                        {
                            CurrentLocation = MoveUp(CurrentLocation, Steps); 
                            break;
                        }
                    case 90:
                        {
                            CurrentLocation = MoveRight(CurrentLocation, Steps);
                            break;
                        }
                    case 180:
                        {
                            CurrentLocation = MoveDown(CurrentLocation, Steps);
                            break;
                        }
                    case 270:
                        { 
                            CurrentLocation = MoveLeft(CurrentLocation, Steps);
                            break; 
                        }
                }
            }

            if (Direction == 0)
            {
                Direction = 3;
            }
            else if (Direction == 90)
            {
                Direction = 0;
            }
            else if (Direction == 180)
            {
                Direction = 1;
            }
            else {
                Direction = 2;
            }


            return ((1000 * (CurrentLocation.y + 1)) + (4 * (CurrentLocation.x + 1) + Direction ));
        }

   
        public static int Part2()
        {
            int answer = 0;

            return answer;
        }
    }
}
