using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022
{

    class Folder
    {
        static public Dictionary<Folder, long> ListofFolderSizes = new Dictionary<Folder, long>();
        static public Folder Root = new Folder("Root"); 
        
        string name;
        public Folder ?Parent;
        List<Item> Files = new List<Item>();
        List<Folder> ChildFolders = new List<Folder>();

        public Folder(string name, Folder Parent) 
        {
            this.name = name;
            this.Parent = Parent;
            ListofFolderSizes.Add(this, 0);
        }
        public Folder(string name)
        {
            this.name = name;
            this.Parent = null;
            ListofFolderSizes.Add(this, 0);
        }

        public Folder GetChild(string name)
        {
            return this.ChildFolders.Single(s => s.name == name);
        }

        public void AddFiles(string name, long size) {
            this.Files.Add(new Item(name, size));
        }
        public void AddChildFolder(string name, Folder Parent)
        {
            this.ChildFolders.Add(new Folder(name, Parent));
        }


        public void GetSize() {
            long size = 0;
            
            size = this.Files.Sum(s => s.size);

            foreach (Folder child in ChildFolders)
            {
                long subsize;
                child.GetSize();
                subsize = ListofFolderSizes[child];
                size+= subsize;

            }
            ListofFolderSizes[this] = size;
        }
    }

    class Item {
        public string Filename;
        public long size;

        public Item(string name, long size)
        {
            this.Filename = name;
            this.size = size;
        }
    
    }

    internal class Day7
        {
        static string Path = ".\\2022\\Input\\InputDay7.txt";

        public static long Part1()
        {
            string[] Data = File.ReadAllLines(Path);
            long answer = 0;
            Folder Root = Folder.Root;
            Folder CurrentFolder = Root;
            foreach (string line in Data) {

                if (line.StartsWith("$"))
                {
                    if (line == "$ cd /")
                    {

                        CurrentFolder = Root;
                    }
                    else if (line == "$ cd ..")
                    {
                        if (CurrentFolder.Parent != null)
                        {
                            CurrentFolder = CurrentFolder.Parent;
                        }
                    }
                    else if (line.StartsWith("$ cd"))
                    {
                        string[] parts = line.Split(" ");
                        CurrentFolder = CurrentFolder.GetChild(parts[2]);

                    }
                    if (line.StartsWith("$ ls"))
                    {

                    }

                }
                else if (line.StartsWith("dir"))
                {
                    string[] parts = line.Split(" ");
                    CurrentFolder.AddChildFolder(parts[1], CurrentFolder);
                }
                else {
                    string[] parts = line.Split(" ");
                    CurrentFolder.AddFiles(parts[1], long.Parse(parts[0]));
                }

            }
            CurrentFolder = Folder.Root;
            CurrentFolder.GetSize();

            answer = Folder.ListofFolderSizes.Where(folder => folder.Value <= 100000).Sum(x=>x.Value);
          
            return answer;
        }

        public static long Part2()
        {
            
            long answer = 70000000;
            Folder Root = Folder.Root;
            long UsedSpace = Folder.ListofFolderSizes[Root];
            long Filesystem = 70000000;
            long NeededSpace = 30000000;
            long FreeSpace = Filesystem - UsedSpace;
            long SpaceToFree = NeededSpace- FreeSpace;

            answer = Folder.ListofFolderSizes.Where(folder => folder.Value >= SpaceToFree).Min(x => x.Value);
          
            return answer;
        }
    }
}
