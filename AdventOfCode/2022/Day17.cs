using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022
{

    abstract class Block{
        static public HashSet<(int x,long y)> AllBlocks = new HashSet<(int, long)>();
        static public long MaxHeight = 0;
        public HashSet<(int x ,long y )> Figure = new HashSet<(int,long)>();
        static public Dictionary<string, Dictionary<string, (int x, int y)>> Memory = new Dictionary<string, Dictionary<string, (int x, int y)>>();
        public Block() { 
        }
        public void GoLeft() {
            bool PossibleMove = true;
            foreach ((int x, int y) point in Figure) { 
                if(point.x == 1 || AllBlocks.Contains((point.x - 1, point.y)))
                { 
                    PossibleMove = false;
                }
            }
            if(PossibleMove)
            {
                HashSet<(int x, long y)> NewLocation = new HashSet<(int, long)>();
                foreach ((int x, long y) point in Figure) {
                    (int x, long y) NewPoint = (point.x - 1, point.y);
                    NewLocation.Add(NewPoint);
                }
                Figure= NewLocation;
            }
        }
        public void GoRight() { 
            bool PossibleMove = true;
            foreach ((int x, long y) point in Figure)
            {
                if (point.x == 7 || AllBlocks.Contains((point.x + 1, point.y)))
                {
                    PossibleMove = false;
                }
            }
            if (PossibleMove)
            {
                HashSet<(int x, long y)> NewLocation = new HashSet<(int, long)>();
                foreach ((int x, long y) point in Figure)
                {
                    (int x, long y) NewPoint = (point.x + 1, point.y);
                    NewLocation.Add(NewPoint);
                }
                Figure = NewLocation;
            }
        }
            
        public bool Godown() {

            bool PossibleMove = true;
            foreach ((int x, long y) point in Figure)
            {
                if (point.y == 1 || AllBlocks.Contains((point.x, point.y-1)))
                {
                    PossibleMove = false;
                }
            }
            if (PossibleMove)
            {
                HashSet<(int x, long y)> NewLocation = new HashSet<(int, long)>();
                foreach ((int x, long y) point in Figure)
                {
                    (int x, long y) NewPoint = (point.x, point.y - 1);
                    NewLocation.Add(NewPoint);
                }
                Figure = NewLocation;
            }
            else {

                AllBlocks.UnionWith(Figure);
            }
            return PossibleMove;

        }
    }
    class HorizontalLine : Block {

        public HorizontalLine(){
            for (int i = 0; i < 4; i++) {
                (int x, long y) Point = (3 + i, Block.MaxHeight + 4);
                Figure.Add(Point);
            }
        }
    }

    class Cross: Block {
        public Cross()
        {
            (int x, long y) Point = (4, Block.MaxHeight + 6);
            Figure.Add(Point);
            Point = (3, Block.MaxHeight + 5);
            Figure.Add(Point);
            Point = (4, Block.MaxHeight + 5);
            Figure.Add(Point);
            Point = (5, Block.MaxHeight + 5);
            Figure.Add(Point);
            Point = (4, Block.MaxHeight + 4);
            Figure.Add(Point);
        }
    }
    class LShape : Block
    {
        public LShape()
        {
            (int x, long y) Point = (5, Block.MaxHeight + 6);
            Figure.Add(Point);
            Point = (5, Block.MaxHeight + 5);
            Figure.Add(Point);
            Point = (5, Block.MaxHeight + 4);
            Figure.Add(Point);
            Point = (4, Block.MaxHeight + 4);
            Figure.Add(Point);
            Point = (3, Block.MaxHeight + 4);
            Figure.Add(Point);
        }
    }
    class VerticalLine : Block
    {
        public VerticalLine()
        {
            for (int i = 0; i < 4; i++)
            {
                (int x, long y) Point = (3, Block.MaxHeight + 4+ i);
                Figure.Add(Point);
            }
        }
    }
    class Square : Block
    {
        public Square()
        {
            (int x, long y) Point = (3, Block.MaxHeight + 5);
            Figure.Add(Point);
            Point = (4, Block.MaxHeight + 5);
            Figure.Add(Point);
            Point = (3, Block.MaxHeight + 4);
            Figure.Add(Point);
            Point = (4, Block.MaxHeight + 4);
            Figure.Add(Point);
         
        }
    }



    internal class Day17
    {
        static string Path = ".\\2022\\Input\\InputDay17.txt";
        static string Data = File.ReadAllText(Path);
        public static long Part1()
        {
            string[] List = { "HorizontalLine", "Cross", "LShape", "VerticalLine", "Square" };
            long answer = 0;
            int times = 0;
            Block Figure = new Square();
            int index = 0;
            long Lastblockheight = 0;
            while (times != 2022) {

                string FigureName = List[times % 5];

                switch(FigureName){
                    case "HorizontalLine": {
                            Figure = new HorizontalLine();
                            break;
                        }
                    case "Cross":
                        {
                            Figure = new Cross();
                            break;
                        }
                    case "LShape":
                        {
                            Figure = new LShape();
                            break;
                        }
                    case "VerticalLine":
                        {
                            Figure = new VerticalLine();
                            break;
                        }
                    case "Square":
                        {
                            Figure = new Square();
                            break;
                        }
                }
                bool MoveRock = true;
                
                while (MoveRock){
                    char direction = Data[index % Data.Length];
                    
                    switch (direction) { 
                    
                        case '<': 
                            {
                                Figure.GoLeft();
                                break;
                            }
                        case '>':
                            {
                                Figure.GoRight();
                                break;
                            }
                    
                    }
                    MoveRock = Figure.Godown();
                    index++;
                    
                }
                Block.MaxHeight = Block.AllBlocks.MaxBy(p => p.y).y;
                
                times++;
                if(times% 100 == 0) {
                    Lastblockheight = Block.MaxHeight - Lastblockheight;
                    
                    Block.AllBlocks.RemoveWhere(p => p.y < 120);
                    Lastblockheight = Block.MaxHeight;
                }

            }
            answer = Block.MaxHeight;
            return answer;
            
        }

   
        public static long Part2()
        {
            long answer = 0;
            Block.MaxHeight = 0;
            Block.AllBlocks = new HashSet<(int, long)>();
            string[] List = { "HorizontalLine", "Cross", "LShape", "VerticalLine", "Square" };
            long times = 0;
            Block Figure = new Square();
            int index = 0;
            long TempHeight = 0;
            long TempTimes = 0;
            bool FoundFixPoint = true;
            while (times != 15100  ||  !FoundFixPoint)
            {   
                string FigureName = List[times % 5];

                switch (FigureName)
                {
                    case "HorizontalLine":
                        {
                            Figure = new HorizontalLine();
                            break;
                        }
                    case "Cross":
                        {
                            Figure = new Cross();
                            break;
                        }
                    case "LShape":
                        {
                            Figure = new LShape();
                            break;
                        }
                    case "VerticalLine":
                        {
                            Figure = new VerticalLine();
                            break;
                        }
                    case "Square":
                        {
                            Figure = new Square();
                            break;
                        }
                }
                //string subset = Data.Substring(index % Data.Length, 3);
                bool Memory = false;
                //if (Block.Memory[subset].Contains(FigureName))
                //{
                //    HashSet<(int x, long y)> NewLocation = new HashSet<(int, long)>();
                //    foreach ((int x, int y) point in Figure.Figure)
                //    {
                //        (int x, int y) NewPoint = (point.x + Block.Memory[subset][FigureName].x, point.y + Block.Memory[subset][FigureName].y);
                //        NewLocation.Add(NewPoint);
                //    }
                //    Figure.Figure = NewLocation;
                //    Memory = true;
                //}
                //int Recordsteps = 0;
                bool MoveRock = true;
                while (MoveRock)
                {
                    //index = index % Data.Length;
                   
                    char direction = Data[index % Data.Length];
                    if ((index % (Data.Length * 5) == 0) && times > 0)
                    {
                        //if (!FoundFixPoint)
                        //{
                        Console.WriteLine("Differents: " + (Block.MaxHeight - TempHeight + " Times: " + (times - TempTimes)));
                        TempTimes = times;
                        TempHeight = Block.MaxHeight;

                            //FoundFixPoint = true;
                            //times = 0;
                        //}
                    }
                    switch (direction)
                    {

                        case '<':
                            {
                                Figure.GoLeft();
                                break;
                            }
                        case '>':
                            {
                                Figure.GoRight();
                                break;
                            }

                    }
                    MoveRock = Figure.Godown();
                    index++;
                    Recordsteps++;
                    //if (Recordsteps == 3 && Memory == false)
                    //{
    
                        

                    //}
                }
                Block.MaxHeight = Block.AllBlocks.MaxBy(p => p.y).y;
                times++;
                if (times % 100 == 0)
                {
                    Block.AllBlocks.RemoveWhere(p => p.y < 120);
                   
                }
                if (times % Data.Length == 0)
                {
                    

                }

            }
            //answer = (Block.MaxHeight - TempHeight) + (((long)13795 * (long)114942527) + (long)13785);
            answer = Block.MaxHeight + ((long)114942527 * 13795);
            return answer;
        }
    }
}
