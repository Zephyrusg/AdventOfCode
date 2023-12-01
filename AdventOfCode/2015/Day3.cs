using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace AdventOfCode
{
    internal class Y2015D3
    {
        public static int Part1() 
        {
            string text = File.ReadAllText(".\\2015\\Input\\inputDay3.txt");
            HashSet<Vector2> VisitedHouses = new HashSet<Vector2>(); 
            Vector2 currentLocation = new Vector2(0,0);
            VisitedHouses.Add(currentLocation);
            foreach (Char character in text)
            {
                switch(character)
                    {
                    case '^':
                        currentLocation = new Vector2(currentLocation.X, currentLocation.Y +1);
                        break;
                    case 'v':
                        currentLocation = new Vector2(currentLocation.X, currentLocation.Y - 1);
                        break;
                    case '<':
                        currentLocation = new Vector2(currentLocation.X - 1, currentLocation.Y);
                        break;
                    case '>':
                        currentLocation = new Vector2(currentLocation.X + 1, currentLocation.Y);
                        break;
                }
                if (!VisitedHouses.Contains(currentLocation))
                {
                    VisitedHouses.Add(currentLocation); ;
                }
                
            }
            return VisitedHouses.Count;

        }
        public static int Part2()
        {
            string text = File.ReadAllText(".\\2015\\Input\\inputDay3.txt");
            HashSet<Vector2> VisitedHouses = new HashSet<Vector2>();
            Vector2 currentLocationSanta = new Vector2(0,0);
            Vector2 currentLocationRoboSanta = new Vector2(0,0);
            Vector2 currentLocation = new Vector2(0,0);
            VisitedHouses.Add(currentLocation);
            int turn = 0;
            foreach (Char character in text)
            {
                switch (character)
                {
                    case '^':
                        currentLocation = new Vector2(currentLocation.X, currentLocation.Y + 1);
                        break;
                    case 'v':
                        currentLocation = new Vector2(currentLocation.X, currentLocation.Y - 1);
                        break;
                    case '<':
                        currentLocation = new Vector2(currentLocation.X - 1, currentLocation.Y);
                        break;
                    case '>':
                        currentLocation = new Vector2(currentLocation.X + 1, currentLocation.Y);
                        break;
                }
                if (!VisitedHouses.Contains(currentLocation))
                {
                    VisitedHouses.Add(currentLocation); ;
                }
                turn++;
                if (turn % 2 == 0)
                {
                    currentLocationRoboSanta = currentLocation;
                    currentLocation = currentLocationSanta;
                }
                else
                {
                    currentLocationSanta = currentLocation;
                    currentLocation = currentLocationRoboSanta;
                }

            }
            return VisitedHouses.Count;

        }
    }
}
