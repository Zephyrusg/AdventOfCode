using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022
{
    internal class Day8
    {

        static int[][] ?Matrix; 
        static string Path = ".\\2022\\Input\\InputDay8.txt";

        public static int Part1()
        {
            string[] Data = File.ReadAllLines(Path);
            int answer = 0;

            int height = Data.Count();
            int width = Data[0].Length;

            Matrix = new int[width][];
            int x = 0;
            foreach (string line in Data)
            {
                Matrix[x] = new int[width];
                for (int y = 0; y < width; y++)
                {
                    
                    Matrix[x][y] = line[y] - '0';


                }
                x++;
            }

            int visibleTreeCount = height * 2 + width * 2 - 4;

            for (int i = 0; i < Matrix.Length; i++)
            {
                for (int j = 0; j < Matrix[0].Length; j++)
                {
                    if (i - 1 >= 0 && i + 1 <= Matrix.Length -1 && j -1 >= 0 && j + 1 <= Matrix[0].Length - 1) {
                        // check if the tree is visible from the left
                        if (j > 0 && Enumerable.Range(0, j).All(k => Matrix[i][k] < Matrix[i][j]))
                        {
                            visibleTreeCount++;
                        }
                        // check if the tree is visible from the right
                        else if (j < Matrix[0].Length - 1 && Enumerable.Range(j + 1, Matrix[0].Length - j - 1).All(k => Matrix[i][k] < Matrix[i][j]))
                        {
                            visibleTreeCount++;
                        }
                        // check if the tree is visible from the top
                        else if (i > 0 && Enumerable.Range(0, i).All(k => Matrix[k][j] < Matrix[i][j]))
                        {
                            visibleTreeCount++;
                        }
                        // check if the tree is visible from the bottom
                        else if (i < Matrix.Length - 1 && Enumerable.Range(i + 1, Matrix.Length - i - 1).All(k => Matrix[k][j] < Matrix[i][j]))
                        {
                            visibleTreeCount++;
                        }
                    }
                }
            }
            answer = visibleTreeCount;
            return answer;
        }
        public static int Part2()
        {
            string[] Data = File.ReadAllLines(Path);
            int answer = 0;

           

            for (int i = 0; i < Matrix.Length; i++)
            {
                for (int j = 0; j < Matrix[0].Length; j++)
                {
                    int SenicScore = 0;
                    int left = 0;
                    int right = 0;
                    int up = 0;
                    int down = 0;

                    //check up
                    if (i - 1 >= 0 && i + 1 <= Matrix.Length - 1 && j - 1 >= 0 && j + 1 <= Matrix[0].Length - 1)
                    {
                        //check up
                        int tempi = i;
                        while (tempi - 1 >= 0 && Matrix[tempi - 1][j] < Matrix[i][j]) {
                            up++;
                            tempi--;
                        }if(tempi - 1 >= 0) { up++; }

                        // check down
                        tempi = i;
                        while (tempi + 1 <= Matrix.Length - 1 && Matrix[tempi + 1][j] < Matrix[i][j])
                        {
                            down++;
                            tempi++;
                        }
                        if (tempi + 1 < Matrix.Length - 1) { down++; }

                        // check left
                        int tempj = j;
                        while (tempj - 1 >= 0  && Matrix[i][tempj - 1] < Matrix[i][j])
                        {
                            left++;
                            tempj--;
                        }
                        if(tempj - 1 >= 0) { left++; }
                        // check right
                        tempj = j;
                        while (tempj + 1 <= Matrix[0].Length - 1 && Matrix[i][tempj + 1] < Matrix[i][j])
                        {
                            right++;
                            tempj++;
                        }
                        if(tempj + 1 <= Matrix[0].Length - 1) { right++; }

                        SenicScore = left * right * up * down;

                        if (answer < SenicScore)
                        {
                            answer = SenicScore;

                        }
                    }
                }
            }

            return answer;
        }
    }
}
