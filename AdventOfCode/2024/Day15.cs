using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class Y2024D15
    {
        public static string[] Lines = File.ReadAllLines(".\\2024\\Input\\inputDay15.txt");
        static Dictionary<char, (int dx, int dy)> directions = new Dictionary<char, (int, int)>
            {
                { '<', (0, -1) },
                { '>', (0, 1) },
                { '^', (-1, 0) },
                { 'v', (1, 0) }
            };
        public string[] ScaleUpWarehouse(string[] originalWarehouse)
        {
            List<string> scaledUpWarehouse = new List<string>();

            foreach (string line in originalWarehouse)
            {
                string scaledLine = "";
                foreach (char tile in line)
                {
                    scaledLine += tile switch
                    {
                        '#' => "##",
                        'O' => "[]",
                        '.' => "..",
                        '@' => "@.",
                        _ => throw new Exception($"Wrong Input Tile: {tile}")
                    };
                }
                scaledUpWarehouse.Add(scaledLine);
            }

            return scaledUpWarehouse.ToArray();
        }

        public int Part1() 
        {
            int answer = 0;

            List<string> warehouseLines = new List<string>();
            string moves = "";

            foreach (string line in Lines)
            {
                if (line.StartsWith("#")) 
                {
                    warehouseLines.Add(line);
                }
                else if (!string.IsNullOrWhiteSpace(line)) 
                {
                    moves += line.Trim();
                }
            }
            

            string[] warehouse  = warehouseLines.ToArray();

            int rows = warehouse.Length; 
            int cols = warehouse[0].Length; 
            char[,] grid = new char[rows, cols];
            (int x, int y) robotPos = (0, 0);

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    grid[r, c] = warehouse[r][c];
                    if (grid[r, c] == '@')
                    {
                        robotPos = (r, c);
                    }
                }
            }

            foreach (char move in moves)
            {
                (int dx, int dy) = directions[move];
                (int newX, int newY) = (robotPos.x + dx, robotPos.y + dy);


                if (grid[newX, newY] == '.')
                {
                    //Console.WriteLine("Move");
                    grid[robotPos.x, robotPos.y] = '.';
                    grid[newX, newY] = '@';
                    robotPos = (newX, newY);
                }
                else if (grid[newX, newY] == 'O')
                {

                    List<(int x, int y)> boxesToMove = new List<(int x, int y)>();
                    (int currentX, int currentY) = (newX, newY);


                    while (grid[currentX, currentY] == 'O')
                    {
                        boxesToMove.Add((currentX, currentY));
                        currentX += dx;
                        currentY += dy;
                    }


                    if (grid[currentX, currentY] == '.')
                    {

                        for (int i = boxesToMove.Count - 1; i >= 0; i--)
                        {
                            (int fromX, int fromY) = boxesToMove[i];
                            (int toX, int toY) = (fromX + dx, fromY + dy);
                            grid[toX, toY] = 'O';
                            grid[fromX, fromY] = '.';
                        }


                        grid[robotPos.x, robotPos.y] = '.';
                        grid[newX, newY] = '@';
                        robotPos = (newX, newY);
                    }
                    else
                    {
                        //Console.WriteLine("Wall");

                    }
                }
                else
                {
                    //Console.WriteLine("Wall");


                }
            
            }

            // Calculate the sum of GPS coordinates of all boxes
            int sumGPS = 0;
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    if (grid[r, c] == 'O')
                    {
                        int gps = 100 * r + c;
                        sumGPS += gps;
                    }
                }
            }

            answer = sumGPS;

            return answer;
        }

        public int Part2()
        {
            int answer = 0;

            List<string> warehouseLines = new List<string>();
            string moves = "";

            foreach (string line in Lines)
            {
                if (line.StartsWith("#"))
                {
                    warehouseLines.Add(line);
                }
                else if (!string.IsNullOrWhiteSpace(line))
                {
                    moves += line.Trim();
                }
            }

            string[] originalWarehouse = warehouseLines.ToArray();

            // Generate scaled-up warehouse
            string[] warehouse = ScaleUpWarehouse(originalWarehouse);

            int rows = warehouse.Length;
            int cols = warehouse[0].Length;
            char[,] grid = new char[rows, cols];
            (int x, int y) robotPos = (0, 0);

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    grid[r, c] = warehouse[r][c];
                    if (grid[r, c] == '@')
                    {
                        robotPos = (r, c);
                    }
                }
            }

            //PrintWarehouse(grid);
            //Console.WriteLine("Start");
            
            foreach (char move in moves)
            {
                (int dx, int dy) = directions[move];
                (int newX, int newY) = (robotPos.x + dx, robotPos.y + dy);


                if (grid[newX, newY] == '.')
                {
                   //Console.WriteLine("Move");
                    grid[robotPos.x, robotPos.y] = '.';
                    grid[newX, newY] = '@';
                    robotPos = (newX, newY);
                }
                else if(grid[newX, newY] == '[' || grid[newX, newY] == ']')
                {
                    switch (move)
                    {
                        case '<':

                            robotPos = MoveHorizontal(grid, robotPos, dx, dy, newX, newY);
                            break;
                        case '>':
                            robotPos = MoveHorizontal(grid, robotPos, dx, dy, newX, newY);
                            break;
                        case '^':
                            robotPos = MoverVertical(grid, robotPos, dx, dy, newX, newY);

                            break;
                        case 'v':
                            robotPos = MoverVertical(grid, robotPos, dx, dy, newX, newY);
                            break;
                    }
                }
              
                else
                {
                    //Console.WriteLine("Wall");
                }
                //PrintWarehouse(grid);
                //Console.WriteLine("Move: " + move);
            }

            int sumGPS = 0;
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols - 1; c++) // Only consider the first part of the box
                {
                    if (grid[r, c] == '[' && grid[r, c + 1] == ']')
                    {
                        int gps = 100 * r + c;
                        sumGPS += gps;
                    }
                }
            }

            Console.WriteLine($"Sum of GPS coordinates of all boxes: {sumGPS}");
            answer = sumGPS;


            return answer;
        }

        private static (int x, int y) MoverVertical(char[,] grid, (int x, int y) robotPos, int dx, int dy, int newX, int newY)
        {
            List<(int x, int y)> boxesPartsToMove = new List<(int x, int y)>();
            HashSet<(int x, int y)> boxesToCheck = new HashSet<(int x, int y)>();
            (int currentX, int currentY) = (newX, newY);
            boxesPartsToMove.Add((newX, newY));
            boxesToCheck.Add((newX, newY));
            if (grid[newX, newY] == ']')
            {
                boxesPartsToMove.Add((newX, newY - 1));
                boxesToCheck.Add((newX, newY - 1));
            }
            else
            {
                boxesPartsToMove.Add((newX, newY + 1));
                boxesToCheck.Add((newX, newY + 1));
            }
            bool StillBox = true;
            bool ValidMove = true;
            while (StillBox)
            {
                HashSet<(int x, int y)> boxesToCheckNext = new HashSet<(int x, int y)>();
                foreach (var boxToCheck in boxesToCheck)
                {
                    
                    if (grid[boxToCheck.x + dx, boxToCheck.y] == '#')
                    {
                        StillBox = false;
                        ValidMove = false;
                        break;
                    }
                    else if (grid[boxToCheck.x + dx, boxToCheck.y + dy] == '[' || grid[boxToCheck.x + dx, boxToCheck.y] == ']')
                    {
                        if (!boxesPartsToMove.Contains((boxToCheck.x + dx, boxToCheck.y + dy)))
                        {
                            boxesPartsToMove.Add((boxToCheck.x + dx, boxToCheck.y + dy));
                        }
                        
                        boxesToCheckNext.Add((boxToCheck.x + dx, boxToCheck.y + dy));
                        if (grid[boxToCheck.x + dx, boxToCheck.y] == ']')
                        {
                            if (!boxesPartsToMove.Contains((boxToCheck.x + dx, boxToCheck.y + dy - 1)))
                            {
                                boxesPartsToMove.Add((boxToCheck.x + dx, boxToCheck.y + dy - 1));
                            }
                            boxesToCheckNext.Add((boxToCheck.x + dx, boxToCheck.y + dy - 1));
                        }
                        else
                        {
                            if (!boxesPartsToMove.Contains((boxToCheck.x + dx, boxToCheck.y + dy + 1)))
                            {
                                boxesPartsToMove.Add((boxToCheck.x + dx, boxToCheck.y + dy + 1));
                            }
                            boxesToCheckNext.Add((boxToCheck.x + dx, boxToCheck.y + dy + 1));
                        }
                    }

                   

                }

                boxesToCheck = boxesToCheckNext;
                if (boxesToCheck.Count == 0)
                {
                    StillBox = false;
                }

            }

            if (ValidMove)
            {
                for (int i = boxesPartsToMove.Count - 1; i >= 0; i--)
                {


                    (int fromX, int fromY) = boxesPartsToMove[i];
                    (int toX, int toY) = (fromX + dx, fromY + dy);
                    grid[toX, toY] = grid[fromX, fromY];
                    grid[fromX, fromY] = '.';
                }


                grid[robotPos.x, robotPos.y] = '.';
                grid[newX, newY] = '@';
                robotPos = (newX, newY);
            }
            return robotPos;
        }

        private static (int x, int y) MoveHorizontal(char[,] grid, (int x, int y) robotPos, int dx, int dy, int newX, int newY)
        {
            List<(int x, int y)> boxesPartsToMove = new List<(int x, int y)>();
            (int currentX, int currentY) = (newX, newY);


            while (grid[currentX, currentY] == grid[newX, newY])
            {
                boxesPartsToMove.Add((currentX, currentY));
                boxesPartsToMove.Add((currentX, currentY + dy));
                currentX += dx;
                currentY += 2 * dy;
            }
            if (grid[currentX, currentY] == '.')
            {

                for (int i = boxesPartsToMove.Count - 1; i >= 0; i--)
                {


                    (int fromX, int fromY) = boxesPartsToMove[i];
                    (int toX, int toY) = (fromX + dx, fromY + dy);
                    grid[toX, toY] = grid[fromX, fromY];
                }

                grid[robotPos.x, robotPos.y] = '.';
                grid[newX, newY] = '@';
                robotPos = (newX, newY);
            }

            return robotPos;
        }
        private static void PrintWarehouse(char[,] grid)
        {
            for (int i = 0; i < grid.GetLength(0); i++) 
            {
                for (int j = 0; j < grid.GetLength(1); j++) 
                {
                    Console.Write(grid[i, j]); 
                }
                Console.WriteLine(); 
            }
            Console.WriteLine(); 
        }
    }
    
}
