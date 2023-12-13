using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class Y2023D13
    {
        static List<List<string>> Patterns = new List<List<string>>();
        public static string[] lines = File.ReadAllLines(".\\2023\\Input\\inputDay13.txt");

        bool TestHorizontal(List<string> Pattern, int FoundPossibleRow) {

            int up = FoundPossibleRow - 1;
            int down = FoundPossibleRow + 2;

            while(up > 0 && down < Pattern.Count) {

                if (Pattern[up] != Pattern[down]) {
                    return false;
                }
                up--;
                down++;
            }

            return true;


            
        }

        bool TestVertical(List<string> Pattern, int FoundPossibleColumn)
        {
            int Left = FoundPossibleColumn - 1;
            int Right = FoundPossibleColumn + 2;
            while (Left > 0 && Right < Pattern[0].Length)
            {


                string ColumnA = "";
                string ColumnB = "";

                for (int y = 0; y < Pattern.Count; y++)
                {
                    ColumnA += Pattern[y][Left];
                    ColumnB += Pattern[y][Right];
                }
                if (ColumnA != ColumnB)
                {
                    return false;
                }

                Left--;
                Right++;
            }


            return true;
           
        }
        public int Part1() 
        {
            int answer = 0;
            List<string> Pattern = new List<string>();
            foreach (string line in lines)
            {

                if (line == "")
                {
                    Patterns.Add(Pattern);
                    Pattern = new List<string>();
                }
                else
                {
                    Pattern.Add(line);

                }
            }
            Patterns.Add(Pattern);

            foreach (List<string> TestPattern in Patterns)
            {
                List<int> FoundPossibeColumns = new List<int>();
                List<int> FoundPossibleRows = new List<int>();
                for (int x = 0; x < TestPattern[0].Length - 1; x++)
                {

                    string ColumnA = "";
                    string ColumnB = "";

                    for (int y = 0; y < TestPattern.Count; y++)
                    {
                        ColumnA += TestPattern[y][x];
                        ColumnB += TestPattern[y][x + 1];
                    }
                    if (ColumnA == ColumnB)
                    {
                        FoundPossibeColumns.Add(x);
                        
                        
                       
                    }
                }
                
                for (int y = 0; y < TestPattern.Count - 1; y++) {
                    if (TestPattern[y] == TestPattern[y + 1]) {
                        FoundPossibleRows.Add(y);
                        
                        //goto next;
                    }
                }
                bool Found = false;
                foreach (int FoundPossibeColumn in FoundPossibeColumns) {
                    if (TestVertical(TestPattern, FoundPossibeColumn)){
                        answer += (FoundPossibeColumn+1);
                        Console.WriteLine("Found Reflection on Column: " + FoundPossibeColumn + "/" + (FoundPossibeColumn + 1));
                        Found = true;
                        break;
                    }
                }
                if(!Found)
                {
                    foreach(int FoundPossibleRow in FoundPossibleRows)
                    {
                        if(TestHorizontal(TestPattern, FoundPossibleRow))
                        {
                            Console.WriteLine("Found Reflection on Rows: " + FoundPossibleRow + "/" + (FoundPossibleRow + 1));
                            answer += (FoundPossibleRow+1)*100;
                        }
                    }
                }

               
                
            }



            return answer;
        }

        public int Part2()
        {
            int answer = 0;




            return answer;
        }

    }
}
