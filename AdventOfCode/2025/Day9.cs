using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class Y2025D9
    {
        public static string[] Lines = File.ReadAllLines(".\\2025\\Input\\inputDay9.txt");
        record struct Point(int X, int Y);
        record struct Edge(Point A, Point B);

        static bool IsOnBoundary(Point p, List<Edge> edges)
        {
            foreach (var e in edges)
            {
                var a = e.A;
                var b = e.B;

                if (a.X == b.X) 
                {
                    if (p.X == a.X &&
                        p.Y >= Math.Min(a.Y, b.Y) &&
                        p.Y <= Math.Max(a.Y, b.Y))
                    {
                        return true;
                    }
                }
                else 
                {
                    if (p.Y == a.Y &&
                        p.X >= Math.Min(a.X, b.X) &&
                        p.X <= Math.Max(a.X, b.X))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        static bool IsInsideOrOn(Point p, List<Edge> edges)
        {
            if (IsOnBoundary(p, edges))
                return true;

            long x = p.X;
            long y = p.Y;
            bool inside = false;

            foreach (var edge in edges)
            {
                var a = edge.A;
                var b = edge.B;

                
                if (a.X != b.X)
                    continue; 

                
                if (a.Y > b.Y)
                {
                    var tmp = a;
                    a = b;
                    b = tmp;
                }

               
                if (y < a.Y || y >= b.Y)
                    continue;

               
                if (a.X > x)
                {
                    inside = !inside;
                }
            }

            return inside;
        }

        static bool PolygonIntersectsRectangleInterior(
    List<Edge> edges,
    int x1, int x2,
    int y1, int y2)
        {
           
            if (x1 == x2 || y1 == y2)
                return false;

            foreach (var edge in edges)
            {
                var a = edge.A;
                var b = edge.B;

                if (a.X == b.X)
                {
                   
                    int x = a.X;
                    if (x <= x1 || x >= x2)
                        continue; 

                    int ey1 = Math.Min(a.Y, b.Y);
                    int ey2 = Math.Max(a.Y, b.Y);

                
                    int low = Math.Max(ey1, y1);
                    int high = Math.Min(ey2, y2);

                    if (low < high) 
                        return true;
                }
                else
                {
                    
                    int y = a.Y;
                    if (y <= y1 || y >= y2)
                        continue; 
                    int ex1 = Math.Min(a.X, b.X);
                    int ex2 = Math.Max(a.X, b.X);

                    int low = Math.Max(ex1, x1);
                    int high = Math.Min(ex2, x2);

                    if (low < high)
                        return true;
                }
            }

            return false;
        }


        public long Part1() 
        {
            long answer = 0;

            var reds = new List<(int x, int y)>();
            foreach (var line in Lines)
            {
                var parts = line.Split(',').Select(n=>int.Parse(n)).ToList();
                reds.Add((parts[0], parts[1]));
            }

            long maxArea = 0;

            for (int i = 0; i < reds.Count; i++)
            {
                for (int j = i + 1; j < reds.Count; j++)
                {
                    int dx = Math.Abs(reds[i].x - reds[j].x);
                    int dy = Math.Abs(reds[i].y - reds[j].y);

                    int width = dx + 1;
                    int height = dy + 1;
                    long area = (long)width * height;

                    if (area > maxArea)
                        maxArea = area;
                }
            }

            answer = maxArea;

            return answer;
        }

        
        public long Part2()
        {
            long answer = 0;

           

            var redsInOrder = new List<Point>();
            var redSet = new HashSet<Point>();

            var vertices = new List<Point>();

            foreach (var line in Lines)
            {
                var parts = line.Split(',');
                int x = int.Parse(parts[0]);
                int y = int.Parse(parts[1]);
                var p = new Point(x, y);
                vertices.Add(p);
            }

            int n = vertices.Count;

            var edges = new List<Edge>(n);
            for (int i = 0; i < n; i++)
            {
                var a = vertices[i];
                var b = vertices[(i + 1) % n]; 
                edges.Add(new Edge(a, b));
            }

            long bestArea = 0;

            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    var a = vertices[i];
                    var b = vertices[j];

                    int x1 = Math.Min(a.X, b.X);
                    int x2 = Math.Max(a.X, b.X);
                    int y1 = Math.Min(a.Y, b.Y);
                    int y2 = Math.Max(a.Y, b.Y);

                    long width = (long)(x2 - x1 + 1);
                    long height = (long)(y2 - y1 + 1);
                    long area = width * height;

                    if (area <= bestArea)
                        continue; 

                    var c = new Point(a.X, b.Y);
                    var d = new Point(b.X, a.Y);

                    
                    if (!IsInsideOrOn(c, edges) || !IsInsideOrOn(d, edges))
                        continue;

                    if (PolygonIntersectsRectangleInterior(edges, x1, x2, y1, y2))
                        continue;

                    bestArea = area;
                }
            }

            answer = bestArea;

            return answer;
        }

    }
}
