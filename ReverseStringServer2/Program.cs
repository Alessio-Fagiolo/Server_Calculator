using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Server myServer = new Server(80);
            myServer.Bind();
            while (true)
            {
                myServer.Process();
            }
        }
        //static void Main(string[] args)
        //{
        //    Client client = new Client(IPAddress.Parse("127.1.1.1") , 80);
        //    client.SendPacket(0, 2, 4);
        //    while (true)
        //    {
        //        Console.WriteLine(client.receive());
        //    }
        //}
    }
}