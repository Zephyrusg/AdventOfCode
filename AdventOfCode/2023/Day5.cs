using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class Y2023D5
    {
        public static List<List<ConversionRow>> CoversionTables = new List<List<ConversionRow>>();
        
               
        public class ConversionRow        {


            public Int64 range;
            public Int64 Destination;
            public Int64 Source;
            public ConversionRow(Int64 Source, Int64 Destination, Int64 Range) {
                this.Source = Source;
                this.Destination = Destination;
                this.range = Range;
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

        public int Part2()
        {
            int answer = 0;




            return answer;
        }

    }
}
