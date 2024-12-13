﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class Y2024D13
    {
        public static string[] Lines = File.ReadAllLines(".\\2024\\Input\\inputDay13.txt");
        static List<(int ax ,int ay,int bx ,int by,long px,long py)> machines = new();
        static List<(int id, long P1Tokens, long P2Tokens)> Tokens = new();
        public static void SolveMachine(int ax, int ay, int bx, int by, long px, long py,int id)
        {
            long P1Tokens = 0;
            long P2Tokens = 0;
            var B = (py * ax - px * ay) / (by * ax - bx * ay);
            var A = (px - B * bx) / ax;

            if (A >= 0 && B >= 0 && B * bx + A * ax == px && B * by + A * ay == py) P1Tokens = A * 3 + B;
                   
 
            px += 10000000000000;
            py += 10000000000000;
            B = (py * ax - px * ay) / (by * ax - bx * ay);
            A = (px - B * bx) / ax;
            if (A >= 0 && B >= 0 && B * bx + A * ax == px && B * by + A * ay == py) P2Tokens = A * 3 + B;

            Tokens.Add((id, P1Tokens, P2Tokens));
            
        }

        public long Part1()
        {
            long answer = 0;
            

            for (int i = 0; i < Lines.Length; i += 4)
            {
                if (string.IsNullOrWhiteSpace(Lines[i])) continue;

                var buttonA = Lines[i].Split(new[] { "+", "," }, StringSplitOptions.RemoveEmptyEntries);
                var ax = int.Parse(buttonA[1]);
                var ay = int.Parse(buttonA[3]);

                var buttonB = Lines[i + 1].Split(new[] { "+", "," }, StringSplitOptions.RemoveEmptyEntries);
                var bx = int.Parse(buttonB[1]);
                var by = int.Parse(buttonB[3]);

                var prize = Lines[i + 2].Split(new[] { "=", "," }, StringSplitOptions.RemoveEmptyEntries);
                var px = long.Parse(prize[1]);
                var py = long.Parse(prize[3]);

                machines.Add((ax, ay, bx, by, px, py));
            }

           
            int id = 0;
            foreach (var machine in machines)
            {
                SolveMachine(machine.ax, machine.ay, machine.bx, machine.by, machine.px, machine.py, id);

                id++;             
            }

            answer = Tokens.Sum(t => t.P1Tokens);
            return answer;
        }

        public long Part2()
        {
            long answer = 0;
       
          

            answer = Tokens.Sum(t => t.P2Tokens);
            return answer;
        }
    }
}