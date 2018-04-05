using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Queue myQ = new Queue();

            myQ.Enqueue("a");
            myQ.Enqueue("b");
            myQ.Enqueue("c");

            // 打印队列的数量和值
            Console.WriteLine("myQ");
            Console.WriteLine("\tCount:    {0}", myQ.Count);

            // 打印队列中的所有值
            Console.Write("Queue values:");
            PrintValues(myQ);

            myQ.Dequeue();

            // 打印队列中的所有值
            Console.Write("Queue values:");
            PrintValues(myQ);

        }

        public static void PrintValues(IEnumerable myCollection)
        {
            foreach (Object obj in myCollection)
                Console.Write("    {0}", obj);
            Console.WriteLine();
        }
    }
}
