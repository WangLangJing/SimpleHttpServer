using System;
using System.IO;
namespace SimpleHttpServer.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpServer server = new HttpServer(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"Web"));
            try
            {

                server.Start();
                Console.WriteLine("start...");
                Console.ReadKey();
                server.Stop();
            }
           
            finally
            {
                server.Close();
            }
        }
    }
}
