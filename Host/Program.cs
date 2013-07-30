using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using Communication.Contracts.Service;
using Communication.Service.Ping;

namespace Host
{
    class Program
    {
        static void Main(string[] args)
        {
            IPing pingService = new PingService();
            ServiceHost host = new ServiceHost(pingService);
            host.Open();

            Console.WriteLine("Service started");
            Console.WriteLine("Press <ENTER> to quit");
            Console.ReadLine();

            host.Close();
        }
    }
}
