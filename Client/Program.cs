using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Communication.Service.Ping;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var pong = new PongService();
            PingPong pingpong = new PingPong("Ping", pong);
            Console.WriteLine("Pong callback service started");
            
            Console.WriteLine("Doing Ping(\"hello\")");
            var result = pingpong.Do("hello");
            Console.WriteLine("Got Pong(\"{0}\")", result);

            Console.WriteLine("Press <ENTER> to quit");
            Console.ReadLine();
        }
    }
}
