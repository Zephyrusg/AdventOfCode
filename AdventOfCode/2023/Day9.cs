using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class Y2023D9
    {
        public static string[] lines = File.ReadAllLines(".\\2023\\Input\\inputDay9.txt");
        public int Part1() 
        {
            int answer = 0;

            foreach (string line in lines) {
                List<List<int>> Histories = new List<List<int>>();
                List<int> History = line.Split(' ').Select(x => int.Parse(x)).ToList();
                Histories.Add(History);
                while (History.Sum() != 0)
                {
                    List<int> Difference  = new List<int>();
                    for(int x = 0; x < History.Count -1; x++)
                    {
                        int DiffernceValue = History[x+1] - History[x];
                        Difference.Add(DiffernceValue);
                    }
                    Histories.Add(Difference);
                    History = Difference;
                }
                Histories[Histories.Count - 1].Add(0);
                for (int x = Histories.Count - 2; x >= 0; x--) {
                    Histories[x].Add(Histories[x].Last() + Histories[x+1].Last());
                }

                answer += Histories[0].Last();
            }
       


            return answer;
        }

        public int Part2()
        {
            int answer = 0;

            foreach (string line in lines)
            {
                List<List<int>> Histories = new List<List<int>>();
                List<int> History = line.Split(' ').Select(x => int.Parse(x)).ToList();
                Histories.Add(History);
                while (History.Sum() != 0)
                {
                    List<int> Difference = new List<int>();
                    for (int x = 0; x < History.Count - 1; x++)
                    {
                        int DiffernceValue = History[x + 1] - History[x];
                        Difference.Add(DiffernceValue);
                    }
                    Histories.Add(Difference);
                    History = Difference;
                }

                Histories[Histories.Count - 1].Insert(0,0);
                for (int x = Histories.Count - 2; x >= 0; x--)
                {
                    Histories[x].Insert(0,(Histories[x].First() - Histories[x + 1].First()));
                }

                answer += Histories[0].First();

            }

                return answer;
        }

    }
}
