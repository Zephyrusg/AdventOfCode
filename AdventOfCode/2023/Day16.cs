using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class Y2023D16
    {
        public static string[] Lines = File.ReadAllLines(".\\2023\\Input\\inputDay16.txt");
        static int Height = Lines.Count();
        static int Width = Lines[0].Length;

        public List<((int x, int y) location, (int x, int y) Direction)> MoveBeam(((int x, int y) location, (int x, int y) Direction) Beam, char CurrentPlace) { 

            List<((int x, int y) location, (int x, int y) Direction)> NewBeams = new();
            switch (CurrentPlace)
            {
                case '.':
                    Beam.location.x += Beam.Direction.x;
                    Beam.location.y += Beam.Direction.y;
                    NewBeams.Add(Beam);
                    break;
                case '/':

                    switch (Beam.Direction)
                    {
                        case (1, 0):
                            {

                                Beam.Direction = (0, -1);
                                break;

                            }
                        case (-1, 0):
                            {
                                Beam.Direction = (0, 1);
                                break;
                            }
                        case (0, 1):
                            {
                                Beam.Direction = (-1, 0);
                                break;
                            }
                        case (0, -1):
                            {
                                Beam.Direction = (1, 0);
                                break;
                            }
                    }
                    Beam.location = (Beam.location.x + Beam.Direction.x, Beam.location.y + Beam.Direction.y);
                    NewBeams.Add(Beam);
                    break;
                case '\\':

                    switch (Beam.Direction)
                    {
                        case (1, 0):
                            {
                                Beam.Direction = (0, 1);
                            }
                            break;
                        case (-1, 0):
                            {
                                Beam.Direction = (0, -1);
                                break;
                            }
                        case (0, 1):
                            {
                                Beam.Direction = (1, 0);
                                break;
                            }
                        case (0, -1):
                            {
                                Beam.Direction = (-1, 0);
                                break;
                            }
                    }

                    Beam.location = (Beam.location.x + Beam.Direction.x, Beam.location.y + Beam.Direction.y);
                    NewBeams.Add(Beam);

                    break;
                case '|':
                    if (Beam.Direction == (0, 1) || Beam.Direction == (0, -1))
                    {
                        Beam.location = (Beam.location.x + Beam.Direction.x, Beam.location.y + Beam.Direction.y);
                        NewBeams.Add(Beam);
                    }
                    else
                    {
                        (int x, int y) Up = (0, 1);
                        (int x, int y) Down = (0, -1);

                        ((int x, int y) location, (int x, int y) Direction) BeamUp = ((Beam.location.x + Up.x, Beam.location.y + Up.y), (Up));
                        ((int x, int y) location, (int x, int y) Direction) BeamDown = ((Beam.location.x + Down.x, Beam.location.y + Down.y), (Down));
                        NewBeams.Add(BeamUp);
                        NewBeams.Add((BeamDown));
                    }

                    break;
                case '-':
                    if (Beam.Direction == (1, 0) || Beam.Direction == (-1, 0))
                    {
                        Beam.location = (Beam.location.x + Beam.Direction.x, Beam.location.y + Beam.Direction.y);
                        NewBeams.Add(Beam);
                    }
                    else
                    {
                        (int x, int y) Left = (1, 0);
                        (int x, int y) Right = (-1, 0);

                        ((int x, int y) location, (int x, int y) Direction) BeamLeft = ((Beam.location.x + Left.x, Beam.location.y + Left.y), (Left));
                        ((int x, int y) location, (int x, int y) Direction) BeamRight = ((Beam.location.x + Right.x, Beam.location.y + Right.y), (Right));
                        NewBeams.Add(BeamLeft);
                        NewBeams.Add((BeamRight));
                    }
                    break;

            }
            return NewBeams;
        }

        public int Part1() 
        {
            int answer = 0;

            Queue<((int x, int y) location,(int x, int y) Direction)> Beams = new Queue<((int x, int y), (int x, int y))>();
            (int x, int y) StartLocation = (0, 0);
            (int x, int y) Direction = (1, 0);
            HashSet<(int x, int y)> Energized = new();
            HashSet < ((int x, int y) location, (int x, int y) Direction)> SeenBeams = new();

            Beams.Enqueue((StartLocation, Direction));
            
            while (Beams.Count != 0) {

                var Beam = Beams.Dequeue();

                
                if (Beam.location.x >= Width ||  Beam.location.y >= Height || Beam.location.x < 0 || Beam.location.y < 0) {
                    continue;
                }
                char CurrentPlace = Lines[Beam.location.y][Beam.location.x];


                if (SeenBeams.Contains(Beam))
                {
                    continue;
                }

                Energized.Add(Beam.location);
                SeenBeams.Add(Beam);

                //if (ResultGrid[Beam.location.x, Beam.location.y] == PassedDirections[Beam.Direction])
                //{
                //    continue;
                //}else if(CurrentPlace != '.')
                //{
                //    ResultGrid[Beam.location.x, Beam.location.y] = '1';
                //}
                //else {
                //    ResultGrid[Beam.location.x, Beam.location.y] = PassedDirections[Beam.Direction];
                //}

                foreach(var NextBeam in MoveBeam(Beam,CurrentPlace))
                {
                    Beams.Enqueue(NextBeam);
                }



               
                
            }

            answer = Energized.Count();


            return answer;
        }

        public int Part2()
        {
            int answer = 0;

            List<((int x, int y) location,(int x, int y) Direction)> Startlocations = new();

            for (int x = 0; x < Width; x++ ){

                Startlocations.Add(((0, x), (1, 0)));
                Startlocations.Add(((Width - 1, x), (-1, 0)));
            }

            for (int y = 0; y < Height; y++)
            {

                Startlocations.Add(((y, 0), (0, 1)));
                Startlocations.Add(((y, Height - 1), (0, -1)));
            }


            ConcurrentBag<int> ResultoRuns = new();
            Parallel.ForEach(Startlocations, Startlocation =>
            {

                Queue<((int x, int y) location, (int x, int y) Direction)> Beams = new Queue<((int x, int y), (int x, int y))>();
                HashSet<(int x, int y)> Energized = new();
                HashSet<((int x, int y) location, (int x, int y) Direction)> SeenBeams = new();

                Beams.Enqueue((Startlocation));

                while (Beams.Count != 0)
                {

                    var Beam = Beams.Dequeue();


                    if (Beam.location.x >= Width || Beam.location.y >= Height || Beam.location.x < 0 || Beam.location.y < 0)
                    {
                        continue;
                    }
                    char CurrentPlace = Lines[Beam.location.y][Beam.location.x];


                    if (SeenBeams.Contains(Beam))
                    {
                        continue;
                    }

                    Energized.Add(Beam.location);
                    SeenBeams.Add(Beam);

                    foreach (var NextBeam in MoveBeam(Beam, CurrentPlace))
                    {
                        Beams.Enqueue(NextBeam);
                    }





                }
                ResultoRuns.Add(Energized.Count());
            });

            answer = ResultoRuns.Max();
            return answer;
        }

    }
}
