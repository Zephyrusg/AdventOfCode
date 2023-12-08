using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    

    internal class Y2023D7
    {
        static public List<Hand> Hands = new List<Hand>();
        static public char[] CardOrder = { '2', '3', '4', '5', '6', '7', '8', '9', 'T', 'J', 'Q', 'K', 'A' };


        public enum HandStrengths
        {
            HighCard = 1,
            OnePair = 2,
            TwoPair = 3,
            ThreeofaKind = 4,
            FullHouse = 5,
            FourofaKind = 6,
            FiveofaKind = 7

        }

        public class Hand : IComparable

        {
            public string HandCards;
            public int bid;
            public int handstrength = 0;


            public Hand(string HandCards, int bid) {
                this.HandCards = HandCards;
                this.bid = bid;
                this.handstrength = GetHandStrength(this.HandCards);
            }


          

            int GetHandStrength(string HandCards)
            {
                char[] Cards = HandCards.ToCharArray();
                Dictionary<Char, int> HandData = new Dictionary<Char, int>();
                foreach (char c in Cards)
                {
                    if (HandData.ContainsKey(c))
                    {
                        HandData[c]++;
                    }
                    else { 
                        HandData.Add(c, 1);
                    }
                }
                if (HandData.Count == 1)
                {
                    return (int)HandStrengths.FiveofaKind;
                }
                else if (HandData.Values.Contains(4))
                {
                    return (int)HandStrengths.FourofaKind;
                }
                else if (HandData.Count() == 2)
                {
                    return (int)(HandStrengths.FullHouse);
                }
                else if (HandData.Values.Contains(3))
                {
                    return (int)(HandStrengths.ThreeofaKind);
                }
                else if (HandData.Count() == 3)
                {
                    return (int)(HandStrengths.TwoPair);
                }
                else if (HandData.Count() == 4)
                {
                    return (int)(HandStrengths.OnePair);
                }
                else {
                    return (int)(HandStrengths.HighCard);
                }
            }

            public void ConvertJoker()
            {
                
                char[] Othercards = { '2', '3', '4', '5', '6', '7', '8', '9', 'T', 'Q', 'K', 'A' };
                if (this.HandCards.ToCharArray().Contains('J')) { 
                    int HighestStrength = this.handstrength;
                    int result = 0;
                    foreach(char Card in Othercards)
                    {
                        string TempCards = this.HandCards.Replace('J', Card);
                        result = GetHandStrength(TempCards);
                        if (result > HighestStrength) { 
                            HighestStrength = result;
                        }
                    }

                    this.handstrength = HighestStrength;
                }
                
            }

            public int CompareTo(object? obj)
            {
                Hand o = (Hand)obj;
                char[] Own = this.HandCards.ToCharArray();
                char[] Other = o.HandCards.ToCharArray();

                if(this.handstrength < o.handstrength)
                {
                    return -1;
                }else if(this.handstrength > o.handstrength) { 
                    return 1; 
                }

                for(int i = 0; i < Own.Length; i++)
                {
                    int OwnCard = Array.IndexOf(CardOrder, Own[i]);
                    int OtherCard = Array.IndexOf(CardOrder, Other[i]);
                    if(OwnCard < OtherCard)
                    {
                        return -1;
                    }else if (OtherCard < OwnCard)
                    { 
                        return 1;
                    }
                }

                return 0;
            }

            public override string ToString()
            {
                return this.HandCards;
            }

        }

            public static string[] lines = File.ReadAllLines(".\\2023\\Input\\inputDay7.txt");
        public int Part1() 
        {
            int answer = 0;

            foreach (string line in lines)
            {
                string[] Parts = line.Split(" ");
                Hands.Add(new(Parts[0], int.Parse(Parts[1])));
            }
            
            Hands.Sort();

            for (int x = 0; x < Hands.Count; x++) {
                answer += Hands[x].bid * (x + 1);
            }

            return answer;
        }

        public int Part2()
        {
            int answer = 0;
            CardOrder = new char[]{ 'J','2', '3', '4', '5', '6', '7', '8', '9', 'T', 'Q', 'K', 'A'};
            foreach (Hand Handcards in Hands)
            {
                Handcards.ConvertJoker();
            }

            Hands.Sort();
      
            for (int x = 0; x < Hands.Count; x++)
            {
                answer += Hands[x].bid * (x + 1);
            }

            return answer;
        }

    }
}
