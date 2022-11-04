using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2020
{
    internal class Day16
    {
        class TicketField
        {
            public string name = "";
            public int leftbound1;
            public int leftbound2;
            public int rightbound1;
            public int rightbound2;
            public List<int> PossiblePosition = new List<int>();
            public int Position;

            public TicketField()
            {
            }
            public TicketField(string name, int leftbound1, int leftbound2, int rightbound1, int rightbound2)
            {
                this.name = name;
                this.leftbound1 = leftbound1;
                this.leftbound2 = leftbound2;
                this.rightbound1 = rightbound1;
                this.rightbound2 = rightbound2;
            }
            public void RemoveFromPossiblePosition(int position) {

                if (this.PossiblePosition.Contains(position)) { 
                    this.PossiblePosition.Remove(position);
                }
            }


        }

        public static int Day16A()
        {
            int leftbound = 25;
            int rightbound = 974;

            int ScanningErrorRate = 0;

            string[] input = File.ReadAllLines(".\\2020\\Input\\InputDay16Tickets.txt");
            foreach (string line in input)
            {
                int[] ticket = line.Split(",").Select(num => Int32.Parse(num)).ToArray();
                foreach (int number in ticket)
                {
                    if (number < leftbound ^ number > rightbound)
                    {
                        ScanningErrorRate += number;
                    }
                }

            }

            return ScanningErrorRate;
        }

        public static long Day16B()
        {

            int leftbound = 25;
            int rightbound = 974;
            long Answer = 1;

            int[] MyTicket = File.ReadAllText(".\\2020\\Input\\InputDay16Myticket.txt").Split(",").Select(num => Int32.Parse(num)).ToArray();

            List<TicketField> TicketFields = new List<TicketField>();
            List<int[]> TicketList = new List<int[]>();

            string[] FieldsInputs = File.ReadAllLines(".\\2020\\Input\\InputDay16TicketRules.txt");

            foreach (string line in FieldsInputs)
            {
                string[] parts = line.Split(":");
                string name = parts[0];
                string[] Ranges = parts[1].Split(" or ");
                int[] range1 = Ranges[0].Split("-").Select(num => Int32.Parse(num)).ToArray();
                int[] range2 = Ranges[1].Split("-").Select(num => Int32.Parse(num)).ToArray();

                TicketField Field = new TicketField(name, range1[0], range1[1], range2[0], range2[1]);
                TicketFields.Add(Field);


            }

            string[] input = File.ReadAllLines(".\\2020\\Input\\InputDay16Tickets.txt");
            foreach (string line in input)
            {
                int[] ticket = line.Split(",").Select(num => Int32.Parse(num)).ToArray();
                foreach (int number in ticket)
                {
                    if (number < leftbound ^ number > rightbound)
                    {
                        goto NextTicket;
                    }
                }
                TicketList.Add(ticket);
            NextTicket:;

            }

            int ticketlength = TicketList[0].Length;


            for (int i = 0; i < ticketlength; i++)
            {
                foreach (TicketField Field in TicketFields)
                {
                    foreach (int[] TicketNumbers in TicketList)
                    {
                        int value = TicketNumbers[i];
                        if (value < Field.leftbound1 || (value > Field.leftbound2 && value < Field.rightbound1) || value > Field.rightbound2)
                        {
                            goto NextField;
                        }
                    }

                    Field.PossiblePosition.Add(i);
                NextField:;
                }
            }

            List<TicketField> SortedTicketFields = TicketFields.OrderBy(o => o.PossiblePosition.Count).ToList();
            foreach (TicketField Field in SortedTicketFields) {
                if (Field.PossiblePosition.Count == 1)
                {
                    Field.Position = Field.PossiblePosition[0];
                    int RemovableNumber = Field.PossiblePosition[0];
                    foreach (TicketField Data in SortedTicketFields)
                    {
                        Data.RemoveFromPossiblePosition(RemovableNumber);
                    }
                }
            
            }

            List<TicketField> NeededTicketFields = SortedTicketFields.Where(Fields=>Fields.name.StartsWith("departure")).ToList();
            foreach (TicketField Field in NeededTicketFields) {
                Answer *= MyTicket[Field.Position];
            }

            return Answer;


        }
    }
}
