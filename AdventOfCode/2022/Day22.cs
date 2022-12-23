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
        static Dictionary<(int x, int y), int> SiteCollection = new Dictionary<(int x, int y), int>();
        static int Direction = 0;
        static string Path = ".\\2022\\Input\\InputDay22.txt";
        static string Data = File.ReadAllText(Path);
        static bool Wall = false;
        static bool Firststar = true;

        static (int x, int y) GetNextMove((int x, int y) CurrentPosition ){
            int site = SiteCollection[CurrentPosition];
            (int x, int y) NextPosition = (0,0);

            if (site == 1)
            {
                if (Direction == 0)
                {
                    Direction = 90;
                    NextPosition = (0, CurrentPosition.x + 100);
                }
                else if (Direction == 270)
                {
                    Direction = 90;
                    NextPosition = (0, 149 - CurrentPosition.y);
                }
            }
            else if (site == 2)
            {
                if (Direction == 0)
                {
                    Direction = 0;
                    NextPosition = (CurrentPosition.x - 100, 199);
                }
                else if (Direction == 90)
                {
                    Direction = 270;
                    NextPosition = (99, 149 - CurrentPosition.y);
                }
                else if (Direction == 180)
                {
                    Direction = 270;
                    NextPosition = (99, CurrentPosition.x - 50);
                }
            }
            else if (site == 3)
            {
                if (Direction == 90)
                {
                    Direction = 0;
                    NextPosition = (CurrentPosition.y + 50, 49);
                }
                else if (Direction == 270)
                {
                    Direction = 180;
                    NextPosition = (CurrentPosition.y - 50, 100);
                }
            }
            else if (site == 4)
            {
                if (Direction == 90)
                {
                    Direction = 270;
                    NextPosition = (149, 149 - CurrentPosition.y);
                }
                else if (Direction == 180)
                {
                    Direction = 270;
                    NextPosition = (49, CurrentPosition.x + 100);
                }
            }
            else if (site == 5)
            {
                if (Direction == 0)
                {
                    Direction = 90;
                    NextPosition = (50, CurrentPosition.x + 50);
                }
                else if (Direction == 270)
                {
                    Direction = 90;
                    NextPosition = (50, 149 - CurrentPosition.y);
                }

            }
            else if (site == 6)
            {
                if (Direction == 90)
                {
                    Direction = 0;
                    NextPosition = (CurrentPosition.y - 100, 149);

                }
                else if (Direction == 180)
                {
                    Direction = 180;
                    NextPosition = (CurrentPosition.x + 100, 0);
                }
                else if (Direction == 270)
                {
                    Direction = 180;
                    NextPosition = (CurrentPosition.y - 100, 0);
                }
            }
            else {
                Console.WriteLine("Error Site: " + site + " x: " + CurrentPosition.x + " y: " + CurrentPosition.y);
            }
            
            return NextPosition;
        
        }
        static (int x, int y) MoveRight((int x, int y) CurrentPosition){

            (int x, int y) Next = (CurrentPosition.x + 1, CurrentPosition.y);
            if (AllLoctions.Contains(Next)) 
            {
                if (AllLocationFields[Next] != Field.Wall)
                {
                    CurrentPosition = Next;
                }
                else {
                    Wall = true;
                }
            }
            else {
                if (Firststar)
                {
                    Next = (AllLoctions.Where(loc => loc.y == CurrentPosition.y).MinBy(loc => loc.x));
                }
                else 
                {
                    Next = GetNextMove(CurrentPosition);
                }
                
                if (AllLocationFields[Next] != Field.Wall)
                {
                    CurrentPosition = Next;
                }
                else
                {
                    Wall = true;
                }
            }            
            return CurrentPosition;
        }
        static (int x, int y) MoveLeft((int x, int y) CurrentPosition)
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
                    Wall = true;
                }
            }
            else
            {
                if (Firststar)
                {
                    Next = (AllLoctions.Where(loc => loc.y == CurrentPosition.y).MaxBy(loc => loc.x));
                }
                else { 
                    Next = GetNextMove(CurrentPosition);
                }
                if (AllLocationFields[Next] != Field.Wall)
                {
                    CurrentPosition = Next;
                }
                else
                {
                    Wall = true;
                }
            }
                
            return CurrentPosition;
        }
        static (int x, int y) MoveUp((int x, int y) CurrentPosition)
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
                    Wall = true;
                }
            }
            else
            {
                if (Firststar)
                {
                    Next = (AllLoctions.Where(loc => loc.x == CurrentPosition.x).MaxBy(loc => loc.y));
                }
                else 
                { 

                    Next= GetNextMove(CurrentPosition);
                }

                if (AllLocationFields[Next] != Field.Wall)
                {
                    CurrentPosition = Next;
                }
                else
                {
                    Wall = true;
                }
            }
          
            return CurrentPosition;
        }
        static (int x, int y) MoveDown((int x, int y) CurrentPosition)
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
                    Wall = true;
                }
            }
            else
            {
                if (Firststar)
                {
                    Next = (AllLoctions.Where(loc => loc.x == CurrentPosition.x).MinBy(loc => loc.y));
                }
                else
                {
                    Next= GetNextMove(CurrentPosition);
                }
                if (AllLocationFields[Next] != Field.Wall)
                {
                    CurrentPosition = Next;
                }
                else
                {
                    Wall = true;
                }
            }

            return CurrentPosition;
        }


        public static int Part1()
        {

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
                        case '#':
                            {
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
            Direction = 0;

            Directions = 'R' + Directions;
            List<string> DirectionMatches = Regex.Matches(Directions, "(R|L)\\d{1,2}").Select(M=>M.Value).ToList();
            int Steps = 0;
            int Count =0;
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
                Wall = false;
                Steps = int.Parse(Regex.Match(Move, "\\d{1,2}").Value);
                while(Steps > 0 && Wall == false){
                    switch (Direction)
                    {
                        case 0:
                            {
                                CurrentLocation = MoveUp(CurrentLocation);
                                break;
                            }
                        case 90:
                            {
                                CurrentLocation = MoveRight(CurrentLocation);
                                break;
                            }
                        case 180:
                            {
                                CurrentLocation = MoveDown(CurrentLocation);
                                break;
                            }
                        case 270:
                            {
                                CurrentLocation = MoveLeft(CurrentLocation);
                                break;
                            }
                    }
                    Steps--;
                }
                Count++;
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
            string MapText = Data.Split("\r\n\r\n")[0];
            string Directions = Data.Split("\r\n\r\n")[1];
            Firststar = false;
            string[] Map = MapText.Split("\r\n");

            for (int y = 0; y < Map.Length; y++)
            {
                for (int x = 0; x < Map[y].Length; x++)
                {


                    if ((x >= 50 && x <= 99) && (y >= 0 && y <= 49))
                    {
                        SiteCollection.Add((x, y), 1);
                    }
                    else if (x >= 100 && x <= 149 && y >= 0 && y <= 49)
                    {
                        SiteCollection.Add((x, y), 2);
                    }
                    else if (x >= 50 && x <= 99 && y >= 50 && y <= 99)
                    {
                        SiteCollection.Add((x, y), 3);
                    }
                    else if (x >= 50 && x <= 99 && y >= 100 && y <= 149)
                    {
                        SiteCollection.Add((x, y), 4);
                    }
                    else if (x >= 0 && x <= 49 && y >= 100 && y <= 149)
                    {
                        SiteCollection.Add((x, y), 5);
                    }
                    else if (x >= 0 && x <= 49 && y >= 150 && y <= 199)
                    {
                        SiteCollection.Add((x, y), 6);
                    }
                }
            }
            Direction = 0;

            Directions = 'R' + Directions;
            List<string> DirectionMatches = Regex.Matches(Directions, "(R|L)\\d{1,2}").Select(M => M.Value).ToList();
            int Steps = 0;
            (int x, int y) CurrentLocation = AllLoctions.Where(loc => loc.y == 0).MinBy(loc => loc.x);
            foreach (string Move in DirectionMatches)
            {

                if (Move[0] == 'R')
                {
                    Direction += 90;
                    if (Direction == 360)
                    {
                        Direction = 0;
                    }
                }
                else
                {
                    Direction -= 90;
                    if (Direction < 0)
                    {
                        Direction = 270;
                    }

                }
                Wall = false;
                Steps = int.Parse(Regex.Match(Move, "\\d{1,2}").Value);
                while (Steps > 0 && Wall == false)
                {
                    switch (Direction)
                    {
                        case 0:
                            {
                                CurrentLocation = MoveUp(CurrentLocation);
                                break;
                            }
                        case 90:
                            {
                                CurrentLocation = MoveRight(CurrentLocation);
                                break;
                            }
                        case 180:
                            {
                                CurrentLocation = MoveDown(CurrentLocation);
                                break;
                            }
                        case 270:
                            {
                                CurrentLocation = MoveLeft(CurrentLocation);
                                break;
                            }
                    }
                    Steps--;
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
            else
            {
                Direction = 2;
            }

            return ((1000 * (CurrentLocation.y + 1)) + (4 * (CurrentLocation.x + 1) + Direction));
        }
    }
}
