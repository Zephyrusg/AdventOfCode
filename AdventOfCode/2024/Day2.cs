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
        public List<List<int>> reports = new();
        static List<List<int>> UnSafeReports = new();
        static int answer = 0;

        public bool IsSafe(List<int> report)
        {

            if (report[0] < report[1])
            {

                for (int x = 0; x < report.Count() - 1; x++)
                {
                    if (!(report[x] < report[x + 1] && (report[x + 1] - report[x] <= 3) && (report[x + 1] - report[x] > 0)))
                    {
                        return false;
                    }
                }
            }
            else
            {

                for (int x = 0; x < report.Count() - 1; x++)
                {
                    if (!(report[x] > report[x + 1] && (report[x] - report[x + 1] <= 3) && (report[x] - report[x + 1] > 0)))
                    {
                        return false;
                    }
                }
            }

            return true;

        }
        public int Part1() 
        {
            foreach(string line in Lines)
            {
                List<int> parts = line.Split(" ").Select(l => int.Parse(l)).ToList();
                reports.Add(parts);
            }

            foreach (List<int> report in reports)
            {
                bool safe = true;
                if (report[0] < report[1])
                {

                    for (int x = 0; x < report.Count() - 1; x++)
                    {
                        if (!(report[x] < report[x + 1] && (report[x + 1] - report[x] <= 3) && (report[x + 1] - report[x] > 0)))
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
            foreach (List<int> report in UnSafeReports)
            {
                List<int> failedlist = report.ToList();
                bool safe = true;
                // Deja vu
                if (report[0] == report[1])
                {
                    failedlist.RemoveAt(0);
                    if (IsSafe(failedlist))
                    {
                        answer++;
                    }

                }
                else if (report[0] < report[1])
                {

                    for (int x = 0; x < report.Count() - 1; x++)
                    {
                        if (!(report[x] < report[x + 1] && (report[x + 1] - report[x] <= 3) && (report[x + 1] - report[x] > 0)))
                        {
                            
                            List<int> failedlist1 = report.ToList();
                            List<int> failedlist2 = report.ToList();
                            if (x > 0)
                            {
                                List<int> failedlist3 = report.ToList();
                                failedlist3.RemoveAt(x - 1);
                                if (IsSafe(failedlist3))
                                {
                                    //FixedReports.Add(failedlist3);
                                    answer++;
                                    break;
                                }

                            }
                            failedlist1.RemoveAt(x);
                            if (IsSafe(failedlist1))
                            {
                                //FixedReports.Add(failedlist1);
                                answer++;
                                break;
                            }
                            failedlist2.RemoveAt(x + 1);
                            if (IsSafe(failedlist2))
                            {
                                //FixedReports.Add(failedlist2);
                                answer++;
                                break;
                            }
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
                           
                            List<int> failedlist1 = report.ToList();
                            List<int> failedlist2 = report.ToList();
                            if (x > 0)
                            {
                                List<int> failedlist3 = report.ToList();
                                failedlist3.RemoveAt(x - 1);
                                if (IsSafe(failedlist3))
                                {
                                    //FixedReports.Add(failedlist3);
                                    answer++;
                                    break;
                                }

                            }
                            failedlist1.RemoveAt(x);
                            if (IsSafe(failedlist1))
                            {
                                //FixedReports.Add(failedlist1);
                                answer++;
                                break;
                            }
                            failedlist2.RemoveAt(x + 1);
                            if (IsSafe(failedlist2))
                            {
                                //FixedReports.Add(failedlist2);
                                answer++;
                                break;
                            }

                            break;

                        }
                    }
                }
                               
            }
            return answer;
        }

    }
}
