using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class Y2024D2
    {
        public static string[] Lines = File.ReadAllLines(".\\2024\\Input\\inputDay2.txt");
        public List<int[]> reports = new();
        static List<int[]> UnSafeReports = new();
        static int answer = 0;
        public int Part1() 
        {
            foreach(string line in Lines)
            {
                int[] parts = line.Split(" ").Select(l => int.Parse(l)).ToArray();
                reports.Add(parts);
            }

            foreach (int[] report in reports)
            {
                bool safe = true;
                if (report[0] < report[1] ){

                    for (int x = 0; x < report.Count() - 1; x++)
                    {
                        if (!(report[x] < report[x+1] && (report[x+1] - report[x] <= 3) && (report[x + 1] - report[x] > 0)))
                        {
                            safe = false;
                            UnSafeReports.Add(report);
                            break;
                        }
                    }
                }
                else
                {

                    for (int x = 0; x < report.Count() - 1; x++)
                    {
                        if (!(report[x] > report[x + 1] && (report[x] - report[x + 1] <= 3) && (report[x] - report[x + 1] > 0)))
                        {
                            safe = false;
                            UnSafeReports.Add(report);
                            break;
                        }
                    }
                }
                if (safe)
                {
                    answer++;
                }

            }
       


            return answer;
        }

        public int Part2()
        {
            List<List<int>> FixedReports = new();
            foreach (int[] report in UnSafeReports)
            {
                List<int> failedlist = report.ToList();
                bool safe = true;
                // Deja vu
                if (report[0] == report[1])
                {
                    failedlist.RemoveAt(0);
                    FixedReports.Add(failedlist);

                }
                else if (report[0] < report[1])
                {

                    for (int x = 0; x < report.Count() - 1; x++)
                    {
                        if (!(report[x] < report[x + 1] && (report[x + 1] - report[x] <= 3) && (report[x + 1] - report[x] > 0)))
                        {
                            // Wrong direction by first number
                            if(x == 1 && report[x ] > report[x + 1] && report[x + 1] > report[x + 2]){
                                failedlist.RemoveAt(x-1);
                            }
                            // At the end drop it
                            else if (x + 1 == report.Count() - 1)
                            {
                                failedlist.RemoveAt(x + 1);
                            }
                            // Is this current number faulty
                            else if (x > 0 && report[x - 1] < report[x + 1] && (report[x + 1] - report[x - 1] <= 3) && (report[x + 1] - report[x - 1] > 0))
                            {
                                failedlist.RemoveAt(x);
                            }
                            // Is this next number faulty
                            else if (report[x] < report[x + 2] && (report[x+2] - report[x] <= 3) && (report[x + 2] - report[x] > 0))
                            {
                                failedlist.RemoveAt(x+1);
                            }
                            //To large steps
                            else if (report[x+1] - report[x] >= 4)
                            {
                                failedlist.RemoveAt(x);
                            }
                            //catch all
                            else
                            {
                                failedlist.RemoveAt(x + 1);
                            }
                            
                            FixedReports.Add(failedlist);
                            break;

                        }

                    }
                }
                else
                {

                    for (int x = 0; x < report.Count() - 1; x++)
                    {
                        if (!(report[x] > report[x + 1] && (report[x] - report[x + 1] <= 3) && (report[x] - report[x + 1] > 0)))
                        {
                            // Wrong direction by first number
                            if (x == 1 && report[x] < report[x + 1] && report[x + 1] < report[x + 2])
                            {
                                failedlist.RemoveAt(x-1);
                            }
                            // At the end drop it
                            else if (x + 1 == report.Count() - 1)
                            {
                                failedlist.RemoveAt(x + 1);
                            }
                            // Is this next number faulty
                            else if (x > 0 && report[x - 1] > report[x + 1] && (report[x - 1] - report[x + 1] <= 3) && (report[x - 1] - report[x + 1] > 0))
                            {
                                failedlist.RemoveAt(x);
                            }
                            // Is this next number faulty
                            else if (report[x] > report[x + 2] && (report[x] - report[x+2] <= 3) && (report[x ] - report[x+ 2] > 0))
                            {
                                failedlist.RemoveAt(x + 1);
                            }
                            //To large steps
                            else if (report[x] - report[x + 1] >= 4)
                            {
                                failedlist.RemoveAt(x);
                            }
                            //catch all
                            else
                            {
                                
                                failedlist.RemoveAt(x + 1);
                            }

                            FixedReports.Add(failedlist);
                            break;

                        }
                    }
                }

            }

            foreach (List<int> report in FixedReports)
            {
                bool safe = true;
                if (report[0] < report[1])
                {

                    for (int x = 0; x < report.Count() - 1; x++)
                    {
                        if (!(report[x] < report[x + 1] && (report[x + 1] - report[x] <= 3) && (report[x + 1] - report[x] > 0)))
                        {

                            safe = false;
                            break;

                        }

                    }
                }
                else
                {

                    for (int x = 0; x < report.Count() - 1; x++)
                    {
                        if (!(report[x] > report[x + 1] && (report[x] - report[x + 1] <= 3) && (report[x] - report[x + 1] > 0)))
                        {

                            safe = false;
                            break;

                        }
                    }
                }


                if (safe)
                {
                    answer++;
                }
            }



            


            return answer;
        }

    }
}
