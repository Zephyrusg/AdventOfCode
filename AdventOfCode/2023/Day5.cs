using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static AdventOfCode.Y2023D5;

namespace AdventOfCode
{
    internal class Y2023D5
    {
        public static List<List<ConversionRow>> CoversionTables = new List<List<ConversionRow>>();
        
               
        public class ConversionRow        {


            public Int64 range;
            public Int64 Destination;
            public Int64 DestinationEnd;
            public Int64 Source;
            public Int64 SourceEnd;
            public Int64 Conversion;
            public ConversionRow(Int64 Source, Int64 Destination, Int64 Range) {
                this.Source = Source;
                this.Destination = Destination;
                this.range = Range;
                this.SourceEnd = Source + Range -1;
                this.DestinationEnd = Destination + Range - 1;
                this.Conversion = this.Destination - this.Source;
            }



        }

        public class SeedRange {
            public Int64 Start;
            public Int64 Range;
            public Int64 End;

            public SeedRange(Int64 Start, Int64 Range)
            {
                this.Start = Start;
                this.Range = Range;
                this.End = Start + Range - 1;
            }
        }
        public static string[] lines = File.ReadAllLines(".\\2023\\Input\\inputDay5.txt");
        public Int64 Part1() 
        {

           
            Int64 answer = 0;
            string[] SeedslineParts = lines[0].Split(": ");
            string[] ConvertionObjList = lines.Skip(2).ToArray();
            Int64[] Seeds = SeedslineParts[1].Split(' ').Select(x=>Int64.Parse(x)).ToArray();

            List<ConversionRow> ConversionTable = new List<ConversionRow>();

            foreach (string s in ConvertionObjList)
            {
                if (s.EndsWith(':')) {
                    //Console.WriteLine("Starting with: " + s + " Conversion");
                    ConversionTable = new List<ConversionRow>();
                    continue;
                }
                if (s == "") {
                    CoversionTables.Add(ConversionTable);
                    
                    ConversionTable = new List<ConversionRow>();
                }
                else{
                    Int64[] ConversionRowArray = s.Split(" ").Select(x => Int64.Parse(x)).ToArray();
                    ConversionTable.Add(new(ConversionRowArray[1], ConversionRowArray[0], ConversionRowArray[2]));
                    
                }
            }

            CoversionTables.Add(ConversionTable);

            Int64[] TempSeeds = Seeds;
            foreach (List<ConversionRow> Table in CoversionTables) {
                
                Int64 index = 0;
                foreach (int Seed in Seeds) {
                    Int64 TempSeed = Seed;
                    var Temp = Table.Where(x => Seed >= x.Source && Seed < x.Source + x.range);
                    if(Temp.Count() != 0)
                    {

                        var Row = Temp.First();
                        Int64 Conversionnumber = TempSeed - Row.Source;
                        TempSeed = Row.Destination + Conversionnumber;
                        TempSeeds[index] = TempSeed;
                        
                    }
                    index++;
                }
                Seeds = TempSeeds;
            }

            answer = Seeds.Min();

            return answer;
        }

        public Int64 Part2()
        {
            Int64 answer = 0;
            string[] SeedslineParts = lines[0].Split(": ");
            Int64[] Seeds = SeedslineParts[1].Split(' ').Select(x => Int64.Parse(x)).ToArray();
            List<SeedRange> SeedRanges = new List<SeedRange>();
            for (int x = 0; x < Seeds.Count(); x = x + 2) { 
                SeedRange Range = new(Seeds[x], Seeds[x+1]);
                SeedRanges.Add(Range);
            }
            List<SeedRange> resultRanges = new List<SeedRange>();

            foreach (var seedRange in SeedRanges)
            {
                resultRanges = new List<SeedRange> { seedRange};
                foreach (var conversionTable in CoversionTables)
                {
                    HashSet<SeedRange> processedRanges = new HashSet<SeedRange>();

                    foreach (var conversionRow in conversionTable)
                    {
                        List<SeedRange> tempResultRanges = new List<SeedRange>();
                        foreach (var inputRange in resultRanges)
                        {
                            if (!processedRanges.Contains(inputRange))
                            {
                                bool processed = false;
                                if (inputRange.Start >= conversionRow.Source && inputRange.End <= conversionRow.SourceEnd)
                                {
                                    // Perform the conversion based on the mapping rules in the conversion row
                                
                                    inputRange.Start = conversionRow.Conversion + inputRange.Start;
                                    tempResultRanges.Add(new SeedRange(inputRange.Start, inputRange.Range));

                                    // Mark the seed range as processed for this conversion row
                                    processedRanges.Add(inputRange);
                                    processed = true;
                                
                                }
                                else
                                {
                                    // Handle the out-of-bound parts at the front of the range
                                    if (inputRange.Start < conversionRow.Source)
                                    {
                                        // Check if either the start or end of the seed range is within the range of the conversion row
                                        if (inputRange.End >= conversionRow.Source)
                                        {
                                            long frontOutOfRangeEnd = Math.Min(inputRange.End, conversionRow.Source - 1);
                                            tempResultRanges.Add(new SeedRange(inputRange.Start, frontOutOfRangeEnd - inputRange.Start + 1));

                                            // Mark the seed range as processed for this conversion row
                                            tempResultRanges.Add(inputRange);
                                        }
                                    }

                                    // Handle the out-of-bound parts at the end of the range
                                    if (inputRange.End > conversionRow.SourceEnd)
                                    {
                                        // Check if either the start or end of the seed range is within the range of the conversion row
                                        if (inputRange.Start <= conversionRow.SourceEnd)
                                        {
                                            long endOutOfRangeStart = Math.Max(inputRange.Start, conversionRow.SourceEnd + 1);
                                            tempResultRanges.Add(new SeedRange(endOutOfRangeStart, inputRange.End - endOutOfRangeStart + 1));

                                            // Mark the seed range as processed for this conversion row
                                            tempResultRanges.Add(inputRange);
                                        }
                                    }

                                    // Check if the seed range is partly in the range of the conversion row
                                    if (inputRange.Start < conversionRow.SourceEnd && inputRange.End > conversionRow.Source)
                                    {
                                        // Calculate the intersection between the input range and the source range
                                        long intersectionStart = Math.Max(inputRange.Start, conversionRow.Source);
                                        long intersectionEnd = Math.Min(inputRange.End, conversionRow.SourceEnd);
                                        Int64 conversionNumber = intersectionEnd - conversionRow.Source;
                                        long convertedStart = conversionRow.Destination + conversionNumber;
                                        tempResultRanges.Add(new SeedRange(convertedStart, intersectionEnd - intersectionStart + 1));

                                        // Mark the seed range as processed for this conversion row
                                        processedRanges.Add(inputRange);
                                        processed = true;
                                    }

                                    if (inputRange.Start < conversionRow.SourceEnd && inputRange.End > conversionRow.Source)
                                    {
                                        // Calculate the intersection between the input range and the source range
                                        long intersectionStart = Math.Max(inputRange.Start, conversionRow.Source);
                                        long intersectionEnd = Math.Min(inputRange.End, conversionRow.SourceEnd);
                                        Int64 conversionNumber = intersectionEnd - conversionRow.Source;
                                        long convertedStart = conversionRow.Destination + conversionNumber;
                                        tempResultRanges.Add(new SeedRange(convertedStart, intersectionEnd - intersectionStart + 1));

                                        // Mark the seed range as processed for this conversion row
                                        processedRanges.Add(inputRange);
                                        processed = true;
                                    }
                                }
                                // Add the unprocessed seed ranges back to the result ranges
                                if (processed != true)
                                {
                                    tempResultRanges.Add(inputRange);
                                }
                            }
                            
                        }

                        
                        // Set the temporary result as the input for the next iteration or table
                        resultRanges = tempResultRanges;
                    }
                }
            }






            return answer;
        }

    }
}
