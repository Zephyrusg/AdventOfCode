using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class Y2023D4
    {
        static List<Card> Cards = new List<Card>();
        
        class Card {
            public int id;
            public int MatchingNumbers;
            public int Quantity = 1;
            List<int> Numbers;
            List<int> WinningNumbers;

            public Card(int id, List<int> numbers, List<int> winningNumbers)
            {
                this.id = id;
                Numbers = numbers;
                WinningNumbers = winningNumbers;
            }

            public int Getvalue() { 
                int value = 0;

                foreach(int number in Numbers)
                {
                    if (WinningNumbers.Contains(number)) {
                        if (value == 0)
                        {
                            value ++;
                        }
                        else {
                            value = value * 2;
                        }
                        this.MatchingNumbers++;
                    }
                }

                return value;
            }
        }
        public static string[] lines = File.ReadAllLines(".\\2023\\Input\\inputDay4.txt");
        public int Part1() 
        {
            int answer = 0;

            foreach(string line in lines)
            {
                string[] parts = line.Split(": ");
                string Pattern = "\\d+";
                var Match = Regex.Match(parts[0], Pattern);
                int id = int.Parse(Match.Value);
                string[] NumberSets = parts[1].Split(" | ");
                string[] NumbersData = NumberSets[0].Split(' ');
               
                var list = NumbersData.Where(y => y != "").ToList();
                List<int> Numbers = list.Select(x => int.Parse(x)).ToList();
                string[] WinningNumbersData = NumberSets[1].Split(' ');
                list = WinningNumbersData.Where(y => y != "").ToList();
                List<int> WinningNumbers = list.Select(x => int.Parse(x)).ToList();

                Cards.Add(new(id,Numbers,WinningNumbers));

            }

            foreach (Card Card in Cards) {

                answer += Card.Getvalue();
            
            }

            return answer;
        }

        public int Part2()
        {

            int answer = 0;

            foreach (Card Card in Cards) {
                
                for (int x = 0; x < Card.MatchingNumbers; x++)
                {
                    int id = Card.id;
                    Cards[id + x].Quantity += Card.Quantity;
                   
                }
                
            }

            answer = Cards.Sum(x => x.Quantity);

            return answer;
        }

    }
}
