using AdventOfCode._2020;
using System;
using System.Diagnostics;
using System.Reflection;


namespace AdventOfCode._2022 // Note: actual namespace depends on the project name.
{
    internal class Program
    {

        static void Main(string[] args)
        {

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            Console.WriteLine("Answer: " + Day7.Part1());
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            Console.WriteLine("Completion Time: " + ts);
            stopWatch.Reset();
            stopWatch.Start();
            Console.WriteLine("Answer: " + Day7.Part2());
            stopWatch.Stop();
            ts = stopWatch.Elapsed;
            Console.WriteLine("Completion Time: " + ts);
        }
    }
}

//namespace AdventOfCode._2020 // Note: actual namespace depends on the project name.
//{
//    internal class Program
//    {
//        static void Main(string[] args)
//        {
//            //int answer;
//            Stopwatch stopWatch = new Stopwatch();
//            stopWatch.Start();

//            //Console.WriteLine(Day15.Day15A());
//            //Console.WriteLine(Day16.Day16A());
//            //Console.WriteLine(Day16.Day16B());
//            //Console.WriteLine(Day17.Day17A());
//            //Console.WriteLine(Day18.Day18A());
//            Console.WriteLine(Day19.Day19A());
//            stopWatch.Stop();
//            TimeSpan ts = stopWatch.Elapsed;
//           
//            //stopWatch.Reset();
//            //stopWatch.Start();
//            //Console.WriteLine(Day18.Day18B());
//            //stopWatch.Stop();
//            //ts = stopWatch.Elapsed;
//            //Console.WriteLine(ts);
//            // Console.ReadLine();
//        }
//    }
//}

//namespace AdventOfCode._2015 // Note: actual namespace depends on the project name.
//{
//    internal class Program
//    {
//        static void Main(string[] args)
//        {
//            //int answer;
//            Stopwatch stopWatch = new Stopwatch();
//            stopWatch.Start();
//            //Console.WriteLine(Day1.Day1A());
//            //Console.WriteLine(Day1.Day1B());
//            //Console.WriteLine(Day2.Day2A());
//            //Console.WriteLine(Day2.Day2B());
//            //Console.WriteLine(Day3.Day3A());
//            //Console.WriteLine(Day3.Day3B());
//            //Console.WriteLine(Day4.Day4A());
//            //Console.WriteLine(Day4.Day4B());
//            //Console.WriteLine(Day5.Day5A());
//            //Console.WriteLine(Day5.Day5B());
//            //Console.WriteLine(Day6.Day6A());
//            //Console.WriteLine(Day6.Day6B());

//            //answer = Day7.Day7A();
//            //Console.WriteLine("answer 7A: " + answer);
//            //answer = Day7.Day7A(answer);
//            //Console.WriteLine("answer 7B: " + answer);
//            Day8.Day8A();
//            stopWatch.Stop();
//            TimeSpan ts = stopWatch.Elapsed;
//            Console.WriteLine(ts);
//            //stopWatch.Reset();
//            //stopWatch.Start();
//            //Console.WriteLine(Day7.Day7B());
//            //stopWatch.Stop();
//            //ts = stopWatch.Elapsed;
//            //Console.WriteLine(ts);
//            //Console.ReadLine();
//        }
//    }
//}

//namespace AdventOfCode._2018 // Note: actual namespace depends on the project name.
//{
//    internal class Program
//    {
//        static void Main(string[] args)
//        {
//            Console.WriteLine(Day1.Day1A());
//            Console.WriteLine(Day1.Day1B());
//            Console.WriteLine(Day8.Day8A());
//            Console.WriteLine(Day8.Day8B());
//            //Console.ReadLine();
//        }
//    }
//}