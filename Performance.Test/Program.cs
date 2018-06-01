using System;
using System.Threading;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Performance.Test
{
    class Program
    {
        //volatile static Int32 _counter = 0;

        //volatile static Int32 _start = 0;

        //private static String WildCardToRegular(String value)
        //{
        //    return "^" + Regex.Escape(value).Replace("\\*", ".*") + "$";
        //}
        static void Main(String[] args)
        {
#if NETCOREAPP2_0
            Console.WriteLine("Net core app2.0");
#endif

#if NET40
            Console.WriteLine("Net 4.0");
#endif
            //String input = "http://127.0.0.1:8080/index.html";
            //String pattern = WildCardToRegular("http://*:8080/*");
            //if (Regex.IsMatch(input, pattern))
            //{
            //    Console.WriteLine("match");
            //}
            //   Int32 num = 10;
            //Parallel.For(1, num, (i) =>
            //  {
            //      Count();
            //  });
            //var task = new Task(Count);

            //task.Start();
            // Task.Factory.StartNew(Count).Start();

            //Console.WriteLine("//");
            //Thread[] threads = new Thread[num];
            //for (Int32 i = 0; i < num; ++i)
            //{
            //    var task = new Task(Count);
            //    task.Start();
            //    //threads[i] = new Thread(Count);
            //    //threads[i].Start();
            //}
            //Interlocked.Increment(ref _start);
            //Thread.Sleep(2000);
            //Console.WriteLine(_counter);
        }
        //static void Count()
        //{
        //    while (_start == 0)
        //    {

        //    }
        //    //  Interlocked.Increment(ref _counter);
        //    ++_counter;
        //}
    }
}
