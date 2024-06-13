using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class Y2015D10
    {
        static string sequence = "1321131112";
        static string LookandSay(string currentSequence)
        {
            StringBuilder newsequence = new StringBuilder();
            int i = 0;
            while (i < currentSequence.Length)
            {
                int count = 1;
                while ((i + 1) < currentSequence.Length && currentSequence[i] == currentSequence[i + 1]) {
                    i++;
                    count++;
                }
                newsequence.Append(count);
                newsequence.Append(currentSequence[i]);
                i++;

            }

            return newsequence.ToString();
        }
        public long Part1() 
        {
            long answer = 0;
            string sequence = Y2015D10.sequence; 

            for(int i = 0; i < 40; i++)
            {
                sequence = Y2015D10.LookandSay(sequence);
                //Console.WriteLine("Done Round: " + i + " Current Length: " + sequence.Length);
            }
            answer = sequence.Length;
            Y2015D10.sequence = sequence;



            return answer;
        }

        public long Part2()
        {
            long answer = 0;
            string sequence = Y2015D10.sequence;
            for (int i = 0; i < 10; i++)
            {
                sequence = Y2015D10.LookandSay(sequence);
                //Console.WriteLine("Done Round: " + i + " Current Length: " + sequence.Length);
            }
            answer = sequence.Length;
            



            return answer;



            
        }

    }
}
