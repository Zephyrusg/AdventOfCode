using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2015
{
    internal class Day9
    {
        //public static void Func() {

            static Dictionary<string, Dictionary<string, int>> distances = ReadDistancesFromFile(".\\2015\\Input\\inputDay9.txt");
            static List<List<string>> routes = GetPermutations(distances.Keys.ToList());
            static Dictionary<string, Dictionary<string, int>> ReadDistancesFromFile(string filePath)
            {
                Dictionary<string, Dictionary<string, int>> distances = new Dictionary<string, Dictionary<string, int>>();


                // Read each line from the file
                foreach (string line in File.ReadLines(filePath))
                {
                    string[] parts = line.Split(new[] { " to ", " = " }, StringSplitOptions.RemoveEmptyEntries);

                    string fromLocation = parts[0];
                    string toLocation = parts[1];
                    int distance = int.Parse(parts[2]);

                    // Add the distance to the dictionary
                    if (!distances.ContainsKey(fromLocation))
                    {
                        distances[fromLocation] = new Dictionary<string, int>();
                    }

                    distances[fromLocation][toLocation] = distance;

                    // Since it's an undirected graph, add the reverse distance as well
                    if (!distances.ContainsKey(toLocation))
                    {
                        distances[toLocation] = new Dictionary<string, int>();
                    }

                    distances[toLocation][fromLocation] = distance;
                }
                return distances;

            }
            static List<List<T>> GetPermutations<T>(List<T> list)
            {
                if (list.Count == 1)
                    return new List<List<T>> { new List<T>(list) };

                return list.SelectMany(
                    element => GetPermutations(list.Where(e => !e.Equals(element)).ToList()),
                    (element, permutation) => permutation.Prepend(element).ToList()
                ).ToList();
            }
        
        //} 
    

        public static int Day9A()
        {
           

            
            static int CalculateTotalDistance(List<string> route, Dictionary<string, Dictionary<string, int>> distances)
            {
                int totalDistance = 0;
                for (int i = 0; i < route.Count - 1; i++)
                {
                    totalDistance += distances[route[i]][route[i + 1]];
                }
                return totalDistance;
            }


            int shortestDistance = int.MaxValue;
            foreach (var route in routes)
            {
                int distance = CalculateTotalDistance(route, distances);
                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                }
            }

            return shortestDistance;
        }

        public static int Day9B() {

            static int CalculateTotalDistance(List<string> route, Dictionary<string, Dictionary<string, int>> distances)
            {
                int totalDistance = 0;
                for (int i = 0; i < route.Count - 1; i++)
                {
                    totalDistance += distances[route[i]][route[i + 1]];
                }
                return totalDistance;
            }


            int longestDistance = int.MinValue; // Initialize to the smallest possible value
            foreach (var route in routes)
            {
                int distance = CalculateTotalDistance(route, distances);
                if (distance > longestDistance)
                {
                    longestDistance = distance;
                }
            }

            return longestDistance;
        }
    }
}
