using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022
{
    
internal class Day23
    {
        class Elf
        {
            public static HashSet<(int x, int y)> AllElfsLocations = new HashSet<(int x, int y)>();
            public static HashSet<(int x, int y)> ValidProposedMoves = new HashSet<(int x, int y)>();
            public static Queue<char> ToProposMoves = new Queue<char>(new List<char> { 'N', 'S', 'W', 'E' });
            //public Queue<char> ToProposMoves = new Queue<char>(new List<char> { 'N', 'S', 'W', 'E' });
            public (int x, int y) CurrentLocation;
            public (int x, int y) ?ProposedLocation = null;
            public bool PossibleMove = true;

            public static void Printmap() {
                int xMax = Elf.AllElfsLocations.MaxBy(e => e.x).x;
                int xMin = Elf.AllElfsLocations.MinBy(e => e.x).x;
                int yMax = Elf.AllElfsLocations.MaxBy(e => e.y).y;
                int yMin = Elf.AllElfsLocations.MinBy(e => e.y).y;
                for (int y = yMin; y <= yMax; y++)
                {
                    string line = "";
                    for (int x = xMin; x <= xMax; x++) {
                        if (AllElfsLocations.Contains((x, y)))
                        {
                            line += "#";
                        }
                        else {
                            line += ".";
                        }
                    }
                    Console.WriteLine(line);
                }
            
            }
            public Elf((int x, int y) currentLocation)
            {
                CurrentLocation = currentLocation;
                AllElfsLocations.Add(currentLocation);
            }

            public bool CanMove() {

                if (
                    (!AllElfsLocations.Contains((CurrentLocation.x,     CurrentLocation.y + 1))) &&
                    (!AllElfsLocations.Contains((CurrentLocation.x - 1, CurrentLocation.y + 1))) &&
                    (!AllElfsLocations.Contains((CurrentLocation.x + 1, CurrentLocation.y + 1))) &&
                    (!AllElfsLocations.Contains((CurrentLocation.x,     CurrentLocation.y - 1))) &&
                    (!AllElfsLocations.Contains((CurrentLocation.x - 1, CurrentLocation.y - 1))) &&
                    (!AllElfsLocations.Contains((CurrentLocation.x + 1, CurrentLocation.y - 1))) &&
                    (!AllElfsLocations.Contains((CurrentLocation.x - 1, CurrentLocation.y    ))) &&
                    (!AllElfsLocations.Contains((CurrentLocation.x + 1, CurrentLocation.y    )))
                    ){
                    ProposedLocation = null;
                    PossibleMove = false;
                    return false;
                }

                return true;
            }
            public void ProposeMove() {

                List<char> OrderProposeMoves = ToProposMoves.ToList();
                int i = 0;
                bool ProposedLocation = false; 
                while (!ProposedLocation && i < 4) {
                    switch(OrderProposeMoves[i]) {
                        case 'N': {
                                if (
                                    (!AllElfsLocations.Contains((CurrentLocation.x, CurrentLocation.y - 1))) &&
                                    (!AllElfsLocations.Contains((CurrentLocation.x - 1, CurrentLocation.y - 1))) &&
                                    (!AllElfsLocations.Contains((CurrentLocation.x + 1, CurrentLocation.y - 1))))
                                {
                                    this.ProposedLocation = (CurrentLocation.x, CurrentLocation.y - 1);
                                    ProposedLocation = true;
                                    break;

                                }
                                break;
                            }
                        case 'S':
                            {
                                if (
                                    (!AllElfsLocations.Contains((CurrentLocation.x,     CurrentLocation.y + 1))) &&
                                    (!AllElfsLocations.Contains((CurrentLocation.x - 1, CurrentLocation.y + 1))) &&
                                    (!AllElfsLocations.Contains((CurrentLocation.x + 1, CurrentLocation.y + 1))))
                                {
                                    this.ProposedLocation = (CurrentLocation.x, CurrentLocation.y + 1);
                                    ProposedLocation = true;
                                    break;
                                }
                                break;
                            }
                        case 'W':
                            {
                                if (
                                    (!AllElfsLocations.Contains((CurrentLocation.x - 1, CurrentLocation.y - 1))) &&
                                    (!AllElfsLocations.Contains((CurrentLocation.x - 1, CurrentLocation.y    ))) &&
                                    (!AllElfsLocations.Contains((CurrentLocation.x - 1, CurrentLocation.y + 1))))
                                {
                                    this.ProposedLocation = (CurrentLocation.x - 1, CurrentLocation.y);
                                    ProposedLocation = true;
                                    break;
                                }
                                break;
                            }
                        case 'E':
                            {
                                if (
                                    (!AllElfsLocations.Contains((CurrentLocation.x + 1, CurrentLocation.y + 1))) &&
                                    (!AllElfsLocations.Contains((CurrentLocation.x + 1, CurrentLocation.y    ))) &&
                                    (!AllElfsLocations.Contains((CurrentLocation.x + 1, CurrentLocation.y - 1))))
                                {
                                    this.ProposedLocation = (CurrentLocation.x + 1, CurrentLocation.y);
                                    ProposedLocation = true;
                                    break;
                                }
                                break;
                            }
                    }
                    i++;
                }
                if (this.ProposedLocation != null)
                {
                    if (!ValidProposedMoves.Contains(((int x, int y))this.ProposedLocation))
                    {
                        ValidProposedMoves.Add(((int x, int y))this.ProposedLocation);
                    }
                    else
                    {
                        ValidProposedMoves.Remove(((int x, int y))this.ProposedLocation);
                    }
                }
                
            }
            public void ProcessMove() {
                if (ProposedLocation != null) { 
                    if (ValidProposedMoves.Contains(((int x, int y))this.ProposedLocation)) {
                        CurrentLocation = ((int x, int y))this.ProposedLocation;
                        AllElfsLocations.Add(CurrentLocation);
                        PossibleMove = true;
                    }
                    else
                    {
                        AllElfsLocations.Add(CurrentLocation);
                        PossibleMove = true;
                    }
                    ProposedLocation = null;
                }
                else
                {
                    AllElfsLocations.Add(CurrentLocation);
                }
                
            }
        }
        static List<Elf> AllElfs = new List<Elf>();
        static string Path = ".\\2022\\Input\\InputDay23.txt";
        static string Data = File.ReadAllText(Path);
        public static int Part1()
        {
            string[] lines = Data.Split("\r\n");
            

            for(int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[0].Length; x++) {
                    if (lines[y][x] == '#')
                    {
                        AllElfs.Add(new Elf((x, y)));
                    }
                }
            }
            int Times = 10;
            while (Times > 0) {
                Elf.ValidProposedMoves = new HashSet<(int x, int y)>();
                foreach(Elf elf in AllElfs)
                {
                    if (elf.CanMove()) {
                        elf.ProposeMove();
                        
                    }
                }
                Elf.AllElfsLocations = new HashSet<(int x, int y)>();
                foreach (Elf elf in AllElfs) {
                    elf.ProcessMove();
                }
                char ToProposMove = Elf.ToProposMoves.Dequeue();
                Elf.ToProposMoves.Enqueue(ToProposMove);
                Times--;

                //Console.WriteLine("Round: " + Times);
                //Elf.Printmap();
            }
            int xMax = Elf.AllElfsLocations.MaxBy(e => e.x).x + 1;
            int xMin = Elf.AllElfsLocations.MinBy(e => e.x).x;
            int yMax = Elf.AllElfsLocations.MaxBy(e => e.y).y + 1;
            int yMin = Elf.AllElfsLocations.MinBy(e => e.y).y ;
            int Xlength = xMax + Math.Abs(xMin);
            int Ylength = yMax + Math.Abs(yMin);

            return (Xlength * Ylength) - AllElfs.Count;
        }

   
        public static int Part2()
        {
            //AllElfs = new List<Elf>();
            //Elf.ToProposMoves = new Queue<char>(new List<char> { 'N', 'S', 'W', 'E' });
            //string[] lines = Data.Split("\r\n");


            //for (int y = 0; y < lines.Length; y++)
            //{
            //    for (int x = 0; x < lines[0].Length; x++)
            //    {
            //        if (lines[y][x] == '#')
            //        {
            //            AllElfs.Add(new Elf((x, y)));
            //        }
            //    }
            //}
            int Times = 10;
            bool Nomove = false;
            while (!Nomove)
            {
                Elf.ValidProposedMoves = new HashSet<(int x, int y)>();
                foreach (Elf elf in AllElfs)
                {
                    if (elf.CanMove())
                    {
                        elf.ProposeMove();
                    }
                }
                Elf.AllElfsLocations = new HashSet<(int x, int y)>();
                foreach (Elf elf in AllElfs)
                {
                    elf.ProcessMove();
                }
                char ToProposMove = Elf.ToProposMoves.Dequeue();
                Elf.ToProposMoves.Enqueue(ToProposMove);
                Times++;
                if (AllElfs.All(e => e.PossibleMove == false)) { 
                    Nomove= true;
                }
                //Console.WriteLine("Round: " + Times);
                //Elf.Printmap();
                
            }


            return Times;
        }
    }
}
