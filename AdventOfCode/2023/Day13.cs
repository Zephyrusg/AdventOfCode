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
        bool part2 = false;
        bool TestHorizontal(List<string> Pattern, int FoundPossibleRow) {
            bool SmudgeFixed = false;
            int up = FoundPossibleRow;
            int down = FoundPossibleRow +1;

            while(up >= 0 && down < Pattern.Count) {

                if (Pattern[up] != Pattern[down]) {
                    
                    if (part2 && CompareString(Pattern[up], Pattern[down]) && SmudgeFixed == false )
                    {
                        SmudgeFixed = true;
                        up--;
                        down++;
                        continue;
                    }

                    return false;
                }
                up--;
                down++;
            }
            if (part2) { return SmudgeFixed; }
            return true;


            
        }
        bool CompareString(string s1, string s2)
        {
            int counter = 0;
            for (int x = 0; x < s1.Length; x++) {
                if (s1[x] != s2[x]) { 
                    counter++;
                }
            }

            if(counter ==1)
            {
                return true;
            }
            else{
                return false;
            }

           
        } 

        bool TestVertical(List<string> Pattern, int FoundPossibleColumn)
        {
            int Left = FoundPossibleColumn;
            int Right = FoundPossibleColumn +1;
            bool SmudgeFixed = false;
            while (Left >= 0 && Right < Pattern[0].Length)
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
                    if (part2 && CompareString(ColumnA, ColumnB) && SmudgeFixed == false){
                        SmudgeFixed = true;
                        Left--;
                        Right++;
                        continue;
                    }
                    return false;
                }

                Left--;
                Right++;
            }

            if (part2) { return SmudgeFixed; }
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
                        //Console.WriteLine("Found Reflection on Column: " + FoundPossibeColumn + "/" + (FoundPossibeColumn + 1));
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
                            //Console.WriteLine("Found Reflection on Rows: " + FoundPossibleRow + "/" + (FoundPossibleRow + 1));
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
            part2 = true;
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
            int i = 0;
            foreach (List<string> TestPattern in Patterns)
            {
                List<int> FoundPossibleColumns = new List<int>();
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


                        FoundPossibleColumns.Add(x);
                    }
                    else if (CompareString(ColumnA, ColumnB)) { 
                        FoundPossibleColumns.Add(x);
                    }
                }

                for (int y = 0; y < TestPattern.Count - 1; y++)
                {
                    if (TestPattern[y] == TestPattern[y + 1])
                    {
                        FoundPossibleRows.Add(y);

                    }    //goto next;
                    else if (CompareString(TestPattern[y], TestPattern[y + 1]))
                    {
                        FoundPossibleRows.Add(y);
                    }
                }
                bool Found = false;
                foreach (int FoundPossibeColumn in FoundPossibleColumns)
                {
                    if (TestVertical(TestPattern, FoundPossibeColumn))
                    {
                        answer += (FoundPossibeColumn + 1);
                        Console.WriteLine("Found Reflection on Column: " + FoundPossibeColumn + "/" + (FoundPossibeColumn + 1));
                        Found = true;
                        break;
                    }
                }
                if (!Found)
                {
                    foreach (int FoundPossibleRow in FoundPossibleRows)
                    {
                        if (TestHorizontal(TestPattern, FoundPossibleRow))
                        {
                            Console.WriteLine("Found Reflection on Rows: " + FoundPossibleRow + "/" + (FoundPossibleRow + 1));
                            answer += (FoundPossibleRow + 1) * 100;
                            Found = true;
                        }
                    }
                }
                if (!Found) { Console.WriteLine("Error not found"); }

            }
            return answer;
        }

    }
}
