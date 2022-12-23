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
            public (int x, int y) CurrentLocation;
            public (int x, int y) ?ProposedLocation = null;


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
                    (!AllElfsLocations.Contains((CurrentLocation.x - 1, CurrentLocation.y + 1))) &&
                    (!AllElfsLocations.Contains((CurrentLocation.x + 1, CurrentLocation.y + 1))) &&
                    (!AllElfsLocations.Contains((CurrentLocation.x - 1, CurrentLocation.y    ))) &&
                    (!AllElfsLocations.Contains((CurrentLocation.x + 1, CurrentLocation.y    )))
                    ){
                    ProposedLocation = null;
                    return false;
                }

                return true;
            }
            public void ProposeMove() {

                List<char> OrderProposeMoves = ToProposMoves.ToList();
                int i = 0;
                bool ProposedLocation = false; 
                while (!ProposedLocation) {
                    switch(OrderProposeMoves[i]) {
                        case 'N': {
                                if (
                                    (!AllElfsLocations.Contains((CurrentLocation.x,     CurrentLocation.y - 1))) &&
                                    (!AllElfsLocations.Contains((CurrentLocation.x - 1, CurrentLocation.y - 1))) &&
                                    (!AllElfsLocations.Contains((CurrentLocation.x + 1, CurrentLocation.y - 1)))) 
                                {
                                    break;
                                }
                                this.ProposedLocation = (CurrentLocation.x, CurrentLocation.y - 1);
                                ProposedLocation = true;
                                break;
                            }
                        case 'S':
                            {
                                if (
                                    (!AllElfsLocations.Contains((CurrentLocation.x,     CurrentLocation.y + 1))) &&
                                    (!AllElfsLocations.Contains((CurrentLocation.x - 1, CurrentLocation.y + 1))) &&
                                    (!AllElfsLocations.Contains((CurrentLocation.x + 1, CurrentLocation.y + 1))))
                                {
                                    break;
                                }
                                this.ProposedLocation = (CurrentLocation.x, CurrentLocation.y + 1);
                                ProposedLocation = true;
                                break;
                            }
                        case 'W':
                            {
                                if (
                                    (!AllElfsLocations.Contains((CurrentLocation.x - 1, CurrentLocation.y - 1))) &&
                                    (!AllElfsLocations.Contains((CurrentLocation.x - 1, CurrentLocation.y    ))) &&
                                    (!AllElfsLocations.Contains((CurrentLocation.x - 1, CurrentLocation.y + 1))))
                                {
                                    break;
                                }
                                this.ProposedLocation = (CurrentLocation.x - 1, CurrentLocation.y);
                                ProposedLocation = true;
                                break;
                            }
                        case 'E':
                            {
                                if (
                                    (!AllElfsLocations.Contains((CurrentLocation.x + 1, CurrentLocation.y + 1))) &&
                                    (!AllElfsLocations.Contains((CurrentLocation.x + 1, CurrentLocation.y    ))) &&
                                    (!AllElfsLocations.Contains((CurrentLocation.x + 1, CurrentLocation.y - 1))))
                                {
                                    break;
                                }
                                this.ProposedLocation = (CurrentLocation.x + 1, CurrentLocation.y);
                                ProposedLocation = true;
                                break;
                            }
                    }
                    i++;
                }
                if (!ValidProposedMoves.Contains(((int x, int y))this.ProposedLocation))
                {
                    ValidProposedMoves.Add(((int x, int y))this.ProposedLocation);
                }
                else { 
                    ValidProposedMoves.Remove(((int x, int y))this.ProposedLocation);
                }
                
            }
            public void ProcessMove() {
                if (ValidProposedMoves.Contains(((int x, int y))this.ProposedLocation)){
                    CurrentLocation = ((int x, int y))this.ProposedLocation;
                    AllElfsLocations.Add(CurrentLocation);
                }
                else
                {
                    AllElfsLocations.Add(CurrentLocation);
                }
                ProposedLocation = null;
            }
        }
        static List<Elf> AllElfs = new List<Elf>();
        static string Path = ".\\2022\\Input\\InputDay23.txt";
        static string Data = File.ReadAllText(Path);
        public static int Part1()
        {
           
            int answer = 0;

            return answer;
        }

   
        public static int Part2()
        {
            int answer = 0;

            return answer;
        }
    }
}
