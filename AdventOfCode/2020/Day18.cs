using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2020
{
    internal class Day18
    {
        class OddCalcullator
        {
            public int memory;

            public void CalculateSum(string line) {
                
                for (int x =0; x < line.Length; x++) {
                    switch( line[x])
                    {
                        case '(':
                            int[] Result = this.SubSum(x+1);
                            //iets
                            x = Result[1];
                            break;
                            
                        default: 
                            break;
                                 
                    }
                }
            }

            public int[] SubSum(int index) {
                    int sum = 0;


                    return new int[] { sum, index };
            }

            public int Addition() {
                int Result = 0;

                return Result;
            }
            public int Multiply() {
                int Result = 0;

                return Result;
            }
        }
    }
}
