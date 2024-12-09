using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class Y2024D9
    {
        public static string diskMap = File.ReadAllText(".\\2024\\Input\\inputDay9.txt");

        static List<string> CompactDisk(string diskMap)
        {
            List<(int Length, bool IsFile, int FileId)> segments = ParseDiskMap(diskMap);
            List<string> disk = FlattenSegments(segments);

            
            for (int readIndex = disk.Count - 1; readIndex >= 0; readIndex--)
            {
                if (disk[readIndex] != ".")
                {
                    
                    int writeIndex = 0;
                    while (writeIndex < disk.Count && disk[writeIndex] != ".")
                    {
                        writeIndex++;
                    }

                    if (writeIndex < readIndex) t
                    {
                        disk[writeIndex] = disk[readIndex];
                        disk[readIndex] = "."; 
                    }
                }
            }

            return disk;
        }

        static long CalculateChecksum(List<string> compactedDisk)
        {
            long checksum = 0;
            int position = 0;
            while (compactedDisk[position] != ".")
            {

                int fileId = int.Parse(compactedDisk[position]);
                checksum += position * fileId;

                position++;
            }
            return checksum;
        }

        static List<(int Length, bool IsFile, int FileId)> ParseDiskMap(string diskMap)
        {
            List<(int Length, bool IsFile, int FileId)> segments = new List<(int, bool, int)>();
            int fileId = 0;

            for (int i = 0; i < diskMap.Length; i++)
            {
                int length = diskMap[i] - '0';
                bool isFile = i % 2 == 0; // Even files, odd free space

                segments.Add((length, isFile, isFile ? fileId++ : -1));
            }

            return segments;
        }

        static List<string> FlattenSegments(List<(int Length, bool IsFile, int FileId)> segments)
        {
            List<string> disk = new List<string>();

            foreach (var segment in segments)
            {
                if (segment.IsFile)
                {
                    string fileIdString = segment.FileId.ToString();
                    for (int i = 0; i < segment.Length; i++)
                    {
                        disk.Add(fileIdString);
                    }
                }
                else
                {
                    for (int i = 0; i < segment.Length; i++)
                    {
                        disk.Add(".");
                    }
                }
            }

            return disk;
        }


        public long Part1()
        {
            long answer = 0;

            List<string> compactedDisk = CompactDisk(diskMap);
            answer = CalculateChecksum(compactedDisk);

            return answer;
        }

        public int Part2()
        {
            int answer = 0;




            return answer;
        }

    }
}
