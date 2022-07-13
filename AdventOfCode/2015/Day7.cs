using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2015
{

    class Instruction
    {
        public static Dictionary<string, int> codex = new Dictionary<string, int>();

        public string Gate = "";
        public string output = "";
        public string input1 = "";
        public string input2 = "";


        public Instruction()
        {

        }
        public Instruction(string input1, string input2, string Gate, string output)
        {

            this.Gate = Gate;
            this.input1 = input1;
            this.input2 = input2;
            this.output = output;
        }
        public Instruction(string input1, string Gate, string output)
        {

            this.Gate = Gate;
            this.input1 = input1;
            this.output = output;
        }

        public static int GetItemFromCodex(string key)
        {
            int value;

            try
            {
                value = codex[key];
            }
            catch (KeyNotFoundException)
            {
                value = -1;
            }
            return value;
        }


        public void caclulate()
        {

            ushort x;
            ushort y = 0;


            if ((this.input1).All(char.IsDigit))
            {
                x = Convert.ToUInt16(this.input1);
            }
            else
            {
                x = (ushort)codex[this.input1];
            }
            if (this.input2 != "")
            {
                if ((this.input2).All(char.IsDigit))
                {
                    y = Convert.ToUInt16(this.input2);
                }
                else
                {
                    y = (ushort)codex[this.input2];
                }
            }

            int result = 0;
            switch (Gate)
            {
                case "AND":
                    // x = codex[this.input1];
                    // y = codex[this.input2];

                    result = (x & y);
                    break;


                case "OR":
                    {
                        // x = codex[this.input1];
                        // y = codex[this.input2];
                        result = (x | y);
                        break;
                    }
                case "NOT":
                    {
                        // x = codex[this.input1];
                        result = ((ushort)~x);
                        break;
                    }
                case "LSHIFT":

                    result = (x << y);
                    break;
                case "RSHIFT":

                    result = x >> y;
                    break;

            }

            codex.Add(this.output, result);
        }


    }
    internal class Day7
    {
        public static int Day7A(int B = 0)
        {
            Queue<Instruction> instructions = new Queue<Instruction>();
            Instruction.codex = new Dictionary<string, int>();
            string[] textInput = File.ReadAllLines(".\\2015\\Input\\inputDay7.txt");
            foreach (string line in textInput)
            {
                string[] result = line.Split(' ');
                int count = result.Count();
                if (B > 0) {
                    if (line.EndsWith(" -> b")) {
                        result[0] = B.ToString();
                    }
                }
                switch (count)
                {
                    case 3:

                        instructions.Enqueue(new Instruction(result[0], "IS", result[2]));
                        break;

                    case 4:
                        ;
                        instructions.Enqueue(new Instruction(result[1], result[0], result[3]));
                        break;

                    case 5:
                        instructions.Enqueue(new Instruction(result[0], result[2], result[1], result[4]));
                        break;


                }
            }



            while (instructions.Count > 0)
            {


                Instruction item = instructions.Dequeue();

                switch (item.Gate)
                {
                    case "IS":
                        if (item.input1.All(char.IsDigit))
                        {
                            int number = Convert.ToInt32(item.input1);
                            Instruction.codex.Add(item.output, (int)number);
                        }
                        else
                        {
                            if (Instruction.codex.ContainsKey(item.input1))
                            {
                                int value = Instruction.codex[item.input1];
                                Instruction.codex.Add(item.output, value);
                            }
                            else
                            {
                                instructions.Enqueue(item);
                            }

                        }
                        break;
                    case ("NOT" or "LSHIFT" or "RSHIFT"):
                        if (Instruction.codex.ContainsKey(item.input1))
                        {
                            item.caclulate();
                        }
                        else
                        {
                            instructions.Enqueue(item);
                        }
                        break;
                    default:
                        if (((Instruction.codex.ContainsKey(item.input1)) && (Instruction.codex.ContainsKey(item.input2))) || (((item.input1).All(char.IsDigit)) && (Instruction.codex.ContainsKey(item.input2))))
                        {
                            item.caclulate();
                        }
                        else
                        {
                            instructions.Enqueue(item);
                        }
                        break;
                }


            }

            int answer = Instruction.GetItemFromCodex("a");
            return answer;
        }
     
    }

    
}