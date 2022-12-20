using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AdventOfCode._2022
{
    internal class Day20
    {
        private static long FindAnswer(long answer, List<LinkedListNode<long>> LinkListNodes)
        {
            LinkedListNode<long> Pointer = LinkListNodes.First(node => node.Value == 0);
            for (int i = 0; i <= 3000; i++)
            {
                if (i == 1000)
                {
                    answer += Pointer.Value;
                }
                else if (i == 2000)
                {
                    answer += Pointer.Value;
                }
                else if (i == 3000)
                {
                    answer += Pointer.Value;
                    break;
                }
                if (Pointer.Next != null)
                {
                    Pointer = Pointer = Pointer.Next;
                }
                else
                {
                    Pointer = Pointer.List.First;
                }

            }

            return answer;
        }

        private static void MixEncryptedFile(LinkedList<long> LinkedList, List<LinkedListNode<long>> LinkListNodes)
        {
            for (int i = 0; i < LinkListNodes.Count; i++)
            {
                LinkedListNode<long> node = LinkListNodes[i];
                long Steps = node.Value % (LinkListNodes.Count - 1);
                if (Steps < 0)
                {
                    for (long j = 0; j < -Steps; j++)
                    {
                        LinkedListNode<long> ChangePos;
                        if (node.Previous != null)
                        {
                            ChangePos = node.Previous;
                        }
                        else
                        {
                            ChangePos = node.List.Last;
                        }
                        LinkedList.Remove(node);
                        LinkedList.AddBefore(ChangePos, node);
                    }
                }
                else if (Steps > 0)
                {
                    for (long j = 0; j < Steps; j++)
                    {
                        LinkedListNode<long> ChangePos;
                        if (node.Next != null)
                        {
                            ChangePos = node.Next;
                        }
                        else
                        {
                            ChangePos = node.List.First;
                        }
                        LinkedList.Remove(node);
                        LinkedList.AddAfter(ChangePos, node);
                    }
                }
            }
        }
        static string Path = ".\\2022\\Input\\InputDay20.txt";
        static string[] Data = File.ReadAllLines(Path);
        public static long Part1()
        {
            long answer = 0;

            LinkedList<long> LinkedList = new LinkedList<long>();
            List<LinkedListNode<long>> LinkListNodes = new List<LinkedListNode<long>>();

            foreach (string line in Data)
            {
                LinkListNodes.Add(LinkedList.AddLast(long.Parse(line)));
            }

            MixEncryptedFile(LinkedList, LinkListNodes);
            answer = FindAnswer(answer, LinkListNodes);

            return answer;
        }

        public static long Part2()
        {
            long answer = 0;
            
            LinkedList<long> LinkedList = new LinkedList<long>();
            List<LinkedListNode<long>> LinkListNodes = new List<LinkedListNode<long>>();

            long multiplier = 811589153;

            foreach (string line in Data)
            {
                LinkListNodes.Add(LinkedList.AddLast(long.Parse(line) * multiplier));
            }

            for (int i = 0; i < 10; i++)
            {
                MixEncryptedFile(LinkedList, LinkListNodes);
            }

            answer = FindAnswer(answer, LinkListNodes);

            return answer;
        }
    }
}
