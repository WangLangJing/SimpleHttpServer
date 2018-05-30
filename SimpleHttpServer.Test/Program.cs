using System;

namespace SimpleHttpServer.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpServer server = new HttpServer();
            try
            {
                ListenAddress address = new ListenAddress();
                address.Host = "127.0.0.1";
                address.Port = 8080;

                ListenAddress address1 = new ListenAddress();
                address1.Host = "192.168.1.106";
                address1.Port = 8081;

                server.Start(new ListenAddress[] { address, address1 });
                Console.WriteLine("start...");
                Console.ReadKey();
                server.Stop();
            }
            catch (Exception exc)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(exc.Message);
            }
            finally
            {
                server.Close();
            }
        }
    }
}
