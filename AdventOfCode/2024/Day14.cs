using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace AdventOfCode
{
    internal class Y2024D14
    {
        public static string[] Lines = File.ReadAllLines(".\\2024\\Input\\inputDay14.txt");
        static int Height = 103;
        static int Width = 101;
        static int halfWidth = Width / 2;
        static int halfHeight = Height / 2;
        static List<Robot> robots = new List<Robot>();

        static Robot ParseRobot(string line)
        {

            // Example line: "p=0,4 v=3,-3"
            string[] parts = line.Split(new[] { "p=", "v=", ",", " " }, StringSplitOptions.RemoveEmptyEntries);

            int x = int.Parse(parts[0]);
            int y = int.Parse(parts[1]);
            int dx = int.Parse(parts[2]);
            int dy = int.Parse(parts[3]);

            return new Robot(x, y, dx, dy);
            
        }

        static int CheckNumberConnected()
        {
            int connected = 0;
            foreach (Robot robot in robots)
            {
                (int x, int y) = robot.GetPosition();
                int[] dx = { -1, 1, 0, 0 };
                int[] dy = { 0, 0, -1, 1 };
                for (int i = 0; i < 4; i++)
                {
                    int newx = x + dx[i];
                    int newy = y + dy[i];
                    if(robots.Any(r=>r.x == newx && r.y == newy))
                    {
                        connected++;
                        break;
                    }
                }
            }
            return connected;
        }

        class Robot
        {
            public int x; 
            public int y;
            int dx; 
            int dy;

            public Robot(int x, int y, int dx, int dy)
            {
                this.x = x;
                this.y = y;
                this.dx = dx;
                this.dy = dy;
            }
            public void Move()
            {
                // Update position with velocity
                x = (x + dx) % Width;
                y = (y + dy) % Height;

                // Handle negative wrapping
                if (x < 0) x += Width;
                if (y < 0) y += Height;
            }

            // Getter methods to access position
            public (int, int) GetPosition()
            {
                return (x, y);
            }
        }

        static void DisplayGrid()
        {
            int minX = robots.Min(r => r.x);
            int maxX = robots.Max(r => r.x);
            int minY = robots.Min(r => r.y);
            int maxY = robots.Max(r => r.y);

            for (int y = minY; y <= maxY; y++)
            {
                for (int x = minX; x <= maxX; x++)
                {
                    Console.Write(robots.Any(r => r.x == x && r.y == y) ? "#" : ".");
                }
                Console.WriteLine();
            }
        }

        static void SaveGridAsImage( int time)
        {
          
            // Create a bitmap
            using (var bitmap = new Bitmap(Width, Height))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.Clear(Color.Black);

                    // Draw each robot as a pixel
                    foreach (var robot in robots)
                    {
                        int x = robot.x;
                        int y = robot.y;
                        bitmap.SetPixel(x, y, Color.White);
                    }
                }

                // Save the image
                string fileName = $".\\grids\\grid_{time:D4}.png";
                bitmap.Save(fileName);
            }
        }

        public int Part1() 
        {
            int answer = 0;
            
            foreach (var line in Lines)
            {
                var robot = ParseRobot(line);
                if (robot != null)
                {
                    robots.Add(robot);
                }
            }

            for (int t = 0; t < 100; t++)
            {
                foreach (var robot in robots)
                {
                    robot.Move();
                }
            }

            // Count robots in each quadrant
            int q1 = 0, q2 = 0, q3 = 0, q4 = 0;
            foreach (var robot in robots)
            {
                var (x, y) = robot.GetPosition();

                if (x > halfWidth && y > halfHeight)
                    q1++; // Quadrant 1
                else if (x < halfWidth && y > halfHeight)
                    q2++; // Quadrant 2 LeftDown
                else if (x < halfWidth && y < halfHeight)
                    q3++; // Quadrant 3
                else if (x > halfWidth && y < halfHeight)
                    q4++; // Quadrant 4 RightUp
                

            }

            // Calculate safety factor
            int safetyFactor = q1 * q2 * q3 * q4;
            answer = safetyFactor;

            return answer;
        }

        public int Part2()
        {
            int answer = 0;
            bool noChristmasThree = true;
            robots = new();
            foreach (var line in Lines)
            {
                var robot = ParseRobot(line);
                if (robot != null)
                {
                    robots.Add(robot);
                }
            }
            int Seconds = 0;
            while (true)
            {
                foreach (var robot in robots)
                {
                    robot.Move();
                    Seconds++;
                    int connected = CheckNumberConnected();
                    if(connected > 50 && Seconds > 7129)
                    {
                        int minX = robots.Min(r => r.x);
                        int maxX = robots.Max(r => r.x);
                        int minY = robots.Min(r => r.y);
                        int maxY = robots.Max(r => r.y);

                        DisplayGrid();
                        SaveGridAsImage(Seconds);
                        Console.WriteLine("Seconds: " + Seconds);
                        //noChristmasThree = true;
                    }
                }
            }



            return Seconds;
        }

    }
}
