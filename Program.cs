using System;

namespace netcore2_websocket_server
{
    class Program
    {
        static void Main(string[] args)
        {
            var app = new MyWebSocket();

            Console.WriteLine("Press any key to exit!");
            Console.ReadLine();

            app.Dispose();
        }


        public const string SocketUrl = "ws://0.0.0.0:8181";

        public const string DbConnectionString = "mongodb://localhost:27017";
        public const string DbName = "DB_Name";
    }

    
}
