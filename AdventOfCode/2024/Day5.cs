using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static AdventOfCode.Y2024D5;

namespace AdventOfCode
{
    internal class Y2024D5
    {
        public static string[] Lines = File.ReadAllLines(".\\2024\\Input\\inputDay5.txt");
        public static List<Rule> Rulelist = new();
        public static List<Update> UpdateList = new();
        public static List<Update> FailedUpdates = new();

        public class Rule(int before, int after)
        {
            public int before = before;
            public int after = after;
        }

        public class Update 
        {
            public List<int> update;
            List<Rule> AppliedRules = new();
            public Update(List<int> update)
            {
                this.update = update;
                foreach(Rule rule in Rulelist)
                {
                    if(update.Contains(rule.before) && update.Contains(rule.after))
                    {
                        
                       AppliedRules.Add(rule);
                     
                    }
                }
            }
            public bool CheckOrder()
            {
                foreach(Rule appliedrule in AppliedRules)
                {
                    int before = update.FindIndex(x=> x== appliedrule.before);
                    int after = update.FindIndex(x => x == appliedrule.after);

                    if(before > after) {

                        return false;
                    }
                }
                
                return true;
            }

            public int ReturnMiddle()
            {
                return update[update.Count / 2];           
            }
            public void FixUpdate()
            {
                bool incorrect = true;
                while (incorrect) {
                    incorrect = false;
                    foreach (Rule appliedrule in AppliedRules)
                    {
                        int indexbefore = update.FindIndex(x => x == appliedrule.before);
                        int indexafter = update.FindIndex(x => x == appliedrule.after);

                        if (indexbefore > indexafter)
                        {
                            update.RemoveAt(indexafter);
                            update.Insert(indexbefore, appliedrule.after);
                            incorrect = true;

                        }
                    }
                }
            }

        }
        public int Part1() 
        {
            int answer = 0;

            foreach(string line in Lines)
            {

                if(line.Length == 5)
                {
                    List<int> parts = line.Replace("|",",").Split(Regex.Escape(",")).Select(x=>int.Parse(x)).ToList();
                    Rulelist.Add(new(parts[0], parts[1]));
                }
                else if(line == "")
                {
                    continue;
                }
                else
                {
                    List<int> update = line.Split(",").Select(x => int.Parse(x)).ToList();
                    UpdateList.Add(new(update));
                }
            }
            foreach(Update update in UpdateList)
            {
                if (update.CheckOrder())
                {
                    answer += update.ReturnMiddle();
                }
                else
                {
                    FailedUpdates.Add(update);
                }
            }

            return answer;
        }

        public int Part2()
        {
            int answer = 0;

            foreach(Update failedupdate in FailedUpdates)
            {
                failedupdate.FixUpdate();
                answer += failedupdate.ReturnMiddle();
            }

            return answer;
        }

    }
}
