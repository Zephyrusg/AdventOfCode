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

            List<SeedRange> Endlist = new List<SeedRange>();
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
                        foreach (SeedRange inputRange in resultRanges)
                        {
                            if (!processedRanges.Contains(inputRange))
                            {
                                bool processed = false;
                                SeedRange SeedInput = inputRange;

                                if (SeedInput.Start < conversionRow.Source && SeedInput.End >= conversionRow.Source)
                                {
                                    Int64 frontOutOfRangeEnd = Math.Min(SeedInput.End, conversionRow.Source - 1);
                                    tempResultRanges.Add(new SeedRange(SeedInput.Start, frontOutOfRangeEnd - SeedInput.Start + 1));
                                    //RestPart
                                    SeedInput = new(frontOutOfRangeEnd + 1, SeedInput.Range - (frontOutOfRangeEnd - SeedInput.Start + 1));
                                    processed = true;
                                }

                                if (SeedInput.End > conversionRow.SourceEnd && SeedInput.Start <= conversionRow.SourceEnd)
                                {
                                    // Check if either the start or end of the seed range is within the range of the conversion row

                                    Int64 endOutOfRangeStart = Math.Max(SeedInput.Start, conversionRow.SourceEnd + 1);
                                    tempResultRanges.Add(new SeedRange(endOutOfRangeStart, SeedInput.End - endOutOfRangeStart + 1));
                                    SeedInput = new(SeedInput.Start, seedRange.Range - (SeedInput.End - endOutOfRangeStart + 1));
                                    processed = true;

                                }

                                if (SeedInput.Start >= conversionRow.Source && SeedInput.End <= conversionRow.SourceEnd)
                                {

                                    SeedInput = new(SeedInput.Start + conversionRow.Conversion, SeedInput.Range);
                                    tempResultRanges.Add(SeedInput);
                                    processedRanges.Add(SeedInput);
                                    processed = true;


                                }

                                if (!processed)
                                {
                                    tempResultRanges.Add(SeedInput);
                                }
                            }
                            else {
                                tempResultRanges.Add(inputRange);
                            }
                            
                        }
                        // Set the temporary result as the input for the next iteration or table
                        resultRanges = tempResultRanges;
                    }
                }
                Endlist.AddRange(resultRanges);
            }



            answer = Endlist.Min( x => x.Start );


            return answer;
        }

    }
}
