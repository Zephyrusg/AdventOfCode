using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AdventOfCode
{
    internal class Y2024D9
    {
        public static string diskMap = File.ReadAllText(".\\2024\\Input\\inputDay9.txt");

        public static int findIndexofFirstFreeSpace(List<(int Length, bool IsFile, int FileId)> Segments)
        {
            

            for (int index = 0; index <= Segments.Count; index++){
                if (Segments[index].IsFile == false)
                {
                    return index;
                }
            }

            return int.MaxValue;
        }

        static List<(int Length, bool IsFile, int FileId)> CompactDisk(string diskMap)
        {
            List<(int Length, bool IsFile, int FileId)> Segments = ParseDiskMap(diskMap);

            Segments.Add((1, false, -1));
            int LastFreeSpaceIndex = Segments.Count - 1;
            bool lastone = false;
            for (int readIndex = Segments.Count - 1; readIndex >= 0; readIndex--)
            {
                if (Segments[readIndex].IsFile == true && lastone == false)
                {
                    (int Length, bool IsFile, int FileId) Segment = Segments[readIndex];
                    //int segmentIndex = Segments.IndexOf(Segment);
                    bool done = false;
                    while (!done)
                    {
                        LastFreeSpaceIndex = Segments.Count - 1;
                        var freeSpaceIndex = findIndexofFirstFreeSpace(Segments);
                        var freeSpace = Segments[freeSpaceIndex];
                        
                        if (freeSpace.Length > Segment.Length)
                        {
                            Segments[LastFreeSpaceIndex] = (Segment.Length + Segments[LastFreeSpaceIndex].Length, false, -1);
                            //Segments.RemoveAt(freeSpaceIndex);
                            Segments[freeSpaceIndex] = (freeSpace.Length - Segment.Length, false, -1);
                            Segments.Insert(freeSpaceIndex, (Segment.Length, true, Segment.FileId));
                            readIndex++;
                            done = true;
                        }
                        else if (freeSpace.Length == Segment.Length)
                        {
                            Segments[LastFreeSpaceIndex] = (freeSpace.Length + Segments[LastFreeSpaceIndex].Length, false, -1);
                            Segments[freeSpaceIndex] = Segment;
                            done = true;
                        }
                        else
                        {
                            Segment.Length -= freeSpace.Length;
                            Segments[LastFreeSpaceIndex] = (freeSpace.Length + Segments[LastFreeSpaceIndex].Length, false, -1);
                            //Segments.RemoveAt(freeSpaceIndex);
                            Segments[freeSpaceIndex] = (freeSpace.Length, true, Segment.FileId);
                        }
                        

                    }
                    Segments[readIndex] = (Segment.Length,false,-1);
                    if (Segment == Segments.Last(x=> x.IsFile == true))
                    {
                        lastone = true;

                    }                    
                }
            }

            return Segments;
        }

        static List<(int Length, bool IsFile, int FileId)> CompactDiskV2(string diskMap) {

            List<(int Length, bool IsFile, int FileId)> Segments = ParseDiskMap(diskMap);
            bool lastone = false;
            for (int readIndex = Segments.Count - 1; readIndex >= 0; readIndex--)
            {
                var Segment = Segments[readIndex];
                if (Segments[readIndex].IsFile == true && lastone == false)
                {
                    
                    (int Length, bool IsFile, int FileId) freeSpace = Segments.Find(x => x.IsFile == false && x.Length >= Segment.Length);
                    var freeSpaceIndex = Segments.IndexOf(freeSpace);
                    //var SegmentIndex = Segments.IndexOf(Segment);
                    if (freeSpace.Length != 0 && freeSpaceIndex < readIndex)
                    {
                        if (freeSpace.Length > Segment.Length)
                        {
                            
                            //Segments.RemoveAt(freeSpaceIndex);
                            Segments[freeSpaceIndex] = (freeSpace.Length - Segment.Length, false, -1);
                            Segments.Insert(freeSpaceIndex, (Segment.Length, true, Segment.FileId));
                            readIndex++;
                            Segments[readIndex++] = (Segment.Length, false, -1);
                            
                        }
                        else
                        {
                            Segments[freeSpaceIndex] = Segment;
                            Segments[readIndex] = freeSpace;
                        }
                       
                    }
                }

            }

           return Segments;

        }

        static long CalculateChecksum(List<(int Length, bool IsFile, int FileId)> compactedDisk)
        {
            long checksum = 0;
            int position = 0;
            
            for(int s = 0; s < compactedDisk.Count - 1; s++)
            {
                
                (int Length, bool IsFile, int FileId) segment = compactedDisk[s];
                if (segment.IsFile == true)
                {
                    int faculty = 1;
                    for (int i = segment.Length; i > 0; i--)
                    {

                        checksum += position * segment.FileId;
                        position++;
                    }



                }
                else
                {
                    position += segment.Length;
                }
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

        public long Part1()
        {
            long answer = 0;

            List<(int Length, bool IsFile, int FileId)> compactedDisk = CompactDisk(diskMap);
            answer = CalculateChecksum(compactedDisk);

            return answer;
        }

        public long Part2()
        {

            long answer = 0;

            List<(int Length, bool IsFile, int FileId)> compactedDisk = CompactDiskV2(diskMap);
            answer = CalculateChecksum(compactedDisk);

       


            return answer;
        }

    }
}
