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
            long SequenceTimes = 0;
            long SequenceHeight = 0;
            bool SequenceFound = false;
            long TimestoSkip = 0;
            long TimestoAdd = 0;
            long HeighttoAdd = 0;
            long TimesTodo = 7000;
            while (times != TimesTodo ||  !SequenceFound)
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

                bool MoveRock = true;
                while (MoveRock)
                {
                    //index = index % Data.Length;
                   
                    char direction = Data[index % Data.Length];
                    if ((index % (Data.Length) == 0) && times > 0)
                    {
                        if (!SequenceFound) { 
                            //{
                            if (Block.MaxHeight - TempHeight == SequenceHeight && (times - TempTimes) == SequenceTimes) {
                                TimestoSkip = (1000000000000 - TimesTodo) / SequenceTimes;
                                TimestoAdd =  (1000000000000 - TimesTodo) % SequenceTimes;
                                HeighttoAdd = TimestoSkip * SequenceHeight;
                                //Console.WriteLine("Timestodo: " + times + " / 10000");
                                TimesTodo += TimestoAdd;
                                //Console.WriteLine("TimestoAdd: " + TimestoAdd);
                                //Console.WriteLine("Timestodo: " + times + " / " + TimesTodo);
                                SequenceFound = true;
                            }
                            //Console.WriteLine("Differents: " + (Block.MaxHeight - TempHeight + " Times: " + (times - TempTimes)));
                            SequenceHeight = TempHeight - SequenceHeight;
                            SequenceTimes = TempTimes - SequenceTimes;
                           
                            TempTimes = times;
                            TempHeight = Block.MaxHeight;
                            
                        }
    
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
            answer = Block.MaxHeight + HeighttoAdd;
            return answer;
        }
    }
}
