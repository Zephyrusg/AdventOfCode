using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using System.Runtime.CompilerServices;
using static AdventOfCode.Y2017D7;

namespace AdventOfCode
{
    internal class Y2017D7
    {
        public static List<Disk> Disks = new List<Disk>();
        public static Disk Bottumdisk;
        public class Disk {
            public string name;
            public List<Disk> Children = new List<Disk>();
            public Disk ?ParentDisk = null;
            public int Weight = 0;
            public int TotalWeight = 0;

            public Disk(string name)
            {
                this.name = name;
            }

            static int CalculateTotalWeight(Disk Disk)
            {
                int totalWeight = Disk.Weight;
                foreach (var child in Disk.Children)
                {
                    totalWeight += CalculateTotalWeight(child);
                }
                Disk.TotalWeight = totalWeight;
                return totalWeight;
            }
            static public Disk FindUnbalancedProgram(Disk Disk)
            {
                if (Disk.Children.Count == 0)
                {
                    return null;
                }

                var weights = Disk.Children.Select(child => CalculateTotalWeight(child)).ToList();

                // Check if all weights are the same
                if (weights.Distinct().Count() == 1)
                {
                    return Disk;
                }

                // Find the unbalanced program
                var groupedWeights = weights.GroupBy(w => w).ToList();

                // Check both most and least frequent values
                int mostFrequentCount = groupedWeights.Max(g => g.Count());
                int leastFrequentCount = groupedWeights.Min(g => g.Count());

                int unbalancedIndex;

                if (mostFrequentCount != leastFrequentCount)
                {
                    int targetCount = mostFrequentCount > leastFrequentCount ? mostFrequentCount : leastFrequentCount;
                    unbalancedIndex = weights.FindIndex(w => groupedWeights.First(g => g.Key == w).Count() != targetCount);
                    Disk = Disk.FindUnbalancedProgram(Disk.Children[unbalancedIndex]);

                }
                else
                {
                    // All weights have the same frequency, indicating a different issue
                    return Disk;
                }

                return Disk;
            }

            public static int FindCorrectWeight(Disk Disk)
            {
                var siblings = Disk.ParentDisk.Children;

                // Calculate the total weight of each sibling, including their respective children
                var siblingWeights = siblings.Select(sibling => CalculateTotalWeight(sibling)).ToList();

                // Find the distinct weights in the siblingWeights list
                var distinctWeights = siblingWeights.Distinct().ToList();

                if (distinctWeights.Count == 1)
                {
                    // All siblings have the same weight, indicating a different issue
                    return -1; // or some other indicator of an issue
                }

                int unbalancedWeight;
                int otherWeight;

                // Determine the unbalanced and other weights
                if (siblingWeights.Count(weight => weight == distinctWeights[0]) == 1)
                {
                    unbalancedWeight = distinctWeights[0];
                    otherWeight = distinctWeights[1];
                }
                else
                {
                    unbalancedWeight = distinctWeights[1];
                    otherWeight = distinctWeights[0];
                }

                // Find the index of the sibling with the unbalanced weight
                int unbalancedSiblingIndex = siblingWeights.FindIndex(weight => weight == unbalancedWeight);

                // Calculate the correct weight for the unbalanced disk
                int weightDifference = otherWeight - unbalancedWeight;
                int correctWeight = Disk.Weight + weightDifference;

                return correctWeight;
            }
        }

        public static string Part1() 
        {
            string[] text = File.ReadAllLines(".\\2017\\Input\\inputDay7.txt");
            //string answer = "";
            string WeightPattern = "\\d+";

            foreach (string line in text)
            {
                string[] parts = line.Split(" -> ");

                string[] DiskParts = parts[0].Split(" ");
                string CurrentDiskName = DiskParts[0];
                var match  = Regex.Match(DiskParts[1], WeightPattern);
                int DiskWeight = int.Parse(match.Value);
                Disk? CurrentDisk = Disks.Find(x => x.name == CurrentDiskName);
                if (CurrentDisk == null) {
                    CurrentDisk = new(CurrentDiskName);
                    Disks.Add(CurrentDisk);
                }
                CurrentDisk.Weight = DiskWeight;

                if (parts.Count() == 2 )
                {
                    string[] Disknames = parts[1].Split(", ");
                    foreach(string Diskname in Disknames)
                    {

                        Disk? ChildDisk = Disks.Find(x => x.name == Diskname);
                        if (ChildDisk == null)
                        {
                            ChildDisk = new(Diskname);
                            Disks.Add(ChildDisk);
                            
                        }
                        ChildDisk.ParentDisk = CurrentDisk;
                        CurrentDisk.Children.Add(ChildDisk);  
                    }
                }
            }
       
            var ParentDisk = Disks.Find(x => x.ParentDisk == null);
            Bottumdisk = ParentDisk;
            return ParentDisk.name;
        }

        public static int Part2()
        {

            string text = File.ReadAllText(".\\2017\\Input\\inputDay7.txt");
            int answer = 0;

            Disk unbalancedProgram = Disk.FindUnbalancedProgram(Bottumdisk);
            int correctWeight = Disk.FindCorrectWeight(unbalancedProgram);
            answer = correctWeight;
            return answer;
        }

    }
}
